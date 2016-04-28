// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    public static partial class NativeMethods
    {
        /// <summary>
        /// Standard library name defines. Using a central helps ensure we keep names consistent.
        /// Using API library definitions over legacy library names is encouraged for future proofing
        /// and a supposed slight perf benefit in looking up apis.
        /// </summary>
        public static class Libraries
        {
            public const string Kernel32 = "kernel32.dll";
            public const string Advapi32 = "advapi32.dll";
            public const string User32 = "user32.dll";
            public const string Ntdll = "ntdll.dll";
            public const string Crypt32 = "crypt32.dll";
            public const string Netapi32 = "netapi32.dll";
        }
    }
}
