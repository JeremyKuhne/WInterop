// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Com.Types
{
    [ComImport,
        Guid("0000000c-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IStream
    {
        /// <summary>
        /// Reads a specified number of bytes from the stream object into memory starting at the current seek pointer.
        /// </summary>
        /// <param name="pv">A pointer to the buffer which the stream data is read into.</param>
        /// <param name="cb">The number of bytes of data to read from the stream object.</param>
        /// <returns>The actual number of bytes read from the stream object.</returns>
        uint Read(
            ref byte pv,
            uint cb);

        /// <summary>
        /// Writes a specified number of bytes into the stream object starting at the current seek pointer.
        /// </summary>
        /// <param name="pv">A pointer to the buffer that contains the data that is to be written to the stream.</param>
        /// <param name="cb">The number of bytes of data to attempt to write into the stream.</param>
        /// <returns>The actual number of bytes written to the stream object. </returns>
        uint Write(
            ref byte pv,
            uint cb);

        /// <summary>
        /// Seeks to the given offset from the given origin. Returns the new position.
        /// </summary>
        /// <param name="dlibMove">Move the given number of bytes from <paramref name="dwOrigin"/>.</param>
        /// <param name="dwOrigin">The start point for <paramref name="dlibMove"/>.</param>
        /// <returns>The new position.</returns>
        long Seek(
            long dlibMove,
            StreamSeek dwOrigin);

        void SetSize(long libNewSize);

        void CopyTo(
            IStream pstm,
            long cb,
            out long pcbRead,
            out long pcbWritten);

        void Commit(StorageCommit grfCommitFlags);

        void Revert();

        void LockRegion(
            long libOffset,
            long cb,
            uint dwLockType);

        void UnlockRegion(
            long libOffset,
            long cb,
            uint dwLockType);

        void Stat(
            out STATSTG pstatstg,
            StatFlag grfStatFlag);

        IStream Clone();
    }
}
