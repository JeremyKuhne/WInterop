// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.DeviceManagement.Types;
using WInterop.ErrorHandling.Types;
using WInterop.FileManagement.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.DeviceManagement
{
    public static partial class DeviceMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
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
            var mountManager = FileManagement.FileMethods.CreateFile(
                @"\\?\MountPointManager", 0, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING, FileAttributes.FILE_ATTRIBUTE_NORMAL);

            uint controlCode = DeviceMacros.CTL_CODE(ControlCodeDeviceType.MOUNTMGRCONTROLTYPE, 12, ControlCodeMethod.METHOD_BUFFERED, ControlCodeAccess.FILE_ANY_ACCESS);

            // Read ulong then get string
            string dosVolumePath = null;

            BufferHelper.BufferInvoke((StringBuffer inBuffer) =>
            {
                // The input is MOUNTMGR_TARGET_NAME which is a short length in bytes followed by the unicode string
                // https://msdn.microsoft.com/en-us/library/windows/hardware/ff562289.aspx

                inBuffer.Append((char)(volume.Length * sizeof(char)));
                    inBuffer.Append(volume);

                BufferHelper.BufferInvoke((StringBuffer outBuffer) =>
                {
                    // Give enough for roughly 50 characters for a start
                    outBuffer.EnsureCharCapacity(50);

                    while (!Imports.DeviceIoControl(
                        hDevice: mountManager.DangerousGetHandle(),
                        dwIoControlCode: controlCode,
                        lpInBuffer: inBuffer.DangerousGetHandle(),
                        nInBufferSize: checked((uint)inBuffer.ByteCapacity),
                        lpOutBuffer: outBuffer.DangerousGetHandle(),
                        nOutBufferSize: checked((uint)outBuffer.ByteCapacity),
                        lpBytesReturned: out _,
                        lpOverlapped: IntPtr.Zero))
                    {
                        WindowsError error = Errors.GetLastError();
                        switch (error)
                        {
                            case WindowsError.ERROR_MORE_DATA:
                                outBuffer.EnsureByteCapacity(checked(outBuffer.ByteCapacity * 2));
                                break;
                            default:
                                throw Errors.GetIoExceptionForError(error, volume);
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
