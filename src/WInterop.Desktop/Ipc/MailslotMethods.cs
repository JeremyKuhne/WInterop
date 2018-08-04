// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Security.Native;
using WInterop.Ipc.Types;
using WInterop.Support;

namespace WInterop.Ipc
{
    public static partial class MailslotMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365147.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern SafeMailslotHandle CreateMailslotW(
                string lpName,
                uint nMaxMessageSize,
                uint lReadTimeout,
                SECURITY_ATTRIBUTES* lpSecurityAttributes);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365435.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern bool GetMailslotInfo(
                SafeMailslotHandle hMailslot,
                uint* lpMaxMessageSize,
                uint* lpNextSize,
                uint* lpMessageCount,
                uint* lpReadTimeout);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365786.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern bool SetMailslotInfo(
                SafeMailslotHandle hMailslot,
                uint lReadTimeout);
        }

        /// <summary>
        /// Create a mailslot.
        /// </summary>
        /// <param name="name">Name of the mailslot.</param>
        /// <param name="maxMessageSize">Maximum size, in bytes, of messages that can be posted to the mailslot. 0 means any size.</param>
        /// <param name="readTimeout">
        /// Timeout, in milliseconds, that a read operation will wait for a message to be posted. 0 means do not wait, uint.MaxValue means
        /// wait indefinitely.
        /// </param>
        public static SafeMailslotHandle CreateMailslot(string name, uint maxMessageSize = 0, uint readTimeout = 0)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            SafeMailslotHandle handle;

            unsafe
            {
                handle = Imports.CreateMailslotW(name, maxMessageSize, readTimeout, null);
            }

            if (handle.IsInvalid)
                throw Errors.GetIoExceptionForLastError(name);

            return handle;
        }

        /// <summary>
        /// Get information for the given mailslot.
        /// </summary>
        public static MailslotInfo GetMailslotInfo(SafeMailslotHandle mailslotHandle)
        {
            MailslotInfo info = new MailslotInfo();

            unsafe
            {
                if (!Imports.GetMailslotInfo(
                    hMailslot: mailslotHandle,
                    lpMaxMessageSize: &info.MaxMessageSize,
                    lpNextSize: &info.NextSize,
                    lpMessageCount: &info.MessageCount,
                    lpReadTimeout: &info.ReadTimeout))
                    throw Errors.GetIoExceptionForLastError();
            }

            return info;
        }

        /// <summary>
        /// Set the given mailslot's read timeout.
        /// </summary>
        /// <param name="readTimeout">Timeout for read operations in milliseconds. Set to uint.MaxValue for infinite timeout.</param>
        public static void SetMailslotTimeout(SafeMailslotHandle mailslotHandle, uint readTimeout)
        {
            if (!Imports.SetMailslotInfo(mailslotHandle, readTimeout))
                throw Errors.GetIoExceptionForLastError();
        }
    }
}
