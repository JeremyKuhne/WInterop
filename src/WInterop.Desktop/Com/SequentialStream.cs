// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.Com;

public unsafe struct SequentialStream : IDisposable
{
    public ISequentialStream* ISequentialStream { get; }

    public SequentialStream(ISequentialStream* stream) => ISequentialStream = stream;

    public uint Read(Span<byte> buffer)
    {
        fixed (byte* b = buffer)
        {
            uint read;
            ISequentialStream->Read(b, (uint)buffer.Length, &read).ThrowIfFailed();
            return read;
        }
    }

    public uint Write(ReadOnlySpan<byte> buffer)
    {
        fixed (byte* b = buffer)
        {
            uint written;
            ISequentialStream->Write(b, (uint)buffer.Length, &written).ThrowIfFailed();
            return written;
        }
    }

    public void Dispose() => ISequentialStream->Release();

    public static implicit operator SequentialStream(Stream stream) => new((ISequentialStream*)stream.IStream);
}