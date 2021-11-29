// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Com.Native;
using WInterop.Errors;
using WInterop.Handles;
using WInterop.Support;

namespace WInterop.Com;

// https://msdn.microsoft.com/en-us/library/windows/desktop/ms221627.aspx
public class Variant : HandleZeroOrMinusOneIsInvalid
{
    protected const ulong DataOffset = 8;
    protected const ulong NativeSize = 24;
    protected static object s_UnsupportedObject = new();

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

    public VariantType VariantType => RawVariantType & VariantType.TypeMask;

    protected unsafe VariantType RawVariantType
        => IsInvalid ? VariantType.Empty : *((VariantType*)handle.ToPointer());

    public bool IsByRef => (RawVariantType & VariantType.ByRef) != 0;

    public virtual bool IsArray => (RawVariantType & VariantType.Array) != 0;

    public virtual unsafe object? GetData()
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

    protected virtual unsafe object? GetCoreType(VariantType propertyType, void* data)
    {
        return propertyType switch
        {
            VariantType.Int32 or VariantType.Integer or VariantType.Error => *((int*)data),
            VariantType.UInt32 or VariantType.UnsignedInteger => *((uint*)data),
            VariantType.Int16 => *((short*)data),
            VariantType.UInt16 => *((ushort*)data),
            VariantType.SignedByte => *((sbyte*)data),
            VariantType.UnsignedByte => *((byte*)data),
            VariantType.Currency => *((long*)data),                                         // Currency (long long)
            VariantType.Single => *((float*)data),
            VariantType.Double => *((double*)data),
            VariantType.Decimal => (*((DECIMAL*)data)).ToDecimal(),
            VariantType.Boolean => *((short*)data) == -1,                                   // VARIANT_TRUE is -1
            VariantType.BasicString => Marshal.PtrToStringBSTR((IntPtr)(*((void**)data))),
            VariantType.Variant => new Variant(new IntPtr(*((void**)data)), ownsHandle: false),
            VariantType.Date => Conversion.VariantDateToDateTime(*((double*)data)),
            VariantType.Record or VariantType.IDispatch or VariantType.IUnknown => throw new NotImplementedException(),
            _ => s_UnsupportedObject,
        };
    }

    protected override bool ReleaseHandle()
    {
        IntPtr handle = Interlocked.Exchange(ref this.handle, IntPtr.Zero);
        if (handle != IntPtr.Zero)
            Imports.VariantClear(handle).ThrowIfFailed();

        return true;
    }
}