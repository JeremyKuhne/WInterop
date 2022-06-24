// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using WInterop.Errors;
using WInterop.Storage.Native;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Storage;

public unsafe class BackupReader : IDisposable
{
    private void* _context = null;
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
        byte* buffer = _buffer.BytePointer;
        uint bytesRead;

        fixed (void** c = &_context)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.BackupRead(
                    hFile: _fileHandle.ToHANDLE(),
                    lpBuffer: buffer,
                    nNumberOfBytesToRead: s_headerSize,
                    lpNumberOfBytesRead: &bytesRead,
                    bAbort: false,
                    bProcessSecurity: true,
                    lpContext: c));
        }

        // Exit if at the end
        if (bytesRead == 0) return null;

        WIN32_STREAM_ID* streamId = (WIN32_STREAM_ID*)buffer;
        if (streamId->dwStreamNameSize > 0)
        {
            _buffer.EnsureByteCapacity(s_headerSize + streamId->dwStreamNameSize);

            fixed (void** c = &_context)
            {
                Error.ThrowLastErrorIfFalse(
                    TerraFXWindows.BackupRead(
                        hFile: _fileHandle.ToHANDLE(),
                        lpBuffer: buffer + s_headerSize,
                        nNumberOfBytesToRead: streamId->dwStreamNameSize,
                        lpNumberOfBytesRead: &bytesRead,
                        bAbort: false,
                        bProcessSecurity: true,
                        lpContext: c));
            }
        }

        if (streamId->Size.QuadPart > 0)
        {
            uint lowSeeked;
            uint highSeeked;

            // Move to the next header, if any
            fixed (void** c = &_context)
            {
                if (!TerraFXWindows.BackupSeek(
                    hFile: _fileHandle.ToHANDLE(),
                    dwLowBytesToSeek: uint.MaxValue,
                    dwHighBytesToSeek: int.MaxValue,
                    lpdwLowByteSeeked: &lowSeeked,
                    lpdwHighByteSeeked: &highSeeked,
                    lpContext: c))
                {
                    Error.ThrowIfLastErrorNot(WindowsError.ERROR_SEEK);
                }
            }
        }

        return new BackupStreamInformation
        {
            Name = new((char*)streamId->cStreamName, 0, (int)streamId->dwStreamNameSize / sizeof(char)),
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

        if (_context is not null)
        {
            fixed (void** c = &_context)
            {
                uint bytesRead;

                // Free the context memory
                if (!TerraFXWindows.BackupRead(
                    hFile: _fileHandle.ToHANDLE(),
                    lpBuffer: null,
                    nNumberOfBytesToRead: 0,
                    lpNumberOfBytesRead: &bytesRead,
                    bAbort: true,
                    bProcessSecurity: false,
                    lpContext: c))
                {
#if DEBUG
                    Error.ThrowLastError();
#endif
                }
            }

            _context = null;
        }
    }

    ~BackupReader()
    {
        Dispose(false);
    }
}