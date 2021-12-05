// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  Stream that wraps a COM <see cref="IStream"/>
/// </summary>
public class ComStream : System.IO.Stream
{
    private readonly bool _ownsStream;
    private Stream _stream;

    public unsafe ComStream(IStream* stream, bool ownsStream = true)
        : this(new Stream(stream), ownsStream) { }

    /// <summary>
    ///  Construct a Stream wrapper around a <see cref="WInterop.Com.Stream"/> object.
    /// </summary>
    /// <param name="stream">The COM stream to wrap.</param>
    /// <param name="ownsStream">If true (default), will release the <see cref="WInterop.Com.Stream"/> object when disposed.</param>
    public ComStream(Stream stream, bool ownsStream = true)
    {
        if (stream.IsNull)
            throw new ArgumentNullException(nameof(stream));

        _stream = stream;
        _ownsStream = ownsStream;
        using var stats = stream.Stat(StatFlag.NoName);
        StorageType = stats.StorageType;
        StorageMode = stats.Mode;
        ClassId = stats.ClassId;
        // new Guid("0000000C-0000-0000-C000-000000000046");
    }

    public StorageType StorageType { get; }

    public StorageMode StorageMode { get; }

    public Guid ClassId { get; }

    public Stream Stream
        => Stream.IsNull ? throw new ObjectDisposedException(nameof(ComStream)) : Stream;

    public override bool CanRead
        // Can read is the default, only can't if in Write only mode
        => (StorageMode & StorageMode.Write) == 0;

    public override bool CanSeek => true;

    public override bool CanWrite
        => (StorageMode & StorageMode.Write) != 0 || (StorageMode & StorageMode.ReadWrite) != 0;

    public override long Length
        => checked((long)Stream.Stat(StatFlag.NoName).Size);

    public override unsafe long Position
    {
        get => checked((long)Stream.Seek(0, StreamSeek.Current));
        set => Stream.Seek(value, StreamSeek.Set);
    }

    public override void Flush() => Stream.Commit(StorageCommit.Default);

    public override unsafe int Read(byte[] buffer, int offset, int count)
        => Read(new Span<byte>(buffer, offset, count));

    public override int Read(Span<byte> buffer) => (int)Stream.Read(buffer);

    public override unsafe void Write(byte[] buffer, int offset, int count)
        => Write(new ReadOnlySpan<byte>(buffer, offset, count));

    public override void Write(ReadOnlySpan<byte> buffer) => Stream.Write(buffer);

    public override unsafe long Seek(long offset, SeekOrigin origin)
    {
        // Not surprisingly SeekOrigin is the same as STREAM_SEEK
        // (given the history of .NET and COM)
        return checked((long)Stream.Seek(offset, (StreamSeek)origin));
    }

    public override void SetLength(long value) => Stream.SetSize((ulong)value);

    public override string ToString()
    {
        using var stats = Stream.Stat();
        return stats.Name.ToString();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && _ownsStream)
        {
            _stream.Dispose();
        }

        _stream = default;
    }
}