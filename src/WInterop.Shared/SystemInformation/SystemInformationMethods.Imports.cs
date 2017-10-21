// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
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

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724338.aspx
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public static extern void GetLocalTime(
                out SYSTEMTIME lpSystemTime);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724295.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetComputerNameW(
                SafeHandle lpBuffer,
                ref uint lpnSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724265.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint ExpandEnvironmentStringsW(
                string lpSrc,
                SafeHandle lpDst,
                uint nSize);
        }
    }
}
