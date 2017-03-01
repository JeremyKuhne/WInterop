// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Support.Buffers;

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
    }
}
