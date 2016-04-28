// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.ErrorHandling
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class ErrorHelper
    {
        /// <summary>
        /// Turns the last Windows error into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)] // Want to try and force the get last error inline
        public static Exception GetIoExceptionForLastError(string path = null)
        {
            uint error = (uint)Marshal.GetLastWin32Error();
            return GetIoExceptionForError(error, path);
        }

        /// <summary>
        /// Turns Windows errors into the appropriate exception (that maps with existing .NET behavior as much as possible).
        /// There are additional IOException derived errors for ease of client error handling.
        /// </summary>
        public static Exception GetIoExceptionForError(uint error, string path = null)
        {
            // http://referencesource.microsoft.com/#mscorlib/system/io/__error.cs,142

            string errorText = $"{NativeMethods.ErrorHandling.LastErrorToString(error)} : '{path ?? WInteropStrings.NoValue}'";

            switch (error)
            {
                case WinErrors.ERROR_FILE_NOT_FOUND:
                    return new FileNotFoundException(errorText, path);
                case WinErrors.ERROR_PATH_NOT_FOUND:
                    return new DirectoryNotFoundException(errorText);
                case WinErrors.ERROR_ACCESS_DENIED:
                // Network access doesn't throw UnauthorizedAccess in .NET
                case WinErrors.ERROR_NETWORK_ACCESS_DENIED:
                    return new UnauthorizedAccessException(errorText);
                case WinErrors.ERROR_FILENAME_EXCED_RANGE:
                    return new PathTooLongException(errorText);
                case WinErrors.ERROR_INVALID_DRIVE:
#if DESKTOP
                    return new DriveNotFoundException(errorText);
#else
                    goto default;
#endif
                case WinErrors.ERROR_OPERATION_ABORTED:
                    return new OperationCanceledException(errorText);
                case WinErrors.ERROR_NOT_READY:
                    return new DriveNotReadyException(errorText);
                case WinErrors.FVE_E_LOCKED_VOLUME:
                    return new DriveLockedException(errorText);
                case WinErrors.ERROR_ALREADY_EXISTS:
                case WinErrors.ERROR_SHARING_VIOLATION:
                case WinErrors.ERROR_FILE_EXISTS:
                default:
                    return new IOException(errorText, (int)NativeMethods.ErrorHandling.GetHResultForWindowsError(error));
            }
        }

        /// <summary>
        /// EntryPointNotFoundException isn't available in portable assemblies. Use this to check the type.
        /// </summary>
        public static bool IsEntryPointNotFoundException(Exception e)
        {
            return e.GetType().FullName.Equals("System.EntryPointNotFoundException", StringComparison.Ordinal);
        }
    }
}
