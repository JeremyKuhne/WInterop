// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Gdi;
using WInterop.Gdi.Native;

namespace WInterop.Windows.Native;

// https://docs.microsoft.com/en-us/windows/desktop/api/winuser/ns-winuser-tagdrawitemstruct
public readonly struct DRAWITEMSTRUCT
{
    public readonly OwnerDrawType CtlType;
    public readonly uint CtlID;
    public readonly uint itemID;
    public readonly OwnerDrawActions itemAction;
    public readonly OwnerDrawStates itemState;
    public readonly HWND hwndItem;
    public readonly HDC hDC;
    public readonly Rect rcItem;
    public readonly IntPtr itemData;
}