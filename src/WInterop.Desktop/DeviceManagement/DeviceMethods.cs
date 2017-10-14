// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.DeviceManagement.Types;
using WInterop.ErrorHandling.Types;
using WInterop.FileManagement.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.DeviceManagement
{
    public static partial class DeviceMethods
    {
        // Access to the MountPointManager is denied for store apps
        public unsafe static string QueryDosVolumePath(string volume)
        {
            var mountManager = FileManagement.FileMethods.CreateFile(
                @"\\?\MountPointManager", CreationDisposition.OpenExisting, 0);

            ControlCode controlCode = ControlCodes.MountManager.QueryDosVolumePath;

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
                        hDevice: mountManager,
                        dwIoControlCode: controlCode,
                        lpInBuffer: inBuffer.VoidPointer,
                        nInBufferSize: checked((uint)inBuffer.ByteCapacity),
                        lpOutBuffer: outBuffer.VoidPointer,
                        nOutBufferSize: checked((uint)outBuffer.ByteCapacity),
                        lpBytesReturned: out _,
                        lpOverlapped: null))
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
