// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using WInterop.ErrorHandling;
using WInterop.Storage;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Storage
{
    public class BackupReader : IDisposable
    {
        private IntPtr _context = IntPtr.Zero;
        private SafeFileHandle _fileHandle;
        private StringBuffer _buffer = StringBufferCache.Instance.Acquire();

        // BackupReader requires us to read the header and its string separately. Given packing, the
        // string starts a uint in from the end.
        private unsafe static uint s_headerSize = (uint)sizeof(WIN32_STREAM_ID) - sizeof(uint);

        public BackupReader(SafeFileHandle fileHandle)
        {
            _fileHandle = fileHandle;
        }

        public unsafe BackupStreamInformation? GetNextInfo()
        {
            void* buffer = _buffer.VoidPointer;
            if (!StorageMethods.Imports.BackupRead(
                hFile: _fileHandle,
                lpBuffer: buffer,
                nNumberOfBytesToRead: s_headerSize,
                lpNumberOfBytesRead: out uint bytesRead,
                bAbort: false,
                bProcessSecurity: true,
                context: ref _context))
            {
                throw Errors.GetIoExceptionForLastError();
            }

            // Exit if at the end
            if (bytesRead == 0) return null;

            WIN32_STREAM_ID* streamId = (WIN32_STREAM_ID*)buffer;
            if (streamId->dwStreamNameSize > 0)
            {
                _buffer.EnsureByteCapacity(s_headerSize + streamId->dwStreamNameSize);
                if (!StorageMethods.Imports.BackupRead(
                    hFile: _fileHandle,
                    lpBuffer: Pointers.Offset(buffer, s_headerSize),
                    nNumberOfBytesToRead: streamId->dwStreamNameSize,
                    lpNumberOfBytesRead: out bytesRead,
                    bAbort: false,
                    bProcessSecurity: true,
                    context: ref _context))
                {
                    throw Errors.GetIoExceptionForLastError();
                }
            }

            if (streamId->Size > 0)
            {
                // Move to the next header, if any
                if (!StorageMethods.Imports.BackupSeek(
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
                Name = streamId->cStreamName.CreateString(),
                StreamType = streamId->dwStreamId,
                Size = streamId->Size
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private unsafe void Dispose(bool disposing)
        {
            if (disposing)
            {
                StringBufferCache.Instance.Release(_buffer);
            }
            _buffer = null;

            if (_context != IntPtr.Zero)
            {
                // Free the context memory
                if (!StorageMethods.Imports.BackupRead(
                    hFile: _fileHandle,
                    lpBuffer: null,
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
