// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Console.Types;

namespace WInterop.Desktop.Console
{
    public static partial class ConsoleMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms681952.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool AttachConsole(
                uint dwProcessId);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683150.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool FreeConsole();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms681944.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool AllocConsole();

            // https://docs.microsoft.com/en-us/windows/console/getstdhandle
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern SafeHandle GetStdHandle(
                StandardHandleType nStdHandle);

            // https://docs.microsoft.com/en-us/windows/console/setstdhandle
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetStdHandle(
                StandardHandleType nStdHandle,
                SafeHandle hHandle);

            // https://docs.microsoft.com/en-us/windows/console/getconsolemode
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetConsoleMode(
                SafeHandle hConsoleHandle,
                out uint lpMode);

            // https://docs.microsoft.com/en-us/windows/console/getconsolecp
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern uint GetConsoleCP();

            // https://docs.microsoft.com/en-us/windows/console/getconsoleoutputcp
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern uint GetConsoleOutputCP();
        }
    }
}
