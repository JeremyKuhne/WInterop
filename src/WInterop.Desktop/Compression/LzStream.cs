// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using WInterop.Storage;

namespace WInterop.Compression
{
    public class LzStream : Stream
    {
        private LzHandle _handle;

        public LzStream(string path, bool useCreateFile = true)
        {
            _handle = useCreateFile
                ? Compression.LzCreateFile(path, out string uncompressedName)
                : Compression.LzOpenFile(path, out uncompressedName);
            UncompressedName = uncompressedName;
        }

        public string UncompressedName { get; private set; }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        public override long Length
        {
            get
            {
                int currentPosition = (int)Position;
                int length = (int)Seek(0, SeekOrigin.End);
                Position = currentPosition;
                return length;
            }
        }

        public override long Position
        {
            get => Seek(0, SeekOrigin.Current);
            set => Seek(value, SeekOrigin.Begin);
        }

        public override void Flush()
        {
            // Do nothing
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return Compression.LzRead(_handle, buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return Compression.LzSeek(_handle, checked((int)offset), (MoveMethod)origin);
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("Cannot set length");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Read only");
        }
    }
}
