// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public static partial class Message
    {
        public unsafe readonly ref struct DrawItem
        {
            private readonly DRAWITEMSTRUCT* _drawItemStruct;

            public DrawItem(LParam lParam)
            {
                _drawItemStruct = (DRAWITEMSTRUCT*)lParam;
            }

            public OwnerDrawType ControlType => _drawItemStruct->CtlType;
            public uint ControlId => _drawItemStruct->CtlID;
            public uint ItemId => _drawItemStruct->itemID;
            public OwnerDrawActions ItemAction => _drawItemStruct->itemAction;
            public OwnerDrawStates ItemState => _drawItemStruct->itemState;
            public WindowHandle ItemWindow => _drawItemStruct->hwndItem;
            public DeviceContext DeviceContext => new DeviceContext(_drawItemStruct->hDC, ownsHandle: false);
            public Rectangle ItemRectangle => _drawItemStruct->rcItem;
            public IntPtr ItemData => _drawItemStruct->itemData;
        }
    }
}
