// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com.Native
{
    /// <summary>
    ///  <see cref="SAFEARRAY"/> flags.
    /// </summary>
    /// <remarks>
    ///  <see cref="https://docs.microsoft.com/windows/win32/api/oaidl/ns-oaidl-safearray"/>
    /// </remarks>
    [Flags]
    public enum FADF : ushort
    {
        /// <summary>
        ///  An array that is allocated on the stack.
        /// </summary>
        AUTO = 0x1,

        /// <summary>
        ///  An array that is statically allocated.
        /// </summary>
        STATIC = 0x2,

        /// <summary>
        ///  An array that is embedded in a structure.
        /// </summary>
        EMBEDDED = 0x4,

        /// <summary>
        ///  An array that may not be resized or reallocated.
        /// </summary>
        FIXEDSIZE = 0x10,

        /// <summary>
        ///  An array that contains records. When set, there will be a pointer to the
        ///  IRecordInfo interface at negative offset 4 in the array descriptor.
        /// </summary>
        RECORD = 0x20,

        /// <summary>
        ///  An array that has an IID identifying interface. When set, there will be a GUID
        ///  at negative offset 16 in the safe array descriptor. Flag is set only when
        ///  FADF_DISPATCH or FADF_UNKNOWN is also set.
        /// </summary>
        HAVEIID = 0x40,

        /// <summary>
        ///  An array that has a variant type. The variant type can be retrieved with SafeArrayGetVartype.
        /// </summary>
        HAVEVARTYPE = 0x80,

        /// <summary>
        ///  An array of BSTRs.
        /// </summary>
        BSTR = 0x100,

        /// <summary>
        ///  An array of IUnknown*.
        /// </summary>
        UNKNOWN = 0x200,

        /// <summary>
        ///  An array of IDispatch*.
        /// </summary>
        DISPATCH = 0x400,

        /// <summary>
        ///  An array of VARIANTs.
        /// </summary>
        VARIANT = 0x800,

        RESERVED = 0xf008
    }
}
