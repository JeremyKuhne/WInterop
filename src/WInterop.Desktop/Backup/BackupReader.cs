// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Backup.DataTypes;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Handles.DataTypes;
using WInterop.Support.Buffers;

namespace WInterop.Backup
{
    public class BackupReader : IDisposable
    {
        private IntPtr _context = IntPtr.Zero;
        private SafeFileHandle _fileHandle;
        private NativeBuffer _buffer = new NativeBuffer(4096);
        private static uint WIN32_STREAM_ID_SIZE = (uint)Marshal.SizeOf<WIN32_STREAM_ID>();

        public BackupReader(SafeFileHandle fileHandle)
        {
            _fileHandle = fileHandle;
        }

        public BackupStreamInformation? GetNextInfo()
        {
            uint bytesRead;
            if (!BackupDesktopMethods.Direct.BackupRead(
                hFile: _fileHandle,
                lpBuffer: _buffer,
                nNumberOfBytesToRead: WIN32_STREAM_ID_SIZE,
                lpNumberOfBytesRead: out bytesRead,
                bAbort: false,
                bProcessSecurity: true,
                context: ref _context))
            {
                throw ErrorHelper.GetIoExceptionForLastError();
            }

            // Exit if at the end
            if (bytesRead == 0) return null;

            WIN32_STREAM_ID streamId = Marshal.PtrToStructure<WIN32_STREAM_ID>(_buffer.DangerousGetHandle());
            string name = null;
            if (streamId.dwStreamNameSize > 0)
            {
                _buffer.EnsureByteCapacity(streamId.dwStreamNameSize);
                if (!BackupDesktopMethods.Direct.BackupRead(
                    hFile: _fileHandle,
                    lpBuffer: _buffer,
                    nNumberOfBytesToRead: streamId.dwStreamNameSize,
                    lpNumberOfBytesRead: out bytesRead,
                    bAbort: false,
                    bProcessSecurity: true,
                    context: ref _context))
                {
                    throw ErrorHelper.GetIoExceptionForLastError();
                }
                name = Marshal.PtrToStringUni(_buffer.DangerousGetHandle(), (int)bytesRead / 2);
            }

            if (streamId.Size > 0)
            {
                // Move to the next header, if any
                uint low, high;
                if (!BackupDesktopMethods.Direct.BackupSeek(
                    hFile: _fileHandle,
                    dwLowBytesToSeek: uint.MaxValue,
                    dwHighBytesToSeek: int.MaxValue,
                    lpdwLowByteSeeked: out low,
                    lpdwHighByteSeeked: out high,
                    context: ref _context))
                {
                    WindowsError error = ErrorHelper.GetLastError();
                    if (error != WindowsError.ERROR_SEEK)
                    {
                        throw ErrorHelper.GetIoExceptionForError(error);
                    }
                }
            }

            return new BackupStreamInformation
            {
                Name = name,
                StreamType = streamId.dwStreamId,
                Size = streamId.Size
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            _buffer.Dispose();
            _buffer = null;

            if (_context != IntPtr.Zero)
            {
                uint bytesRead;
                if (!BackupDesktopMethods.Direct.BackupRead(
                    hFile: _fileHandle,
                    lpBuffer: EmptySafeHandle.Instance,
                    nNumberOfBytesToRead: 0,
                    lpNumberOfBytesRead: out bytesRead,
                    bAbort: true,
                    bProcessSecurity: false,
                    context: ref _context))
                {
                    throw ErrorHelper.GetIoExceptionForLastError();
                }
            }
        }

        ~BackupReader()
        {
            Dispose(false);
        }
    }
}
