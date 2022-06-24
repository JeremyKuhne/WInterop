// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Handles.Native;
using WInterop.SafeString.Native;

namespace WInterop.Storage.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class StorageImports
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/Winefs/nf-winefs-queryusersonencryptedfile
    [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe WindowsError QueryUsersOnEncryptedFile(
        string lpFileName,
        ENCRYPTION_CERTIFICATE_HASH_LIST** pUsers);

    // https://docs.microsoft.com/en-us/windows/desktop/api/winefs/nf-winefs-freeencryptioncertificatehashlist
    [DllImport(Libraries.Advapi32, ExactSpelling = true)]
    public static extern unsafe void FreeEncryptionCertificateHashList(
        ENCRYPTION_CERTIFICATE_HASH_LIST* pUsers);

    // https://docs.microsoft.com/windows/win32/api/winefs/nf-winefs-removeusersfromencryptedfile
    [DllImport(Libraries.Advapi32, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe WindowsError RemoveUsersFromEncryptedFile(
        string lpFileName,
        ENCRYPTION_CERTIFICATE_HASH_LIST* pHashes);

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

    // https://docs.microsoft.com/en-us/windows/desktop/api/winbase/nf-winbase-copyfileexw
    // CopyFile calls CopyFileEx with COPY_FILE_FAIL_IF_EXISTS if fail if exists is set
    // (Neither are available in WinRT- use CopyFile2)
}