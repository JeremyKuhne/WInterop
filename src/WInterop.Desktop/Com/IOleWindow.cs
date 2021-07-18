// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Windows.Native;

namespace WInterop.Com
{
    /// <summary>
    ///  The IOleWindow interface provides methods that allow an application to obtain the
    ///  handle to the various windows that participate in in-place activation.
    /// </summary>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/oleidl/nn-oleidl-iolewindow"/>
    /// <see cref="https://docs.microsoft.com/en-us/windows/win32/com/implementing-in-place-activation"/>
    [ComImport,
        Guid("00000114-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IOleWindow
    {
        /// <summary>
        ///  Retrieves a handle to one of the windows participating in in-place activation.
        /// </summary>
        /// <see cref="https://docs.microsoft.com/windows/win32/api/oleidl/nf-oleidl-iolewindow-getwindow"/>
        HWND GetWindow();

        /// <summary>
        ///  Determines whether context-sensitive help mode should be entered during an in-place activation session.
        /// </summary>
        /// <see cref="https://docs.microsoft.com/windows/win32/api/oleidl/nf-oleidl-iolewindow-contextsensitivehelp"/>
        void ContextSensitiveHelp([MarshalAs(UnmanagedType.Bool)] bool fEnterMode);
    }
}