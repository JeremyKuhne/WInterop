// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Support.Buffers;
using Xunit;

namespace BufferTests;

public class NativeBufferReaderTests
{
    [Fact]
    public void NullConstructorThrows()
    {
        Action action = () => new CheckedReader(null);
        action.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void SetOffsetPastEndOfStreamFails()
    {
        using var buffer = new HeapBuffer(0);
        var reader = new CheckedReader(buffer);
        reader.ByteOffset = buffer.ByteCapacity;

        Action action = () => reader.ByteOffset = buffer.ByteCapacity + 1;
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ReadShortPastCapacityFails()
    {
        using var buffer = new HeapBuffer(sizeof(short));
        var reader = new CheckedReader(buffer);

        Action action = () => reader.ReadShort();

        for (uint i = 1; i < sizeof(short); i++)
        {
            reader.ByteOffset = buffer.ByteCapacity - i;
            action.Should().Throw<System.IO.EndOfStreamException>();
        }
    }

    [Fact]
    public void ReadShortInCapacityPasses()
    {
        using var buffer = new HeapBuffer(2);
        var reader = new CheckedReader(buffer);
        reader.ByteOffset = buffer.ByteCapacity - 2;
        reader.ReadShort();
    }

    [Fact]
    public void ReadIntPastCapacityFails()
    {
        using var buffer = new HeapBuffer(sizeof(int));
        var reader = new CheckedReader(buffer);

        Action action = () => reader.ReadInt();

        for (uint i = 1; i < sizeof(int); i++)
        {
            reader.ByteOffset = buffer.ByteCapacity - i;
            action.Should().Throw<System.IO.EndOfStreamException>();
        }
    }

    [Fact]
    public void ReadIntInCapacityPasses()
    {
        using var buffer = new HeapBuffer(4);
        var reader = new CheckedReader(buffer);
        reader.ByteOffset = buffer.ByteCapacity - 4;
        reader.ReadInt();
    }

    [Fact]
    public void ReadLongPastCapacityFails()
    {
        using var buffer = new HeapBuffer(sizeof(long));
        var reader = new CheckedReader(buffer);

        Action action = () => reader.ReadLong();

        for (uint i = 1; i < sizeof(long); i++)
        {
            reader.ByteOffset = buffer.ByteCapacity - i;
            action.Should().Throw<System.IO.EndOfStreamException>();
        }
    }

    [Fact]
    public void ReadLongInCapacityPasses()
    {
        using var buffer = new HeapBuffer(8);
        var reader = new CheckedReader(buffer);
        reader.ByteOffset = buffer.ByteCapacity - 8;
        reader.ReadLong();
    }

    [Theory,
        InlineData(0, 0x0201),
        InlineData(1, 0x0302),
        InlineData(2, 0x0403)
        ]
    public void ReadShort(ulong offset, short expected)
    {
        using var buffer = new HeapBuffer(4);
        for (ulong i = 0; i < 4; i++)
        {
            buffer[i] = (byte)(i + 1);
        }

        var reader = new CheckedReader(buffer);
        reader.ByteOffset = offset;
        reader.ReadShort().Should().Be(expected, $"offset is {offset}");
    }

    [Fact]
    public void ReadTwoShorts()
    {
        using var buffer = new HeapBuffer(4);
        for (ulong i = 0; i < 4; i++)
        {
            buffer[i] = (byte)(i + 1);
        }

        var reader = new CheckedReader(buffer);
        reader.ReadShort().Should().Be(0x0201);
        reader.ReadShort().Should().Be(0x0403);
    }

    [Theory,
        InlineData(0, 0x04030201),
        InlineData(1, 0x05040302),
        InlineData(2, 0x06050403),
        InlineData(3, 0x07060504),
        InlineData(4, 0x08070605),
        ]
    public void ReadInt(ulong offset, int expected)
    {
        using var buffer = new HeapBuffer(8);
        for (ulong i = 0; i < 8; i++)
        {
            buffer[i] = (byte)(i + 1);
        }

        var reader = new CheckedReader(buffer);
        reader.ByteOffset = offset;
        reader.ReadInt().Should().Be(expected, $"offset is {offset}");
    }

    [Fact]
    public void ReadTwoInts()
    {
        using var buffer = new HeapBuffer(8);
        for (ulong i = 0; i < 8; i++)
        {
            buffer[i] = (byte)(i + 1);
        }

        var reader = new CheckedReader(buffer);
        reader.ReadInt().Should().Be(0x04030201);
        reader.ReadInt().Should().Be(0x08070605);
    }

    [Theory,
        InlineData(0, 0x0807060504030201),
        InlineData(1, 0x0908070605040302),
        InlineData(2, 0x0A09080706050403),
        InlineData(3, 0x0B0A090807060504),
        InlineData(4, 0x0C0B0A0908070605),
        InlineData(5, 0x0D0C0B0A09080706),
        InlineData(6, 0x0E0D0C0B0A090807),
        InlineData(7, 0x0F0E0D0C0B0A0908),
        InlineData(8, 0x100F0E0D0C0B0A09),
        ]
    public void ReadLong(ulong offset, long expected)
    {
        using var buffer = new HeapBuffer(16);
        for (ulong i = 0; i < 16; i++)
        {
            buffer[i] = (byte)(i + 1);
        }

        var reader = new CheckedReader(buffer);
        reader.ByteOffset = offset;
        reader.ReadLong().Should().Be(expected, $"offset is {offset}");
    }

    [Fact]
    public void ReadTwoLongs()
    {
        using var buffer = new HeapBuffer(16);
        for (ulong i = 0; i < 16; i++)
        {
            buffer[i] = (byte)(i + 1);
        }

        var reader = new CheckedReader(buffer);
        reader.ReadLong().Should().Be(0x0807060504030201);
        reader.ReadLong().Should().Be(0x100F0E0D0C0B0A09);
    }

    [Fact]
    public void ReadStringPastEndFails()
    {
        using var buffer = new HeapBuffer(1);
        var reader = new CheckedReader(buffer);

        Action action = () => reader.ReadString(1);

        reader.ByteOffset = buffer.ByteCapacity - 1;
        action.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void ReadAlignedString()
    {
        using var buffer = new HeapBuffer(4);
        buffer[0] = 65;
        buffer[1] = 0;
        buffer[2] = 66;
        buffer[3] = 0;

        var reader = new CheckedReader(buffer);
        reader.ReadString(1).Should().Be("A");
        reader.ReadString(1).Should().Be("B");
    }

    [Fact]
    public void ReadUnalignedString()
    {
        using var buffer = new HeapBuffer(5);
        buffer[0] = 67;
        buffer[1] = 65;
        buffer[2] = 0;
        buffer[3] = 66;
        buffer[4] = 0;

        var reader = new CheckedReader(buffer);
        reader.ByteOffset = 1;
        reader.ReadString(1).Should().Be("A");
        reader.ReadString(1).Should().Be("B");
    }
}
