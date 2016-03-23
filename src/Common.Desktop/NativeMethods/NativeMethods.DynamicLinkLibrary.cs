// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Security;
    using Handles;

    public static partial class NativeMethods
    {
        public static class DynamicLinkLibrary
        {
            // Putting private P/Invokes in a subclass to allow exact matching of signatures for perf on initial call and reduce string count
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            private static class Private
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684179.aspx
                [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                internal static extern SafeLibraryHandle LoadLibraryExW(
                    string lpFileName,
                    IntPtr hReservedNull,
                    LoadLibraryFlags dwFlags);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683152.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                internal static extern bool FreeLibrary(
                    IntPtr hModule);

                // This API is only available in ANSI
                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683212.aspx
                [DllImport(Libraries.Kernel32, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, BestFitMapping = false)]
                internal static extern IntPtr GetProcAddress(
                    SafeLibraryHandle hModule,
                    [MarshalAs(UnmanagedType.LPStr)] string methodName);
            }

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684179.aspx
            [Flags]
            public enum LoadLibraryFlags : uint
            {
                DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
                LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
                LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008,
                LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
                LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
                LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
                LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
                LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
                LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
                LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
                LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000
            }

            /// <summary>
            /// Free the given library.
            /// </summary>
            internal static bool FreeLibrary(SafeLibraryHandle handle)
            {
                return Private.FreeLibrary(handle.DangerousGetHandle());
            }

            /// <summary>
            /// Load the library at the given path.
            /// </summary>
            internal static SafeLibraryHandle LoadLibrary(string path, LoadLibraryFlags flags)
            {
                SafeLibraryHandle handle = Private.LoadLibraryExW(path, IntPtr.Zero, flags);
                if (handle.IsInvalid)
                {
                    uint error = (uint)Marshal.GetLastWin32Error();
                    throw Errors.GetIoExceptionForError(error, path);
                }
                return handle;
            }

            /// <summary>
            /// Get a delegate for the given native method
            /// </summary>
            /// <remarks>
            /// Here is a sample delegate definition:
            ///
            ///     [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
            ///     private delegate int DoubleDelegate(int value);
            ///
            /// And it's native signature:
            /// 
            ///     extern "C" __declspec (dllexport) int Double(int);
            /// </remarks>
            internal static DelegateType GetFunctionDelegate<DelegateType>(SafeLibraryHandle library, string methodName)
            {
                IntPtr method = Private.GetProcAddress(library, methodName);
                if (method == IntPtr.Zero)
                {
                    uint error = (uint)Marshal.GetLastWin32Error();
                    throw Errors.GetIoExceptionForError(error, methodName);
                }

                return Marshal.GetDelegateForFunctionPointer<DelegateType>(method);
            }
        }
    }
}
