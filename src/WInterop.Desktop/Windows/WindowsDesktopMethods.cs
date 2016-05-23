// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WInterop.ErrorHandling;
using WInterop.Handles;
using WInterop.Handles.Desktop;
using WInterop.Windows.Desktop;

namespace WInterop.Windows
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static class WindowsDesktopMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633515.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetWindow(
                WindowHandle hWnd,
                GetWindowOptions uCmd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633509.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetNextWindow(
                WindowHandle hWnd,
                GetWindowOptions uCmd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633510.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetParent(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633502.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetAncestor(
                WindowHandle hWnd,
                GetWindowOptions gaFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633514.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern WindowHandle GetTopWindow(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633505.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetForegroundWindow();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633512.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetShellWindow();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633504.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern WindowHandle GetDesktopWindow();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633528.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsWindow(WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633529.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsWindowUnicode(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633530.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsWindowVisible(
                WindowHandle hWnd);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633524.aspx
            [DllImport(Libraries.User32, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool IsChild(
                WindowHandle hWndParent,
                WindowHandle hWnd);

            [DllImport(Libraries.User32, ExactSpelling = true)]
            public static extern int IsGUIThread(
                bool bConvert);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633586.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern Atom RegisterClass(
                WNDCLASS lpWndClass);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms633587.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            public static extern Atom RegisterClassEx(
                WNDCLASSEX lpwcx);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644899.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnregisterClass(
                IntPtr lpClassName,
                ModuleHandle hInstance);
        }

        public static WindowHandle GetShellWindow()
        {
            return Direct.GetShellWindow();
        }

        public static WindowHandle GetDesktopWindow()
        {
            return Direct.GetDesktopWindow();
        }

        public static bool IsWindow(WindowHandle handle)
        {
            return Direct.IsWindow(handle);
        }

        public static bool IsWindowVisible(WindowHandle handle)
        {
            return Direct.IsWindowVisible(handle);
        }

        public static bool IsWindowUnicode(WindowHandle handle)
        {
            return Direct.IsWindowUnicode(handle);
        }

        public static WindowHandle GetTopWindow(WindowHandle handle)
        {
            return Direct.GetTopWindow(handle);
        }

        public static bool IsGuiThread(bool convertToGuiIfFalse = false)
        {
            int result = Direct.IsGUIThread(convertToGuiIfFalse);
            if (result == 0
                || (convertToGuiIfFalse & result == (int)WindowsError.ERROR_NOT_ENOUGH_MEMORY))
                return false;
            else
                return true;
        }

        public static void UnregisterClass(Atom atom, ModuleHandle module)
        {
            if (!Direct.UnregisterClass(atom, module))
                throw ErrorHelper.GetIoExceptionForLastError();
        }

        public static void UnregisterClass(string className, ModuleHandle module)
        {
            unsafe
            {
                fixed (char* name = className)
                {
                    if (!Direct.UnregisterClass((IntPtr)name, module))
                        throw ErrorHelper.GetIoExceptionForLastError();
                }
            }
        }
    }
}
