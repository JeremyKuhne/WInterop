// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;
    using System.Security;
    using Handles;
    using Buffers;
    using ErrorHandling;

    public static partial class NativeMethods
    {
        // Design Guidelines and Notes
        // ===========================
        //
        // Keep P/Invokes private and provide internal wrappers that do appropriate preparation and error handling.
        //
        // In/Out attributes implicitly applied for parameter & return values:
        //
        //      None Specified -> [In]
        //      out            -> [Out]
        //      ref            -> [In],[Out]
        //      return value   -> [Out]
        //
        // [PreserveSig(false)]
        //
        //  When this is explicitly set to false (the default is true), failed HRESULT return values will be turned into Exceptions
        //  (and the return value in the definition becomes null as a result)
        //
        // [DllImport(SetLastError=true)]
        //
        //  Set this if the API uses GetLastError and use Marshal.GetLastWin32Error to get the value. If the API sets a condition
        //  that says it has an error, get the error before making other calls to avoid inadvertently having it overwritten.
        //
        // [DllImport(ExactSpelling=true)]
        //
        //  Set this and the framework will avoid looking for an "A"/"W" version. (See NDirectMethodDesc::FindEntryPoint)
        //
        // Strings:
        // --------
        //
        // "Default Marshalling for Strings"     https://msdn.microsoft.com/en-us/library/s9ts558h.aspx
        // "Windows Data Types for Strings"      http://msdn.microsoft.com/en-us/library/dd374131.aspx
        //
        // Strings are marshalled as LPTSTR by default, which means it will match the CharSet property in the DllImport attribute.
        // The CharSet is, by default, ANSI, which isn't appropriate for anything post Windows 9x (which isn't supported by .NET
        // anymore). As such, the mapping is actually as follows:
        //
        //      CharSet.None    -> Ansi
        //      CharSet.Ansi    -> Ansi
        //      CharSet.Unicode -> Unicode
        //      CharSet.Auto    -> Unicode
        //
        // When the CharSet is Unicode or the argument is explicitly marked as [MarshalAs(UnmanagedType.LPWSTR)], and the string is
        // is passed by value (not ref/out) the string can be pinned and used directly by managed code (rather than copied).
        //
        // The CLR will use CoTaskMemFree by default to free strings that are passed as [Out] or SysStringFree for strings that are marked
        // as BSTR.
        //
        // (StringBuilder - ILWSTRBufferMarshaler) [AVOID]
        // StringBuilder marshalling *always* creates a native buffer copy. As such it can be extremely inefficient. Take the typical
        // scenario of calling a Windows API that takes a string:
        //
        //      1. Create a SB of the desired capacity (allocates managed capacity) {1}
        //      2. Invoke
        //          2a. Allocates a native buffer {2}
        //          2b. Copies the contents if [In] (default)
        //          2c. Copies the native buffer into a newly allocated managed array if [Out] (default) {3}
        //      3. ToString allocates yet another managed array {4}
        //
        // That is {4} allocations to get a string out of native code. The best you can do to limit this is to reuse the StringBuilder
        // in another call, but this still only saves *1* allocation. It is much better to use and cache a native buffer- you can then
        // get down to just the allocation for the ToString() on subsequent calls. So {4 -> 3} versus {2 -> 1} allocations.
        //
        // By default it is passed as [In, Out]. If explicitly specified as out it will not copy the contents to the native buffer before
        // calling the native method.
        //
        // StringBuilder is guaranteed to have a null that is not counted in the capacity. As such the count of characters when using as a
        // character buffer is Capacity + 1.
        //
        // Booleans:
        // ---------
        //
        // "Default Marshalling for Boolean Types"  https://msdn.microsoft.com/en-us/library/t2t3725f.aspx
        //
        // Booleans are easy to mess up. The default marshalling for P/Invoke is as the Windows type BOOL, where it is a 4 byte value.
        // BOOLEAN, however, is a single byte. You need to use [MarshalAs(UnmanagedType.U1)] or [MarshalAs(UnmanagedType.I1)] either
        // should work as TRUE is defined as 1 and FALSE is defined as 0. U1 is technically more correct as it is defined as an
        // unsigned char.
        //
        // For COM (VARIANT_BOOL) the type is 2 bytes where true is -1 and false is 0. Marshalling uses this by default for bool in
        // COM calls (UnmanagedType.VariantBool).

        // For most APIs with an output buffer:
        //
        // The passed in character count must include the null. If the returned value is less than the passed in character count the call
        // has succeeded and the value is the number of characters *without* the trailing null. Otherwise the count is the required size
        // of the buffer *including* the null character.
        //
        // Pass in 5, get 4-> The string is 4 characters long with a trailing null.
        // Pass in 5, get 6-> The string is 5 characters long, need a 6 character buffer to hold the null.

        // Useful Interop Links
        // ====================
        //
        // "MarshalAs Attribute"                 http://msdn.microsoft.com/en-us/library/system.runtime.interopservices.marshalasattribute.aspx
        // "GetLastError and managed code"       http://blogs.msdn.com/b/adam_nathan/archive/2003/04/25/56643.aspx
        // "Copying and Pinning"                 https://msdn.microsoft.com/en-us/library/23acw07k.aspx
        // "Marshalling between Managed and Unmanaged Code" (MSDN Magazine January 2008)
        //
        // PInvoke code is in dllimport, method, and ilmarshalers in coreclr\src\vm.

        // Mapping for common Windows data types
        //
        // "Windows Data Types"                  http://msdn.microsoft.com/en-us/library/aa383751.aspx
        // "Data Type Ranges"                    http://msdn.microsoft.com/en-us/library/s3f49ktz.aspx
        //
        //  Windows         C               C#          Alt
        //  -------         -               --          ---
        //  BOOL            int             int         bool
        //  BOOLEAN         unsigned char   byte        [MarshalAs(UnmanagedType.U1)] bool
        //  BYTE            unsigned char   byte
        //  CHAR            char            sbyte
        //  DWORD           unsigned long   uint
        //  HANDLE          void*           IntPtr
        //  LARGE_INTEGER   __int64         long
        //  LONGLONG        __int64         long
        //  UCHAR           unsigned char   byte

        public static class Libraries
        {
            public const string Kernel32 = "kernel32.dll";
            public const string Advapi32 = "advapi32.dll";
            public const string User32 = "user32.dll";
            public const string Ntdll = "ntdll.dll";
            public const string Crypt32 = "crypt32.dll";
            public const string Netapi32 = "netapi32.dll";
        }

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
        }

        /// <summary>
        /// Uses the stringbuilder cache and increases the buffer size if needed.
        /// </summary>
        [SuppressMessage("Microsoft.Interoperability", "CA1404:CallGetLastErrorImmediatelyAfterPInvoke")]
        public static string BufferInvoke(Func<StringBuffer, uint> invoker, string value = null, Func<uint, bool> shouldThrow = null)
        {
            return StringBufferCache.CachedBufferInvoke(minCapacity: 260u, func: (buffer) =>
            {
                uint returnValue = 0;

                while ((returnValue = invoker(buffer)) > buffer.CharCapacity)
                {
                    // Need more room for the output string
                    buffer.EnsureCharCapacity(returnValue);
                }

                if (returnValue == 0)
                {
                    // Failed
                    uint error = (uint)Marshal.GetLastWin32Error();

                    if (shouldThrow != null && !shouldThrow(error))
                    {
                        return null;
                    }
                    throw ErrorHelper.GetIoExceptionForError(error, value);
                }

                buffer.Length = returnValue;
                return buffer.ToString();
            });
        }
    }
}
