// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Storage;

namespace WInterop.Support
{
    public static class Conversion
    {
        private static readonly DateTime s_oleBaseDate = new DateTime(year: 1899, month: 12, day: 30);
        private const double MinOleDate = -657435.0;
        private const double MaxOleDate = 2958466.0;

        public static ulong HighLowToLong(uint high, uint low) => ((ulong)high) << 32 | low;
        public static long HighLowToLong(int high, int low) => (long)HighLowToLong((uint)high, (uint)low);
        public static uint HighLowToInt(ushort high, ushort low) => ((uint)high) << 16 | low;
        public static int HighLowToInt(short high, short low) => (int)HighLowToInt((ushort)high, (ushort)low);

        // Note that we always cast IntPtr to ulong to avoid checked blocks

        public static ushort HighWord(IntPtr value) => (ushort)(((ulong)value >> 16) & 0xFFFF);
        public static ushort HighWord(UIntPtr value) => (ushort)(((ulong)value >> 16) & 0xFFFF);
        public static uint HighWord(ulong value) => (uint)(value >> 32);
        public static ushort HighWord(uint value) => (ushort)(value >> 16);
        public static short HighWord(int value) => (short)HighWord((uint)value);

        public static ushort LowWord(IntPtr value) => (ushort)((ulong)value);
        public static ushort LowWord(UIntPtr value) => (ushort)((ulong)value);
        public static uint LowWord(ulong value) => (uint)value;
        public static ushort LowWord(uint value) => (ushort)value;
        public static short LowWord(int value) => (short)LowWord((uint)value);

        public static DesiredAccess FileAccessToDesiredAccess(System.IO.FileAccess fileAccess)
            => fileAccess switch
            {
                // See FileStream.Init to see how the mapping is done in .NET
                System.IO.FileAccess.Read => DesiredAccess.GenericRead,
                System.IO.FileAccess.Write => DesiredAccess.GenericWrite,
                System.IO.FileAccess.ReadWrite => DesiredAccess.GenericRead | DesiredAccess.GenericWrite,
                _ => 0,
            };

        public static System.IO.FileAccess DesiredAccessToFileAccess(DesiredAccess desiredAccess)
        {
            System.IO.FileAccess fileAccess = 0;
            if ((desiredAccess & (DesiredAccess.GenericRead | DesiredAccess.ReadData)) > 0)
                fileAccess = System.IO.FileAccess.Read;

            if ((desiredAccess & (DesiredAccess.GenericWrite | DesiredAccess.WriteData)) > 0)
                fileAccess = fileAccess == System.IO.FileAccess.Read ? System.IO.FileAccess.ReadWrite : System.IO.FileAccess.Write;

            return fileAccess;
        }

        public static ShareModes FileShareToShareMode(System.IO.FileShare fileShare)
        {
            // See additional comments on ShareMode
            fileShare &= ~System.IO.FileShare.Inheritable;
            return unchecked((ShareModes)fileShare);
        }

        public static FileFlags FileOptionsToFileFlags(System.IO.FileOptions fileOptions)
            => unchecked((FileFlags)fileOptions);

        public static CreationDisposition FileModeToCreationDisposition(System.IO.FileMode fileMode)
            => fileMode == System.IO.FileMode.Append ? CreationDisposition.OpenAlways : (CreationDisposition)fileMode;

        /// <summary>
        ///  Convert a native OLE Variant VT_DATE to a DateTime.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        ///  Thrown if the value is outside of the allowable range for an OLE Date (Dates must be between 100AD
        ///  and 10000AD).
        /// </exception>
        public static DateTime VariantDateToDateTime(double value)
        {
            // The base time for OLE is 12:00:00 AM 12/30/1899, with the integer part being number of
            // days ahead or behind of the base date and the fractional part being the fraction of a
            // day _ahead_ of the calculated day. (e.g. 0.75 == -0.75). The fractional day is then rounded
            // to the nearest half second.
            //
            // Dates must be between 100AD and 10000AD.
            //
            // https://blogs.msdn.microsoft.com/ericlippert/2003/09/16/erics-complete-guide-to-vt_date/

            if (value < MinOleDate || value > MaxOleDate)
                throw new ArgumentOutOfRangeException(nameof(value));

            long dayOffsetInTicks = (long)value * TimeSpan.TicksPerDay;
            long fractionalDayTicks = Math.Abs((long)((value - (long)value) * TimeSpan.TicksPerDay));

            // TODO: Technically we need to round to the nearest half second, but .NET doesn't do this

            return new DateTime(s_oleBaseDate.Ticks + dayOffsetInTicks + fractionalDayTicks);
        }

        /// <summary>
        ///  Endian swaps an unsigned short.
        /// </summary>
        /// <param name="source">The unsigned short to swap.</param>
        /// <returns>The swapped unsigned short.</returns>
        public static ushort EndianSwap(ushort source) => (ushort)(((uint)source << 8) | ((uint)source >> 8));

        /// <summary>
        ///  Endian swaps a short.
        /// </summary>
        /// <param name="source">The short to swap.</param>
        /// <returns>The swapped  short.</returns>
        public static short EndianSwap(short source) => (short)EndianSwap((ushort)source);

        /// <summary>
        ///  Endian swaps an unsigned int.
        /// </summary>
        /// <param name="source">The unsigned int to swap.</param>
        /// <returns>The swapped unsigned int.</returns>
        public static uint EndianSwap(uint source)
        {
            return (source << 24)
                | ((source & 0x0000FF00) << 8)
                | ((source & 0x00FF0000) >> 8)
                | (source >> 24);
        }

        /// <summary>
        ///  Endian swaps an int.
        /// </summary>
        /// <param name="source">The int to swap.</param>
        /// <returns>The swapped int.</returns>
        public static int EndianSwap(int source) => (int)EndianSwap((uint)source);

        /// <summary>
        ///  Endian swaps an unsigned long.
        /// </summary>
        /// <param name="source">The unsigned long to swap.</param>
        /// <returns>The swapped unsigned long.</returns>
        public static ulong EndianSwap(ulong source)
        {
            return (source << 56)
                | ((source & 0x000000000000FF00) << 40)
                | ((source & 0x0000000000FF0000) << 24)
                | ((source & 0x00000000FF000000) << 8)
                | ((source & 0x000000FF00000000) >> 8)
                | ((source & 0x0000FF0000000000) >> 24)
                | ((source & 0x00FF000000000000) >> 40)
                | (source >> 56);
        }

        /// <summary>
        ///  Endian swaps a long.
        /// </summary>
        /// <param name="source">The long to swap.</param>
        /// <returns>The swapped long.</returns>
        public static long EndianSwap(long source) => (long)EndianSwap((ulong)source);

        /// <summary>
        ///  Wraps a span around a buffer that points to a null terminated string.
        /// </summary>
        public static unsafe ReadOnlySpan<char> NullTerminatedStringToSpan(char* buffer)
        {
            if (buffer == null)
                return default;

            char* end = buffer;
            while (*(++end) != '\0') { }

            return new ReadOnlySpan<char>(buffer, (int)(end - buffer));
        }
    }
}
