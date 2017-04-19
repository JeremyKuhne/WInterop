// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.Modules.DataTypes;
using WInterop.Support;

namespace WInterop.Resources
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static partial class ResourceMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647486.aspx
            [DllImport(Libraries.User32, SetLastError = true, ExactSpelling = true)]
            unsafe public static extern int LoadStringW(
                ModuleHandle hInstance,
                int uID,
                out char* lpBuffer,
                int nBufferMax);
        }

        /// <summary>
        /// Get the specified string resource from the given library.
        /// </summary>
        unsafe public static string LoadString(ModuleHandle library, int identifier)
        {
            // A string resource is mapped in with the dll, there is no need to allocate
            // or free a buffer.

            // Passing 0 will give us a read only handle to the string resource
            int result = Direct.LoadStringW(library, identifier, out char* buffer, 0);
            if (result <= 0)
                throw Errors.GetIoExceptionForLastError(identifier.ToString());

            // Null is not included in the result
            return new string(buffer, 0, result);
        }
    }
}
