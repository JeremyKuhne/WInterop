// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Console.Native;
using WInterop.Errors;
using WInterop.Support.Buffers;
using WInterop.Windows;

namespace WInterop.Console
{
    public static partial class Console
    {
        public static WindowHandle GetConsoleWindow() => Imports.GetConsoleWindow();

        public static void FreeConsole() => Error.ThrowLastErrorIfFalse(Imports.FreeConsole());

        public static bool TryFreeConsole() => Imports.FreeConsole();

        public static uint GetConsoleInputCodePage() => Imports.GetConsoleCP();

        public static uint GetConsoleOuputCodePage() => Imports.GetConsoleOutputCP();

        public static ConsoleInputMode GetConsoleInputMode(SafeFileHandle inputHandle)
        {
            Error.ThrowLastErrorIfFalse(Imports.GetConsoleMode(inputHandle, out uint mode));
            return (ConsoleInputMode)mode;
        }

        public static void SetConsoleInputMode(SafeFileHandle inputHandle, ConsoleInputMode mode)
            => Error.ThrowLastErrorIfFalse(Imports.SetConsoleMode(inputHandle, (uint)mode));

        public static ConsoleOuputMode GetConsoleOutputMode(SafeFileHandle outputHandle)
        {
            Error.ThrowLastErrorIfFalse(Imports.GetConsoleMode(outputHandle, out uint mode));
            return (ConsoleOuputMode)mode;
        }

        public static void SetConsoleOutputMode(SafeFileHandle outputHandle, ConsoleOuputMode mode)
            => Error.ThrowLastErrorIfFalse(Imports.SetConsoleMode(outputHandle, (uint)mode));

        /// <summary>
        ///  Get the specified standard handle.
        /// </summary>
        /// <returns>Handle or null if there is no associated handle for the given type.</returns>
        public static SafeFileHandle? GetStandardHandle(StandardHandleType type)
        {
            IntPtr handle = Imports.GetStdHandle(type);
            if (handle == (IntPtr)(-1))
                Error.ThrowLastError();

            // If there is no associated standard handle, return null
            if (handle == IntPtr.Zero)
                return null;

            // The standard handles do not need to be released.
            return new SafeFileHandle(handle, ownsHandle: false);
        }

        /// <summary>
        ///  Reads input from the console. Will wait for next input, exit the iterator to stop listening.
        /// </summary>
        public static IEnumerable<InputRecord> ReadConsoleInput(SafeFileHandle inputHandle)
        {
            InputRecord buffer = default;
            while (Imports.ReadConsoleInputW(inputHandle, ref buffer, 1, out _))
            {
                yield return buffer;
            }

            Error.ThrowLastError();
        }

        /// <summary>
        ///  Peek at the console input records.
        /// </summary>
        /// <param name="count">The maximum number of records to investigate.</param>
        public static IEnumerable<InputRecord> PeekConsoleInput(SafeFileHandle inputHandle, int count)
        {
            var owner = OwnedMemoryPool.Rent<InputRecord>(count);
            if (!Imports.PeekConsoleInputW(inputHandle, ref MemoryMarshal.GetReference(owner.Memory.Span), (uint)count, out uint read))
            {
                owner.Dispose();
                Error.ThrowLastError();
            }

            return new OwnedMemoryEnumerable<InputRecord>(owner, 0, (int)read);
        }

        /// <summary>
        ///  Writes the specified <paramref name="text"/> to the given console output handle.
        /// </summary>
        public static unsafe uint WriteConsole(SafeFileHandle outputHandle, ReadOnlySpan<char> text)
        {
            fixed (char* c = &MemoryMarshal.GetReference(text))
            {
                Error.ThrowLastErrorIfFalse(
                    Imports.WriteConsoleW(outputHandle, c, (uint)text.Length, out uint charsWritten));

                return charsWritten;
            }
        }
    }
}