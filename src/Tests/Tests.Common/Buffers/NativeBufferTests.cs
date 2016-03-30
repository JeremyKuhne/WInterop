// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Tests.Buffers
{
    using FluentAssertions;
    using System;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using WInterop.Buffers;
    using Xunit;
    using static WInterop.NativeMethods;

    public class NativeBufferTests
    {
        [Fact]
        public void EnsureZeroCapacityDoesNotFreeBuffer()
        {
            using (var buffer = new NativeBuffer(10))
            {
                buffer.DangerousGetHandle().Should().NotBe(IntPtr.Zero);
                buffer.EnsureByteCapacity(0);
                buffer.DangerousGetHandle().Should().NotBe(IntPtr.Zero);
            }
        }

        [Fact]
        public void GetOverIndexThrowsArgumentOutOfRange()
        {
            using (var buffer = new NativeBuffer())
            {
                Action action = () => { byte c = buffer[buffer.ByteCapacity]; };
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void SetOverIndexThrowsArgumentOutOfRange()
        {
            using (var buffer = new NativeBuffer())
            {
                Action action = () => { buffer[buffer.ByteCapacity] = 0; };
                action.ShouldThrow<ArgumentOutOfRangeException>();
            }
        }

        [Fact]
        public void CanGetSetBytes()
        {
            using (var buffer = new NativeBuffer(1))
            {
                buffer[0] = 0xA;
                buffer[0].Should().Be(0xA);
            }
        }

        [Fact]
        public void NullSafePointerInTest()
        {
            NativeBuffer buffer;
            using (buffer = new NativeBuffer(0))
            {
                ((SafeHandle)buffer).IsInvalid.Should().BeFalse();
                buffer.ByteCapacity.Should().BeGreaterOrEqualTo(0);
            }

            ((SafeHandle)buffer).IsInvalid.Should().BeTrue();
            buffer.ByteCapacity.Should().Be(0);
            GetCurrentDirectorySafe((uint)buffer.ByteCapacity, buffer);
        }

        [Fact]
        public void CreateNativeBufferOver32BitCapacity()
        {
            if (!Utility.Environment.Is64BitProcess)
            {
                Action action = () => new NativeBuffer(uint.MaxValue + 1ul);
                action.ShouldThrow<OverflowException>();
            }
        }

        [Theory
            InlineData(0)
            InlineData(1)
            ]
        public void ResizeNativeBufferOver32BitCapacity(ulong initialBufferSize)
        {
            if (!Utility.Environment.Is64BitProcess)
            {
                using (var buffer = new NativeBuffer(initialBufferSize))
                {
                    Action action = () => buffer.EnsureByteCapacity(uint.MaxValue + 1ul);
                    action.ShouldThrow<OverflowException>();
                }
            }
        }

        [Fact]
        public void MultithreadedSetCapacityIsMax()
        {
            using (var buffer = new NativeBuffer(0))
            {
                ulong[] bufferCapacity = new ulong[100];
                for (ulong i = 0; i < 100; i++)
                {
                    bufferCapacity[i] = i + 1;
                }

                Parallel.ForEach(bufferCapacity, capacity =>
                {
                    buffer.EnsureByteCapacity(capacity);
                });

                buffer.ByteCapacity.Should().Be(100);
            }
        }

        [Fact]
        public void DisposedBufferIsEmpty()
        {
            var buffer = new NativeBuffer(5);
            buffer.ByteCapacity.Should().BeGreaterOrEqualTo(5);
            buffer.Dispose();
            buffer.ByteCapacity.Should().Be(0);
            buffer.DangerousGetHandle().Should().Be(IntPtr.Zero);
        }

        // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364934.aspx
        [DllImport(Libraries.Kernel32, EntryPoint = "GetCurrentDirectoryW", SetLastError = true, ExactSpelling = true)]
        private static extern uint GetCurrentDirectorySafe(
            uint nBufferLength,
            SafeHandle lpBuffer);
    }
}
