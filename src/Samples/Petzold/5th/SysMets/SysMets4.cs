// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace SysMets;

internal class SysMets4 : SysMets3
{
    protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
    {
        switch (message)
        {

            case MessageType.KeyDown:
                switch ((VirtualKey)wParam)
                {
                    case VirtualKey.Home:
                        window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.Top, 0);
                        break;
                    case VirtualKey.End:
                        window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.Bottom, 0);
                        break;
                    case VirtualKey.Prior:
                        window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.PageUp, 0);
                        break;
                    case VirtualKey.Next:
                        window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.PageDown, 0);
                        break;
                    case VirtualKey.Up:
                        window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.LineUp, 0);
                        break;
                    case VirtualKey.Down:
                        window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.LineDown, 0);
                        break;
                    case VirtualKey.Left:
                        window.SendMessage(MessageType.HorizontalScroll, (uint)ScrollCommand.PageUp, 0);
                        break;
                    case VirtualKey.Right:
                        window.SendMessage(MessageType.HorizontalScroll, (uint)ScrollCommand.PageDown, 0);
                        break;
                }
                return 0;
        }

        return base.WindowProcedure(window, message, wParam, lParam);
    }
}
