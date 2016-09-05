// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Support.Buffers
{
    /// <summary>
    /// Unchecked helpers for reading data from a Buffer. Use CheckedReader whereever possible.
    /// </summary>
    public class Reader
    {
        // Windows Data Alignment on IPF, x86, and x64
        // https://msdn.microsoft.com/en-us/library/aa290049.aspx

        private IBuffer _buffer;
        private ulong _byteOffset;

        public Reader(IBuffer buffer)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            _buffer = buffer;
        }

        /// <summary>
        /// Get/set the offset in bytes
        /// </summary>
        public virtual ulong ByteOffset
        {
            get
            {
                return _byteOffset;
            }
            set
            {
                _byteOffset = value;
            }
        }

        /// <summary>
        /// Read a string of the given amount of characters from the buffer. Advances the reader offset.
        /// </summary>
        unsafe public virtual string ReadString(int charCount)
        {
            if (charCount == 0) return string.Empty;

            string value = new string((char*)((byte*)_buffer.DangerousGetHandle() + _byteOffset), startIndex: 0, length: charCount);
            _byteOffset += (ulong)(charCount * sizeof(char));
            return value;
        }

        /// <summary>
        /// Read a ushort at the current offset. Advances the reader offset.
        /// </summary>
        public virtual ushort ReadUshort()
        {
            return (ushort)ReadShort();
        }

        /// <summary>
        /// Read a short at the current offset. Advances the reader offset.
        /// </summary>
        unsafe public virtual short ReadShort()
        {
            byte* address = (byte*)_buffer.DangerousGetHandle() + _byteOffset;
            _byteOffset += sizeof(short);

            if (((short)address & (sizeof(short) - 1)) == 0)
            {
                // aligned read
                return *((short*)address);
            }
            else
            {
                // unaligned read
                return (short)(*address | *(address + 1) << 8);
            }
        }

        /// <summary>
        /// Read a uint at the current offset. Advances the reader offset.
        /// </summary>
        public virtual uint ReadUint()
        {
            return (uint)ReadInt();
        }

        /// <summary>
        /// Read an int at the current offset. Advances the reader offset.
        /// </summary>
        unsafe public virtual int ReadInt()
        {
            byte* address = (byte*)_buffer.DangerousGetHandle() + _byteOffset;
            _byteOffset += sizeof(int);

            if (((int)address & (sizeof(int) - 1)) == 0)
            {
                // aligned read
                return *((int*)address);
            }
            else
            {
                // unaligned read
                return *address | *(address + 1) << 8 | *(address + 2) << 16 | *(address + 3) << 24;
            }
        }

        /// <summary>
        /// Read a ulong at the current offset. Advances the reader offset.
        /// </summary>
        public virtual ulong ReadUlong()
        {
            return (ulong)ReadLong();
        }

        /// <summary>
        /// Read a long at the current offset. Advances the reader offset.
        /// </summary>
        unsafe public virtual long ReadLong()
        {
            byte* address = (byte*)_buffer.DangerousGetHandle() + _byteOffset;
            _byteOffset += sizeof(long);

            if (((long)address & (sizeof(long) - 1)) == 0)
            {
                // aligned read
                return *((long*)address);
            }
            else
            {
                // unaligned read
                int lo = *address | *(address + 1) << 8 | *(address + 2) << 16 | *(address + 3) << 24;
                int hi = *(address + 4) | *(address + 5) << 8 | *(address + 6) << 16 | *(address + 7) << 24;
                return ((long)hi << 32) | (uint)lo;
            }
        }

        /// <summary>
        /// Get a pointer at the current offset.
        /// </summary>
        public virtual IntPtr ReadIntPtr()
        {
            if (Support.Environment.Is64BitProcess)
            {
                return (IntPtr)ReadUlong();
            }
            else
            {
                return (IntPtr)ReadUint();
            }
        }

        /// <summary>
        /// Read the given struct type at the current offset.
        /// </summary>
        public virtual T ReadStruct<T>() where T : struct
        {
            ulong sizeOfStruct = (ulong)Marshal.SizeOf<T>();

            T value = Marshal.PtrToStructure<T>(_buffer.DangerousGetHandle() + checked((int)_byteOffset));
            _byteOffset += sizeOfStruct;
            return value;
        }
    }
}
