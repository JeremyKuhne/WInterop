// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.RemoteDesktop.Unsafe;

namespace WInterop.RemoteDesktop
{
    public static partial class RemoteDesktop
    {
        /// <summary>
        /// Get the session Id for the given process
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public static uint ProcessIdToSessionId(uint processId)
        {
            if (!Imports.ProcessIdToSessionId(processId, out uint sessionId))
                throw Error.GetExceptionForLastError();

            return sessionId;
        }

        /// <summary>
        /// Gets the session ID of the physical console. If there is no session attached,
        /// returns uint.MaxValue.
        /// </summary>
        public static uint GetActiveConsoleSessionId()
        {
            return Imports.WTSGetActiveConsoleSessionId();
        }
    }
}
