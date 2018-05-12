// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling.Types;
using WInterop.FileManagement.Types;
using WInterop.Handles.Types;
using WInterop.Synchronization.Types;

namespace WInterop.FileManagement
{
    public static partial class FileMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // NTFS Technical Reference
            // https://technet.microsoft.com/en-us/library/cc758691.aspx

            // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449422.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_2_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern SafeFileHandle CreateFile2(
                string lpFileName,
                DesiredAccess dwDesiredAccess,
                ShareModes dwShareMode,
                CreationDisposition dwCreationDisposition,
                ref CREATEFILE2_EXTENDED_PARAMETERS pCreateExParams);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449404.aspx (kernel32)
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern HRESULT CopyFile2(
                string pwszExistingFileName,
                string pwszNewFileName,
                ref COPYFILE2_EXTENDED_PARAMETERS pExtendedParameters);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365512.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool ReplaceFileW(
                string lpReplacedFileName,
                string lpReplacementFileName,
                string lpBackupFileName,
                ReplaceFileFlags dwReplaceFlags,
                IntPtr lpExclude,
                IntPtr lpReserved);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364946.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetFileAttributesExW(
                string lpFileName,
                GET_FILEEX_INFO_LEVELS fInfoLevelId,
                out WIN32_FILE_ATTRIBUTE_DATA lpFileInformation);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365535.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetFileAttributesW(
                string lpFileName,
                FileAttributes dwFileAttributes);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364963.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetFullPathNameW(
                string lpFileName,
                uint nBufferLength,
                SafeHandle lpBuffer,
                IntPtr lpFilePart);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364418.aspx
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern IntPtr FindFirstFileW(
                string lpFileName,
                out WIN32_FIND_DATA lpFindFileData);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364419.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern IntPtr FindFirstFileExW(
                    string lpFileName,
                    FINDEX_INFO_LEVELS fInfoLevelId,
                    out WIN32_FIND_DATA lpFindFileData,
                    uint fSearchOp,                        // This never actually has meaning and is likely a holdover of 9x
                                                           // set it to 0 to avoid failing parameter checks.
                    IntPtr lpSearchFilter,                 // Reserved
                    FindFirstFileExFlags dwAdditionalFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364428.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool FindNextFileW(
                IntPtr hFindFile,
                out WIN32_FIND_DATA lpFindFileData);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364413.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, ExactSpelling = true)]
            public static extern bool FindClose(
                IntPtr hFindFile);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364953.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l2_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern bool GetFileInformationByHandleEx(
                IntPtr hFile,
                FileInfoClass FileInformationClass,
                void* lpFileInformation,
                uint dwBufferSize);

            public unsafe static bool GetFileInformationByHandleEx(
                SafeFileHandle hFile,
                FileInfoClass FileInformationClass,
                void* lpFileInformation,
                uint dwBufferSize)
            {
                using (var handle = new UnwrapHandle(hFile))
                {
                    return GetFileInformationByHandleEx(handle, FileInformationClass, lpFileInformation, dwBufferSize);
                }
            }

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363915.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool DeleteFileW(
                string lpFilename);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365467.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern bool ReadFile(
                SafeFileHandle hFile,
                byte* lpBuffer,
                uint nNumberOfBytesToRead,
                uint* lpNumberOfBytesRead,
                OVERLAPPED* lpOverlapped);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365747.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern bool WriteFile(
                SafeFileHandle hFile,
                byte* lpBuffer,
                uint nNumberOfBytesToWrite,
                uint* lpNumberOfBytesWritten,
                OVERLAPPED* lpOverlapped);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365542.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetFilePointerEx(
                SafeFileHandle hFile,
                long liDistanceToMove,
                out long lpNewFilePointer,
                MoveMethod dwMoveMethod);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364957.aspx
            // This returns FILE_STANDARD_INFO.EndOfFile
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetFileSizeEx(
                SafeFileHandle hFile,
                out long lpFileSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364960.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern FileType GetFileType(
                SafeFileHandle hFile);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364439.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, ExactSpelling = true)]
            public static extern bool FlushFileBuffers(
                SafeFileHandle hFile);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364992.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_2_0, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetTempPathW(
                uint nBufferLength,
                SafeHandle lpBuffer);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364991.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetTempFileNameW(
                string lpPathName,
                string lpPrefixString,
                uint uUnique,
                SafeHandle lpTempFileName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363792.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern bool CancelIoEx(
                SafeFileHandle hFile,
                OVERLAPPED* lpOverlapped);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/hh448542.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetOverlappedResultEx(
                SafeFileHandle hFile,
                ref OVERLAPPED lpOverlapped,
                out uint lpNumberOfBytesTransferred,
                uint dwMilliseconds,
                bool bAlertable);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365716.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool UnlockFileEx(
                SafeFileHandle hFile,
                uint dwReserved,
                uint nNumberOfBytesToUnlockLow,
                uint nNumberOfBytesToUnlockHigh,
                ref OVERLAPPED lpOverlapped);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365203.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool LockFileEx(
                SafeFileHandle hFile,
                uint dwFlags,
                uint dwReserved,
                uint nNumberOfBytesToUnlockLow,
                uint nNumberOfBytesToUnlockHigh,
                ref OVERLAPPED lpOverlapped);
        }
    }
}
