// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Support.Buffers;
using WInterop.SystemInformation.DataTypes;

namespace WInterop.SystemInformation
{
    public static class SystemInformationDesktopMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724432.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetUserNameW(
                SafeHandle lpBuffer,
                ref uint lpnSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/mt668928.aspx
            [DllImport(Libraries.Ntdll, ExactSpelling = true)]
            public static extern SuiteMask RtlGetSuiteMask();
        }

        /// <summary>
        /// Get the current user name.
        /// </summary>
        public static string GetUserName()
        {
            return BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                uint sizeInChars = buffer.CharCapacity;
                while (!Direct.GetUserNameW(buffer, ref sizeInChars))
                {
                    ErrorHelper.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    buffer.EnsureCharCapacity(sizeInChars);
                }

                buffer.Length = sizeInChars - 1;
                return buffer.ToString();
            });
        }

        /// <summary>
        /// Returns the suite mask for the OS which defines the "edition" of Windows.
        /// </summary>
        public static SuiteMask GetSuiteMask()
        {
            return Direct.RtlGetSuiteMask();
        }
    }
}
