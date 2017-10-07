// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using WInterop.Authorization.Types;
using WInterop.ErrorHandling.Types;
using WInterop.FileManagement.Types;
using WInterop.Handles.Types;
using WInterop.SafeString.Types;
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
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern SafeFileHandle CreateFileW(
                string lpFileName,
                DesiredAccess dwDesiredAccess,
                ShareModes dwShareMode,
                SECURITY_ATTRIBUTES* lpSecurityAttributes,
                CreationDisposition dwCreationDisposition,
                uint dwFlagsAndAttributes,
                IntPtr hTemplateFile);

            // https://https://msdn.microsoft.com/en-us/library/windows/desktop/aa365497.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr ReOpenFile(
                SafeFileHandle hOriginalFile,
                DesiredAccess dwDesiredAccess,
                ShareModes dwShareMode,
                uint dwFlags);

            // Ex version is supported by WinRT apps
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364944.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern FileAttributes GetFileAttributesW(
                string lpFileName);

            // Ex version is supported by WinRT apps
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364952.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetFileInformationByHandle(
                SafeFileHandle hFile,
                out BY_HANDLE_FILE_INFORMATION lpFileInformation);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364980.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetLongPathNameW(
                string lpszShortPath,
                SafeHandle lpszLongPath,
                uint cchBuffer);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364989.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetShortPathNameW(
                string lpszLongPath,
                SafeHandle lpszShortPath,
                uint cchBuffer);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364962.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetFinalPathNameByHandleW(
                SafeFileHandle hFile,
                SafeHandle lpszFilePath,
                uint cchFilePath,
                GetFinalPathNameByHandleFlags dwFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363852.aspx
            // CopyFile calls CopyFileEx with COPY_FILE_FAIL_IF_EXISTS if fail if exists is set
            // (Neither are available in WinRT- use CopyFile2)
            [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CopyFileExW(
                string lpExistingFileName,
                string lpNewFileName,
                CopyProgressRoutine lpProgressRoutine,
                IntPtr lpData,
                [MarshalAs(UnmanagedType.Bool)] ref bool pbCancel,
                CopyFileFlags dwCopyFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363866.aspx
            // Note that CreateSymbolicLinkW returns a BOOLEAN (byte), not a BOOL (int)
            [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.U1)]
            public static extern bool CreateSymbolicLinkW(
                string lpSymlinkFileName,
                string lpTargetFileName,
                SYMBOLIC_LINK_FLAG dwFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364021.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool EncryptFileW(
                string lpFileName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363903.aspx
            [DllImport(Libraries.Advapi32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DecryptFileW(
                string lpFileName,
                uint dwReserved);

            // Adding Users to an Encrypted File
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363765.aspx
            //
            // 1. LookupAccountName() to get SID
            // 2. CertOpenSystemStore((HCRYPTPROV)NULL,L"TrustedPeople") to get cert store
            // 3. CertFindCertificateInStore() to find the desired cert (PCCERT_CONTEXT)
            //
            //   EFS_CERTIFICATE.cbTotalLength = Marshal.Sizeof(EFS_CERTIFICATE)
            //   EFS_CERTIFICATE.pUserSid = &SID
            //   EFS_CERTIFICATE.pCertBlob.dwCertEncodingType = CCERT_CONTEXT.dwCertEncodingType
            //   EFS_CERTIFICATE.pCertBlob.cbData = CCERT_CONTEXT.cbCertEncoded
            //   EFS_CERTIFICATE.pCertBlob.pbData = CCERT_CONTEXT.pbCertEncoded

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363770.aspx
            //
            //  DWORD WINAPI AddUsersToEncryptedFile(
            //      _In_ LPCWSTR lpFileName,
            //      _In_ PENCRYPTION_CERTIFICATE_LIST pUsers
            //  );
            [DllImport(Libraries.Advapi32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern uint AddUsersToEncryptedFile(
                string lpFileName,

                /// <summary>
                /// Pointer to ENCRYPTION_CERTIFICATE_LIST array
                /// </summary>
                IntPtr pUsers);

            // https://msdn.microsoft.com/en-us/library/bb432380.aspx
            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff566424.aspx
            [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern NTSTATUS NtCreateFile(
                out IntPtr FileHandle,
                DesiredAccess DesiredAccess,
                ref OBJECT_ATTRIBUTES ObjectAttributes,
                out IO_STATUS_BLOCK IoStatusBlock,
                long* AllocationSize,
                FileAttributes FileAttributes,
                ShareModes ShareAccess,
                CreateDisposition CreateDisposition,
                CreateOptions CreateOptions,
                void* EaBuffer,
                uint EaLength);

            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff567052.aspx
            // http://www.pinvoke.net/default.aspx/ntdll/NtQueryInformationFile.html
            [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern NTSTATUS NtQueryInformationFile(
                SafeFileHandle FileHandle,
                out IO_STATUS_BLOCK IoStatusBlock,
                void* FileInformation,
                uint Length,
                FileInformationClass FileInformationClass);

            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff567096.aspx
            [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern NTSTATUS NtSetInformationFile(
                SafeFileHandle FileHandle,
                out IO_STATUS_BLOCK IoStatusBlock,
                void* FileInformation,
                FileInformationClass FileInformationClass);

            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff556633.aspx
            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff567047.aspx
            [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern NTSTATUS NtQueryDirectoryFile(
                IntPtr FileHandle,
                IntPtr Event,
                AsyncProcedureCall ApcRoutine,
                IntPtr ApcContext,
                out IO_STATUS_BLOCK IoStatusBlock,
                void* FileInformation,
                uint Length,
                FileInformationClass FileInformationClass,
                BOOLEAN ReturnSingleEntry,
                UNICODE_STRING* FileName,
                BOOLEAN RestartScan);

            [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern NTSTATUS NtQueryDirectoryFile(
                SafeFileHandle FileHandle,
                IntPtr Event,
                AsyncProcedureCall ApcRoutine,
                IntPtr ApcContext,
                out IO_STATUS_BLOCK IoStatusBlock,
                void* FileInformation,
                uint Length,
                FileInformationClass FileInformationClass,
                BOOLEAN ReturnSingleEntry,
                UNICODE_STRING* FileName,
                BOOLEAN RestartScan);

            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff546850.aspx
            // https://msdn.microsoft.com/en-us/library/hh551132.aspx
            [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern BOOLEAN RtlIsNameInExpression(
                UNICODE_STRING* Expression,
                UNICODE_STRING* Name,
                BOOLEAN IgnoreCase,
                IntPtr UpcaseTable);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365465.aspx
            [DllImport(Libraries.Kernel32, ExactSpelling = true)]
            public unsafe static extern bool ReadDirectoryChangesW(
                SafeFileHandle hDirectory,
                void* lpBuffer,
                uint nBufferLength,
                bool bWatchSubtree,
                FileNotifyChange dwNotifyFilter,
                out uint lpBytesReturned,
                ref OVERLAPPED lpOverlapped,
                FileIOCompletionRoutine lpCompletionRoutine);
        }
    }
}
