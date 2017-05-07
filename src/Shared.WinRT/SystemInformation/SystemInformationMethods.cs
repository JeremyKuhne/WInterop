// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Support;
using WInterop.SystemInformation.Types;

namespace WInterop.SystemInformation
{
    public static partial class SystemInformationMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724482.aspx
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern bool IsProcessorFeaturePresent(
                ProcessorFeature ProcessorFeature);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/dn482415.aspx
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern bool CeipIsOptedIn();

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644905.aspx
            // This never returns false on XP or later
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern bool QueryPerformanceFrequency(
                out long lpFrequency);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms644904.aspx
            // This never returns false on XP or later
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern bool QueryPerformanceCounter(
                out long lpPerformanceCount);
        }

        /// <summary>
        /// Returns true if the specified processor feature is present.
        /// </summary>
        public static bool IsProcessorFeaturePresent(ProcessorFeature feature)
        {
            return Imports.IsProcessorFeaturePresent(feature);
        }

        /// <summary>
        /// Returns true if the user is currently opted in for the Customer Experience
        /// Improvement Program.
        /// </summary>
        public static bool CeipIsOptedIn()
        {
            return Imports.CeipIsOptedIn();
        }

        /// <summary>
        /// Returns the performance counter frequency in counts per second.
        /// </summary>
        public static long QueryPerformanceFrequency()
        {
            Imports.QueryPerformanceFrequency(out long frequency);
            return frequency;
        }

        /// <summary>
        /// Returns the current performance counter value in counts.
        /// </summary>
        public static long QueryPerformanceCounter()
        {
            Imports.QueryPerformanceCounter(out long counts);
            return counts;
        }
    }
}
