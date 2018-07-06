// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Console.Types;
using WInterop.Support;

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

        public static IEnumerable<INPUT_RECORD> ReadConsoleInput(SafeFileHandle inputHandle)
        {
            INPUT_RECORD buffer = new INPUT_RECORD();
            while (Imports.ReadConsoleInputW(inputHandle, ref buffer, 1, out uint read))
            {
                yield return buffer;
            }
            throw Errors.GetIoExceptionForLastError();
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
