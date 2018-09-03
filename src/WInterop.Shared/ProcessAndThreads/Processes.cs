// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.ProcessAndThreads.Unsafe;

namespace WInterop.ProcessAndThreads
{
    public static partial class Processes
    {
        /// <summary>
        /// Get the handle for the current process.
        /// Note that this handle is only relevant in the current process- it
        /// can't be passed to other processes.
        /// </summary>
        public static ProcessHandle GetCurrentProcess() => Imports.GetCurrentProcess();

        /// <summary>
        /// Get the handle for the current process.
        /// </summary>
        public static uint GetCurrentProcessId() => Imports.GetCurrentProcessId();

        /// <summary>
        /// Open a handle to the specified process by id.
        /// </summary>
        public static SafeProcessHandle OpenProcess(uint processId, ProcessAccessRights desiredAccess, bool inheritHandle = false)
        {
            SafeProcessHandle handle = Imports.OpenProcess(desiredAccess, inheritHandle, processId);
            if (handle.IsInvalid)
                throw Error.GetExceptionForLastError();
            return handle;
        }
    }
}
