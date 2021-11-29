// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Security;

namespace WInterop.ProcessAndThreads;

[Flags]
public enum ProcessAccessRights : uint
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684880.aspx

    /// <summary>
    ///  The right to delete the object. [DELETE]
    /// </summary>
    Delete = StandardAccessRights.Delete,

    /// <summary>
    ///  The right to read the information in the object's security descriptor.
    ///  Doesn't include system access control list info (SACL). [READ_CONTROL]
    /// </summary>
    ReadControl = StandardAccessRights.ReadControl,

    /// <summary>
    ///  The right to use the object for synchronization. Enables a thread to wait
    ///  until the object is in the signaled state. [SYNCHRONIZE]
    /// </summary>
    Synchronize = StandardAccessRights.Synchronize,

    /// <summary>
    ///  The right to modify the discretionary access control list (DACL) in the
    ///  object's security descriptor. [WRITE_DAC]
    /// </summary>
    WriteDac = StandardAccessRights.WriteDac,

    /// <summary>
    ///  The right to change the owner in the object's security descriptor. [WRITE_OWNER]
    /// </summary>
    WriteOwner = StandardAccessRights.WriteOwner,

    /// <summary>
    ///  Required to terminate using TerminateProcess. [PROCESS_TERMINATE]
    /// </summary>
    Terminate = 0x0001,

    /// <summary>
    ///  Required to create a thread. [PROCESS_CREATE_THREAD]
    /// </summary>
    CreateThread = 0x0002,

    /// <summary>
    ///  Not documented. [PROCESS_SET_SESSIONID]
    /// </summary>
    SetSessionId = 0x0004,

    /// <summary>
    ///  Required to perform an operation on the address space of a process. [PROCESS_VM_OPERATION]
    /// </summary>
    VirtualMemoryOperation = 0x0008,

    /// <summary>
    ///  Required to read memory in a process using ReadProcessMemory. [PROCESS_VM_READ]
    /// </summary>
    VirtualMemoryRead = 0x0010,

    /// <summary>
    ///  Required to write to memory in a process using WriteProcessMemory. [PROCESS_VM_WRITE]
    /// </summary>
    VirtualMemoryWrite = 0x0020,

    /// <summary>
    ///  Required to duplicate a handle using DuplicateHandle. [PROCESS_DUP_HANDLE]
    /// </summary>
    DuplicateHandle = 0x0040,

    /// <summary>
    ///  Required to create a process. [PROCESS_CREATE_PROCESS]
    /// </summary>
    CreateProcess = 0x0080,

    /// <summary>
    ///  Required to set memory limits using SetProcessWorkingSetSize. [PROCESS_SET_QUOTA]
    /// </summary>
    SetQuota = 0x0100,

    /// <summary>
    ///  Required to set certain information about a process, such as its priority class (see SetPriorityClass).
    ///  [PROCESS_SET_INFORMATION]
    /// </summary>
    SetInformation = 0x0200,

    /// <summary>
    ///  Required to retrieve certain information about a process, such as its token, exit code,
    ///  and priority class (see OpenProcessToken). [PROCESS_QUERY_INFORMATION]
    /// </summary>
    QueryInformation = 0x0400,

    /// <summary>
    ///  Required to suspend or resume a process. [PROCESS_SUSPEND_RESUME]
    /// </summary>
    SuspendResume = 0x0800,

    /// <summary>
    ///  Required to retrieve certain information about a process (see GetExitCodeProcess, GetPriorityClass,
    ///  IsProcessInJob, QueryFullProcessImageName). QueryInformation includes this by default.
    ///  [PROCESS_QUERY_LIMITED_INFORMATION]
    /// </summary>
    QueryLimitedInfomration = 0x1000,

    /// <summary>
    ///  Not documented. [PROCESS_SET_LIMITED_INFORMATION]
    /// </summary>
    SetLimitedInformation = 0x2000
}