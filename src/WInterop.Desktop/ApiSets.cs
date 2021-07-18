// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

#pragma warning disable SA1303 // Const field names should begin with upper-case letter
#pragma warning disable SA1310 // Field names should not contain underscore
#pragma warning disable IDE1006 // Naming Styles

namespace WInterop
{
    /// <summary>
    ///  Using API library definitions over legacy library names is encouraged for future proofing
    ///  and a supposed slight perf benefit in looking up apis.
    /// </summary>
    public static class ApiSets
    {
        // Windows 7 and Windows Server 2008 R2 Kernel Changes
        // https://channel9.msdn.com/events/pdc/pdc09/p09-20

        // Windows API Sets
        // https://msdn.microsoft.com/en-us/library/windows/desktop/hh802935.aspx

        // A number of these were dropped on Win7 through a variety of updates.

        // COM API sets not available on Win7
        // Windows 8.0

        public const string api_ms_win_core_com_l1_1_0 = "api-ms-win-core-com-l1-1-0.dll";

        // Windows 8.1
        public const string api_ms_win_core_com_l1_1_1 = "api-ms-win-core-com-l1-1-1.dll";

        // Windows 10.0?
        // CreateILockBytesOnHGlobal, FmtIdToPropStgName, GetConvertStg, GetHGlobalFromILockBytes, PropStgNameToFmtId, ReadClassStg, ReadClassStm,
        // StgCreateDocfile, StgCreateDocfileOnILockBytes, StgCreatePropSetStg, StgCreatePropStg, StgCreateStorageEx, StgIsStorageFile, StgIsStorageILockBytes,
        // StgOpenPropStg, StgOpenStorage, StgOpenStorageEx, StgOpenStorageOnILockBytes, StgSetTimes, WriteClassStg, WriteClassStm
        public const string api_ms_win_core_com_l2_1_1 = "api-ms-win-core-com-l2-1-1.dll";

        // Not on Win7?
        public const string api_ms_win_core_comm_l1_1_0 = "api-ms-win-core-comm-l1-1-0.dll";

        // V1.0 is available on Win7
        public const string api_ms_win_core_console_l1_1_0 = "api-ms-win-core-console-l1-1-0.dll";

        // Not on Win7?
        public const string api_ms_win_core_console_l2_1_0 = "api-ms-win-core-console-l2-1-0.dll";

        // V1.0 is available on Win7
        public const string api_ms_win_core_datetime_l1_1_0 = "api-ms-win-core-datetime-l1-1-0.dll";

        // Not on Win7? Adds GetDateFormatEx and GetTimeFormatEx
        public const string api_ms_win_core_datetime_l1_1_1 = "api-ms-win-core-datetime-l1-1-1.dll";

        // V1.0 is avalable on Win7
        public const string api_ms_win_core_debug_l1_1_0 = "api-ms-win-core-debug-l1-1-0.dll";

        // Not on Win7? Adds CheckRemoteDebuggerPresent, ContinueDebugEvent, DebugActiveProcess, DebugActiveProcessStop, WaitForDebugEvent
        public const string api_ms_win_core_debug_l1_1_1 = "api-ms-win-core-debug-l1-1-1.dll";

        // V1.0 is avalable on Win7
        public const string api_ms_win_core_errorhandling_l1_1_0 = "api-ms-win-core-errorhandling-l1-1-0.dll";

        // Not on Win7? Adds AddVectoredContinueHandler, AddVectoredExceptionHandler, RemoveVectoredContinueHandler, RemoveVectoredExceptionHandler and RestoreLastError
        public const string api_ms_win_core_errorhandling_l1_1_1 = "api-ms-win-core-errorhandling-l1-1-1.dll";

        // V1.0 is available on Win7
        public const string api_ms_win_core_fibers_l1_1_0 = "api-ms-win-core-fibers-l1-1-0.dll";

        // Not on Win7? Adds IsThreadAFiber
        public const string api_ms_win_core_fibers_l1_1_1 = "api-ms-win-core-fibers-l1-1-1.dll";

        // V1.0 is available on Win7
        public const string api_ms_win_core_file_l1_1_0 = "api-ms-win-core-file-l1-1-0.dll";

        // V2.0 is available on Win7 adds CreateFile2, GetTempPathW, GetVolumeNameForVolumeMountPointW, and GetVolumePathNamesForVolumeNameW
        public const string api_ms_win_core_file_l1_2_0 = "api-ms-win-core-file-l1-2-0.dll";

        // Not on Win7? V2.1 adds GetCompressedFileSizeA, GetCompressedFileSizeW, and SetFileIoOverlappedRange
        public const string api_ms_win_core_file_l1_2_1 = "api-ms-win-core-file-l1-2-1.dll";

        // V1.0 is available on Win7
        public const string api_ms_win_core_file_l2_1_0 = "api-ms-win-core-file-l2-1-0.dll";

        // Windows 8.1 V1.1 adds OpenFileById
        public const string api_ms_win_core_file_l2_1_1 = "api-ms-win-core-file-l2-1-1.dll";

        // Not on Win7?
        public const string api_ms_win_core_firmware_l1_1_0 = "api-ms-win-core-firmware-l1-1-0.dll";

        // V1.0 is available on Win7
        public const string api_ms_win_core_handle_l1_1_0 = "api-ms-win-core-handle-l1-1-0.dll";

        // Not on Win7? GlobalAlloc, GlobalFlags, GlobalFree, GlobalHandle, GlobalLock, GlobalReAlloc, GlobalSize, GlobalUnlock,
        // LocalAlloc, LocalFlags, LocalFree, LocalLock, LocalReAlloc, LocalSize, LocalUnlock
        public const string api_ms_win_core_heap_obsolete_l1_1_0 = "api-ms-win-core-heap-obsolete-l1-1-0.dll";

        // V1.0 is available on Win7
        public const string api_ms_win_core_heap_l1_1_0 = "api-ms-win-core-heap-l1-1-0.dll";

        // Same contents as V1.0, not sure why version is different
        public const string api_ms_win_core_heap_l1_2_0 = "api-ms-win-core-heap-l1-2-0.dll";

        public const string api_ms_win_core_interlocked_l1_2_0 = "api-ms-win-core-interlocked-l1-2-0.dll";
        public const string api_ms_win_core_io_l1_1_1 = "api-ms-win-core-io-l1-1-1.dll";
        public const string api_ms_win_core_job_l1_1_0 = "api-ms-win-core-job-l1-1-0.dll";
        public const string api_ms_win_core_libraryloader_l1_1_1 = "api-ms-win-core-libraryloader-l1-1-1.dll";
        public const string api_ms_win_core_localization_l1_2_0 = "api-ms-win-core-localization-l1-2-0.dll";
        public const string api_ms_win_core_memory_l1_1_1 = "api-ms-win-core-memory-l1-1-1.dll";
        public const string api_ms_win_core_namedpipe_l1_2_0 = "api-ms-win-core-namedpipe-l1-2-0.dll";
        public const string api_ms_win_core_namespace_l1_1_0 = "api-ms-win-core-namespace-l1-1-0.dll";
        public const string api_ms_win_core_path_l1_1_0 = "api-ms-win-core-path-l1-1-0.dll";
        public const string api_ms_win_core_processenvironment_l1_2_0 = "api-ms-win-core-processenvironment-l1-2-0.dll";
        public const string api_ms_win_core_processthreads_l1_1_1 = "api-ms-win-core-processthreads-l1-1-1.dll";
        public const string api_ms_win_core_processtopology_l1_1_0 = "api-ms-win-core-processtopology-l1-1-0.dll";
        public const string api_ms_win_core_profile_l1_1_0 = "api-ms-win-core-profile-l1-1-0.dll";
        public const string api_ms_win_core_psapi_l1_1_0 = "api-ms-win-core-psapi-l1-1-0.dll";
        public const string api_ms_win_core_realtime_l1_1_0 = "api-ms-win-core-realtime-l1-1-0.dll";
        public const string api_ms_win_core_registry_l1_1_0 = "api-ms-win-core-registry-l1-1-0.dll";
        public const string api_ms_win_core_rtlsupport_l1_2_0 = "api-ms-win-core-rtlsupport-l1-2-0.dll";
        public const string api_ms_win_core_shutdown_l1_1_0 = "api-ms-win-core-shutdown-l1-1-0.dll";
        public const string api_ms_win_core_string_l1_1_0 = "api-ms-win-core-string-l1-1-0.dll";
        public const string api_ms_win_core_string_l2_1_0 = "api-ms-win-core-string-l2-1-0.dll";
        public const string api_ms_win_core_synch_l1_2_0 = "api-ms-win-core-synch-l1-2-0.dll";
        public const string api_ms_win_core_sysinfo_l1_2_0 = "api-ms-win-core-sysinfo-l1-2-0.dll";
        public const string api_ms_win_core_systemtopology_l1_1_0 = "api-ms-win-core-systemtopology-l1-1-0.dll";
        public const string api_ms_win_core_threadpool_l1_2_0 = "api-ms-win-core-threadpool-l1-2-0.dll";
        public const string api_ms_win_core_timezone_l1_1_0 = "api-ms-win-core-timezone-l1-1-0.dll";
        public const string api_ms_win_core_util_l1_1_0 = "api-ms-win-core-util-l1-1-0.dll";
        public const string api_ms_win_core_version_l1_1_0 = "api-ms-win-core-version-l1-1-0.dll";
        public const string api_ms_win_core_xstate_l1_1_1 = "api-ms-win-core-xstate-l1-1-1.dll";

        // Available on Windows 7
        public const string api_ms_win_security_lsalookup_l1_1_0 = "api-ms-win-security-lsalookup-l1-1-0.dll";
    }

#pragma warning restore SA1310 // Field names should not contain underscore
#pragma warning restore SA1303 // Const field names should begin with upper-case letter
#pragma warning restore IDE1006 // Naming Styles
}