// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Com.Native
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct BINDPTR
    {
        [FieldOffset(0)]
        private readonly FUNCDESC* lpfuncdesc;

        [FieldOffset(0)]
        private readonly VARDESC* lpvardesc;

        /// <summary>
        ///  This is <see cref="ITypeComp"/>.
        /// </summary>
        [FieldOffset(0)]
        private readonly IntPtr lptcomp;
    }
}