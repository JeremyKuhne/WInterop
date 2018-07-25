// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Types
{
    [Flags]
    public enum KeyState : short
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/.aspx

        /// <summary>
        /// Key is down if set. Otherwise key is up.
        /// </summary>
        Down = -1,

        /// <summary>
        /// Key is toggled if set. Othewise key is untoggled.
        /// (CAPS LOCK, NUM LOCK, etc.)
        /// </summary>
        Toggled = 1
    }
}
