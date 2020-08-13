// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using WInterop.Gdi;
using WInterop.Windows;

namespace Windows101
{
    internal class Program
    {
        // To create a Windows Application in .NET Core you must do the following things:
        //
        //  1. Create a .NET Core Console App.
        //  2. Double click on the project and change <OutputType> to WinExe.
        //  3. Mark the Main method as [STAThread].

        // Optional- to make things look better, add an Application Manifest file and
        // change it to match the one included in this project.

        [STAThread]
        private static void Main()
        {
            Windows.CreateMainWindowAndRun(new EditWindow(), windowTitle: "Edit control");

            ////  You can just show message boxes to interact
            //Windows.MessageBox("Hello World.", caption: "Hello");

            ////  Or create actual Window classes and run them. A Window Class in Windows
            ////  includes a few basic things:
            ////
            ////   1. Appearance settings (border, icon, background, etc.)
            ////   2. A callback pointer for messages (mouse, keyboard, etc.)
            ////   3. An optional menu
            ////
            ////  The Window Class is a template that actual Window instances are created
            ////  from. WInterop wraps the registration and callbacks in "WindowClass"
            ////  that you can derive from. "CreateMainWindowAndRun" will create an
            ////  instance of the Window for the given WindowClass and loop processing
            ////  messages until the Window is closed.

            //Windows.CreateMainWindowAndRun(new WindowClass(), windowTitle: "So Simple");

            ////  To display a message in a Window you have to draw it yourself in response
            ////  to a message to draw the window contents.
            //Windows.CreateMainWindowAndRun(new HelloWindow(), windowTitle: "Hello!");
        }

        private class HelloWindow : WindowClass
        {
            // Overriding the callback method will allow us to provide our own custom behavior
            protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
            {
                switch (message)
                {
                    // The Paint message is sent when the Window contents need drawn.
                    case MessageType.Paint:

                        // Drawing is done in a Device Context by calling BeginPaint(). When the
                        // DeviceContext is disposed it will call EndPaint().
                        using (DeviceContext dc = window.BeginPaint())
                        {
                            Rectangle client = window.GetClientRectangle();

                            // The default font is really small on modern PCs, so we'll take the extra step
                            // to select a font into our device context before drawing the text.
                            using FontHandle font = Gdi.CreateFont(
                                height: client.Height / 5,
                                family: FontFamilyType.Swiss);
                            dc.SelectObject(font);

                            // Draw the given text in the middle of the client area of the Window.
                            dc.DrawText(
                                "Hello, .NET Core!",
                                client,
                                TextFormat.SingleLine | TextFormat.Center | TextFormat.VerticallyCenter);

                            // Put the system font back as we're going to dispose our font
                            dc.SelectObject(StockFont.System);
                        }

                        // Return 0 to indicate we've handled the message
                        return 0;
                }

                // Let the base class handle any other messages
                return base.WindowProcedure(window, message, wParam, lParam);
            }
        }


        private class EditWindow : WindowClass
        {
            private EditClass _editClass;
            private WindowHandle _editHandle;

            protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
            {
                switch (message)
                {
                    case MessageType.Create:
                        _editClass = new EditClass(EditStyles.Multiline | EditStyles.Left
                            | EditStyles.AutoHorizontalScroll | EditStyles.AutoVerticalScroll);
                        _editHandle = _editClass.CreateWindow(
                            style: WindowStyles.Child | WindowStyles.Visible | WindowStyles.HorizontalScroll
                                | WindowStyles.VerticalScroll | WindowStyles.Border,
                            parentWindow: window);
                        return 0;
                    case MessageType.SetFocus:
                        _editHandle.SetFocus();
                        return 0;
                    case MessageType.Size:
                        var size = new Message.Size(wParam, lParam);
                        _editHandle.MoveWindow(new Rectangle(new Point(), size.NewSize), repaint: true);
                        return 0;
                }

                return base.WindowProcedure(window, message, wParam, lParam);
            }
        }
    }
}
