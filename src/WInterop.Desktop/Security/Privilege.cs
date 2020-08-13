// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/bb530716.aspx">Privilege constants</a>.
    /// </summary>
    /// <remarks>
    ///  Very few privileges are granted when not running elevated. ChangeNotify, Shutdown, Undock,
    ///  IncreaseWorkingSet, and TimeZone are available.
    ///
    ///  Running elevated adds IncreaseQuota, Security, TakeOwnership, LoadDriver, SystemProfile,
    ///  SystemTime, ProfileSingleProcessPrivilege, IncreseBasePriority, CreatePagefile, Backup,
    ///  Restore, Debug, SystemEnvironment, RemoteShutdown, ManageVolume, Impersonate, CreateGlobal,
    ///  CreateSymbolicLink and DelegateSessionUserImpersonate.
    /// </remarks>
    public enum Privilege
    {
        // Names have to match the strings Windows uses exactly

        /// <summary>
        ///  Unknown privilege
        /// </summary>
        Unknown,

        /// <summary>
        ///  Required to assign the primary access token for a process.
        ///  [SE_ASSIGNPRIMARYTOKEN_NAME]["SeAssignPrimaryTokenPrivilege"]
        /// </summary>
        AssignPrimaryToken,

        /// <summary>
        ///  Required to generate security audit-log entries.
        ///  [SE_AUDIT_NAME]["SeAuditPrivilege"]
        /// </summary>
        Audit,

        /// <summary>
        ///  Grants all read access control to all files for backup purposes.
        ///  [SE_BACKUP_NAME]["SeBackupPrivilege"]
        /// </summary>
        Backup,

        // The Bypass Traverse Checking (or is it the Change Notify?) Privilege
        // https://blogs.technet.microsoft.com/markrussinovich/2005/10/19/the-bypass-traverse-checking-or-is-it-the-change-notify-privilege/

        /// <summary>
        ///  Required to receive file and directory change notifications. Also skips
        ///  all traversal access checks. By default all users have this right.
        ///  [SE_CHANGE_NOTIFY_NAME]["SeChangeNotifyPrivilege"]
        /// </summary>
        ChangeNotify,

        /// <summary>
        ///  Required to create named file mapping objects in the global namespace during Terminal Services sessions.
        ///  [SE_CREATE_GLOBAL_NAME]["SeCreateGlobalPrivilege"]
        /// </summary>
        CreateGlobal,

        /// <summary>
        ///  Required to create a paging file.
        ///  [SE_CREATE_PAGEFILE_NAME]["SeCreatePagefilePrivilege"]
        /// </summary>
        CreatePagefile,

        /// <summary>
        ///  Required to create a permanent object.
        ///  [SE_CREATE_PERMANENT_NAME]["SeCreatePermanentPrivilege"]
        /// </summary>
        CreatePermanent,

        /// <summary>
        ///  Required to create a symbolic link. Note that this privilege was restricted to
        ///  Administrators by default until Windows 10 Fall Creators Update.
        ///  [SE_CREATE_SYMBOLIC_LINK_NAME]["SeCreateSymbolicLinkPrivilege"]
        /// </summary>
        CreateSymbolicLink,

        /// <summary>
        ///  Required to create a primary token.
        ///  [SE_CREATE_TOKEN_NAME]["SeCreateTokenPrivilege"]
        /// </summary>
        CreateToken,

        /// <summary>
        ///  Required to debug and adjust the memory of a process owned by another account.
        ///  [SE_DEBUG_NAME]["SeDebugPrivilege"]
        /// </summary>
        Debug,

        /// <summary>
        ///  Required to mark user and computer accounts as trusted for delegation.
        ///  [SE_ENABLE_DELEGATION_NAME]["SeEnableDelegationPrivilege"]
        /// </summary>
        EnableDelegation,

        /// <summary>
        ///  Required to impersonate.
        ///  [SE_IMPERSONATE_NAME]["SeImpersonatePrivilege"]
        /// </summary>
        Impersonate,

        /// <summary>
        ///  Required to increase the base priority of a process.
        ///  [SE_INC_BASE_PRIORITY_NAME]["SeIncreaseBasePriorityPrivilege"]
        /// </summary>
        IncreaseBasePriority,

        /// <summary>
        ///  Required to increase the quota assigned to a process.
        ///  [SE_INCREASE_QUOTA_NAME]["SeIncreaseQuotaPrivilege"]
        /// </summary>
        IncreaseQuota,

        /// <summary>
        ///  Required to allocate more memory for applications that run in the context of users.
        ///  [SE_INC_WORKING_SET_NAME]["SeIncreaseWorkingSetPrivilege"]
        /// </summary>
        IncreaseWorkingSet,

        /// <summary>
        ///  Required to load or unload a device driver.
        ///  [SE_LOAD_DRIVER_NAME]["SeLoadDriverPrivilege"]
        /// </summary>
        LoadDriver,

        /// <summary>
        ///  Required to lock physical pages in memory.
        ///  [SE_LOCK_MEMORY_NAME]["SeLockMemoryPrivilege"]
        /// </summary>
        LockMemory,

        /// <summary>
        ///  Required to create a computer account.
        ///  [SE_MACHINE_ACCOUNT_NAME]["SeMachineAccountPrivilege"]
        /// </summary>
        MachineAccount,

        /// <summary>
        ///  Required to enable volume management privileges.
        ///  [SE_MANAGE_VOLUME_NAME]["SeManageVolumePrivilege"]
        /// </summary>
        ManageVolume,

        /// <summary>
        ///  Required to gather profiling information for a single process.
        ///  [SE_PROF_SINGLE_PROCESS_NAME]["SeProfileSingleProcessPrivilege"]
        /// </summary>
        ProfileSingleProcess,

        /// <summary>
        ///  Required to modify the mandatory integrity level of an object.
        ///  [SE_RELABEL_NAME]["SeRelabelPrivilege"]
        /// </summary>
        Relabel,

        /// <summary>
        ///  Required to shut down a system using a network request.
        ///  [SE_REMOTE_SHUTDOWN_NAME]["SeRemoteShutdownPrivilege"]
        /// </summary>
        RemoteShutdown,

        /// <summary>
        ///  Required to perform restore operations. This privilege causes the system to grant all write access control to any file.
        ///  [SE_RESTORE_NAME]["SeRestorePrivilege"]
        /// </summary>
        Restore,

        /// <summary>
        ///  Required to perform a number of security-related functions, such as controlling and viewing audit messages.
        ///  This privilege identifies its holder as a security operator.
        ///  [SE_SECURITY_NAME]["SeSecurityPrivilege"]
        /// </summary>
        Security,

        /// <summary>
        ///  Required to shut down a local system.
        ///  [SE_SHUTDOWN_NAME]["SeShutdownPrivilege"]
        /// </summary>
        Shutdown,

        /// <summary>
        ///  Required for a domain controller to use the LDAP directory synchronization services.
        ///  [SE_SYNC_AGENT_NAME]["SeSyncAgentPrivilege"]
        /// </summary>
        SyncAgent,

        /// <summary>
        ///  Required to modify the nonvolatile RAM of systems that use this type of memory to store configuration information.
        ///  [SE_SYSTEM_ENVIRONMENT_NAME]["SeSystemEnvironmentPrivilege"]
        /// </summary>
        SystemEnvironment,

        /// <summary>
        ///  Required to gather profiling information for the entire system.
        ///  [SE_SYSTEM_PROFILE_NAME]["SeSystemProfilePrivilege"]
        /// </summary>
        SystemProfile,

        /// <summary>
        ///  Required to modify the system time.
        ///  [SE_SYSTEMTIME_NAME]["SeSystemtimePrivilege"]
        /// </summary>
        SystemTime,

        /// <summary>
        ///  Required to take ownership of an object without being granted discretionary access.
        ///  [SE_TAKE_OWNERSHIP_NAME]["SeTakeOwnershipPrivilege"]
        /// </summary>
        TakeOwnership,

        /// <summary>
        ///  This privilege identifies its holder as part of the trusted computer base.
        ///  [SE_TCB_NAME]["SeTcbPrivilege"]
        /// </summary>
        TrustedComputerBase,

        /// <summary>
        ///  Required to adjust the time zone associated with the computer's internal clock.
        ///  [SE_TIME_ZONE_NAME]["SeTimeZonePrivilege"]
        /// </summary>
        TimeZone,

        /// <summary>
        ///  Required to access Credential Manager as a trusted caller.
        ///  [SE_TRUSTED_CREDMAN_ACCESS_NAME]["SeTrustedCredManAccessPrivilege"]
        /// </summary>
        TrustedCredentialManagerAccess,

        /// <summary>
        ///  Required to undock a laptop.
        ///  [SE_UNDOCK_NAME]["SeUndockPrivilege"]
        /// </summary>
        Undock,

        /// <summary>
        ///  Required to read unsolicited input from a terminal device.
        ///  [SE_UNSOLICITED_INPUT_NAME]["SeUnsolicitedInputPrivilege"]
        /// </summary>
        UnsolicitedInput
    }
}
