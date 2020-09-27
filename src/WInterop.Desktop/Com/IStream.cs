// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// Modified from .NET source code which is licensed under the MIT license to the .NET Foundation.

using System.IO;
using System.Runtime.InteropServices;
using WInterop.Com.Native;
using WInterop.Errors;

namespace WInterop.Com
{
    /// <summary>
    ///  COM IStream interface. <see href="https://docs.microsoft.com/windows/win32/api/objidl/nn-objidl-istream"/>
    /// </summary>
    /// <remarks>
    ///  The definition in <see cref="System.Runtime.InteropServices.ComTypes"/> does not lend
    ///  itself to efficiently accessing / implementing IStream.
    /// </remarks>
    [ComImport,
        Guid("0000000C-0000-0000-C000-000000000046"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public unsafe interface IStream
    {
        /// <summary>
        ///  Reads a specified number of bytes from the stream object into memory starting at the current seek pointer.
        /// </summary>
        /// <param name="pv">A pointer to the buffer which the stream data is read into.</param>
        /// <param name="cb">The number of bytes of data to read from the stream object.</param>
        /// <param name="pcbRead">Optional actual number of read bytes.</param>
        void Read(byte* pv, uint cb, uint* pcbRead);

        /// <summary>
        ///  Writes a specified number of bytes into the stream object starting at the current seek pointer.
        /// </summary>
        /// <param name="pv">A pointer to the buffer that contains the data that is to be written to the stream.</param>
        /// <param name="cb">The number of bytes of data to attempt to write into the stream.</param>
        /// <param name="pcbWritten">Optional actual number of written bytes.</param>
        void Write(byte* pv, uint cb, uint* pcbWritten);

        /// <summary>
        ///  Seeks to the given offset from the given origin. Returns the new position.
        /// </summary>
        /// <param name="dlibMove">Move the given number of bytes from <paramref name="dwOrigin"/>.</param>
        /// <param name="dwOrigin">The start point for <paramref name="dlibMove"/>.</param>
        /// <param name="plibNewPosition">Optional position after seek.</param>
        void Seek(long dlibMove, StreamSeek dwOrigin, ulong* plibNewPosition);

        void SetSize(ulong libNewSize);

        /// <summary>
        ///  Copy the specified count of bytes from the current stream to the given stream.
        /// </summary>
        /// <param name="pstm">The stream to copy to.</param>
        /// <param name="cb">Count of bytes to copy.</param>
        /// <param name="pcbRead">Optional count of bytes read.</param>
        /// <param name="pcbWritten">Optional count of bytes written.</param>
        void CopyTo(
            IStream pstm,
            ulong cb,
            ulong* pcbRead,
            ulong* pcbWritten);

        void Commit(StorageCommit grfCommitFlags);

        void Revert();

        // Using PreserveSig to allow explicitly returning the HRESULT for "not supported".

        [PreserveSig]
        HResult LockRegion(
            ulong libOffset,
            ulong cb,
            uint dwLockType);

        [PreserveSig]
        HResult UnlockRegion(
            ulong libOffset,
            ulong cb,
            uint dwLockType);

        void Stat(
            out STATSTG pstatstg,
            StatFlag grfStatFlag);

        IStream Clone();
    }
}
