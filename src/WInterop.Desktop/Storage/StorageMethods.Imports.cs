// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Runtime.InteropServices;
using WInterop.Authorization;
using WInterop.ErrorHandling;
using WInterop.Storage.Types;
using WInterop.Handles.Types;
using WInterop.SafeString.Types;

namespace WInterop.Storage
{
    public static partial class StorageMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-createfilew
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern SafeFileHandle CreateFileW(
                string lpFileName,
                DesiredAccess dwDesiredAccess,
                ShareModes dwShareMode,
                SECURITY_ATTRIBUTES* lpSecurityAttributes,
                CreationDisposition dwCreationDisposition,
                uint dwFlagsAndAttributes,
                IntPtr hTemplateFile);

            // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-reopenfile
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern IntPtr ReOpenFile(
                SafeFileHandle hOriginalFile,
                DesiredAccess dwDesiredAccess,
                ShareModes dwShareMode,
                uint dwFlags);

            // Ex version is supported by WinRT apps
            // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getfileinformationbyhandle
            [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool GetFileInformationByHandle(
                SafeFileHandle hFile,
                out BY_HANDLE_FILE_INFORMATION lpFileInformation);

            // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getshortpathnamew (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetShortPathNameW(
                string lpszLongPath,
                SafeHandle lpszShortPath,
                uint cchBuffer);

            // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-createsymboliclinkw
            [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern BOOLEAN CreateSymbolicLinkW(
                string lpSymlinkFileName,
                string lpTargetFileName,
                SYMBOLIC_LINK_FLAG dwFlags);

            // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-encryptfilew
            [DllImport(Libraries.Advapi32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool EncryptFileW(
                string lpFileName);

            // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-decryptfilew
            [DllImport(Libraries.Advapi32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern bool DecryptFileW(
                string lpFileName,
                uint dwReserved);

            // https://docs.microsoft.com/en-us/windows/desktop/FileIO/adding-users-to-an-encrypted-file
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

            // https://docs.microsoft.com/en-us/windows/desktop/api/winefs/nf-winefs-adduserstoencryptedfile
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

            // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-findfirstvolumew
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern FindVolumeHandle FindFirstVolumeW(
                SafeHandle lpszVolumeName,
                uint cchBufferLength);

            // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-findnextvolumew
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool FindNextVolumeW(
                FindVolumeHandle hFindVolume,
                SafeHandle lpszVolumeName,
                uint cchBufferLength);

            // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-findvolumeclose
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool FindVolumeClose(
                IntPtr hFindVolume);

            // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-findfirstvolumemountpointw
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern FindVolumeMountPointHandle FindFirstVolumeMountPointW(
                string lpszRootPathName,
                SafeHandle lpszVolumeMountPoint,
                uint cchBufferLength);

            // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-findnextvolumemountpointw
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool FindNextVolumeMountPointW(
                FindVolumeMountPointHandle hFindVolumeMountPoint,
                SafeHandle lpszVolumeMountPoint,
                uint cchBufferLength);

            // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-findvolumemountpointclose
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool FindVolumeMountPointClose(
                IntPtr hFindVolumeMountPoint);

            // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-querydosdevicew
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint QueryDosDeviceW(
                string lpDeviceName,
                SafeHandle lpTargetPath,
                uint ucchMax);

            // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getlogicaldrivestringsw
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetLogicalDriveStringsW(
                uint nBufferLength,
                SafeHandle lpBuffer);

            // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getvolumepathnamew
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetVolumePathNameW(
                string lpszFileName,
                SafeHandle lpszVolumePathName,
                uint cchBufferLength);

            // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getvolumepathnamesforvolumenamew
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetVolumePathNamesForVolumeNameW(
                string lpszVolumeName,
                SafeHandle lpszVolumePathNames,
                uint cchBuferLength,
                ref uint lpcchReturnLength);

            // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getvolumenameforvolumemountpointw
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetVolumeNameForVolumeMountPointW(
                string lpszVolumeMountPoint,
                SafeHandle lpszVolumeName,
                uint cchBufferLength);

            // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-backupread
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern bool BackupRead(
                SafeFileHandle hFile,
                void* lpBuffer,
                uint nNumberOfBytesToRead,
                out uint lpNumberOfBytesRead,
                bool bAbort,
                bool bProcessSecurity,
                ref IntPtr context);

            // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-backupseek
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool BackupSeek(
                SafeFileHandle hFile,
                uint dwLowBytesToSeek,
                uint dwHighBytesToSeek,
                out uint lpdwLowByteSeeked,
                out uint lpdwHighByteSeeked,
                ref IntPtr context);

            // https://docs.microsoft.com/en-us/windows/desktop/api/winternl/nf-winternl-ntcreatefile
            // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntifs/nf-ntifs-ntcreatefile
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

            // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntifs/nf-ntifs-ntqueryinformationfile
            // http://www.pinvoke.net/default.aspx/ntdll/NtQueryInformationFile.html
            [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern NTSTATUS NtQueryInformationFile(
                SafeFileHandle FileHandle,
                out IO_STATUS_BLOCK IoStatusBlock,
                void* FileInformation,
                uint Length,
                FileInformationClass FileInformationClass);

            // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntifs/nf-ntifs-ntsetinformationfile
            [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern NTSTATUS NtSetInformationFile(
                SafeFileHandle FileHandle,
                out IO_STATUS_BLOCK IoStatusBlock,
                void* FileInformation,
                FileInformationClass FileInformationClass);

            // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntifs/nf-ntifs-ntquerydirectoryfile
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
            // https://docs.microsoft.com/en-us/windows/desktop/DevNotes/rtlisnameinexpression
            [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern BOOLEAN RtlIsNameInExpression(
                UNICODE_STRING* Expression,
                UNICODE_STRING* Name,
                BOOLEAN IgnoreCase,
                IntPtr UpcaseTable);

            // https://docs.microsoft.com/en-us/windows/desktop/DevNotes/ntqueryattributesfile
            [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern NTSTATUS NtQueryAttributesFile(
                ref OBJECT_ATTRIBUTES ObjectAttributes,
                out FILE_BASIC_INFORMATION FileInformation);

            // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/wdm/nf-wdm-zwqueryfullattributesfile
            [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern NTSTATUS NtQueryFullAttributesFile(
                ref OBJECT_ATTRIBUTES ObjectAttributes,
                out FILE_NETWORK_OPEN_INFORMATION FileInformation);
        }
    }
}
