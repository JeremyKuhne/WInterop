// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    using ErrorHandling;
    using System.Runtime.InteropServices;
    using System.Security;

    public static partial class NativeMethods
    {
        // See the all platform common file for more information

        public static partial class ErrorHandling
        {
            // Putting private P/Invokes in a subclass to allow exact matching of signatures for perf on initial call and reduce string count
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
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
                {
                    uint error = (uint)Marshal.GetLastWin32Error();
                    throw GetIoExceptionForError(error);
                }
            }

            /// <summary>
            /// Play the specified sound (as defined in the Sound control panel).
            /// </summary>
            public static void MessageBeep(MessageBeepType type)
            {
                if (!Direct.MessageBeep(type))
                {
                    uint error = (uint)Marshal.GetLastWin32Error();
                    throw GetIoExceptionForError(error);
                }
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
                {
                    uint error = (uint)Marshal.GetLastWin32Error();
                    throw GetIoExceptionForError(error);
                }
                return oldMode;
            }
        }
    }
}
