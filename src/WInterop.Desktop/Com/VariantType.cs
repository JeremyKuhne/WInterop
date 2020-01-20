// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com
{
    // https://docs.microsoft.com/en-us/windows/win32/api/wtypes/ne-wtypes-varenum
    // [MS-OAUT] https://msdn.microsoft.com/en-us/library/cc237865.aspx

    /// <summary>
    ///  [VARENUM]
    /// </summary>
    public enum VariantType : ushort
    {
        /// <summary>
        ///  [VT_EMPTY]
        /// </summary>
        Empty = 0,

        /// <summary>
        ///  [VT_NULL]
        /// </summary>
        Null = 1,

        /// <summary>
        ///  [VT_I2]
        /// </summary>
        Int16 = 2,

        /// <summary>
        ///  [VT_I4]
        /// </summary>
        Int32 = 3,

        /// <summary>
        ///  [VT_R4]
        /// </summary>
        Single = 4,

        /// <summary>
        ///  [VT_R8]
        /// </summary>
        Double = 5,

        /// <summary>
        ///  [VT_CY]
        /// </summary>
        Currency = 6,

        /// <summary>
        ///  [VT_DATE]
        /// </summary>
        Date = 7,

        /// <summary>
        ///  [VT_BSTR]
        /// </summary>
        BasicString = 8,

        /// <summary>
        ///  [VT_DISPATCH]
        /// </summary>
        IDispatch = 9,

        /// <summary>
        ///  SCODE value. [VT_ERROR]
        /// </summary>
        Error = 10,

        /// <summary>
        ///  -1 is true, 0 is false. [VT_BOOL]
        /// </summary>
        Boolean = 11,

        /// <summary>
        ///  Variant pointer. [VT_VARIANT]
        /// </summary>
        Variant = 12,

        /// <summary>
        ///  [VT_UNKNOWN]
        /// </summary>
        IUnknown = 13,

        /// <summary>
        ///  [VT_DECIMAL]
        /// </summary>
        Decimal = 14,

        /// <summary>
        ///  [VT_I1]
        /// </summary>
        SignedByte = 16,

        /// <summary>
        ///  [VT_UI1]
        /// </summary>
        UnsignedByte = 17,

        /// <summary>
        ///  [VT_UI2]
        /// </summary>
        UInt16 = 18,

        /// <summary>
        ///  [VT_UI4]
        /// </summary>
        UInt32 = 19,

        /// <summary>
        ///  [VT_I8]
        /// </summary>
        Int64 = 20,

        /// <summary>
        ///  [VT_UI8]
        /// </summary>
        UInt64 = 21,

        /// <summary>
        ///  [VT_INT]
        /// </summary>
        Integer = 22,

        /// <summary>
        ///  [VT_UINT]
        /// </summary>
        UnsignedInteger = 23,

        /// <summary>
        ///  C-style void. [VT_VOID]
        /// </summary>
        Void = 24,

        /// <summary>
        ///  [VT_HRESULT]
        /// </summary>
        HResult = 25,

        /// <summary>
        ///  [VT_PTR]
        /// </summary>
        Pointer = 26,

        /// <summary>
        ///  [VT_SAFEARRAY]
        /// </summary>
        SafeArray = 27,

        /// <summary>
        ///  [VT_CARRAY]
        /// </summary>
        CArray = 28,

        /// <summary>
        ///  [VT_USERDEFINED]
        /// </summary>
        UserDefined = 29,

        /// <summary>
        ///  [VT_LPSTR]
        /// </summary>
        LPSTR = 30,

        /// <summary>
        ///  [VT_LPWSTR]
        /// </summary>
        LPWSTR = 31,

        /// <summary>
        ///  [VT_RECORD]
        /// </summary>
        Record = 36,

        /// <summary>
        ///  [VT_INT_PTR]
        /// </summary>
        NativeInteger = 37,

        /// <summary>
        ///  [VT_UINT_PTR]
        /// </summary>
        NativeUnsignedInteger = 38,

        /// <summary>
        ///  [VT_FILETIME]
        /// </summary>
        FileTime = 64,

        /// <summary>
        ///  [VT_BLOB]
        /// </summary>
        Blob = 65,

        /// <summary>
        ///  [VT_STREAM]
        /// </summary>
        Stream = 66,

        /// <summary>
        ///  [VT_STORAGE]
        /// </summary>
        Storage = 67,

        /// <summary>
        ///  [VT_STREAMED_OBJECT]
        /// </summary>
        StreamedObject = 68,

        /// <summary>
        ///  [VT_STORED_OBJECT]
        /// </summary>
        StoredObject = 69,

        /// <summary>
        ///  [VT_BLOB_OBJECT]
        /// </summary>
        BlobObject = 70,

        /// <summary>
        ///  [VT_CF]
        /// </summary>
        ClipboardFormat = 71,

        /// <summary>
        ///  [VT_CLSID]
        /// </summary>
        ClassId = 72,

        /// <summary>
        ///  [VT_VERSIONED_STREAM]
        /// </summary>
        VersionedStream = 73,

        /// <summary>
        ///  Reserved. [VT_BSTR_BLOB]
        /// </summary>
        BasicStringBlob = 0xfff,

        /// <summary>
        ///  Simple counted array. [VT_VECTOR]
        /// </summary>
        Vector = 0x1000,

        /// <summary>
        ///  SafeArray pointer. [VT_ARRAY]
        /// </summary>
        Array = 0x2000,

        /// <summary>
        ///  Void pointer for local use. [VT_BYREF]
        /// </summary>
        ByRef = 0x4000,

        /// <summary>
        ///  [VT_RESERVED]
        /// </summary>
        Reserved = 0x8000,

        /// <summary>
        ///  [VT_ILLEGAL]
        /// </summary>
        Illegal = 0xffff,

        /// <summary>
        ///  [VT_TYPEMASK]
        /// </summary>
        TypeMask = 0xfff
    }
}
