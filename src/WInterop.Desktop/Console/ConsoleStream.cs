// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using Microsoft.Win32.SafeHandles;

namespace WInterop.Console
{
    public class ConsoleStream : Stream
    {
        private readonly bool _output;
        private readonly SafeFileHandle _handle;

        public ConsoleStream(StandardHandleType type)
        {
            SafeFileHandle? handle = Console.GetStandardHandle(type);
            _handle = handle ?? throw new InvalidOperationException($"Could not get standard handle of type {type}.");
            _output = type != StandardHandleType.Input;
        }

        public override bool CanRead => !_output;

        public override bool CanSeek => false;

        public override bool CanWrite => _output;

        public override long Length => throw new NotSupportedException();

        public override long Position { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

        public override void SetLength(long value) => throw new NotSupportedException();

        public override void Flush()
        {
            if (!CanWrite)
                throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!CanRead)
                throw new InvalidOperationException();

            return (int)Storage.Storage.ReadFile(_handle, buffer.AsSpan().Slice(offset, count));
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (!CanWrite)
                throw new InvalidOperationException();

            Storage.Storage.WriteFile(_handle, buffer.AsSpan().Slice(offset, count));
        }
    }
}
