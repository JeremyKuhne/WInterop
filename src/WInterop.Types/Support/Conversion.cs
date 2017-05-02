// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices.ComTypes;
using WInterop.FileManagement.Types;

namespace WInterop.Support
{
    public static class Conversion
    {
        private static DateTime OleBaseDate = new DateTime(year: 1899, month: 12, day: 30);
        private const double MinOleDate = -657435.0;
        private const double MaxOleDate = 2958466.0;

        public static ulong HighLowToLong(uint high, uint low)
        {
            return ((ulong)high) << 32 | ((ulong)low & 0xFFFFFFFFL);
        }

        public static ushort HighWord(IntPtr value) => (ushort)(((ulong)value >> 16) & 0xFFFF);

        public static ushort HighWord(UIntPtr value) => (ushort)(((ulong)value >> 16) & 0xFFFF);

        public static ushort HighWord(uint value) => (ushort)((value >> 16) & 0xFFFF);

        public static ushort LowWord(IntPtr value) => (ushort)((ulong)value & 0xFFFF);

        public static ushort LowWord(UIntPtr value) => (ushort)((ulong)value & 0xFFFF);

        public static ushort LowWord(uint value) => (ushort)(value & 0xFFFF);

        public static DateTime FileTimeToDateTime(FILETIME fileTime)
        {
            return DateTime.FromFileTime((((long)fileTime.dwHighDateTime) << 32) + (uint)fileTime.dwLowDateTime);
        }

        public static DesiredAccess FileAccessToDesiredAccess(System.IO.FileAccess fileAccess)
        {
            // See FileStream.Init to see how the mapping is done in .NET
            switch (fileAccess)
            {
                case System.IO.FileAccess.Read:
                    return DesiredAccess.FILE_GENERIC_READ;
                case System.IO.FileAccess.Write:
                    return DesiredAccess.FILE_GENERIC_WRITE;
                case System.IO.FileAccess.ReadWrite:
                    return DesiredAccess.FILE_GENERIC_READ | DesiredAccess.FILE_GENERIC_WRITE;
                default:
                    return 0;
            }
        }

        public static System.IO.FileAccess DesiredAccessToFileAccess(DesiredAccess desiredAccess)
        {
            System.IO.FileAccess fileAccess = 0;
            if ((desiredAccess & (DesiredAccess.FILE_GENERIC_READ | DesiredAccess.FILE_READ_DATA)) > 0)
                fileAccess = System.IO.FileAccess.Read;

            if ((desiredAccess & (DesiredAccess.FILE_GENERIC_WRITE | DesiredAccess.FILE_WRITE_DATA)) > 0)
                fileAccess = fileAccess == System.IO.FileAccess.Read ? System.IO.FileAccess.ReadWrite : System.IO.FileAccess.Write;

            return fileAccess;
        }

        public static ShareMode FileShareToShareMode(System.IO.FileShare fileShare)
        {
            // See additional comments on ShareMode
            fileShare &= ~System.IO.FileShare.Inheritable;
            return unchecked((ShareMode)fileShare);
        }

        public static FileFlags FileOptionsToFileFlags(System.IO.FileOptions fileOptions)
        {
            return unchecked((FileFlags)fileOptions);
        }

        public static CreationDisposition FileModeToCreationDisposition(System.IO.FileMode fileMode)
        {
            if (fileMode == System.IO.FileMode.Append)
                return CreationDisposition.OPEN_ALWAYS;
            else
                return unchecked((CreationDisposition)fileMode);
        }

        /// <summary>
        /// Convert a native OLE Variant VT_DATE to a DateTime.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the value is outside of the allowable range for an OLE Date (Dates must be between 100AD and 10000AD).</exception>
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

            return new DateTime(OleBaseDate.Ticks + dayOffsetInTicks + fractionalDayTicks);
        }

        /// <summary>
        /// Endian swaps an unsigned short.
        /// </summary>
        /// <param name="source">The unsigned short to swap.</param>
        /// <returns>The swapped unsigned short.</returns>
        public static ushort EndianSwap(ushort source)
        {
            return (ushort)(((uint)source << 8) | ((uint)source >> 8));
        }

        /// <summary>
        /// Endian swaps a short.
        /// </summary>
        /// <param name="source">The short to swap.</param>
        /// <returns>The swapped  short.</returns>
        public static short EndianSwap(short source) => (short)EndianSwap((ushort)source);

        /// <summary>
        /// Endian swaps an unsigned int.
        /// </summary>
        /// <param name="source">The unsigned int to swap.</param>
        /// <returns>The swapped unsigned int.</returns>
        public static uint EndianSwap(uint source)
        {
            return ((source << 24)
                | ((source & 0x0000FF00) << 8)
                | ((source & 0x00FF0000) >> 8)
                | (source >> 24));
        }

        /// <summary>
        /// Endian swaps an int.
        /// </summary>
        /// <param name="source">The int to swap.</param>
        /// <returns>The swapped int.</returns>
        public static int EndianSwap(int source) => (int)EndianSwap((uint)source);

        /// <summary>
        /// Endian swaps an unsigned long.
        /// </summary>
        /// <param name="source">The unsigned long to swap.</param>
        /// <returns>The swapped unsigned long.</returns>
        public static ulong EndianSwap(ulong source)
        {
            return ((source << 56)
                | ((source & 0x000000000000FF00) << 40)
                | ((source & 0x0000000000FF0000) << 24)
                | ((source & 0x00000000FF000000) << 8)
                | ((source & 0x000000FF00000000) >> 8)
                | ((source & 0x0000FF0000000000) >> 24)
                | ((source & 0x00FF000000000000) >> 40)
                | (source >> 56));
        }

        /// <summary>
        /// Endian swaps a long.
        /// </summary>
        /// <param name="source">The long to swap.</param>
        /// <returns>The swapped long.</returns>
        public static long EndianSwap(long source) => (long)EndianSwap((ulong)source);
    }
}
