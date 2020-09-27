// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// Modified from .NET source code which is licensed under the MIT license to the .NET Foundation.

using System.Buffers;
using System.IO;
using WInterop.Com;
using WInterop.Com.Native;
using WInterop.Errors;

namespace System.Drawing.Internal
{
    public sealed class DotNetStream : IStream
    {
        private readonly Stream _wrappedStream;

        public DotNetStream(Stream stream) => _wrappedStream = stream;

        public IStream Clone() => new DotNetStream(_wrappedStream);

        public void Commit(StorageCommit grfCommitFlags) => _wrappedStream.Flush();

        public unsafe void CopyTo(IStream pstm, ulong cb, ulong* pcbRead, ulong* pcbWritten)
        {
            byte[] buffer = ArrayPool<byte>.Shared.Rent(4096);

            ulong remaining = cb;
            ulong totalWritten = 0;
            ulong totalRead = 0;

            fixed (byte* b = buffer)
            {
                while (remaining > 0)
                {
                    uint read = remaining < (ulong)buffer.Length ? (uint)remaining : (uint)buffer.Length;
                    Read(b, read, &read);
                    remaining -= read;
                    totalRead += read;

                    if (read == 0)
                    {
                        break;
                    }

                    uint written;
                    pstm.Write(b, read, &written);
                    totalWritten += written;
                }
            }

            ArrayPool<byte>.Shared.Return(buffer);

            if (pcbRead != null)
            {
                *pcbRead = totalRead;
            }

            if (pcbWritten != null)
            {
                *pcbWritten = totalWritten;
            }
        }

        public unsafe void Read(byte* pv, uint cb, uint* pcbRead)
        {
            Span<byte> buffer = new Span<byte>(pv, checked((int)cb));
            int read = _wrappedStream.Read(buffer);

            if (pcbRead != null)
            {
                *pcbRead = (uint)read;
            }
        }

        public void Revert()
        {
            // We never report ourselves as Transacted, so we can just ignore this.
        }

        public unsafe void Seek(long dlibMove, StreamSeek dwOrigin, ulong* plibNewPosition)
        {
            long length = _wrappedStream.Length;
            switch (dwOrigin)
            {
                case StreamSeek.Set:
                    _wrappedStream.Position = dlibMove;
                    break;
                case StreamSeek.End:
                    _wrappedStream.Position = length + dlibMove;
                    break;
                case StreamSeek.Current:
                    _wrappedStream.Position += dlibMove;
                    break;
            }

            if (plibNewPosition == null)
                return;

            *plibNewPosition = (ulong)_wrappedStream.Position;
        }

        public void SetSize(ulong value)
        {
            _wrappedStream.SetLength(checked((long)value));
        }

        public void Stat(out STATSTG pstatstg, StatFlag grfStatFlag)
        {
            pstatstg = new STATSTG
            {
                cbSize = (ulong)_wrappedStream.Length,
                type = StorageType.Stream,

                // Default read/write access is STGM_READ, which == 0
                grfMode = _wrappedStream.CanWrite
                    ? _wrappedStream.CanRead
                        ? StorageMode.ReadWrite
                        : StorageMode.Write
                    : default
            };

            if (grfStatFlag == StatFlag.Default)
            {
                // Caller wants a name
                pstatstg.AllocName(_wrappedStream is FileStream fs ? fs.Name : _wrappedStream.ToString());
            }
        }

        public unsafe void Write(byte* pv, uint cb, uint* pcbWritten)
        {
            var buffer = new ReadOnlySpan<byte>(pv, checked((int)cb));
            _wrappedStream.Write(buffer);

            if (pcbWritten != null)
            {
                *pcbWritten = cb;
            }
        }

        public HResult LockRegion(ulong libOffset, ulong cb, uint dwLockType)
        {
            // Documented way to say we don't support locking
            return HResult.STG_E_INVALIDFUNCTION;
        }

        public HResult UnlockRegion(ulong libOffset, ulong cb, uint dwLockType)
        {
            // Documented way to say we don't support locking
            return HResult.STG_E_INVALIDFUNCTION;
        }
    }
}
