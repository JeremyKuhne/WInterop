// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Authorization.Types;
using WInterop.Handles.Types;

namespace WInterop.Synchronization
{
    public static class SynchronizationMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
        {
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static unsafe extern EventHandle CreateEventW(
                SECURITY_ATTRIBUTES* lpEventAttributes,
                bool bManualReset,
                bool bInitialState,
                string lpName);
        }
    }
}
