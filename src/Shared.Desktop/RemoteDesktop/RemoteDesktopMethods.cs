// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Support;

namespace WInterop.Desktop.RemoteDesktop
{
    public static partial class RemoteDesktopMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
        {
            // https://msdn.microsoft.com/en-us/library/aa382990.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool ProcessIdToSessionId(
                uint dwProcessId,
                out uint pSessionId);

            // https://msdn.microsoft.com/en-us/library/aa383835.aspx
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern uint WTSGetActiveConsoleSessionId();
        }

        /// <summary>
        /// Get the session Id for the given process
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static uint ProcessIdToSessionId(uint processId)
        {
            if (!Direct.ProcessIdToSessionId(processId, out uint sessionId))
                throw Errors.GetIoExceptionForLastError();

            return sessionId;
        }

        /// <summary>
        /// Gets the session ID of the physical console. If there is no session attached,
        /// returns uint.MaxValue.
        /// </summary>
        public static uint GetActiveConsoleSessionId()
        {
            return Direct.WTSGetActiveConsoleSessionId();
        }
    }
}
