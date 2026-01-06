// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.Com;

public unsafe partial struct Stream : IDisposable
{
    public IStream* IStream { get; }

    public Stream(IStream* stream) => IStream = stream;

    public Stream(System.IO.Stream stream) : this(CCW.CreateInstance(stream))
    {
    }

    public bool IsNull => IStream is null;

    public uint Read(Span<byte> buffer) => ((SequentialStream)this).Read(buffer);

    public uint Write(ReadOnlySpan<byte> buffer) => ((SequentialStream)this).Write(buffer);

    public ulong Seek(long move, StreamSeek origin = StreamSeek.Set)
    {
        ULARGE_INTEGER newPosition;
        IStream->Seek(move.ToLARGE_INTEGER(), (uint)origin, &newPosition).ThrowIfFailed();
        return newPosition.QuadPart;
    }

    public StorageStats Stat(StatFlag flag = StatFlag.Default)
    {
        StorageStats stats;
        IStream->Stat((STATSTG*)&stats, (uint)flag).ThrowIfFailed();
        return stats;
    }

    public void Commit(StorageCommit commit = StorageCommit.Default)
        => IStream->Commit((uint)commit).ThrowIfFailed();

    public void SetSize(ulong size) => IStream->SetSize(size.ToULARGE_INTEGER()).ThrowIfFailed();

    public void Dispose() => IStream->Release();
}