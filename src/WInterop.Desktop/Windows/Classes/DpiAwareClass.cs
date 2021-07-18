// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public class DpiAwareClass : WindowClass
    {
        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.DpiChanged:
                    {
                        // Resize and reposition for the new DPI
                        Message.DpiChanged dpiChanged = new(wParam, lParam);
                        window.MoveWindow(dpiChanged.SuggestedBounds, repaint: true);

                        break;
                    }
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}