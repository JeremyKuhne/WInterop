// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace WInterop.Com.Types
{
    public class ComStream : Stream
    {
        public ComStream(IStream stream)
        {
            Stream = stream ?? throw new ArgumentNullException(nameof(stream));
            stream.Stat(out STATSTG stat, StatFlag.NoName);
            StorageType = stat.type;
            StorageMode = stat.grfMode;
            ClassId = ClassId;
        }

        public StorageType StorageType { get; }

        public StorageMode StorageMode { get; }

        public Guid ClassId { get; }

        public IStream Stream { get; }

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

        public override long Position
        {
            get => Stream.Seek(0, StreamSeek.Current);
            set => Stream.Seek(value, StreamSeek.Set);
        }

        public override void Flush() => Stream.Commit(StorageCommit.Default);

        public override int Read(byte[] buffer, int offset, int count)
        {
            ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(buffer, offset, buffer.Length - offset);
            return (int)Stream.Read(span[0], (uint)count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            ReadOnlySpan<byte> span = new ReadOnlySpan<byte>(buffer, offset, buffer.Length - offset);
            Stream.Write(span[0], (uint)count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            // Not surprisingly SeekOrigin is the same as STREAM_SEEK
            // (given the history of .NET and COM)
            return Stream.Seek(offset, (StreamSeek)origin);
        }

        public override void SetLength(long value)
        {
            Stream.SetSize(value);
        }

        public unsafe override string ToString()
        {
            Stream.Stat(out STATSTG stat, StatFlag.Default);
            string name = new string(stat.pwcsName);
            Marshal.FreeCoTaskMem((IntPtr)stat.pwcsName);
            return name;
        }
    }
}
