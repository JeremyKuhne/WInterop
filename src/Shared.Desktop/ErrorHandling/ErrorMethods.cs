// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Support;

namespace WInterop.ErrorHandling
{
    public static partial class ErrorMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679355.aspx
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern ErrorMode GetErrorMode();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679355.aspx
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern ErrorMode GetThreadErrorMode();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680621.aspx
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern ErrorMode SetErrorMode(
                ErrorMode uMode);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms680621.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetThreadErrorMode(
                ErrorMode dwNewMode,
                out ErrorMode lpOldMode);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679277.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool Beep(
                uint dwFreq,
                uint dwDurations);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms679277.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool MessageBeep(
                MessageBeepType uType);
        }

        /// <summary>
        /// Emit a beep.
        /// </summary>
        /// <param name="frequency">Frequency in hertz.</param>
        /// <param name="duration">Duration in milliseconds.</param>
        public static void Beep(uint frequency, uint duration)
        {
            if (!Direct.Beep(frequency, duration))
                throw Errors.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Play the specified sound (as defined in the Sound control panel).
        /// </summary>
        public static void MessageBeep(MessageBeepType type)
        {
            if (!Direct.MessageBeep(type))
                throw Errors.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Gets the error mode for the current process.
        /// </summary>
        public static ErrorMode GetProcessErrorMode()
        {
            return Direct.GetErrorMode();
        }

        /// <summary>
        /// Gets the error mode for the current thread.
        /// </summary>
        public static ErrorMode GetThreadErrorMode()
        {
            return Direct.GetThreadErrorMode();
        }

        /// <summary>
        /// Set a new error mode for the current thread.
        /// </summary>
        /// <returns>The old error mode for the thread.</returns>
        public static ErrorMode SetThreadErrorMode(ErrorMode mode)
        {
            ErrorMode oldMode;
            if (!Direct.SetThreadErrorMode(mode, out oldMode))
                throw Errors.GetIoExceptionForLastError();

            return oldMode;
        }

        /// <summary>
        /// Turns NTSTATUS errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetIoExceptionForNTStatus(NTSTATUS status, string path = null)
        {
            switch (status)
            {
                case NTSTATUS.STATUS_NOT_IMPLEMENTED:
                    return new NotImplementedException(path ?? WInteropStrings.NoValue);
            }

            return Errors.GetIoExceptionForError(ErrorMethods.NtStatusToWinError(status), path);
        }
    }
}
