// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace WInterop.Support.Buffers
{
    /// <summary>
    ///  Checked helpers for reading data from a Buffer. Use StreamBuffer for more complicated read operations.
    /// </summary>
    /// <remarks>
    ///  Didn't use extension methods to avoid dirtying the usage of derived classes. We don't want to encourage accidentally
    ///  grabbing strings for StringBuffer using these methods, for example.
    /// </remarks>
    public class CheckedReader : Reader
    {
        // Windows Data Alignment on IPF, x86, and x64
        // https://msdn.microsoft.com/en-us/library/aa290049.aspx

        private readonly ulong _byteCapacity;

        public CheckedReader(ISizedBuffer buffer)
            : this(buffer, buffer?.ByteCapacity ?? 0)
        {
        }

        public CheckedReader(IBuffer buffer, ulong byteCapacity)
            : base(buffer)
        {
            _byteCapacity = byteCapacity;
        }

        /// <summary>
        ///  Get/set the offset in bytes
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if setting an offset greater than the capacity.</exception>
        public override ulong ByteOffset
        {
            set
            {
                if (value > _byteCapacity) throw new ArgumentOutOfRangeException(nameof(ByteOffset));
                base.ByteOffset = value;
            }
        }

        /// <summary>
        ///  Read a string of the given amount of characters from the buffer. Advances the reader offset.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the count of characters would go past the end of the buffer.</exception>
        public override string ReadString(int charCount)
        {
            if ((uint)charCount * sizeof(char) > _byteCapacity - ByteOffset) throw new ArgumentOutOfRangeException(nameof(charCount));
            return base.ReadString(charCount);
        }

        /// <summary>
        ///  Read a ushort at the current offset. Advances the reader offset.
        /// </summary>
        /// <exception cref="EndOfStreamException">Thrown if reading a ushort would go past the end of the buffer.</exception>
        public override ushort ReadUshort()
        {
            return (ushort)ReadShort();
        }

        /// <summary>
        ///  Read a short at the current offset. Advances the reader offset.
        /// </summary>
        /// <exception cref="EndOfStreamException">Thrown if reading a short would go past the end of the buffer.</exception>
        public override short ReadShort()
        {
            if (_byteCapacity < sizeof(short) || ByteOffset > _byteCapacity - sizeof(short)) throw new EndOfStreamException();
            return base.ReadShort();
        }

        /// <summary>
        ///  Read a uint at the current offset. Advances the reader offset.
        /// </summary>
        /// <exception cref="EndOfStreamException">Thrown if reading a uint would go past the end of the buffer.</exception>
        public override uint ReadUint()
        {
            return (uint)ReadInt();
        }

        /// <summary>
        ///  Read an int at the current offset. Advances the reader offset.
        /// </summary>
        /// <exception cref="EndOfStreamException">Thrown if reading an int would go past the end of the buffer.</exception>
        public override int ReadInt()
        {
            if (_byteCapacity < sizeof(int) || ByteOffset > _byteCapacity - sizeof(int)) throw new EndOfStreamException();
            return base.ReadInt();
        }

        /// <summary>
        ///  Read a ulong at the current offset. Advances the reader offset.
        /// </summary>
        /// <exception cref="EndOfStreamException">Thrown if reading a ulong would go past the end of the buffer.</exception>
        public override ulong ReadUlong()
        {
            return (ulong)ReadLong();
        }

        /// <summary>
        ///  Read a long at the current offset. Advances the reader offset.
        /// </summary>
        /// <exception cref="EndOfStreamException">Thrown if reading a long would go past the end of the buffer.</exception>
        public override long ReadLong()
        {
            if (_byteCapacity < sizeof(long) || ByteOffset > _byteCapacity - sizeof(long)) throw new EndOfStreamException();
            return base.ReadLong();
        }

        /// <summary>
        ///  Get a pointer at the current offset.
        /// </summary>
        /// <exception cref="EndOfStreamException">Thrown if reading a pointer would go past the end of the buffer.</exception>
        public override IntPtr ReadIntPtr()
        {
            if (Environment.Is64BitProcess)
            {
                return (IntPtr)ReadUlong();
            }
            else
            {
                return (IntPtr)ReadUint();
            }
        }

        /// <summary>
        ///  Read the given struct type at the current offset.
        /// </summary>
        /// <exception cref="EndOfStreamException">Thrown if reading the given struct would go past the end of the buffer.</exception>
        public override T ReadStruct<T>()
        {
            ulong sizeOfStruct = (ulong)Marshal.SizeOf<T>();
            if (_byteCapacity < sizeOfStruct || ByteOffset > _byteCapacity - sizeOfStruct) throw new EndOfStreamException();

            return base.ReadStruct<T>();
        }
    }
}