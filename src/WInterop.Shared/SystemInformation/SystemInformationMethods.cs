// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling.Types;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.SystemInformation.Types;

namespace WInterop.SystemInformation
{
    public static partial class SystemInformationMethods
    {
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

        /// <summary>
        /// Returns the local system time.
        /// </summary>
        public static SYSTEMTIME GetLocalTime()
        {
            Imports.GetLocalTime(out SYSTEMTIME time);
            return time;
        }

        /// <summary>
        /// Gets the NetBIOS computer name.
        /// </summary>
        public static string GetComputerName()
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                uint size = buffer.CharCapacity;
                while (!Imports.GetComputerNameW(buffer, ref size))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_BUFFER_OVERFLOW);
                    buffer.EnsureCharCapacity(size);
                }
                buffer.Length = size;
                return buffer.ToString();
            });
        }
    }
}
