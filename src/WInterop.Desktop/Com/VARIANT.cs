// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using WInterop.Handles;
using WInterop.Support;
using WInterop.Com.Native;
using WInterop.Errors;

namespace WInterop.Com
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms221627.aspx
    public class Variant : HandleZeroOrMinusOneIsInvalid
    {
        protected const ulong DataOffset = 8;
        protected const ulong NativeSize = 24;
        protected static object s_UnsupportedObject = new object();

        public Variant() : this(ownsHandle: true)
        {
        }

        public Variant(bool ownsHandle) : base(ownsHandle)
        {
        }

        public Variant(IntPtr handle, bool ownsHandle)
            : base(ownsHandle)
        {
            this.handle = handle;
        }

        public VariantType VariantType
        {
            get { return RawVariantType & VariantType.TypeMask; }
        }

        protected unsafe VariantType RawVariantType
        {
            get
            {
                if (IsInvalid)
                    return VariantType.Empty;

                return *((VariantType*)handle.ToPointer());
            }
        }

        public bool IsByRef
        {
            get { return (RawVariantType & VariantType.ByRef) != 0; }
        }

        public virtual bool IsArray
        {
            get { return (RawVariantType & VariantType.Array) != 0; }
        }

        public unsafe virtual object? GetData()
        {
            VariantType propertyType = VariantType;

            switch (propertyType)
            {
                case VariantType.Empty:
                case VariantType.Null:
                    return null;
            }

            if (IsArray)
            {
                throw new NotImplementedException();
            }

            void* data = handle.Offset(DataOffset);

            if (IsByRef)
            {
                data = *((void**)data);
            }
            else if (propertyType == VariantType.Decimal)
            {
                // DECIMAL starts at the beginning of the VARIANT, with the VARIANT's type occupying
                // the reserved ushort in the DECIMAL and the DECIMAL occupying the reserved WORDs
                // in the VARIANT.
                data = handle.ToPointer();
            }

            object? result = GetCoreType(propertyType, data);
            if (ReferenceEquals(result, s_UnsupportedObject))
                throw new InvalidOleVariantTypeException();

            return result;
        }


        // VARENUMs that are VARIANT only (e.g. not valid for PROPVARIANT)
        //
        //  VT_DISPATCH
        //  VT_UNKNOWN
        //  VT_DECIMAL
        //  VT_UINT
        //  VT_ARRAY
        //  VT_BYREF
        //
        protected virtual unsafe object? GetCoreType(VariantType propertyType, void* data)
        {
            switch (propertyType)
            {
                case VariantType.Int32:
                case VariantType.Integer:
                case VariantType.Error: // SCODE
                    return *((int*)data);
                case VariantType.UInt32:
                case VariantType.UnsignedInteger:
                    return *((uint*)data);
                case VariantType.Int16:
                    return *((short*)data);
                case VariantType.UInt16:
                    return *((ushort*)data);
                case VariantType.SignedByte:
                    return *((sbyte*)data);
                case VariantType.UnsignedByte:
                    return *((byte*)data);
                case VariantType.Currency: // Currency (long long)
                    return *((long*)data);
                case VariantType.Single:
                    return *((float*)data);
                case VariantType.Double:
                    return *((double*)data);
                case VariantType.Decimal:
                    return (*((DECIMAL*)data)).ToDecimal();
                case VariantType.Boolean:
                    // VARIANT_TRUE is -1
                    return *((short*)data) == -1;
                case VariantType.BasicString:
                    return Marshal.PtrToStringBSTR((IntPtr)(*((void**)data)));
                case VariantType.Variant:
                    return new Variant(new IntPtr(*((void**)data)), ownsHandle: false);
                case VariantType.Date:
                    return Conversion.VariantDateToDateTime(*((double*)data));
                case VariantType.Record:
                case VariantType.IDispatch:
                case VariantType.IUnknown:
                    throw new NotImplementedException();
                default:
                    return s_UnsupportedObject;
            }
        }

        protected override bool ReleaseHandle()
        {
            IntPtr handle = Interlocked.Exchange(ref this.handle, IntPtr.Zero);
            if (handle != IntPtr.Zero)
                Imports.VariantClear(handle).ThrowIfFailed();

            return true;
        }
    }
}
