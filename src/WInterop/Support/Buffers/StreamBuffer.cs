// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;

namespace WInterop.Support.Buffers
{
    /// <summary>
    /// Stream wrapper for access to the native heap that allows for automatic growth when writing.
    /// Also provides implicit conversion to IntPtr for P/Invoke convenience.
    /// Dispose to free the memory. Try to use with using statements.
    /// </summary>
    public class StreamBuffer : Stream
    {
        private HeapBuffer _buffer;
        private Stream _stream;
        private bool _disposed;

        public StreamBuffer(uint initialLength = 0, uint initialCapacity = 0)
        {
            if (initialCapacity < initialLength) initialCapacity = initialLength;
            _buffer = new HeapBuffer(initialCapacity);
            SetLength(initialLength);
        }

        public override bool CanRead
        {
            get { return _stream?.CanRead ?? !_disposed; }
        }

        public override bool CanSeek
        {
            get { return _stream?.CanSeek ?? !_disposed; }
        }

        public override bool CanWrite
        {
            get { return _stream?.CanWrite ?? !_disposed; }
        }

        public override long Length
        {
            get { return _stream?.Length ?? 0; }
        }

        public override long Position
        {
            get
            {
                return _stream?.Position ?? 0;
            }
            set
            {
                if (value < 0 || value > Length) throw new ArgumentOutOfRangeException(nameof(value));
                if (Position != value)
                    _stream.Position = value;
            }
        }

        public static implicit operator SafeHandle(StreamBuffer buffer)
        {
            return buffer._buffer;
        }

        public unsafe static implicit operator void*(StreamBuffer buffer)
        {
            return buffer._buffer.VoidPointer;
        }

        public void EnsureLength(long value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            if (Length < value) SetLength(value);
        }

        public override void SetLength(long value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
            if (value == Length) return;

            Resize(value);
            _stream.SetLength(value);
        }

        private unsafe void Resize(long size)
        {
            Debug.Assert(size >= 0);

            if (_stream != null && _buffer.ByteCapacity >= (ulong)size) return;
            _buffer.EnsureByteCapacity((ulong)size);

            long oldLength = Length;
            long oldPosition = Position;
            _stream?.Dispose();

            _stream = new UnmanagedMemoryStream(
                pointer: (byte*)_buffer.DangerousGetHandle().ToPointer(),
                length: oldLength,
                capacity: size,
                access: FileAccess.ReadWrite);

            if (oldPosition <= size) { Position = oldPosition; }
        }

        [SuppressMessage("Microsoft.Usage", "CA2213:DisposableFieldsShouldBeDisposed", MessageId = "stream")]
        protected override void Dispose(bool disposing)
        {
            _disposed = true;
            _buffer.Dispose();

            if (disposing)
            {
                _stream?.Dispose();
                _stream = null;
            }
        }

        public override void Flush()
        {
            _stream?.Flush();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (_stream == null)
            {
                // Only 0 makes any sense, otherwise throw IOException like UnmanagedMemoryStream would
                if (offset != 0) throw new IOException();
                else return 0;
            }

            return _stream.Seek(offset, origin);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_stream == null)
            {
                // Mimic UnmanagedMemoryStream with a 0 length buffer
                if (buffer == null) throw new ArgumentNullException(nameof(buffer));
                if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
                if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
                if (offset != 0) throw new ArgumentException();
                return 0;
            }

            return _stream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0) return;

            if (_stream == null)
            {
                // Mimic UnmanagedMemoryStream with a 0 length buffer
                if (offset != 0) throw new ArgumentException();
            }

            EnsureLength(Length + count);
            _stream.Write(buffer, offset, count);
        }
    }
}
