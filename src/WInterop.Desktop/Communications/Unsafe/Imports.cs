// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using WInterop.Synchronization;
using WInterop.Windows;

namespace WInterop.Communications.Unsafe
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363260.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool GetCommState(
            SafeFileHandle hFile,
            ref DeviceControlBlock lpDCB);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363436.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetCommState(
            SafeFileHandle hFile,
            ref DeviceControlBlock lpDCB);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363143.aspx
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern bool BuildCommDCBW(
            string lpDef,
            out DeviceControlBlock lpDCB);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363259.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool GetCommProperties(
            SafeFileHandle hFile,
            out CommunicationsProperties lpCommProp);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363262.aspx
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern bool GetDefaultCommConfigW(
            string lpszName,
            ref CommunicationsConfig lpCC,
            ref uint lpdwSize);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363256.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool GetCommConfig(
            SafeFileHandle hCommDev,
            ref CommunicationsConfig lpCC,
            ref uint lpdwSize);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363187.aspx
        [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
        public static extern bool CommConfigDialogW(
            string lpszName,
            WindowHandle hWnd,
            ref CommunicationsConfig lpCC);

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363473.aspx
        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public static extern bool TransmitCommChar(
            SafeFileHandle hFile,
            sbyte cChar);

        [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
        public unsafe static extern bool WaitCommEvent(
            SafeFileHandle hFile,
            out EventMask lpEvtMask,
            OVERLAPPED* lpOverlapped);
    }
}
