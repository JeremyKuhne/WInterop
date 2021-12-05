// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using WInterop.Errors;
using WInterop.Storage.Native;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Storage;

public class BackupReader : IDisposable
{
    private IntPtr _context = IntPtr.Zero;
    private readonly SafeFileHandle _fileHandle;
    private readonly StringBuffer _buffer = StringBufferCache.Instance.Acquire();

    // BackupReader requires us to read the header and its string separately. Given packing, the
    // string starts a uint in from the end.
    private static readonly unsafe uint s_headerSize = (uint)sizeof(WIN32_STREAM_ID) - sizeof(uint);

    public BackupReader(SafeFileHandle fileHandle)
    {
        _fileHandle = fileHandle;
    }

    public unsafe BackupStreamInformation? GetNextInfo()
    {
        void* buffer = _buffer.VoidPointer;
        Error.ThrowLastErrorIfFalse(
            StorageImports.BackupRead(
                hFile: _fileHandle,
                lpBuffer: buffer,
                nNumberOfBytesToRead: s_headerSize,
                lpNumberOfBytesRead: out uint bytesRead,
                bAbort: false,
                bProcessSecurity: true,
                context: ref _context));

        // Exit if at the end
        if (bytesRead == 0) return null;

        WIN32_STREAM_ID* streamId = (WIN32_STREAM_ID*)buffer;
        if (streamId->dwStreamNameSize > 0)
        {
            _buffer.EnsureByteCapacity(s_headerSize + streamId->dwStreamNameSize);
            Error.ThrowLastErrorIfFalse(
                StorageImports.BackupRead(
                    hFile: _fileHandle,
                    lpBuffer: Pointers.Offset(buffer, s_headerSize),
                    nNumberOfBytesToRead: streamId->dwStreamNameSize,
                    lpNumberOfBytesRead: out bytesRead,
                    bAbort: false,
                    bProcessSecurity: true,
                    context: ref _context));
        }

        if (streamId->Size.QuadPart > 0)
        {
            // Move to the next header, if any
            if (!StorageImports.BackupSeek(
                hFile: _fileHandle,
                dwLowBytesToSeek: uint.MaxValue,
                dwHighBytesToSeek: int.MaxValue,
                lpdwLowByteSeeked: out _,
                lpdwHighByteSeeked: out _,
                context: ref _context))
            {
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_SEEK);
            }
        }

        return new BackupStreamInformation
        {
            Name = new((char*)streamId->cStreamName, 0, (int)streamId->dwStreamNameSize),
            StreamType = (BackupStreamType)streamId->dwStreamId,
            Size = streamId->Size.QuadPart
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

        if (_context != IntPtr.Zero)
        {
            // Free the context memory
            if (!StorageImports.BackupRead(
                hFile: _fileHandle,
                lpBuffer: null,
                nNumberOfBytesToRead: 0,
                lpNumberOfBytesRead: out _,
                bAbort: true,
                bProcessSecurity: false,
                context: ref _context))
            {
#if DEBUG
                Error.ThrowLastError();
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