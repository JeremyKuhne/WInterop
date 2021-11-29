// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop;
using WInterop.Gdi;
using WInterop.Windows;
using WInterop.Windows.Classes;

namespace Controls;

internal class Program
{
    [STAThread]
    private static void Main()
    {
        Windows.Run(new EditWindow("Edit control"));
    }

    private class EditWindow : Window
    {
        private readonly ReplaceableLayout _replaceableLayout;

        private readonly EditControl _editControl;
        private readonly ButtonControl _buttonControl;
        private readonly StaticControl _staticControl;
        private readonly TextLabelControl _textLabel;

        public EditWindow(string title) : base(
            new WindowClass(),
            Windows.DefaultBounds,
            text: title,
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

            _staticControl = new StaticControl(
                Windows.DefaultBounds,
                text: "You pushed it!",
                style: WindowStyles.Child | WindowStyles.Visible,
                parentWindow: this);

            _textLabel = new TextLabelControl(
                Windows.DefaultBounds,
                text: "Text Label Control",
                style: WindowStyles.Child | WindowStyles.Visible,
                parentWindow: this);

            var font = _staticControl.GetFont().GetLogicalFont();
            _staticControl.SetWindowText($"{font.FaceName.CreateString()} {font.Quality}");

            //this.SetLayout(Layout.FixedSize(new (200, 400), _editControl));

            //this.SetLayout(
            //    Layout.Margin(10, Layout.FixedPercent
            //        (new (.6f, .4f), _editControl, VerticalAlignment.Top, HorizontalAlignment.Left)));

            //this.SetLayout(Layout.Horizontal(
            //    (.5f, Layout.Margin(5, Layout.Fill(_editControl))),
            //    (.5f, Layout.Empty)));

            _replaceableLayout = new ReplaceableLayout(_textLabel);

            this.AddLayoutHandler(Layout.Vertical(
                (.5f, Layout.Margin((5, 5, 0, 0), Layout.Fill(_editControl))),
                (.5f, Layout.Horizontal(
                    (.7f, Layout.FixedPercent(.4f, _replaceableLayout)),
                    (.3f, Layout.FixedPercent(.5f, _buttonControl))))));

            MouseHandler handler = new MouseHandler(_buttonControl);
            handler.MouseUp += Handler_MouseUp;
        }

        private void Handler_MouseUp(object sender, WindowHandle window, Point position, Button button, MouseKey mouseState)
        {
            if (_replaceableLayout.Handler == _staticControl)
            {
                _staticControl.ShowWindow(ShowWindowCommand.Hide);
                _textLabel.ShowWindow(ShowWindowCommand.Show);
                _replaceableLayout.Handler = _textLabel;
            }
            else
            {
                _replaceableLayout.Handler = _staticControl;
                _textLabel.ShowWindow(ShowWindowCommand.Hide);
                _staticControl.ShowWindow(ShowWindowCommand.Show);
            }
        }
    }
}
