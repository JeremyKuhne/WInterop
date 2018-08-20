// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Direct2d
{
    /// <summary>
    /// This specifies options that apply to the device context for its lifetime. [D2D1_DEVICE_CONTEXT_OPTIONS]
    /// </summary>
    [Flags]
    public enum DeviceContextOptions : uint
    {
        None = 0,

        /// <summary>
        /// Geometry rendering will be performed on many threads in parallel, a single
        /// thread is the default.
        /// </summary>
        EnableMultithreadedOptimizations = 1,
    }
}
