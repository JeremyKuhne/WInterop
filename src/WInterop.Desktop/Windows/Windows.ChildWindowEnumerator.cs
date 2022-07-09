// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Windows;

public static partial class Windows
{
    private ref struct ChildWindowEnumerator
    {
        private readonly GCHandle _callback;

        public unsafe ChildWindowEnumerator(WindowHandle parent, Func<WindowHandle, bool> callback)
        {
            _callback = GCHandle.Alloc(callback, GCHandleType.Normal);
            TerraFXWindows.EnumChildWindows(parent, &CallBack, (IntPtr)_callback);
        }

        [UnmanagedCallersOnly]
        private static BOOL CallBack(HWND hwnd, LPARAM lParam)
        {
            var callback = (Func<WindowHandle, bool>)(GCHandle.FromIntPtr(lParam).Target!);
            return callback(hwnd);
        }

        public void Dispose()
        {
            if (_callback.IsAllocated)
            {
                _callback.Free();
            }
        }
    }
}