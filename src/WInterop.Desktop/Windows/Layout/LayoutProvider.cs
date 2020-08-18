// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;

namespace WInterop.Windows
{
    public class LayoutProvider
    {
        private readonly ILayoutHandler _handler;

        public LayoutProvider(Window window, ILayoutHandler handler)
        {
            _handler = handler;
            window.MessageHandler += Window_MessageHandler;
        }

        private LResult? Window_MessageHandler(
            object sender,
            WindowHandle window,
            MessageType message,
            WParam wParam,
            LParam lParam)
        {
            if (message != MessageType.WindowPositionChanged)
            {
                return null;
            }

            _handler.Layout(new Message.WindowPositionChanged(lParam).ClientBounds);
            return 0;
        }
    }
}
