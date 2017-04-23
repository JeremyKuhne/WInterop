// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WInterop.Backup.Types;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.Handles.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Backup
{
    public class BackupReader : IDisposable
    {
        private IntPtr _context = IntPtr.Zero;
        private SafeFileHandle _fileHandle;
        private HeapBuffer _buffer = new HeapBuffer(4096);

        public BackupReader(SafeFileHandle fileHandle)
        {
            _fileHandle = fileHandle;
        }

        public unsafe BackupStreamInformation? GetNextInfo()
        {
            if (!BackupMethods.Imports.BackupRead(
                hFile: _fileHandle,
                lpBuffer: _buffer,
                nNumberOfBytesToRead: (uint)sizeof(WIN32_STREAM_ID),
                lpNumberOfBytesRead: out uint bytesRead,
                bAbort: false,
                bProcessSecurity: true,
                context: ref _context))
            {
                throw Errors.GetIoExceptionForLastError();
            }

            // Exit if at the end
            if (bytesRead == 0) return null;

            WIN32_STREAM_ID streamId = *((WIN32_STREAM_ID*)_buffer.DangerousGetHandle());
            string name = null;
            if (streamId.dwStreamNameSize > 0)
            {
                _buffer.EnsureByteCapacity(streamId.dwStreamNameSize);
                if (!BackupMethods.Imports.BackupRead(
                    hFile: _fileHandle,
                    lpBuffer: _buffer,
                    nNumberOfBytesToRead: streamId.dwStreamNameSize,
                    lpNumberOfBytesRead: out bytesRead,
                    bAbort: false,
                    bProcessSecurity: true,
                    context: ref _context))
                {
                    throw Errors.GetIoExceptionForLastError();
                }
                name = Marshal.PtrToStringUni(_buffer.DangerousGetHandle(), (int)bytesRead / 2);
            }

            if (streamId.Size > 0)
            {
                // Move to the next header, if any
                if (!BackupMethods.Imports.BackupSeek(
                    hFile: _fileHandle,
                    dwLowBytesToSeek: uint.MaxValue,
                    dwHighBytesToSeek: int.MaxValue,
                    lpdwLowByteSeeked: out _,
                    lpdwHighByteSeeked: out _,
                    context: ref _context))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_SEEK);
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
            if (disposing)
            {
                _buffer?.Dispose();
            }
            _buffer = null;

            if (_context != IntPtr.Zero)
            {
                // Free the context memory
                if (!BackupMethods.Imports.BackupRead(
                    hFile: _fileHandle,
                    lpBuffer: EmptySafeHandle.Instance,
                    nNumberOfBytesToRead: 0,
                    lpNumberOfBytesRead: out _,
                    bAbort: true,
                    bProcessSecurity: false,
                    context: ref _context))
                {
#if DEBUG
                    throw Errors.GetIoExceptionForLastError();
#endif
                }

                _context = IntPtr.Zero;
            }
        }

        ~BackupReader()
        {
            Dispose(false);
        }
    }
}
