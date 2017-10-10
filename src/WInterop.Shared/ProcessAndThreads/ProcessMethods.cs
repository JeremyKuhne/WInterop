// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.ProcessAndThreads.Types;

namespace WInterop.ProcessAndThreads
{
    public static partial class ProcessMethods
    {
        public static ProcessHandle GetCurrentProcess()
        {
            return Imports.GetCurrentProcess();
        }

        public static uint GetCurrentProcessId()
        {
            return Imports.GetCurrentProcessId();
        }
    }
}
