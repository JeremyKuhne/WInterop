// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    using ErrorHandling;
    using Handles;
    using System.Runtime.InteropServices;
    using System.Security;

    public static partial class NativeMethods
    {
        public static class Strings
        {
            // Putting private P/Invokes in a subclass to allow exact matching of signatures for perf on initial call and reduce string count
#if DESKTOP
        [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            private static class Private
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647486.aspx
                [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                unsafe internal static extern int LoadStringW(
                    SafeLibraryHandle hInstance,
                    int uID,
                    out char* lpBuffer,
                    int nBufferMax);
            }

            /// <summary>
            /// Get the specified string resource from the given library.
            /// </summary>
            unsafe public static string LoadString(SafeLibraryHandle library, int identifier)
            {
                // Passing 0 will give us a read only handle to the string resource
                char* buffer;
                int result = Private.LoadStringW(library, identifier, out buffer, 0);
                if (result <= 0)
                    throw ErrorHelper.GetIoExceptionForLastError(identifier.ToString());

                // Null is not included in the result
                return new string(buffer, 0, result);
            }
        }
    }
}
