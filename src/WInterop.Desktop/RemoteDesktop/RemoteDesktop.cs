// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.RemoteDesktop.Native;

namespace WInterop.RemoteDesktop
{
    public static partial class RemoteDesktop
    {
        /// <summary>
        ///  Get the session Id for the given process
        /// </summary>
        public static uint ProcessIdToSessionId(uint processId)
        {
            Error.ThrowLastErrorIfFalse(
                Imports.ProcessIdToSessionId(processId, out uint sessionId));

            return sessionId;
        }

        /// <summary>
        ///  Gets the session ID of the physical console. If there is no session attached,
        ///  returns uint.MaxValue.
        /// </summary>
        public static uint GetActiveConsoleSessionId() => Imports.WTSGetActiveConsoleSessionId();
    }
}
