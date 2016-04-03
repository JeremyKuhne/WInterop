// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    using Authorization;
    using Backup;
    using Buffers;
    using FileManagement;
    using Handles;
    using Microsoft.Win32.SafeHandles;
    using Synchronization;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;

    public static partial class NativeMethods
    {
        public static class FileManagement
        {
            /// <summary>
            /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
            /// </summary>
            /// <remarks>
            /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
            /// </remarks>
#if DESKTOP
            [SuppressUnmanagedCodeSecurity] // We don't want a stack walk with every P/Invoke.
#endif
            public static class Direct
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364944.aspx
                [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern FileAttributes GetFileAttributesW(
                    string lpFileName);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365535.aspx
                [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool SetFileAttributesW(
                    string lpFileName,
                    uint dwFileAttributes);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364980.aspx
                [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern uint GetLongPathNameW(
                    string lpszShortPath,
                    SafeHandle lpszLongPath,
                    uint cchBuffer);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364989.aspx
                [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern uint GetShortPathNameW(
                    string lpszLongPath,
                    SafeHandle lpszShortPath,
                    uint cchBuffer);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364963.aspx
                [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern uint GetFullPathNameW(
                    string lpFileName,
                    uint nBufferLength,
                    SafeHandle lpBuffer,
                    IntPtr lpFilePart);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364962.aspx
                [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern uint GetFinalPathNameByHandleW(
                    SafeFileHandle hFile,
                    SafeHandle lpszFilePath,
                    uint cchFilePath,
                    GetFinalPathNameByHandleFlags dwFlags);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx
                [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern SafeFileHandle CreateFileW(
                    string lpFileName,
                    uint dwDesiredAccess,
                    [MarshalAs(UnmanagedType.U4)] System.IO.FileShare dwShareMode,
                    IntPtr lpSecurityAttributes,
                    [MarshalAs(UnmanagedType.U4)] System.IO.FileMode dwCreationDisposition,
                    FileAttributes dwFlagsAndAttributes,
                    IntPtr hTemplateFile);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364419.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                public static extern SafeFindHandle FindFirstFileExW(
                     string lpFileName,
                     FINDEX_INFO_LEVELS fInfoLevelId,
                     out WIN32_FIND_DATA lpFindFileData,
                     FINDEX_SEARCH_OPS fSearchOp,
                     IntPtr lpSearchFilter,                 // Reserved
                     int dwAdditionalFlags);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364428.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool FindNextFileW(
                    SafeFindHandle hFindFile,
                    out WIN32_FIND_DATA lpFindFileData);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364413.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool FindClose(
                    IntPtr hFindFile);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364952.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool GetFileInformationByHandle(
                    SafeFileHandle hFile,
                    out BY_HANDLE_FILE_INFORMATION lpFileInformation);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363915.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool DeleteFileW(
                    string lpFilename);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365467.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                unsafe public static extern bool ReadFile(
                    SafeFileHandle hFile,
                    byte* lpBuffer,
                    uint nNumberOfBytesToRead,
                    out uint lpNumberOfBytesRead,
                    ref OVERLAPPED lpOverlapped);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363852.aspx
                // CopyFile calls CopyFileEx with COPY_FILE_FAIL_IF_EXISTS if fail if exists is set
                [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool CopyFileExW(
                    string lpExistingFileName,
                    string lpNewFileName,
                    CopyProgressRoutine lpProgressRoutine,
                    IntPtr lpData,
                    [MarshalAs(UnmanagedType.Bool)] ref bool pbCancel,
                    CopyFileExFlags dwCopyFlags);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363866.aspx
                // Note that CreateSymbolicLinkW returns a BOOLEAN (byte), not a BOOL (int)
                [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                public static extern byte CreateSymbolicLinkW(
                    string lpSymlinkFileName,
                    string lpTargetFileName,
                    uint dwFlags);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364960.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                public static extern FileType GetFileType(
                    SafeFileHandle hFile);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364439.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool FlushFileBuffers(
                    SafeFileHandle hFile);

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
                internal static extern uint AddUsersToEncryptedFile(
                    string lpFileName,

                    /// <summary>
                    /// Pointer to ENCRYPTION_CERTIFICATE_LIST array
                    /// </summary>
                    IntPtr pUsers);
            }

            public static SafeFileHandle CreateFile(
                string path,
                System.IO.FileAccess fileAccess,
                System.IO.FileShare fileShare,
                System.IO.FileMode creationDisposition,
                FileAttributes flagsAndAttributes)
            {
                if (creationDisposition == System.IO.FileMode.Append) creationDisposition = System.IO.FileMode.OpenOrCreate;

                uint dwDesiredAccess =
                    ((fileAccess & System.IO.FileAccess.Read) != 0 ? (uint)GenericAccessRights.GENERIC_READ : 0) |
                    ((fileAccess & System.IO.FileAccess.Write) != 0 ? (uint)GenericAccessRights.GENERIC_WRITE : 0);

                SafeFileHandle handle = Direct.CreateFileW(path, dwDesiredAccess, fileShare, IntPtr.Zero, creationDisposition, flagsAndAttributes, IntPtr.Zero);
                if (handle.IsInvalid)
                {
                    uint error = (uint)Marshal.GetLastWin32Error();
                    throw Errors.GetIoExceptionForError(error, path);
                }

                return handle;
            }
        }
    }
}
