// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Storage;

/// <summary>
///  Use to tie lifetime of a stream to a handle.
/// </summary>
public class SafeHandleStreamWrapper : System.IO.Stream
{
    private readonly System.IO.Stream _baseStream;
    private readonly SafeHandle _baseHandle;

    public SafeHandleStreamWrapper(System.IO.Stream baseStream, SafeHandle baseHandle)
    {
        _baseStream = baseStream;
        _baseHandle = baseHandle;
    }

    public override bool CanRead => _baseStream.CanRead;

    public override bool CanSeek => _baseStream.CanSeek;

    public override bool CanWrite => _baseStream.CanWrite;

    public override long Length => _baseStream.Length;

    public override long Position
    {
        get => _baseStream.Position;
        set => _baseStream.Position = value;
    }

    public override void Flush() => _baseStream.Flush();

    public override int Read(byte[] buffer, int offset, int count) => _baseStream.Read(buffer, offset, count);

    public override long Seek(long offset, System.IO.SeekOrigin origin) => _baseStream.Seek(offset, origin);

    public override void SetLength(long value) => _baseStream.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count) => _baseStream.Write(buffer, offset, count);

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _baseHandle.Dispose();
        _baseStream.Dispose();
    }
}