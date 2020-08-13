// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.ProcessAndThreads
{
    /// <summary>
    ///  [PROCESSINFOCLASS]
    /// </summary>
    public enum ProcessInfoClass : uint
    {
        ProcessBasicInformation = 0,
        ProcessDebugPort = 7,
        ProcessWow64Information = 26,
        ProcessImageFileName = 27,
        ProcessBreakOnTermination = 29
    }
}
