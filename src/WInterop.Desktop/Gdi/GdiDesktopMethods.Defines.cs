// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static partial class GdiDesktopMethods
    {
        public static class Defines
        {
            public const uint ENUM_CURRENT_SETTINGS = unchecked((uint)-1);
            public const uint ENUM_REGISTRY_SETTINGS = unchecked((uint)-2);
        }
    }
}
