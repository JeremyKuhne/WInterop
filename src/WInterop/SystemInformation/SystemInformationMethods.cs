// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.SystemInformation.DataTypes;

namespace WInterop.SystemInformation
{
    public static class SystemInformationMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724482.aspx
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern bool IsProcessorFeaturePresent(
                ProcessorFeature ProcessorFeature);
        }

        public static bool IsProcessorFeaturePresent(ProcessorFeature feature)
        {
            return Direct.IsProcessorFeaturePresent(feature);
        }
    }
}
