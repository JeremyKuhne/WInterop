// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Buffers;
using System.IO;
using WInterop.Errors;

namespace WInterop.Com
{
    public class DotNetStream : IStream
    {
        public DotNetStream(Stream stream)
        {
            InnerStream = stream;
        }

        public Stream InnerStream { get; private set; }

        unsafe uint IStream.Read(byte* pv, uint cb)
        {
            if (!InnerStream.CanRead)
                throw new InvalidOperationException();

            // This should be moved to Read(Span<byte>) when/where available
            byte[] sharedBuffer = ArrayPool<byte>.Shared.Rent(checked((int)cb));
            try
            {
                fixed (byte* ps = sharedBuffer)
                {
                    int read = InnerStream.Read(sharedBuffer, 0, (int)cb);
                    Buffer.MemoryCopy(ps, pv, cb, read);
                    return (uint)read;
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(sharedBuffer);
            }
        }

        unsafe uint IStream.Write(byte* pv, uint cb)
        {
            if (!InnerStream.CanWrite)
                throw new InvalidOperationException();

            // This should be moved to Write(ReadOnlySpan<byte>) when/where available
            byte[] sharedBuffer = ArrayPool<byte>.Shared.Rent(checked((int)cb));
            try
            {
                fixed (byte* ps = sharedBuffer)
                {
                    Buffer.MemoryCopy(pv, ps, sharedBuffer.LongLength, cb);
                    InnerStream.Write(sharedBuffer, 0, (int)cb);
                }
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(sharedBuffer);
            }

            return cb;
        }

        long IStream.Seek(long dlibMove, StreamSeek dwOrigin)
        {
            if (!InnerStream.CanSeek)
                throw new InvalidOperationException();

            return InnerStream.Seek(dlibMove, (SeekOrigin)dwOrigin);
        }

        void IStream.SetSize(long libNewSize)
        {
            InnerStream.SetLength(libNewSize);
        }

        void IStream.CopyTo(IStream pstm, long cb, out long pcbRead, out long pcbWritten)
        {
            pcbRead = 0;
            pcbWritten = 0;
            long remaining = cb;

            byte[] sharedBuffer = ArrayPool<byte>.Shared.Rent(4096);
            try
            {
                // TODO:
                throw new NotImplementedException();
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(sharedBuffer);
            }
        }

        void IStream.Commit(StorageCommit grfCommitFlags)
        {
            InnerStream.Flush();
        }

        void IStream.Revert()
        {
            // Ignore. (Should this return STG_E_INVALIDFUNCTION? It isn't clear from the docs.)
        }

        HResult IStream.LockRegion(long libOffset, long cb, uint dwLockType)
        {
            // Not supported
            return HResult.STG_E_INVALIDFUNCTION;
        }

        HResult IStream.UnlockRegion(long libOffset, long cb, uint dwLockType)
        {
            // Not supported
            return HResult.STG_E_INVALIDFUNCTION;
        }

        void IStream.Stat(out STATSTG pstatstg, StatFlag grfStatFlag)
        {
            throw new NotImplementedException();
        }

        IStream IStream.Clone()
        {
            return new DotNetStream(InnerStream);
        }
    }
}
