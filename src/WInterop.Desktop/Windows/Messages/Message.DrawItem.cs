// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;

namespace WInterop.Windows;

public static partial class Message
{
    public readonly unsafe ref struct DrawItem
    {
        private readonly DRAWITEMSTRUCT* _drawItemStruct;

        public DrawItem(LParam lParam)
        {
            _drawItemStruct = (DRAWITEMSTRUCT*)lParam;
        }

        public OwnerDrawType ControlType => (OwnerDrawType)_drawItemStruct->CtlType;
        public uint ControlId => _drawItemStruct->CtlID;
        public uint ItemId => _drawItemStruct->itemID;
        public OwnerDrawActions ItemAction => (OwnerDrawActions)_drawItemStruct->itemAction;
        public OwnerDrawStates ItemState => (OwnerDrawStates)_drawItemStruct->itemState;
        public WindowHandle ItemWindow => _drawItemStruct->hwndItem;
        public DeviceContext DeviceContext => new(_drawItemStruct->hDC, ownsHandle: false);
        public Rectangle ItemRectangle => _drawItemStruct->rcItem.ToRectangle();
        public nuint ItemData => _drawItemStruct->itemData;
    }
}