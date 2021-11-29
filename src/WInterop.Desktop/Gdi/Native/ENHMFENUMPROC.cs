// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace WInterop.Gdi.Native;

/// <docs>https://docs.microsoft.com/windows/win32/api/wingdi/nf-wingdi-enumenhmetafile</docs>
public unsafe delegate IntBoolean ENHMFENUMPROC(
    HDC hdc,
    HGDIOBJ* lpht,
    ENHMETARECORD* lpmr,
    int nHandles,
    LParam data);