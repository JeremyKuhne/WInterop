// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace WInterop.Com
{
    /// <summary>
    /// Stream that wraps a COM <see cref="IStream"/>
    /// </summary>
    public class ComStream : Stream
    {
        private readonly bool _ownsStream;

        /// <summary>
        /// Construct a Stream wrapper around an <see cref="IStream"/> object.
        /// </summary>
        /// <param name="stream">The COM stream to wrap.</param>
        /// <param name="ownsStream">If true (default), will release the <see cref="IStream"/> object when disposed.</param>
        public ComStream(IStream stream, bool ownsStream = true)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _ownsStream = ownsStream;
            stream.Stat(out STATSTG stat, StatFlag.NoName);
            StorageType = stat.type;
            StorageMode = stat.grfMode;
            ClassId = ClassId;
        }

        public StorageType StorageType { get; }

        public StorageMode StorageMode { get; }

        public Guid ClassId { get; }

        public IStream Stream { get; private set; }

        public override bool CanRead
            // Can read is the default, only can't if in Write only mode
            => (StorageMode & StorageMode.Write) == 0;

        public override bool CanSeek => true;

        public override bool CanWrite
            => (StorageMode & StorageMode.Write) != 0 || (StorageMode & StorageMode.ReadWrite) != 0;

        public override long Length
        {
            get
            {
                if (Stream == null)
                    throw new ObjectDisposedException(nameof(ComStream));

                Stream.Stat(out STATSTG stat, StatFlag.NoName);
                return (long)stat.cbSize;
            }
        }

        public override long Position
        {
            get
            {
                if (Stream == null)
                    throw new ObjectDisposedException(nameof(ComStream));

                return Stream.Seek(0, StreamSeek.Current);
            }
            set
            {
                if (Stream == null)
                    throw new ObjectDisposedException(nameof(ComStream));

                Stream.Seek(value, StreamSeek.Set);
            }
        }

        public override void Flush()
        {
            if (Stream == null)
                throw new ObjectDisposedException(nameof(ComStream));

            Stream.Commit(StorageCommit.Default);
        }

        public unsafe override int Read(byte[] buffer, int offset, int count)
        {
            if (Stream == null)
                throw new ObjectDisposedException(nameof(ComStream));

            fixed (byte* b = buffer)
            {
                return (int)Stream.Read(b, (uint)count);
            }
        }

        public unsafe override void Write(byte[] buffer, int offset, int count)
        {
            if (Stream == null)
                throw new ObjectDisposedException(nameof(ComStream));

            fixed (byte* b = buffer)
            {
                Stream.Write(b, (uint)count);
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (Stream == null)
                throw new ObjectDisposedException(nameof(ComStream));

            // Not surprisingly SeekOrigin is the same as STREAM_SEEK
            // (given the history of .NET and COM)
            return Stream.Seek(offset, (StreamSeek)origin);
        }

        public override void SetLength(long value)
        {
            if (Stream == null)
                throw new ObjectDisposedException(nameof(ComStream));
            Stream.SetSize(value);
        }

        public unsafe override string ToString()
        {
            if (Stream == null)
                throw new ObjectDisposedException(nameof(ComStream));

            Stream.Stat(out STATSTG stat, StatFlag.Default);
            string name = new string(stat.pwcsName);
            Marshal.FreeCoTaskMem((IntPtr)stat.pwcsName);
            return name;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _ownsStream)
            {
                Marshal.ReleaseComObject(Stream);
            }
            Stream = null;
        }
    }
}
