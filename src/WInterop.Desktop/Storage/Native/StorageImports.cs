// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Handles;
using WInterop.Handles.Native;
using WInterop.SafeString.Native;
using WInterop.Security.Native;
using WInterop.Synchronization;

namespace WInterop.Storage.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class StorageImports
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-createfilew
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe SafeFileHandle CreateFileW(
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
    // https://docs.microsoft.com/windows/desktop/api/fileapi/nf-fileapi-getfileinformationbyhandle
    [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern bool GetFileInformationByHandle(
        SafeFileHandle hFile,
        out ByHandleFileInformation lpFileInformation);

    // https://docs.microsoft.com/windows/desktop/api/fileapi/nf-fileapi-getshortpathnamew (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe uint GetShortPathNameW(
        char* lpszLongPath,
        char* lpszShortPath,
        uint cchBuffer);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-createsymboliclinkw
    [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern ByteBoolean CreateSymbolicLinkW(
        string lpSymlinkFileName,
        string lpTargetFileName,
        SymbolicLinkFlag dwFlags);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-encryptfilew
    [DllImport(Libraries.Advapi32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern bool EncryptFileW(
        string lpFileName);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-decryptfilew
    [DllImport(Libraries.Advapi32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern bool DecryptFileW(
        string lpFileName,
        uint dwReserved);

    // https://docs.microsoft.com/en-us/windows/desktop/api/Winefs/nf-winefs-queryusersonencryptedfile
    [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe WindowsError QueryUsersOnEncryptedFile(
        string lpFileName,
        ENCRYPTION_CERTIFICATE_HASH_LIST** pUsers);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winefs/nf-winefs-freeencryptioncertificatehashlist
    [DllImport(Libraries.Advapi32, ExactSpelling = true)]
    public static extern unsafe void FreeEncryptionCertificateHashList(
        ENCRYPTION_CERTIFICATE_HASH_LIST* pUsers);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winefs/nf-winefs-removeusersfromencryptedfile
    [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe WindowsError RemoveUsersFromEncryptedFile(
        string lpFileName,
        ENCRYPTION_CERTIFICATE_HASH_LIST* pHashes);

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

    /// <param name="pUsers">Pointer to ENCRYPTION_CERTIFICATE_LIST array</param>
    [DllImport(Libraries.Advapi32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern uint AddUsersToEncryptedFile(
        string lpFileName,
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
        string? lpDeviceName,
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
    public static extern unsafe bool GetVolumeNameForVolumeMountPointW(
        string lpszVolumeMountPoint,
        char* lpszVolumeName,
        uint cchBufferLength);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-backupread
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe bool BackupRead(
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
    public static extern unsafe NTStatus NtCreateFile(
        out IntPtr FileHandle,
        DesiredAccess DesiredAccess,
        ref Handles.Native.OBJECT_ATTRIBUTES ObjectAttributes,
        out IO_STATUS_BLOCK IoStatusBlock,
        long* AllocationSize,
        AllFileAttributes FileAttributes,
        ShareModes ShareAccess,
        CreateDisposition CreateDisposition,
        CreateOptions CreateOptions,
        void* EaBuffer,
        uint EaLength);

    // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntifs/nf-ntifs-ntqueryinformationfile
    // http://www.pinvoke.net/default.aspx/ntdll/NtQueryInformationFile.html
    [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe NTStatus NtQueryInformationFile(
        SafeFileHandle FileHandle,
        out IO_STATUS_BLOCK IoStatusBlock,
        void* FileInformation,
        uint Length,
        FileInformationClass FileInformationClass);

    // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntifs/nf-ntifs-ntsetinformationfile
    [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe NTStatus NtSetInformationFile(
        SafeFileHandle FileHandle,
        out IO_STATUS_BLOCK IoStatusBlock,
        void* FileInformation,
        FileInformationClass FileInformationClass);

    // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/ntifs/nf-ntifs-ntquerydirectoryfile
    [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe NTStatus NtQueryDirectoryFile(
        IntPtr FileHandle,
        IntPtr Event,
        AsyncProcedureCall ApcRoutine,
        IntPtr ApcContext,
        out IO_STATUS_BLOCK IoStatusBlock,
        void* FileInformation,
        uint Length,
        FileInformationClass FileInformationClass,
        ByteBoolean ReturnSingleEntry,
        SafeString.Native.UNICODE_STRING* FileName,
        ByteBoolean RestartScan);

    [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe NTStatus NtQueryDirectoryFile(
        SafeFileHandle FileHandle,
        IntPtr Event,
        AsyncProcedureCall ApcRoutine,
        IntPtr ApcContext,
        out IO_STATUS_BLOCK IoStatusBlock,
        void* FileInformation,
        uint Length,
        FileInformationClass FileInformationClass,
        ByteBoolean ReturnSingleEntry,
        SafeString.Native.UNICODE_STRING* FileName,
        ByteBoolean RestartScan);

    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff546850.aspx
    // https://docs.microsoft.com/en-us/windows/desktop/DevNotes/rtlisnameinexpression
    [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe ByteBoolean RtlIsNameInExpression(
        SafeString.Native.UNICODE_STRING* Expression,
        SafeString.Native.UNICODE_STRING* Name,
        ByteBoolean IgnoreCase,
        IntPtr UpcaseTable);

    // https://docs.microsoft.com/en-us/windows/desktop/DevNotes/ntqueryattributesfile
    [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern NTStatus NtQueryAttributesFile(
        ref Handles.Native.OBJECT_ATTRIBUTES ObjectAttributes,
        out FileBasicInformation FileInformation);

    // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/wdm/nf-wdm-zwqueryfullattributesfile
    [DllImport(Libraries.Ntdll, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern NTStatus NtQueryFullAttributesFile(
        ref Handles.Native.OBJECT_ATTRIBUTES ObjectAttributes,
        out FileNetworkOpenInformation FileInformation);

    // NTFS Technical Reference
    // https://technet.microsoft.com/en-us/library/cc758691.aspx

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-createfile2 (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_2_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern SafeFileHandle CreateFile2(
        ref char lpFileName,
        DesiredAccess dwDesiredAccess,
        ShareModes dwShareMode,
        CreationDisposition dwCreationDisposition,
        ref CREATEFILE2_EXTENDED_PARAMETERS pCreateExParams);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-copyfileexw
    // CopyFile calls CopyFileEx with COPY_FILE_FAIL_IF_EXISTS if fail if exists is set
    // (Neither are available in WinRT- use CopyFile2)
    [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern bool CopyFileExW(
        string lpExistingFileName,
        string lpNewFileName,
        CopyProgressRoutine? lpProgressRoutine,
        IntPtr lpData,
        ref bool pbCancel,
        CopyFileFlags dwCopyFlags);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-copyfile2 (kernel32)
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern HResult CopyFile2(
        string pwszExistingFileName,
        string pwszNewFileName,
        ref COPYFILE2_EXTENDED_PARAMETERS pExtendedParameters);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-replacefilew
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern bool ReplaceFileW(
        string lpReplacedFileName,
        string lpReplacementFileName,
        string lpBackupFileName,
        ReplaceFileFlags dwReplaceFlags,
        IntPtr lpExclude,
        IntPtr lpReserved);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getfileattributesw
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern AllFileAttributes GetFileAttributesW(
        string lpFileName);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getfileattributesexw (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern bool GetFileAttributesExW(
        string lpFileName,
        GetFileExtendedInformationLevels fInfoLevelId,
        ref Win32FileAttributeData lpFileInformation);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-setfileattributesw (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern bool SetFileAttributesW(
        string lpFileName,
        AllFileAttributes dwFileAttributes);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getfullpathnamew (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe uint GetFullPathNameW(
        char* lpFileName,
        uint nBufferLength,
        char* lpBuffer,
        char* lpFilePart);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getfinalpathnamebyhandlew (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe uint GetFinalPathNameByHandleW(
        SafeFileHandle hFile,
        char* lpszFilePath,
        uint cchFilePath,
        GetFinalPathNameByHandleFlags dwFlags);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getlongpathnamew (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe uint GetLongPathNameW(
        char* lpszShortPath,
        char* lpszLongPath,
        uint cchBuffer);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-findfirstfilew
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern IntPtr FindFirstFileW(
        string lpFileName,
        out Win32FindData lpFindFileData);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-findfirstfileexw (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern IntPtr FindFirstFileExW(
            string lpFileName,
            FindExtendedInfoLevels fInfoLevelId,
            out Win32FindData lpFindFileData,
            uint fSearchOp,                        // This never actually has meaning and is likely a holdover of 9x
                                                   // set it to 0 to avoid failing parameter checks.
            IntPtr lpSearchFilter,                 // Reserved
            FindFirstFileExtendedFlags dwAdditionalFlags);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-findnextfilew (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern bool FindNextFileW(
        IntPtr hFindFile,
        out Win32FindData lpFindFileData);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-findclose (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, ExactSpelling = true)]
    public static extern bool FindClose(
        IntPtr hFindFile);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-getfileinformationbyhandleex (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l2_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe bool GetFileInformationByHandleEx(
        IntPtr hFile,
        FileInfoClass FileInformationClass,
        void* lpFileInformation,
        uint dwBufferSize);

    public static unsafe bool GetFileInformationByHandleEx(
        SafeFileHandle hFile,
        FileInfoClass FileInformationClass,
        void* lpFileInformation,
        uint dwBufferSize)
    {
        using var handle = new UnwrapHandle(hFile);
        return GetFileInformationByHandleEx(handle, FileInformationClass, lpFileInformation, dwBufferSize);
    }

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-deletefilew (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern bool DeleteFileW(
        string lpFilename);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-readfile
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe bool ReadFile(
        SafeFileHandle hFile,
        ref byte lpBuffer,
        uint nNumberOfBytesToRead,
        out uint lpNumberOfBytesRead,
        OVERLAPPED* lpOverlapped);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-writefile
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe bool WriteFile(
        SafeFileHandle hFile,
        ref byte lpBuffer,
        uint nNumberOfBytesToWrite,
        out uint lpNumberOfBytesWritten,
        OVERLAPPED* lpOverlapped);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-setfilepointerex
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool SetFilePointerEx(
        SafeFileHandle hFile,
        long liDistanceToMove,
        out long lpNewFilePointer,
        MoveMethod dwMoveMethod);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getfilesizeex
    // This returns FILE_STANDARD_INFO.EndOfFile
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool GetFileSizeEx(
        SafeFileHandle hFile,
        out long lpFileSize);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getfiletype
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern FileType GetFileType(
        SafeFileHandle hFile);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-flushfilebuffers (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, ExactSpelling = true)]
    public static extern bool FlushFileBuffers(
        SafeFileHandle hFile);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-gettemppathw (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_2_0, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe uint GetTempPathW(
        uint nBufferLength,
        char* lpBuffer);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-gettempfilenamew (kernel32)
    [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern uint GetTempFileNameW(
        string lpPathName,
        string lpPrefixString,
        uint uUnique,
        SafeHandle lpTempFileName);

    // https://docs.microsoft.com/en-us/windows/desktop/FileIO/cancelioex-func
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe bool CancelIoEx(
        SafeFileHandle hFile,
        OVERLAPPED* lpOverlapped);

    // https://docs.microsoft.com/en-us/windows/desktop/api/ioapiset/nf-ioapiset-getoverlappedresultex
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool GetOverlappedResultEx(
        SafeFileHandle hFile,
        ref OVERLAPPED lpOverlapped,
        out uint lpNumberOfBytesTransferred,
        uint dwMilliseconds,
        bool bAlertable);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-unlockfileex
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool UnlockFileEx(
        SafeFileHandle hFile,
        uint dwReserved,
        uint nNumberOfBytesToUnlockLow,
        uint nNumberOfBytesToUnlockHigh,
        ref OVERLAPPED lpOverlapped);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-lockfileex
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern bool LockFileEx(
        SafeFileHandle hFile,
        uint dwFlags,
        uint dwReserved,
        uint nNumberOfBytesToUnlockLow,
        uint nNumberOfBytesToUnlockHigh,
        ref OVERLAPPED lpOverlapped);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-readdirectorychangesw
    [DllImport(Libraries.Kernel32, ExactSpelling = true)]
    public static extern unsafe bool ReadDirectoryChangesW(
        SafeFileHandle hDirectory,
        void* lpBuffer,
        uint nBufferLength,
        bool bWatchSubtree,
        FileNotifyChange dwNotifyFilter,
        out uint lpBytesReturned,
        ref OVERLAPPED lpOverlapped,
        FileIOCompletionRoutine lpCompletionRoutine);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-removedirectoryw
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern bool RemoveDirectoryW(
        string lpPathName);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-createdirectoryw
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern bool CreateDirectoryW(
        string lpPathName,
        IntPtr lpSecurityAttributes);

    // https://docs.microsoft.com/windows/desktop/api/winbase/nf-winbase-getcurrentdirectory
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe uint GetCurrentDirectoryW(
        uint nBufferLength,
        char* lpBuffer);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-setcurrentdirectory
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern bool SetCurrentDirectoryW(
        ref char lpPathName);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getlogicaldrives
    [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
    public static extern LogicalDrives GetLogicalDrives();

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getvolumeinformationw
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe bool GetVolumeInformationW(
        string? lpRootPathName,
        char* lpVolumeNameBuffer,
        uint nVolumeNameSize,
        out uint lpVolumeSerialNumber,
        out uint lpMaximumComponentLength,
        out FileSystemFeature lpFileSystemFlags,
        char* lpFileSystemNameBuffer,
        uint nFileSystemNameSize);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getdrivetypew
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern DriveType GetDriveTypeW(
        string? lpRootPathName);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getdiskfreespacew
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe bool GetDiskFreeSpaceW(
        string? lpRootPathName,
        uint* lpSectorsPerCluster,
        uint* lpBytesPerSector,
        uint* lpNumberOfFreeClusters,
        uint* lpTotalNumberOfClusters);

    // https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-getdiskfreespaceexw
    [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
    public static extern unsafe bool GetDiskFreeSpaceExW(
        string lpDirectoryName,
        ulong* lpFreeBytesAvailable,
        ulong* lpTotalNumberOfBytes,
        ulong* lpTotalNumberOfFreeBytes);
}