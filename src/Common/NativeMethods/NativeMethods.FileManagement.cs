// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop
{
    using Authorization;
    using ErrorHandling;
    using FileManagement;
    using Handles;
    using Microsoft.Win32.SafeHandles;
    using Synchronization;
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public static partial class NativeMethods
    {
        public static partial class FileManagement
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
            public static partial class Direct
            {
                // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449422.aspx (kernel32)
                [DllImport(Libraries.api_ms_win_core_file_l1_2_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern SafeFileHandle CreateFile2(
                    string lpFileName,
                    uint dwDesiredAccess,
                    [MarshalAs(UnmanagedType.U4)] System.IO.FileShare dwShareMode,
                    [MarshalAs(UnmanagedType.U4)] System.IO.FileMode dwCreationDisposition,
                    CREATEFILE2_EXTENDED_PARAMETERS pCreateExParams);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364946.aspx (kernel32)
                [DllImport(Libraries.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool GetFileAttributesExW(
                    string lpFileName,
                    GET_FILEEX_INFO_LEVELS fInfoLevelId,
                    out WIN32_FILE_ATTRIBUTE_DATA lpFileInformation);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365535.aspx (kernel32)
                [DllImport(Libraries.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool SetFileAttributesW(
                    string lpFileName,
                    uint dwFileAttributes);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364980.aspx (kernel32)
                [DllImport(Libraries.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern uint GetLongPathNameW(
                    string lpszShortPath,
                    SafeHandle lpszLongPath,
                    uint cchBuffer);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364989.aspx (kernel32)
                [DllImport(Libraries.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern uint GetShortPathNameW(
                    string lpszLongPath,
                    SafeHandle lpszShortPath,
                    uint cchBuffer);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364963.aspx (kernel32)
                [DllImport(Libraries.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
                public static extern uint GetFullPathNameW(
                    string lpFileName,
                    uint nBufferLength,
                    SafeHandle lpBuffer,
                    IntPtr lpFilePart);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364419.aspx (kernel32)
                [DllImport(Libraries.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                public static extern SafeFindHandle FindFirstFileExW(
                     string lpFileName,
                     FINDEX_INFO_LEVELS fInfoLevelId,
                     out WIN32_FIND_DATA lpFindFileData,
                     FINDEX_SEARCH_OPS fSearchOp,
                     IntPtr lpSearchFilter,                 // Reserved
                     int dwAdditionalFlags);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364428.aspx (kernel32)
                [DllImport(Libraries.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool FindNextFileW(
                    SafeFindHandle hFindFile,
                    out WIN32_FIND_DATA lpFindFileData);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364413.aspx (kernel32)
                [DllImport(Libraries.api_ms_win_core_file_l1_1_0, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool FindClose(
                    IntPtr hFindFile);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364952.aspx (kernel32 / api-ms-win-core-file-l2-1-0)
                [DllImport(Libraries.Kernel32, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool GetFileInformationByHandleEx(
                    SafeFileHandle hFile,
                    FILE_INFO_BY_HANDLE_CLASS FileInformationClass,
                    IntPtr lpFileInformation,
                    uint dwBufferSize);

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

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364960.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                public static extern FileType GetFileType(
                    SafeFileHandle hFile);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364439.aspx
                [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool FlushFileBuffers(
                    SafeFileHandle hFile);
            }

#if !DESKTOP
            public static SafeFileHandle CreateFile(
                string path,
                System.IO.FileAccess fileAccess,
                System.IO.FileShare fileShare,
                System.IO.FileMode creationDisposition,
                FileAttributes fileAttributes = FileAttributes.NONE,
                FileFlags fileFlags = FileFlags.NONE,
                SecurityQosFlags securityQosFlags = SecurityQosFlags.NONE)
            {
                if (creationDisposition == System.IO.FileMode.Append) creationDisposition = System.IO.FileMode.OpenOrCreate;

                uint dwDesiredAccess =
                    ((fileAccess & System.IO.FileAccess.Read) != 0 ? (uint)GenericAccessRights.GENERIC_READ : 0) |
                    ((fileAccess & System.IO.FileAccess.Write) != 0 ? (uint)GenericAccessRights.GENERIC_WRITE : 0);

                CREATEFILE2_EXTENDED_PARAMETERS extended = new CREATEFILE2_EXTENDED_PARAMETERS();
                extended.dwSize = (uint)Marshal.SizeOf<CREATEFILE2_EXTENDED_PARAMETERS>();
                extended.dwFileAttributes = fileAttributes;
                extended.dwFileFlags = fileFlags;
                extended.dwSecurityQosFlags = securityQosFlags;
                extended.lpSecurityAttributes = IntPtr.Zero;
                extended.hTemplateFile = IntPtr.Zero;

                SafeFileHandle handle = Direct.CreateFile2(
                    lpFileName: path,
                    dwDesiredAccess: dwDesiredAccess,
                    dwShareMode: fileShare,
                    dwCreationDisposition: creationDisposition,
                    pCreateExParams: extended);

                if (handle.IsInvalid)
                    throw ErrorHelper.GetIoExceptionForLastError(path);

                return handle;
            }
#endif
        }
    }
}
