// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Security;
using WInterop.DynamicLinkLibrary;
using WInterop.ErrorHandling;
using WInterop.Handles;

namespace WInterop
{
    public static partial class NativeMethods
    {
        public static class DynamicLinkLibrary
        {
            /// <summary>
            /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
            /// </summary>
            public static class Desktop
            {
                /// <summary>
                /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
                /// </summary>
                /// <remarks>
                /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
                /// </remarks>
#if DESKTOP
                [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
                public static class Direct
                {
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684179.aspx
                    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                    public static extern SafeLibraryHandle LoadLibraryExW(
                        string lpFileName,
                        IntPtr hReservedNull,
                        LoadLibraryFlags dwFlags);

                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683152.aspx
                    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                    [return: MarshalAs(UnmanagedType.Bool)]
                    public static extern bool FreeLibrary(
                        IntPtr hModule);

                    // This API is only available in ANSI
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683212.aspx
                    [DllImport(Libraries.Kernel32, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, BestFitMapping = false)]
                    public static extern IntPtr GetProcAddress(
                        SafeLibraryHandle hModule,
                        [MarshalAs(UnmanagedType.LPStr)] string methodName);
                }

                /// <summary>
                /// Free the given library.
                /// </summary>
                public static bool FreeLibrary(SafeLibraryHandle handle)
                {
                    return Direct.FreeLibrary(handle.DangerousGetHandle());
                }

                /// <summary>
                /// Load the library at the given path.
                /// </summary>
                public static SafeLibraryHandle LoadLibrary(string path, LoadLibraryFlags flags)
                {
                    SafeLibraryHandle handle = Direct.LoadLibraryExW(path, IntPtr.Zero, flags);
                    if (handle.IsInvalid)
                        throw ErrorHelper.GetIoExceptionForLastError(path);

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
                public static DelegateType GetFunctionDelegate<DelegateType>(SafeLibraryHandle library, string methodName)
                {
                    IntPtr method = Direct.GetProcAddress(library, methodName);
                    if (method == IntPtr.Zero)
                        throw ErrorHelper.GetIoExceptionForLastError(methodName);

                    return Marshal.GetDelegateForFunctionPointer<DelegateType>(method);
                }
            }
        }
    }
}
