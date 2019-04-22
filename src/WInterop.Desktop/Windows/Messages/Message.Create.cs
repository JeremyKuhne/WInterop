// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Modules;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public static partial class Message
    {
        public unsafe readonly ref struct Create
        {
            private readonly CREATESTRUCT* _createStruct;

            public Create(LParam lParam)
            {
                _createStruct = (CREATESTRUCT*)lParam;
            }

            public ModuleInstance Instance => _createStruct->hInstance;
            public MenuHandle MenuHandle => new MenuHandle(_createStruct->hMenu, ownsHandle: false);
            public WindowHandle Parent => _createStruct->hwndParent;
            public Rectangle Bounds => new Rectangle(_createStruct->x, _createStruct->y, _createStruct->cx, _createStruct->cy);
            public ReadOnlySpan<char> WindowName => _createStruct->WindowName;
            public ReadOnlySpan<char> ClassName => _createStruct->ClassName;
            public Atom Atom => _createStruct->Atom;
        }
    }
}
