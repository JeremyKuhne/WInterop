// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace WInterop.Com.Native
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct BINDPTR
    {
        [FieldOffset(0)]
        FUNCDESC* lpfuncdesc;

        [FieldOffset(0)]
        VARDESC* lpvardesc;

        /// <summary>
        ///  This is <see cref="ITypeComp"/>.
        /// </summary>
        [FieldOffset(0)]
        IntPtr lptcomp;
    }
}
