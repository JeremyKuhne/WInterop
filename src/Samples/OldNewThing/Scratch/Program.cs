// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Gdi;
using WInterop.Windows;

namespace Scratch;

internal class Program
{
    [STAThread]
    private static void Main()
    {
        Windows.CreateMainWindowAndRun(new ScratchClass(), windowTitle: "Scratch");
    }

    // https://devblogs.microsoft.com/oldnewthing/20030723-00
    public class ScratchClass : WindowClass
    {
        protected WindowHandle ChildWindow { get; set; }

        public ScratchClass()
            : base(className: "Scratch")
        {
        }

        /// <summary>
        ///  Interesting things will be painted here eventually.
        /// </summary>
        public virtual void PaintContent(DeviceContext dc)
        {
        }

        /// <summary>
        ///  Applications will typically override this and maybe even create a child window.
        /// </summary>
        public virtual bool OnCreate(WindowHandle window, Message.Create message)
        {
            return true;
        }

        /// <summary>
        ///  If we have an inner child, resize it to fit.
        /// </summary>
        public virtual void OnSize(WindowHandle window, Message.Size message)
        {
            if (!ChildWindow.IsNull)
            {
                ChildWindow.MoveWindow(new(new(), message.NewSize), repaint: true);
            }
        }

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    return OnCreate(window, new Message.Create(lParam)) ? 1 : 0;
                case MessageType.Size:
                    OnSize(window, new Message.Size(wParam, lParam));
                    return 0;
                case MessageType.Paint:
                    using (DeviceContext dc = window.BeginPaint())
                    {
                        PaintContent(dc);
                    }
                    return 0;
                case MessageType.PrintClient:
                    PaintContent(new Message.PrintClient(wParam).DeviceContext);
                    return 0;
            }

            // Let the base class handle any other messages
            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
