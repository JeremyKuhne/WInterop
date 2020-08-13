// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// To simplify pending NetStandard2.0 exluding this for now (as it isn't part of 1.6)
#if FEATURE_STREAMBUFFER

using FluentAssertions;
using System;
using System.IO;
using WInterop.Support.Buffers;
using Xunit;

namespace Tests.Buffers
{
    public class StreamBufferTests
    {
        const string testString = "The quick brown fox jumped over the lazy dog.";

        [Fact]
        public void EmptyBufferHasZeroLength()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Length.Should().Be(0);
            }
        }

        [Fact]
        public void EmptyBufferPositionIsZero()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Position.Should().Be(0);
            }
        }

        [Fact]
        public void EmptyBufferCanSetPositionToZero()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Position = 0;
                buffer.Position.Should().Be(0);
            }
        }

        [Fact]
        public void PositionCannotBeSetOutsideLength()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action under = () => buffer.Position = -1;
                under.ShouldThrow<ArgumentOutOfRangeException>();
                Action over = () => buffer.Position = 1;
                over.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void NonEmptyBufferCanRead()
        {
            using (StreamBuffer buffer = new StreamBuffer(1))
            {
                buffer.CanRead.Should().BeTrue();
            }
        }

        [Fact]
        public void EmptyBufferCanRead()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.CanRead.Should().BeTrue();
            }
        }

        [Fact]
        public void DisposedEmptyBufferCannotRead()
        {
            StreamBuffer buffer;
            using (buffer = new StreamBuffer(0))
            {
            }
            buffer.CanRead.Should().BeFalse();
        }

        [Fact]
        public void NonEmptyBufferCanSeek()
        {
            using (StreamBuffer buffer = new StreamBuffer(1))
            {
                buffer.CanSeek.Should().BeTrue();
            }
        }

        [Fact]
        public void EmptyBufferCanSeek()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.CanSeek.Should().BeTrue();
            }
        }

        [Fact]
        public void DisposedEmptyBufferCannotSeek()
        {
            StreamBuffer buffer;
            using (buffer = new StreamBuffer(0))
            {
            }
            buffer.CanSeek.Should().BeFalse();
        }

        [Fact]
        public void NonEmptyBufferCanWrite()
        {
            using (StreamBuffer buffer = new StreamBuffer(1))
            {
                buffer.CanWrite.Should().BeTrue();
            }
        }

        [Fact]
        public void EmptyBufferCanWrite()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.CanWrite.Should().BeTrue();
            }
        }

        [Fact]
        public void DisposedEmptyBufferCannotWrite()
        {
            StreamBuffer buffer;
            using (buffer = new StreamBuffer(0))
            {
            }
            buffer.CanWrite.Should().BeFalse();
        }

        [Fact]
        public void EmptyBufferCanFlush()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Flush();
            }
        }

        [Fact]
        public void EmptyBufferCanReadNothing()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Read(new byte[0], 0, 0).Should().Be(0);
            }
        }

        [Fact]
        public void EmptyBufferCanWriteNothing()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Write(new byte[0], 0, 0);
            }
        }

        [Fact]
        public void EmptyBufferCanSeekNowhere()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Seek(0, SeekOrigin.Begin).Should().Be(0);
            }
        }

        [Fact]
        public void EmptyBufferThrowsOnSeek()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action action = () => buffer.Seek(1, SeekOrigin.Begin);
                action.ShouldThrow<IOException>();
            }
        }

        [Fact]
        public void EmptyBufferThrowsOnNullBufferWrite()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action action = () => buffer.Write(null, 0, 0);
                action.ShouldThrow<ArgumentNullException>();
            }
        }

        [Fact]
        public void EmptyBufferThrowsOnNullBufferRead()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action action = () => buffer.Read(null, 0, 0);
                action.ShouldThrow<ArgumentNullException>();
            }
        }

        [Fact]
        public void EmptyBufferThrowsOnNegativeOffsetWrite()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action action = () => buffer.Write(new byte[0], -1, 0);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void EmptyBufferThrowsOnNegativeOffsetRead()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action action = () => buffer.Read(new byte[0], -1, 0);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void EmptyBufferThrowsOnNegativeCountWrite()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action action = () => buffer.Write(new byte[0], 0, -1);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void EmptyBufferThrowsOnNegativeCountRead()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action action = () => buffer.Read(new byte[0], 0, -1);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void EmptyBufferDoesNotThrowOnPositiveOffsetWriteOfNoCharacters()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Write(new byte[0], 1, 0);
            }
        }

        [Fact]
        public void EmptyBufferThrowsOnPositiveOffsetWrite()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action action = () => buffer.Write(new byte[] { 7 }, 1, 1);
                action.ShouldThrow<ArgumentException>();
            }
        }

        [Fact]
        public void EmptyBufferThrowsOnPositiveOffsetRead()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action action = () => buffer.Read(new byte[0], 1, 0);
                action.ShouldThrow<ArgumentException>();
            }
        }

        [Fact]
        public void EmptyBufferThrowsOnPositiveCountWrite()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action action = () => buffer.Write(new byte[0], 0, 1);
                action.ShouldThrow<ArgumentException>();
            }
        }

        [Fact]
        public void EmptyBufferCanReadNoBytes()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Read(new byte[0], 0, 1).Should().Be(0);
            }
        }

        [Fact]
        public void WriteToEmptyBuffer()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Length.Should().Be(0);
                buffer.WriteByte(7);
                buffer.Length.Should().Be(1);
                buffer.Position = 0;
                buffer.ReadByte().Should().Be(7);
            }
        }

        [Fact]
        public void StreamWriterOnEmptyBuffer()
        {
            using (StreamBuffer buffer = new StreamBuffer())
            {
                using (StreamWriter writer = new StreamWriter(buffer))
                using (StreamReader reader = new StreamReader(buffer))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(testString);
                    reader.BaseStream.Position = 0;
                    reader.ReadLine().Should().Be(testString);
                }
            }
        }

        [Fact]
        public void StreamWriterSetLengthToZero()
        {
            using (StreamBuffer buffer = new StreamBuffer())
            {
                using (StreamWriter writer = new StreamWriter(buffer))
                using (StreamReader reader = new StreamReader(buffer))
                {
                    writer.AutoFlush = true;
                    writer.WriteLine(testString);
                    reader.BaseStream.Position = 0;
                    reader.ReadLine().Should().Be(testString);
                    writer.BaseStream.SetLength(0);
                    reader.ReadLine().Should().BeNull();
                }
            }
        }

        [Fact]
        public void SetNegativeLengthThrows()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                Action action = () => buffer.SetLength(-1);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void NewBufferLengthShouldBeSpecified()
        {
            using (StreamBuffer buffer = new StreamBuffer(1))
            {
                buffer.Length.Should().Be(1);
            }
        }

        [Fact]
        public void NewBufferCapacityShouldNotImpactLength()
        {
            using (StreamBuffer buffer = new StreamBuffer(1, 10))
            {
                buffer.Length.Should().Be(1);
            }
        }

        [Fact]
        public void SeekSucceeds()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Length.Should().Be(0);
                buffer.Write(new byte[] { 0xA, 0xB, 0xC }, 0, 3);
                buffer.Length.Should().Be(3);
                buffer.Seek(0, SeekOrigin.Begin);
                buffer.ReadByte().Should().Be(0xA);
                buffer.Seek(-1, SeekOrigin.Current);
                buffer.ReadByte().Should().Be(0xA);
                buffer.Seek(1, SeekOrigin.Current);
                buffer.ReadByte().Should().Be(0xC);
                buffer.Seek(1, SeekOrigin.Begin);
                buffer.ReadByte().Should().Be(0xB);
                buffer.Seek(-2, SeekOrigin.End);
                buffer.ReadByte().Should().Be(0xB);
            }
        }

        [Fact]
        public void EnsureLengthThrowsOnNegativeValue()
        {
            using (StreamBuffer buffer = new StreamBuffer())
            {
                Action action = () => buffer.EnsureLength(-1);
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void SetLengthOnEmptyStream()
        {
            using (StreamBuffer buffer = new StreamBuffer())
            {
                buffer.Length.Should().Be(0);
                buffer.SetLength(7);
                buffer.Length.Should().Be(7);
            }
        }

        [Fact]
        public void SetLengthToZero()
        {
            using (StreamBuffer buffer = new StreamBuffer(7))
            {
                buffer.Length.Should().Be(7);
                buffer.SetLength(0);
                buffer.Length.Should().Be(0);
            }
        }

        [Fact]
        public void AppendTwice()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Length.Should().Be(0);
                buffer.Write(new byte[] { 0xA, 0xB, 0xC }, 0, 3);
                buffer.Position.Should().Be(3);
                buffer.Length.Should().Be(3);
                buffer.Position = 0;
                byte[] output = new byte[3];
                buffer.Read(output, 0, 3);
                buffer.Position.Should().Be(3);
                output.ShouldAllBeEquivalentTo(new byte[] { 0xA, 0xB, 0xC });
                buffer.Write(new byte[] { 0xD, 0xE, 0xF }, 0, 3);
                buffer.Position.Should().Be(6);
                buffer.Length.Should().Be(6);
                buffer.Position = 0;
                output = new byte[6];
                buffer.Read(output, 0, 6);
                output.ShouldAllBeEquivalentTo(new byte[] { 0xA, 0xB, 0xC, 0xD, 0xE, 0xF });
            }
        }

        [Fact]
        public void ShrinkPositionStaysAtEnd()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                buffer.Write(new byte[] { 0xA, 0xB, 0xC }, 0, 3);
                buffer.Position.Should().Be(3);
                buffer.Length.Should().Be(3);
                buffer.SetLength(2);
                buffer.Position.Should().Be(2);
                buffer.Length.Should().Be(2);
            }
        }

        [Fact]
        public void PositionTest()
        {
            using (StreamBuffer buffer = new StreamBuffer(0))
            {
                byte[] data = new byte[] { 0xA, 0xB, 0xC };
                buffer.Write(data, 0, data.Length);
                buffer.Position.Should().Be(data.Length);
                for (int i = 0; i < data.Length; i++)
                {
                    buffer.Position = i;
                    buffer.ReadByte().Should().Be(data[i]);
                }
            }
        }
    }
}
#endif
