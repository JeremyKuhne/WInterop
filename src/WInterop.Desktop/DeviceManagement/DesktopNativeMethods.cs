// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Buffers;
using WInterop.DeviceManagement.Desktop;
using WInterop.ErrorHandling;
using WInterop.FileManagement;

namespace WInterop.DeviceManagement
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static class DesktopNativeMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363216.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DeviceIoControl(
                IntPtr hDevice,
                uint dwIoControlCode,
                IntPtr lpInBuffer,
                uint nInBufferSize,
                IntPtr lpOutBuffer,
                uint nOutBufferSize,
                out uint lpBytesReturned,
                IntPtr lpOverlapped);
        }

        // Access to the MountPointManager is denied for store apps
        public static string QueryDosVolumePath(string volume)
        {
            var mountManager = FileManagement.NativeMethods.CreateFile(
                @"\\?\MountPointManager", 0, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING, WInterop.FileManagement.FileAttributes.FILE_ATTRIBUTE_NORMAL);

            uint controlCode = DeviceMacros.CTL_CODE(ControlCodeDeviceType.MOUNTMGRCONTROLTYPE, 12, ControlCodeMethod.METHOD_BUFFERED, ControlCodeAccess.FILE_ANY_ACCESS);

            // Read ulong then get string
            string dosVolumePath = null;

            StringBufferCache.CachedBufferInvoke((inBuffer) =>
            {
                // The input is MOUNTMGR_TARGET_NAME which is a short length in bytes followed by the unicode string
                // https://msdn.microsoft.com/en-us/library/windows/hardware/ff562289.aspx

                inBuffer.Append((char)(volume.Length * sizeof(char)));
                    inBuffer.Append(volume);

                StringBufferCache.CachedBufferInvoke((outBuffer) =>
                {
                    // Give enough for roughly 50 characters for a start
                    outBuffer.EnsureCharCapacity(50);

                    uint bytesReturned;

                    while (!Direct.DeviceIoControl(
                        hDevice: mountManager.DangerousGetHandle(),
                        dwIoControlCode: controlCode,
                        lpInBuffer: inBuffer.DangerousGetHandle(),
                        nInBufferSize: checked((uint)inBuffer.ByteCapacity),
                        lpOutBuffer: outBuffer.DangerousGetHandle(),
                        nOutBufferSize: checked((uint)outBuffer.ByteCapacity),
                        lpBytesReturned: out bytesReturned,
                        lpOverlapped: IntPtr.Zero))
                    {
                        uint error = (uint)Marshal.GetLastWin32Error();
                        switch (error)
                        {
                            case WinErrors.ERROR_MORE_DATA:
                                outBuffer.EnsureByteCapacity(checked(outBuffer.ByteCapacity * 2));
                                break;
                            default:
                                throw ErrorHelper.GetIoExceptionForError(error, volume);
                        }
                    }

                // MOUNTMGR_VOLUME_PATHS is a uint length followed by a multi string (double null terminated)
                // we only care about the first string in this case (should only be one?) so we can skip beyond
                // the 4 bytes and read to null.
                unsafe
                    {
                        dosVolumePath = new string(outBuffer.CharPointer + 2);
                    }
                });
            });

            return dosVolumePath;
        }
    }
}
