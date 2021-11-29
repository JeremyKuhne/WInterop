// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.ProcessAndThreads;
using WInterop.SafeString.Native;

namespace WInterop.Handles.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class Imports
{
    // http://forum.sysinternals.com/howto-enumerate-handles_topic18892.html

    // https://msdn.microsoft.com/en-us/library/bb432383.aspx
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff567062.aspx
    [DllImport(Libraries.Ntdll, ExactSpelling = true)]
    public static extern NTStatus NtQueryObject(
        SafeHandle Handle,
        ObjectInformationClass ObjectInformationClass,
        IntPtr ObjectInformation,
        uint ObjectInformationLength,
        out uint ReturnLength);

    // https://msdn.microsoft.com/en-us/library/bb470234.aspx
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff566492.aspx
    [DllImport(Libraries.Ntdll, ExactSpelling = true)]
    public static extern NTStatus NtOpenDirectoryObject(
        out DirectoryObjectHandle DirectoryHandle,
        DirectoryObjectRights DesiredAccess,
        ref OBJECT_ATTRIBUTES ObjectAttributes);

    // https://msdn.microsoft.com/en-us/library/bb470236.aspx
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff567030.aspx
    [DllImport(Libraries.Ntdll, ExactSpelling = true)]
    public static extern unsafe NTStatus NtOpenSymbolicLinkObject(
        out SymbolicLinkObjectHandle LinkHandle,
        SymbolicLinkObjectRights DesiredAccess,
        ref OBJECT_ATTRIBUTES ObjectAttributes);

    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff567068.aspx
    // https://msdn.microsoft.com/en-us/library/bb470241.aspx
    [DllImport(Libraries.Ntdll, ExactSpelling = true)]
    public static extern NTStatus NtQuerySymbolicLinkObject(
        SymbolicLinkObjectHandle LinkHandle,
        ref UNICODE_STRING LinkTarget,
        out uint ReturnedLength);

    // https://msdn.microsoft.com/en-us/library/bb470238.aspx
    [DllImport(Libraries.Ntdll, ExactSpelling = true)]
    public static extern NTStatus NtQueryDirectoryObject(
        DirectoryObjectHandle DirectoryHandle,
        SafeHandle Buffer,
        uint Length,
        ByteBoolean ReturnSingleEntry,
        ByteBoolean RestartScan,
        ref uint Context,
        out uint ReturnLength);

    // typedef struct _OBJECT_TYPES_INFORMATION
    //  {
    //      ULONG NumberOfTypes;
    //      OBJECT_TYPE_INFORMATION TypeInformation;
    //  } OBJECT_TYPES_INFORMATION, *POBJECT_TYPES_INFORMATION;

    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724211.aspx
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool CloseHandle(
        IntPtr handle);

    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724251.aspx
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool DuplicateHandle(
        SafeProcessHandle hSourceProcessHandle,
        IntPtr hSourceHandle,
        SafeProcessHandle hTargetProcessHandle,
        SafeHandle lpTargetHandle,
        uint dwDesiredAccess,
        bool bInheritHandle,
        uint dwOptions);
}