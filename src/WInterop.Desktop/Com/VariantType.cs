// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

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
    Empty = VARENUM.VT_EMPTY,

    /// <summary>
    ///  [VT_NULL]
    /// </summary>
    Null = VARENUM.VT_NULL,

    /// <summary>
    ///  [VT_I2]
    /// </summary>
    Int16 = VARENUM.VT_I2,

    /// <summary>
    ///  [VT_I4]
    /// </summary>
    Int32 = VARENUM.VT_I4,

    /// <summary>
    ///  [VT_R4]
    /// </summary>
    Single = VARENUM.VT_R4,

    /// <summary>
    ///  [VT_R8]
    /// </summary>
    Double = VARENUM.VT_R8,

    /// <summary>
    ///  [VT_CY]
    /// </summary>
    Currency = VARENUM.VT_CY,

    /// <summary>
    ///  [VT_DATE]
    /// </summary>
    Date = VARENUM.VT_DATE,

    /// <summary>
    ///  [VT_BSTR]
    /// </summary>
    BasicString = VARENUM.VT_BSTR,

    /// <summary>
    ///  [VT_DISPATCH]
    /// </summary>
    IDispatch = VARENUM.VT_DISPATCH,

    /// <summary>
    ///  SCODE value. [VT_ERROR]
    /// </summary>
    Error = VARENUM.VT_ERROR,

    /// <summary>
    ///  -1 is true, 0 is false. [VT_BOOL]
    /// </summary>
    Boolean = VARENUM.VT_BOOL,

    /// <summary>
    ///  Variant pointer. [VT_VARIANT]
    /// </summary>
    Variant = VARENUM.VT_VARIANT,

    /// <summary>
    ///  [VT_UNKNOWN]
    /// </summary>
    IUnknown = VARENUM.VT_UNKNOWN,

    /// <summary>
    ///  [VT_DECIMAL]
    /// </summary>
    Decimal = VARENUM.VT_DECIMAL,

    /// <summary>
    ///  [VT_I1]
    /// </summary>
    SignedByte = VARENUM.VT_I1,

    /// <summary>
    ///  [VT_UI1]
    /// </summary>
    UnsignedByte = VARENUM.VT_UI1,

    /// <summary>
    ///  [VT_UI2]
    /// </summary>
    UInt16 = VARENUM.VT_UI2,

    /// <summary>
    ///  [VT_UI4]
    /// </summary>
    UInt32 = VARENUM.VT_UI4,

    /// <summary>
    ///  [VT_I8]
    /// </summary>
    Int64 = VARENUM.VT_I8,

    /// <summary>
    ///  [VT_UI8]
    /// </summary>
    UInt64 = VARENUM.VT_UI8,

    /// <summary>
    ///  [VT_INT]
    /// </summary>
    Integer = VARENUM.VT_INT,

    /// <summary>
    ///  [VT_UINT]
    /// </summary>
    UnsignedInteger = VARENUM.VT_UINT,

    /// <summary>
    ///  C-style void. [VT_VOID]
    /// </summary>
    Void = VARENUM.VT_VOID,

    /// <summary>
    ///  [VT_HRESULT]
    /// </summary>
    HResult = VARENUM.VT_HRESULT,

    /// <summary>
    ///  [VT_PTR]
    /// </summary>
    Pointer = VARENUM.VT_PTR,

    /// <summary>
    ///  [VT_SAFEARRAY]
    /// </summary>
    SafeArray = VARENUM.VT_SAFEARRAY,

    /// <summary>
    ///  [VT_CARRAY]
    /// </summary>
    CArray = VARENUM.VT_CARRAY,

    /// <summary>
    ///  [VT_USERDEFINED]
    /// </summary>
    UserDefined = VARENUM.VT_USERDEFINED,

    /// <summary>
    ///  [VT_LPSTR]
    /// </summary>
    LPSTR = VARENUM.VT_LPSTR,

    /// <summary>
    ///  [VT_LPWSTR]
    /// </summary>
    LPWSTR = VARENUM.VT_LPWSTR,

    /// <summary>
    ///  [VT_RECORD]
    /// </summary>
    Record = VARENUM.VT_RECORD,

    /// <summary>
    ///  [VT_INT_PTR]
    /// </summary>
    NativeInteger = VARENUM.VT_INT_PTR,

    /// <summary>
    ///  [VT_UINT_PTR]
    /// </summary>
    NativeUnsignedInteger = VARENUM.VT_UINT_PTR,

    /// <summary>
    ///  [VT_FILETIME]
    /// </summary>
    FileTime = VARENUM.VT_FILETIME,

    /// <summary>
    ///  [VT_BLOB]
    /// </summary>
    Blob = VARENUM.VT_BLOB,

    /// <summary>
    ///  [VT_STREAM]
    /// </summary>
    Stream = VARENUM.VT_STREAM,

    /// <summary>
    ///  [VT_STORAGE]
    /// </summary>
    Storage = VARENUM.VT_STORAGE,

    /// <summary>
    ///  [VT_STREAMED_OBJECT]
    /// </summary>
    StreamedObject = VARENUM.VT_STREAMED_OBJECT,

    /// <summary>
    ///  [VT_STORED_OBJECT]
    /// </summary>
    StoredObject = VARENUM.VT_STORED_OBJECT,

    /// <summary>
    ///  [VT_BLOB_OBJECT]
    /// </summary>
    BlobObject = VARENUM.VT_BLOB_OBJECT,

    /// <summary>
    ///  [VT_CF]
    /// </summary>
    ClipboardFormat = VARENUM.VT_CF,

    /// <summary>
    ///  [VT_CLSID]
    /// </summary>
    ClassId = VARENUM.VT_CLSID,

    /// <summary>
    ///  [VT_VERSIONED_STREAM]
    /// </summary>
    VersionedStream = VARENUM.VT_VERSIONED_STREAM,

    /// <summary>
    ///  Reserved. [VT_BSTR_BLOB]
    /// </summary>
    BasicStringBlob = VARENUM.VT_BSTR_BLOB,

    /// <summary>
    ///  Simple counted array flag. [VT_VECTOR]
    /// </summary>
    Vector = VARENUM.VT_VECTOR,

    /// <summary>
    ///  SafeArray pointer flag. [VT_ARRAY]
    /// </summary>
    Array = VARENUM.VT_ARRAY,

    /// <summary>
    ///  Void pointer for local use flag. [VT_BYREF]
    /// </summary>
    ByRef = VARENUM.VT_BYREF,

    /// <summary>
    ///  Reserved mask. [VT_RESERVED]
    /// </summary>
    Reserved = VARENUM.VT_RESERVED,

    /// <summary>
    ///  [VT_ILLEGAL]
    /// </summary>
    Illegal = VARENUM.VT_ILLEGAL,

    /// <summary>
    ///  Type maks. [VT_TYPEMASK]
    /// </summary>
#pragma warning disable CA1069 // Enums values should not be duplicated
    TypeMask = VARENUM.VT_TYPEMASK
#pragma warning restore CA1069 // Enums values should not be duplicated
}