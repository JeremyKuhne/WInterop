// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Console.Types;
using WInterop.Support;

namespace WInterop.Desktop.Console
{
    public static partial class ConsoleMethods
    {
        public static uint GetConsoleInputCodePage()
        {
            return Imports.GetConsoleCP();
        }

        public static uint GetConsoleOuputCodePage()
        {
            return Imports.GetConsoleOutputCP();
        }

        public static SafeHandle GetStandardHandle(StandardHandleType type)
        {
            SafeHandle handle = Imports.GetStdHandle(type);
            if (handle.IsInvalid)
                throw Errors.GetIoExceptionForLastError();
            return handle;
        }
    }
}
