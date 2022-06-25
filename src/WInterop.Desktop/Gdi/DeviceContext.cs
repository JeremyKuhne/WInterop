// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace WInterop.Gdi;

/// <summary>
///  DeviceContext handle (HDC)
/// </summary>
public readonly struct DeviceContext : IDisposable
{
    public HDC Handle { get; }
    private readonly WindowHandle _window;
    private readonly CollectionType _type;

    public DeviceContext(HDC handle, bool ownsHandle = false)
    {
        _window = default;
        Handle = handle;
        _type = ownsHandle ? CollectionType.Delete : CollectionType.None;
    }

    public DeviceContext(HDC handle, WindowHandle windowHandle)
    {
        _window = windowHandle;
        Handle = handle;
        _type = CollectionType.Release;
    }

    public DeviceContext(in PAINTSTRUCT paint, WindowHandle windowHandle)
    {
        _window = windowHandle;
        _type = CollectionType.EndPaint;
        Handle = paint.hdc;
    }

    public unsafe void Dispose()
    {
        switch (_type)
        {
            case CollectionType.Delete:
                if (!TerraFXWindows.DeleteDC(Handle))
                { } // Debug.Fail("Failed to delete DC");
                break;
            case CollectionType.Release:
                if (TerraFXWindows.ReleaseDC(_window, Handle) == 0)
                { } // Debug.Fail("Failed to release DC");
                break;
            case CollectionType.EndPaint:
                PAINTSTRUCT ps = new()
                {
                    hdc = Handle
                };

                if (!TerraFXWindows.EndPaint(_window, &ps))
                { } // Debug.Fail("Failed to end paint");
                break;
            case CollectionType.None:
                break;
            default:
                throw new InvalidOperationException();
        }
    }

    public bool IsInvalid => Handle == HDC.INVALID_VALUE;

    public static implicit operator HDC(DeviceContext context) => context.Handle;
    public static unsafe explicit operator DeviceContext(WParam wparam) => new(new(wparam));

    private enum CollectionType
    {
        None,
        Delete,
        Release,
        EndPaint
    }
}