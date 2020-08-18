// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;

namespace WInterop.Windows
{
    public static partial class Message
    {
        public readonly ref struct WindowPositionChanged
        {
            public WindowHandle InsertAfter { get; }
            public WindowHandle Handle { get; }
            public Rectangle Bounds { get; }
            public Rectangle ClientBounds { get; }

            public unsafe WindowPositionChanged(LParam lParam)
            {
                WindowPosition* position = (WindowPosition*)lParam;
                InsertAfter = position->InsertAfter;
                Handle = position->Handle;
                Rectangle bounds = new Rectangle(position->X, position->Y, position->Width, position->Height);
                Handle.ScreenToClient(ref bounds);
                Bounds = bounds;
                ClientBounds = Handle.GetClientRectangle();
            }
        }
    }
}
