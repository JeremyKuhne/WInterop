// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.SystemInformation.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class SystemInformationImports
{
    // https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-getusernamew
    [DllImport(Libraries.Advapi32, SetLastError = true, ExactSpelling = true)]
    public static extern bool GetUserNameW(
        SafeHandle lpBuffer,
        ref uint lpnSize);

    // https://docs.microsoft.com/windows/win32/api/secext/nf-secext-getusernameexw
    [DllImport(Libraries.Secur32, SetLastError = true, ExactSpelling = true)]
    public static extern ByteBoolean GetUserNameExW(
        ExtendedNameFormat NameFormat,
        SafeHandle lpNameBuffer,
        ref uint lpnSize);

    // https://docs.microsoft.com/windows/win32/api/sysinfoapi/nf-sysinfoapi-getcomputernameexw
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool GetComputerNameExW(
        ComputerNameFormat NameType,
        SafeHandle lpBuffer,
        ref uint lpnSize);

    // https://docs.microsoft.com/windows/win32/sysinfo/rtlgetsuitemask
    [DllImport(Libraries.Ntdll, ExactSpelling = true)]
    public static extern SuiteMask RtlGetSuiteMask();

    // https://docs.microsoft.com/windows/win32/api/winternl/nf-winternl-ntquerysysteminformation
    [DllImport(Libraries.Ntdll, ExactSpelling = true)]
    public static extern int NtQuerySystemInformation(
        SystemInformationClass SystemInformationClass,
        IntPtr SystemInformation,
        uint SystemInformationLength,
        out uint ReturnLength);

    // https://docs.microsoft.com/windows/win32/api/processthreadsapi/nf-processthreadsapi-isprocessorfeaturepresent
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern bool IsProcessorFeaturePresent(
        ProcessorFeature ProcessorFeature);

    // https://docs.microsoft.com/windows/win32/api/windowsceip/nf-windowsceip-ceipisoptedin
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern bool CeipIsOptedIn();

    // https://docs.microsoft.com/windows/win32/api/profileapi/nf-profileapi-queryperformancefrequency
    // This never returns false on XP or later
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern bool QueryPerformanceFrequency(
        out long lpFrequency);

    // https://docs.microsoft.com/windows/win32/api/profileapi/nf-profileapi-queryperformancecounter
    // This never returns false on XP or later
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern bool QueryPerformanceCounter(
        out long lpPerformanceCount);

    // https://docs.microsoft.com/windows/win32/api/sysinfoapi/nf-sysinfoapi-getlocaltime
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern void GetLocalTime(
        out SystemTime lpSystemTime);

    // https://docs.microsoft.com/windows/win32/api/winbase/nf-winbase-getcomputernamew
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool GetComputerNameW(
        SafeHandle lpBuffer,
        ref uint lpnSize);

    // https://docs.microsoft.com/windows/win32/api/processenv/nf-processenv-expandenvironmentstringsw
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern uint ExpandEnvironmentStringsW(
        string lpSrc,
        SafeHandle lpDst,
        uint nSize);

    // https://docs.microsoft.com/windows-hardware/drivers/ddi/content/wdm/nf-wdm-rtlverifyversioninfo
    [DllImport(Libraries.Ntdll, ExactSpelling = true)]
    public static extern NTStatus RtlVerifyVersionInfo(
        ref OsVersionInfo VersionInfo,
        VersionTypeMask TypeMask,
        ulong ConditionMask);

    // https://docs.microsoft.com/windows-hardware/drivers/ddi/content/wdm/nf-wdm-rtlverifyversioninfo
    [DllImport(Libraries.Ntdll, ExactSpelling = true)]
    public static extern NTStatus RtlGetVersion(
        ref OsVersionInfo VersionInfo);

    // https://docs.microsoft.com/windows/win32/perfctrs/performance-counters-portal

    // https://docs.microsoft.com/windows/win32/api/perflib/nf-perflib-perfopenqueryhandle
    [DllImport(Libraries.Advapi32, ExactSpelling = true, CharSet = CharSet.Unicode)]
    public static extern WindowsError PerfOpenQueryHandle(
        string szMachine,
        out IntPtr phQuery);

    // https://docs.microsoft.com/windows/win32/api/perflib/nf-perflib-perfclosequeryhandle
    [DllImport(Libraries.Advapi32, ExactSpelling = true)]
    public static extern WindowsError PerfCloseQueryHandle(
        IntPtr hQuery);

    // https://docs.microsoft.com/windows/win32/api/perflib/nf-perflib-perfaddcounters
    [DllImport(Libraries.Advapi32, ExactSpelling = true)]
    public static extern unsafe WindowsError PerfAddCounters(
        IntPtr hQuery,
        PERF_COUNTER_IDENTIFIER* pCounters,
        uint cbCounters);

    // https://docs.microsoft.com/windows/win32/api/perflib/nf-perflib-perfdeletecounters
    [DllImport(Libraries.Advapi32, ExactSpelling = true)]
    public static extern unsafe WindowsError PerfDeleteCounters(
        IntPtr hQuery,
        PERF_COUNTER_IDENTIFIER* pCounters,
        uint cbCounters);

    // https://docs.microsoft.com/windows/win32/api/perflib/nf-perflib-perfquerycounterinfo
    [DllImport(Libraries.Advapi32, ExactSpelling = true)]
    public static extern unsafe WindowsError PerfQueryCounterInfo(
        IntPtr hQuery,
        PERF_COUNTER_IDENTIFIER* pCounters,
        uint cbCounters,
        out uint pcbCountersActual);

    // https://docs.microsoft.com/windows/win32/api/perflib/nf-perflib-perfquerycounterdata
    [DllImport(Libraries.Advapi32, ExactSpelling = true)]
    public static extern unsafe WindowsError PerfQueryCounterData(
        IntPtr hQuery,
        PERF_DATA_HEADER* pCounterBlock,
        uint cbCounterBlock,
        out uint pcbCounterBlockActual);

    // https://docs.microsoft.com/windows/win32/api/perflib/nf-perflib-perfenumeratecounterset
    [DllImport(Libraries.Advapi32, ExactSpelling = true, CharSet = CharSet.Unicode)]
    public static extern unsafe WindowsError PerfEnumerateCounterSet(
        string szMachine,
        Guid* pCounterSetIds,
        uint cCounterSetIds,
        out uint pcCounterSetIdsActual);

    // https://docs.microsoft.com/windows/win32/api/perflib/nf-perflib-perfenumeratecountersetinstances
    [DllImport(Libraries.Advapi32, ExactSpelling = true, CharSet = CharSet.Unicode)]
    public static extern unsafe WindowsError PerfEnumerateCounterSetInstances(
        string szMachine,
        ref Guid pCounterSetId,
        PERF_INSTANCE_HEADER* pInstances,
        uint cbInstances,
        out uint pcbInstancesActual);

    // https://docs.microsoft.com/windows/win32/api/perflib/nf-perflib-perfenumeratecountersetinstances
    [DllImport(Libraries.Advapi32, ExactSpelling = true, CharSet = CharSet.Unicode)]
    public static extern unsafe WindowsError PerfQueryCounterSetRegistrationInfo(
        string szMachine,
        ref Guid pCounterSetId,
        PerfRegInfoType requestCode,
        uint requestLangId,
        byte* pbRegInfo,
        uint cbRegInfo,
        out uint pcbRegInfoActual);
}