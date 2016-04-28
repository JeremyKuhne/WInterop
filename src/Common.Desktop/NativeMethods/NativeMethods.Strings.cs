// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling;
using WInterop.Handles;
using System.Runtime.InteropServices;
using System.Security;

namespace WInterop
{
    public static partial class NativeMethods
    {
        public static class Strings
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
                    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647486.aspx
                    [DllImport(Libraries.User32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                    unsafe public static extern int LoadStringW(
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
                    int result = Direct.LoadStringW(library, identifier, out buffer, 0);
                    if (result <= 0)
                        throw ErrorHelper.GetIoExceptionForLastError(identifier.ToString());

                    // Null is not included in the result
                    return new string(buffer, 0, result);
                }
            }
        }
    }
}
