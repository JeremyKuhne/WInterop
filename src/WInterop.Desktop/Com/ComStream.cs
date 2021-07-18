// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using WInterop.Com.Native;

namespace WInterop.Com
{
    /// <summary>
    ///  Stream that wraps a COM <see cref="IStream"/>
    /// </summary>
    public class ComStream : Stream
    {
        private readonly bool _ownsStream;
        private IStream? _stream;

        /// <summary>
        ///  Construct a Stream wrapper around an <see cref="IStream"/> object.
        /// </summary>
        /// <param name="stream">The COM stream to wrap.</param>
        /// <param name="ownsStream">If true (default), will release the <see cref="IStream"/> object when disposed.</param>
        public ComStream(IStream stream, bool ownsStream = true)
        {
            _stream = stream ?? throw new ArgumentNullException(nameof(stream));
            _ownsStream = ownsStream;
            stream.Stat(out STATSTG stat, StatFlag.NoName);
            StorageType = stat.type;
            StorageMode = stat.grfMode;
            ClassId = new Guid("0000000C-0000-0000-C000-000000000046");
        }

        public StorageType StorageType { get; }

        public StorageMode StorageMode { get; }

        public Guid ClassId { get; }

        public IStream Stream
            => _stream ?? throw new ObjectDisposedException(nameof(ComStream));

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
                Stream.Stat(out STATSTG stat, StatFlag.NoName);
                return (long)stat.cbSize;
            }
        }

        public unsafe override long Position
        {
            get
            {
                ulong position;
                Stream.Seek(0, StreamSeek.Current, &position);
                return checked((long)position);
            }
            set
            {
                Stream.Seek(value, StreamSeek.Set, null);
            }
        }

        public override void Flush()
        {
            Stream.Commit(StorageCommit.Default);
        }

        public unsafe override int Read(byte[] buffer, int offset, int count)
        {
            if (Stream == null)
                throw new ObjectDisposedException(nameof(ComStream));

            fixed (byte* b = buffer)
            {
                uint read;
                Stream.Read(b, (uint)count, &read);
                return checked((int)read);
            }
        }

        public unsafe override void Write(byte[] buffer, int offset, int count)
        {
            fixed (byte* b = buffer)
            {
                Stream.Write(b, (uint)count, null);
            }
        }

        public override unsafe long Seek(long offset, SeekOrigin origin)
        {
            // Not surprisingly SeekOrigin is the same as STREAM_SEEK
            // (given the history of .NET and COM)

            ulong position;
            Stream.Seek(offset, (StreamSeek)origin, &position);
            return checked((long)position);
        }

        public override void SetLength(long value)
        {
            Stream.SetSize((ulong)value);
        }

        public unsafe override string ToString()
        {
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

            _stream = null;
        }
    }
}