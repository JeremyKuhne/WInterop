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

namespace WInterop.Com
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms221627.aspx
    public class VARIANT : HandleZeroOrMinusOneIsInvalid
    {
        protected const ulong DataOffset = 8;
        protected const ulong NativeSize = 24;
        protected static object s_UnsupportedObject = new object();

        public VARIANT() : this(ownsHandle: true)
        {
        }

        public VARIANT(bool ownsHandle) : base(ownsHandle)
        {
        }

        public VARIANT(IntPtr handle, bool ownsHandle)
            : base(ownsHandle)
        {
            this.handle = handle;
        }

        public VARENUM VariantType
        {
            get { return RawVariantType & VARENUM.VT_TYPEMASK; }
        }

        protected unsafe VARENUM RawVariantType
        {
            get
            {
                if (IsInvalid)
                    return VARENUM.VT_EMPTY;

                return *((VARENUM*)handle.ToPointer());
            }
        }

        public bool IsByRef
        {
            get { return (RawVariantType & VARENUM.VT_BYREF) != 0; }
        }

        public virtual bool IsArray
        {
            get { return (RawVariantType & VARENUM.VT_ARRAY) != 0; }
        }

        public unsafe virtual object GetData()
        {
            VARENUM propertyType = VariantType;

            switch (propertyType)
            {
                case VARENUM.VT_EMPTY:
                case VARENUM.VT_NULL:
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
            else if (propertyType == VARENUM.VT_DECIMAL)
            {
                // DECIMAL starts at the beginning of the VARIANT, with the VARIANT's type occupying
                // the reserved ushort in the DECIMAL and the DECIMAL occupying the reserved WORDs
                // in the VARIANT.
                data = handle.ToPointer();
            }

            object result = GetCoreType(propertyType, data);
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
        protected virtual unsafe object GetCoreType(VARENUM propertyType, void* data)
        {
            switch (propertyType)
            {
                case VARENUM.VT_I4:
                case VARENUM.VT_INT:
                case VARENUM.VT_ERROR: // SCODE
                    return *((int*)data);
                case VARENUM.VT_UI4:
                case VARENUM.VT_UINT:
                    return *((uint*)data);
                case VARENUM.VT_I2:
                    return *((short*)data);
                case VARENUM.VT_UI2:
                    return *((ushort*)data);
                case VARENUM.VT_I1:
                    return *((sbyte*)data);
                case VARENUM.VT_UI1:
                    return *((byte*)data);
                case VARENUM.VT_CY: // Currency (long long)
                    return *((long*)data);
                case VARENUM.VT_R4:
                    return *((float*)data);
                case VARENUM.VT_R8:
                    return *((double*)data);
                case VARENUM.VT_DECIMAL:
                    return (*((DECIMAL*)data)).ToDecimal();
                case VARENUM.VT_BOOL:
                    // VARIANT_TRUE is -1
                    return *((short*)data) == -1;
                case VARENUM.VT_BSTR:
                    return Marshal.PtrToStringBSTR((IntPtr)(*((void**)data)));
                case VARENUM.VT_VARIANT:
                    return new VARIANT(new IntPtr(*((void**)data)), ownsHandle: false);
                case VARENUM.VT_DATE:
                    return Conversion.VariantDateToDateTime(*((double*)data));
                case VARENUM.VT_RECORD:
                case VARENUM.VT_DISPATCH:
                case VARENUM.VT_UNKNOWN:
                    throw new NotImplementedException();
                default:
                    return s_UnsupportedObject;
            }
        }

        protected override bool ReleaseHandle()
        {
            IntPtr handle = Interlocked.Exchange(ref this.handle, IntPtr.Zero);
            if (handle != IntPtr.Zero) Support.Internal.Imports.VariantClear(handle);

            return true;
        }
    }
}
