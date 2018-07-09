// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Console.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Console
{
    public static partial class ConsoleMethods
    {
        public static void FreeConsole()
        {
            if (!Imports.FreeConsole())
                throw Errors.GetIoExceptionForLastError();
        }

        public static bool TryFreeConsole()
        {
            return Imports.FreeConsole();
        }

        public static uint GetConsoleInputCodePage()
        {
            return Imports.GetConsoleCP();
        }

        public static uint GetConsoleOuputCodePage()
        {
            return Imports.GetConsoleOutputCP();
        }

        public static ConsoleInputMode GetConsoleInputMode(SafeFileHandle inputHandle)
        {
            if (!Imports.GetConsoleMode(inputHandle, out uint mode))
                throw Errors.GetIoExceptionForLastError();

            return (ConsoleInputMode)mode;
        }

        public static void SetConsoleInputMode(SafeFileHandle inputHandle, ConsoleInputMode mode)
        {
            if (!Imports.SetConsoleMode(inputHandle, (uint)mode))
                throw Errors.GetIoExceptionForLastError();
        }

        public static ConsoleOuputMode GetConsoleOutputMode(SafeFileHandle outputHandle)
        {
            if (!Imports.GetConsoleMode(outputHandle, out uint mode))
                throw Errors.GetIoExceptionForLastError();

            return (ConsoleOuputMode)mode;
        }

        public static void SetConsoleOutputMode(SafeFileHandle outputHandle, ConsoleOuputMode mode)
        {
            if (!Imports.SetConsoleMode(outputHandle, (uint)mode))
                throw Errors.GetIoExceptionForLastError();
        }

        public static SafeFileHandle GetStandardHandle(StandardHandleType type)
        {
            IntPtr handle = Imports.GetStdHandle(type);
            if (handle == (IntPtr)(-1))
                throw Errors.GetIoExceptionForLastError();

            // If there is no associated standard handle, return null
            if (handle == IntPtr.Zero)
                return null;

            // The standard handles
            return new SafeFileHandle(handle, ownsHandle: false);
        }

        /// <summary>
        /// Reads input from the console. Will wait for next input, exit the iterator to stop listening.
        /// </summary>
        public static IEnumerable<INPUT_RECORD> ReadConsoleInput(SafeFileHandle inputHandle)
        {
            INPUT_RECORD buffer = new INPUT_RECORD();
            while (Imports.ReadConsoleInputW(inputHandle, ref buffer, 1, out uint read))
            {
                yield return buffer;
            }
            throw Errors.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Peek at the console input records.
        /// </summary>
        /// <param name="count">The maximum number of records to investigate.</param>
        public static IEnumerable<INPUT_RECORD> PeekConsoleInput(SafeFileHandle inputHandle, int count)
        {
            var owner = OwnedMemoryPool.Rent<INPUT_RECORD>(count);
            if (!Imports.PeekConsoleInputW(inputHandle, ref MemoryMarshal.GetReference(owner.Memory.Span), (uint)count, out uint read))
            {
                owner.Dispose();
                throw Errors.GetIoExceptionForLastError();
            }

            return new OwnedMemoryEnumerable<INPUT_RECORD>(owner, 0, (int)read);
        }

        public unsafe static uint WriteConsole(SafeFileHandle outputHandle, ReadOnlySpan<char> text)
        {
            fixed (char* c = &MemoryMarshal.GetReference(text))
            {
                if (!Imports.WriteConsoleW(outputHandle, c, (uint)text.Length, out uint charsWritten))
                    throw Errors.GetIoExceptionForLastError();

                return charsWritten;
            }
        }
    }
}
