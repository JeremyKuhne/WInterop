// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows;

namespace Controls
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            Windows.Run(new EditWindow("Edit control"));
        }

        private class EditWindow : Window
        {
            private readonly EditControl _editControl;
            private readonly ButtonControl _buttonControl;

            public EditWindow(string title) : base(
                new WindowClass(),
                Windows.DefaultBounds,
                windowName: title,
                style: WindowStyles.OverlappedWindow,
                isMainWindow: true)
            {
                _editControl = new EditControl(
                    Windows.DefaultBounds,
                    editStyle: EditStyles.Multiline | EditStyles.Left
                        | EditStyles.AutoHorizontalScroll | EditStyles.AutoVerticalScroll,
                    style: WindowStyles.Child | WindowStyles.Visible | WindowStyles.HorizontalScroll
                        | WindowStyles.VerticalScroll | WindowStyles.Border,
                    parentWindow: this);

                _buttonControl = new ButtonControl(
                    Windows.DefaultBounds,
                    text: "Push Me",
                    style: WindowStyles.Child | WindowStyles.Visible,
                    parentWindow: this);

                // this.SetLayout(Layout.FixedSize(_editControl, new Size(200, 400)));
                // this.SetLayout(Layout.Margin(10, Layout.FixedPercent(_editControl, new SizeF(.6f, .4f), VerticalAlignment.Top, HorizontalAlignment.Left)));

                //this.SetLayout(Layout.Horizontal(
                //    (.5f, Layout.Margin(5, Layout.Fill(_editControl))),
                //    (.5f, Layout.Empty)));

                this.SetLayout(Layout.Vertical(
                    (.5f, Layout.Margin((5, 5, 0, 0), Layout.Fill(_editControl))),
                    (.5f, Layout.Horizontal(
                        (.7f, Layout.Empty),
                        (.3f, Layout.FixedPercent(_buttonControl, .5f))))));
            }
        }
    }
}
