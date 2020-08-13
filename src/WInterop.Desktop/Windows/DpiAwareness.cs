// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://docs.microsoft.com/windows/win32/api/windef/ne-windef-dpi_awareness
    public enum DpiAwareness
    {
        /// <summary>
        ///  [DPI_AWARENESS_INVALID]
        /// </summary>
        Invalid = -1,

        /// <summary>
        ///  [DPI_AWARENESS_UNAWARE]
        /// </summary>
        Unaware = 0,

        /// <summary>
        ///  [DPI_AWARENESS_SYSTEM_AWARE]
        /// </summary>
        SystemAware = 1,

        /// <summary>
        ///  [DPI_AWARENESS_PER_MONITOR_AWARE]
        /// </summary>
        PerMonitorAware = 2
    }
}
