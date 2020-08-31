// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace WInterop.Gdi.Native
{
    /// <docs>https://docs.microsoft.com/windows/win32/api/wingdi/nc-wingdi-mfenumproc</docs>
    public unsafe delegate int MFENUMPROC(
        HDC hdc,
        HGDIOBJ* lpht,
        METARECORD* lpMR,
        int nObj,
        LParam param);
}
