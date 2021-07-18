// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Com.Native
{
    /// <summary>
    ///  https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-oaut/95bb92a7-f783-477f-acbc-c947d754fa8b
    /// </summary>
    public unsafe struct TYPEDESC
    {
        public UnionType Union;
        public VariantType vt;

        [StructLayout(LayoutKind.Explicit)]
        public unsafe struct UnionType
        {
            /// <summary>
            ///  Set if <see cref="VariantType.Pointer"/> or <see cref="VariantType.SafeArray"/>.
            /// </summary>
            [FieldOffset(0)]
            public TYPEDESC* lptdesc;

            /// <summary>
            ///  Set if <see cref="VariantType.CArray"/>.
            /// </summary>
            [FieldOffset(0)]
            public ARRAYDESC* llpadesc;

            /// <summary>
            ///  Set if <see cref="VariantType.UserDefined"/>.
            /// </summary>
            [FieldOffset(0)]
            public RefTypeHandle hreftype;
        }
    }
}