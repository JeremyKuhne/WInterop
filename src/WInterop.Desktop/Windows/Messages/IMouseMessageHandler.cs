// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi;

namespace WInterop.Windows;

public interface IMouseMessageHandler
{
    public void OnMouseMove(Point position, MouseKey mouseState) { }
    public void OnButtonDown(Point position, Button button, MouseKey mouseState) { }
    public void OnButtonUp(Point position, Button button, MouseKey mouseState) { }
    public void OnButtonDoubleClick(Point position, Button button, MouseKey mouseState) { }
    public void OnNonClientButtonDown(Point position, Button button, MouseKey mouseState) { }
    public void OnNonClientButtonUp(Point position, Button button, MouseKey mouseState) { }
    public void OnNonClientButtonDoubleClick(Point position, Button button, MouseKey mouseState) { }
}

public enum Button
{
    Left,
    Right,
    Middle,
    X1,
    X2
}

public class MouseHandler : IMouseMessageHandler
{
    private Window _attachedWindow;
    public event MouseMessageEvent? MouseUp;

    public MouseHandler(Window window)
    {
        window.MessageHandler += Window_MessageHandler;
        _attachedWindow = window;
    }

    private unsafe LResult? Window_MessageHandler(
        object sender,
        WindowHandle window,
        MessageType message,
        WParam wParam,
        LParam lParam)
    {
        switch (message)
        {
            case MessageType.MouseMove:
                OnMouseMove(*(PointS*)&lParam, (MouseKey)wParam);
                break;
            case MessageType.LeftButtonUp:
                OnButtonUp(*(PointS*)&lParam, Button.Left, (MouseKey)wParam);
                break;
            case MessageType.RightButtonUp:
            case MessageType.MiddleButtonUp:
            case MessageType.ExtraButtonUp:
                break;
        }

        return null;
    }

    public virtual void OnMouseMove(Point position, MouseKey mouseState) { }
    public virtual void OnButtonDown(Point position, Button button, MouseKey mouseState) { }

    public virtual void OnButtonUp(Point position, Button button, MouseKey mouseState)
    {
        MouseUp?.Invoke(_attachedWindow, _attachedWindow.Handle, position, button, mouseState);
    }

    public virtual void OnButtonDoubleClick(Point position, Button button, MouseKey mouseState) { }
    public virtual void OnNonClientButtonDown(Point position, Button button, MouseKey mouseState) { }
    public virtual void OnNonClientButtonUp(Point position, Button button, MouseKey mouseState) { }
    public virtual void OnNonClientButtonDoubleClick(Point position, Button button, MouseKey mouseState) { }
}