// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.FileManagement.DataTypes;
using WInterop.FileManagement.DataTypes.CopyFile2;
using WInterop.Handles.DataTypes;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.Synchronization.DataTypes;

namespace WInterop.FileManagement
{
    public static partial class FileMethods
    {
        // Asynchronous Disk I/O Appears as Synchronous on Windows
        // https://support.microsoft.com/en-us/kb/156932

        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449422.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_2_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern SafeFileHandle CreateFile2(
                string lpFileName,
                DesiredAccess dwDesiredAccess,
                ShareMode dwShareMode,
                CreationDisposition dwCreationDisposition,
                [In] ref CREATEFILE2_EXTENDED_PARAMETERS pCreateExParams);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449404.aspx (kernel32)
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public unsafe static extern int CopyFile2(
                string pwszExistingFileName,
                string pwszNewFileName,
                COPYFILE2_EXTENDED_PARAMETERS* pExtendedParameters);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365512.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ReplaceFileW(
                string lpReplacedFileName,
                string lpReplacementFileName,
                string lpBackupFileName,
                ReplaceFileFlags dwReplaceFlags,
                IntPtr lpExclude,
                IntPtr lpReserved);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364946.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetFileAttributesExW(
                string lpFileName,
                GET_FILEEX_INFO_LEVELS fInfoLevelId,
                out WIN32_FILE_ATTRIBUTE_DATA lpFileInformation);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365535.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
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

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364419.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern IntPtr FindFirstFileExW(
                    string lpFileName,
                    FINDEX_INFO_LEVELS fInfoLevelId,
                    out WIN32_FIND_DATA lpFindFileData,
                    FINDEX_SEARCH_OPS fSearchOp,
                    IntPtr lpSearchFilter,                 // Reserved
                    FindFirstFileExFlags dwAdditionalFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364428.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FindNextFileW(
                IntPtr hFindFile,
                out WIN32_FIND_DATA lpFindFileData);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364413.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FindClose(
                IntPtr hFindFile);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364953.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l2_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            unsafe public static extern bool GetFileInformationByHandleEx(
                SafeFileHandle hFile,
                FILE_INFO_BY_HANDLE_CLASS FileInformationClass,
                void* lpFileInformation,
                uint dwBufferSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363915.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DeleteFileW(
                string lpFilename);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365467.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            unsafe public static extern bool ReadFile(
                SafeFileHandle hFile,
                byte* lpBuffer,
                uint nNumberOfBytesToRead,
                uint* lpNumberOfBytesRead,
                OVERLAPPED* lpOverlapped);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365747.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            unsafe public static extern bool WriteFile(
                SafeFileHandle hFile,
                byte* lpBuffer,
                uint nNumberOfBytesToWrite,
                uint* lpNumberOfBytesWritten,
                OVERLAPPED* lpOverlapped);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365542.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool SetFilePointerEx(
                SafeFileHandle hFile,
                long liDistanceToMove,
                out long lpNewFilePointer,
                MoveMethod dwMoveMethod);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364957.aspx
            // This returns FILE_STANDARD_INFO.EndOfFile
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetFileSizeEx(
                SafeFileHandle hFile,
                out long lpFileSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364960.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern FileType GetFileType(
                SafeFileHandle hFile);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364439.aspx (kernel32)
            [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
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
        }

        /// <summary>
        /// Get the temporary directory path.
        /// </summary>
        public static string GetTempPath()
        {
            return BufferHelper.CachedApiInvoke((buffer) => Direct.GetTempPathW(buffer.CharCapacity, buffer));
        }

        /// <summary>
        /// Get the fully resolved path name.
        /// </summary>
        public static string GetFullPathName(string path)
        {
            return BufferHelper.CachedApiInvoke((buffer) => Direct.GetFullPathNameW(path, buffer.CharCapacity, buffer, IntPtr.Zero), path);
        }

        /// <summary>
        /// Get a temporary file name. Creates a 0 length file.
        /// </summary>
        /// <param name="path">The directory for the file.</param>
        /// <param name="prefix">Three character prefix for the filename.</param>
        public static string GetTempFileName(string path, string prefix)
        {
            return BufferHelper.CachedInvoke((StringBuffer buffer) =>
            {
                buffer.EnsureCharCapacity(Paths.MaxPath);
                uint result = Direct.GetTempFileNameW(
                    lpPathName: path,
                    lpPrefixString: prefix,
                    uUnique: 0,
                    lpTempFileName: buffer);

                if (result == 0) throw ErrorHelper.GetIoExceptionForLastError(path);

                buffer.SetLengthToFirstNull();
                return buffer.ToString();
            });
        }

        /// <summary>
        /// Delete the given file.
        /// </summary>
        public static void DeleteFile(string path)
        {
            if (!Direct.DeleteFileW(path))
                throw ErrorHelper.GetIoExceptionForLastError(path);
        }

        /// <summary>
        /// Wrapper that allows getting a file stream using System.IO defines.
        /// </summary>
        public static System.IO.Stream CreateFileStream(
            string path,
            System.IO.FileAccess fileAccess,
            System.IO.FileShare fileShare,
            System.IO.FileMode fileMode,
            System.IO.FileAttributes fileAttributes = 0,
            FileFlags fileFlags = FileFlags.NONE,
            SecurityQosFlags securityFlags = SecurityQosFlags.NONE)
        {
            return CreateFileStream(
                path: path,
                desiredAccess: Conversion.FileAccessToDesiredAccess(fileAccess),
                shareMode: Conversion.FileShareToShareMode(fileShare),
                creationDisposition: Conversion.FileModeToCreationDisposition(fileMode),
                fileAttributes: (FileAttributes)fileAttributes,
                fileFlags: fileFlags,
                securityQosFlags: securityFlags);
        }

        /// <summary>
        /// Get a stream for the specified file.
        /// </summary>
        public static System.IO.Stream CreateFileStream(
            string path,
            DesiredAccess desiredAccess,
            ShareMode shareMode,
            CreationDisposition creationDisposition,
            FileAttributes fileAttributes = FileAttributes.NONE,
            FileFlags fileFlags = FileFlags.NONE,
            SecurityQosFlags securityQosFlags = SecurityQosFlags.NONE)
        {
            var fileHandle = CreateFile(path, desiredAccess, shareMode, creationDisposition, fileAttributes, fileFlags, securityQosFlags);
            var fileStream = new System.IO.FileStream(
                handle: new Microsoft.Win32.SafeHandles.SafeFileHandle(fileHandle.DangerousGetHandle(), ownsHandle: false),
                access: Conversion.DesiredAccessToFileAccess(desiredAccess),
                bufferSize: 4096,
                isAsync: (fileFlags & FileFlags.FILE_FLAG_OVERLAPPED) != 0);

            return new SafeHandleStreamWrapper(fileStream, fileHandle);
        }

        private delegate SafeFileHandle CreateFileDelegate(
            string path,
            DesiredAccess desiredAccess,
            ShareMode shareMode,
            CreationDisposition creationDisposition,
            FileAttributes fileAttributes,
            FileFlags fileFlags,
            SecurityQosFlags securityQosFlags);

        private static CreateFileDelegate s_createFileDelegate;

        /// <summary>
        /// Wrapper that allows using System.IO defines where available. Calls CreateFile2 if available.
        /// </summary>
        public static SafeFileHandle CreateFile(
            string path,
            System.IO.FileAccess fileAccess,
            System.IO.FileShare fileShare,
            System.IO.FileMode fileMode,
            System.IO.FileAttributes fileAttributes = 0,
            FileFlags fileFlags = FileFlags.NONE,
            SecurityQosFlags securityFlags = SecurityQosFlags.NONE)
        {
            return CreateFile(
                path: path,
                desiredAccess: Conversion.FileAccessToDesiredAccess(fileAccess),
                shareMode: Conversion.FileShareToShareMode(fileShare),
                creationDisposition: Conversion.FileModeToCreationDisposition(fileMode),
                fileAttributes: (FileAttributes)fileAttributes,
                fileFlags: fileFlags,
                securityQosFlags: securityFlags);
        }

        /// <summary>
        /// CreateFile wrapper that attempts to use CreateFile2 if running as Windows Store app.
        /// </summary>
        public static SafeFileHandle CreateFile(
            string path,
            DesiredAccess desiredAccess,
            ShareMode shareMode,
            CreationDisposition creationDisposition,
            FileAttributes fileAttributes = FileAttributes.NONE,
            FileFlags fileFlags = FileFlags.NONE,
            SecurityQosFlags securityQosFlags = SecurityQosFlags.NONE)
        {
            // Prefer CreateFile2, falling back to CreateFileEx if we can
            if (s_createFileDelegate == null)
            {
                s_createFileDelegate = CreateFile2;
                try
                {
                    return s_createFileDelegate(path, desiredAccess, shareMode, creationDisposition, fileAttributes, fileFlags, securityQosFlags);
                }
                catch (Exception exception)
                {
                    // Any error other than EntryPointNotFound we've found CreateFile2, rethrow
                    if (!ErrorHelper.IsEntryPointNotFoundException(exception))
                        throw;

                    s_createFileDelegate = Delegates.CreateDelegate<CreateFileDelegate>(
                        "WInterop.FileManagement.Desktop.NativeMethods, " + Delegates.DesktopLibrary,
                        "CreateFileW");
                }
            }

            return s_createFileDelegate(path, desiredAccess, shareMode, creationDisposition, fileAttributes, fileFlags, securityQosFlags);
        }

        /// <summary>
        /// CreateFile2 wrapper. Only available on Windows 8 and above.
        /// </summary>
        public static SafeFileHandle CreateFile2(
            string path,
            DesiredAccess desiredAccess,
            ShareMode shareMode,
            CreationDisposition creationDisposition,
            FileAttributes fileAttributes = FileAttributes.NONE,
            FileFlags fileFlags = FileFlags.NONE,
            SecurityQosFlags securityQosFlags = SecurityQosFlags.NONE)
        {
            CREATEFILE2_EXTENDED_PARAMETERS extended = new CREATEFILE2_EXTENDED_PARAMETERS();
            extended.dwSize = (uint)Marshal.SizeOf<CREATEFILE2_EXTENDED_PARAMETERS>();
            extended.dwFileAttributes = fileAttributes;
            extended.dwFileFlags = fileFlags;
            extended.dwSecurityQosFlags = securityQosFlags;
            unsafe
            {
                extended.lpSecurityAttributes = null;
            }
            extended.hTemplateFile = IntPtr.Zero;

            SafeFileHandle handle = Direct.CreateFile2(
                lpFileName: path,
                dwDesiredAccess: desiredAccess,
                dwShareMode: shareMode,
                dwCreationDisposition: creationDisposition,
                pCreateExParams: ref extended);

            if (handle.IsInvalid)
                throw ErrorHelper.GetIoExceptionForLastError();

            return handle;
        }

        private delegate void CopyFileDelegate(
            string source,
            string destination,
            bool overwrite);

        private static CopyFileDelegate s_copyFileDelegate;

        /// <summary>
        /// CopyFile wrapper that attempts to use CopyFile2 if running as Windows Store app.
        /// </summary>
        public static void CopyFile(string source, string destination, bool overwrite = false)
        {
            // Prefer CreateFile2, falling back to CreateFileEx if we can
            if (s_copyFileDelegate == null)
            {
                s_copyFileDelegate = CopyFile2;
                try
                {
                    s_copyFileDelegate(source, destination, overwrite);
                    return;
                }
                catch (Exception exception)
                {
                    // Any error other than EntryPointNotFound we've found CreateFile2, rethrow
                    if (!ErrorHelper.IsEntryPointNotFoundException(exception))
                        throw;

                    s_copyFileDelegate = Delegates.CreateDelegate<CopyFileDelegate>(
                        "WInterop.FileManagement.Desktop.NativeMethods, " + Delegates.DesktopLibrary,
                        "CopyFileEx");
                }
            }

            s_copyFileDelegate(source, destination, overwrite);
        }

        /// <summary>
        /// CopyFile2 wrapper. Only available on Windows8 and above.
        /// </summary>
        public static void CopyFile2(string source, string destination, bool overwrite = false)
        {
            unsafe
            {
                int cancel = 0;
                COPYFILE2_EXTENDED_PARAMETERS parameters = new COPYFILE2_EXTENDED_PARAMETERS();
                parameters.dwSize = (uint)Marshal.SizeOf<COPYFILE2_EXTENDED_PARAMETERS>();
                parameters.pfCanel = &cancel;
                parameters.dwCopyFlags = overwrite ? 0 : CopyFileFlags.COPY_FILE_FAIL_IF_EXISTS;

                int hr = Direct.CopyFile2(source, destination, &parameters);
                if (ErrorMacros.FAILED(hr))
                    throw ErrorHelper.GetIoExceptionForHResult(hr, source);
            }
        }

        /// <summary>
        /// Creates a wrapper for finding files.
        /// </summary>
        /// <param name="path">
        /// The search path. The path must not end in a directory separator. The final file/directory name (after the last
        /// directory separator) can contain wildcards, the full details can be found at
        /// <a href="https://msdn.microsoft.com/en-us/library/ff469270.aspx">[MS-FSA] 2.1.4.4 Algorithm for Determining if a FileName Is in an Expression</a>.
        /// </param>
        /// <aram name="directoriesOnly">Attempts to filter to just directories where supported.</param>
        /// <param name="getAlternateName">Returns the alternate (short) file name in the FindResult.AlternateName field if it exists.</param>
        public static FindOperation CreateFindOperation(
            string path,
            bool directoriesOnly = false,
            bool getAlternateName = false)
        {
            return new FindOperation(path, directoriesOnly, getAlternateName);
        }

        /// <summary>
        /// Gets the file attributes for the given path.
        /// </summary>
        public static FileInfo GetFileAttributesEx(string path)
        {
            WIN32_FILE_ATTRIBUTE_DATA data;
            if (!Direct.GetFileAttributesExW(path, GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, out data))
                throw ErrorHelper.GetIoExceptionForLastError(path);

            return new FileInfo(data);
        }

        /// <summary>
        /// Simple wrapper to check if a given path exists.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException">Thrown if there aren't rights to get attributes on the given path.</exception>
        public static bool PathExists(string path)
        {
            return TryGetFileInfo(path).HasValue;
        }

        /// <summary>
        /// Simple wrapper to check if a given path exists and is a file.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException">Thrown if there aren't rights to get attributes on the given path.</exception>
        public static bool FileExists(string path)
        {
            var data = TryGetFileInfo(path);
            return data.HasValue && (data.Value.Attributes & FileAttributes.FILE_ATTRIBUTE_DIRECTORY) != FileAttributes.FILE_ATTRIBUTE_DIRECTORY;
        }

        /// <summary>
        /// Simple wrapper to check if a given path exists and is a directory.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException">Thrown if there aren't rights to get attributes on the given path.</exception>
        public static bool DirectoryExists(string path)
        {
            var data = TryGetFileInfo(path);
            return data.HasValue && (data.Value.Attributes & FileAttributes.FILE_ATTRIBUTE_DIRECTORY) == FileAttributes.FILE_ATTRIBUTE_DIRECTORY;
        }

        /// <summary>
        /// Tries to get file info, returns null if the given path doesn't exist.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException">Thrown if there aren't rights to get attributes on the given path.</exception>
        public static FileInfo? TryGetFileInfo(string path)
        {
            WIN32_FILE_ATTRIBUTE_DATA data;
            if (!Direct.GetFileAttributesExW(path, GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, out data))
            {
                WindowsError error = ErrorHelper.GetLastError();
                switch (error)
                {
                    case WindowsError.ERROR_ACCESS_DENIED:
                    case WindowsError.ERROR_NETWORK_ACCESS_DENIED:
                        throw ErrorHelper.GetIoExceptionForError(error, path);
                    case WindowsError.ERROR_PATH_NOT_FOUND:
                    default:
                        return null;
                }
            }

            return new FileInfo(data);
        }

        /// <summary>
        /// Sets the file attributes for the given path.
        /// </summary>
        public static void SetFileAttributes(string path, FileAttributes attributes)
        {
            if (!Direct.SetFileAttributesW(path, attributes))
                throw ErrorHelper.GetIoExceptionForLastError(path);
        }

        /// <summary>
        /// Flush file buffers.
        /// </summary>
        public static void FlushFileBuffers(SafeFileHandle fileHandle)
        {
            if (!Direct.FlushFileBuffers(fileHandle))
                throw ErrorHelper.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Gets the file name from the given handle. This uses GetFileInformationByHandleEx, which does not give back the drive
        /// name for the path- but is available from Windows Store apps.
        /// </summary>
        public static string GetFileNameByHandle(SafeFileHandle fileHandle)
        {
            return BufferHelper.CachedInvoke((NativeBuffer buffer) =>
            {
                unsafe
                {
                    while (!Direct.GetFileInformationByHandleEx(
                        fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileNameInfo,
                        buffer.VoidPointer,
                        checked((uint)buffer.ByteCapacity)))
                    {
                        WindowsError error = ErrorHelper.GetLastError();
                        if (error != WindowsError.ERROR_MORE_DATA)
                            throw ErrorHelper.GetIoExceptionForError(error);
                        buffer.EnsureByteCapacity(buffer.ByteCapacity * 2);
                    }
                }

                var reader = new NativeBufferReader(buffer);
                return reader.ReadString((int)(reader.ReadUint() / 2));
            });
        }

        /// <summary>
        /// Get standard file info from the given file handle.
        /// </summary>
        public static FileStandardInfo GetFileStandardInfoByHandle(SafeFileHandle fileHandle)
        {
            FILE_STANDARD_INFO info;
            unsafe
            {
                if (!Direct.GetFileInformationByHandleEx(fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileStandardInfo, &info, (uint)Marshal.SizeOf<FILE_STANDARD_INFO>()))
                    throw ErrorHelper.GetIoExceptionForLastError();
            }

            return new FileStandardInfo(info);
        }

        /// <summary>
        /// Get basic file info from the given file handle.
        /// </summary>
        public static FileBasicInfo GetFileBasicInfoByHandle(SafeFileHandle fileHandle)
        {
            FILE_BASIC_INFO info;
            unsafe
            {
                if (!Direct.GetFileInformationByHandleEx(fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileBasicInfo, &info, (uint)Marshal.SizeOf<FILE_BASIC_INFO>()))
                    throw ErrorHelper.GetIoExceptionForLastError();
            }
            return new FileBasicInfo(info);
        }

        /// <summary>
        /// Get the list of data streams for the given handle.
        /// </summary>
        public static IEnumerable<StreamInformation> GetStreamInformationByHandle(SafeFileHandle fileHandle)
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364406.aspx

            // typedef struct _FILE_STREAM_INFO
            // {
            //     DWORD NextEntryOffset;
            //     DWORD StreamNameLength;
            //     LARGE_INTEGER StreamSize;
            //     LARGE_INTEGER StreamAllocationSize;
            //     WCHAR StreamName[1];
            // } FILE_STREAM_INFO, *PFILE_STREAM_INFO;

            return BufferHelper.CachedInvoke<IEnumerable<StreamInformation>, NativeBuffer>((buffer) =>
            {
                unsafe
                {
                    while (!Direct.GetFileInformationByHandleEx(fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileStreamInfo,
                        buffer.VoidPointer, checked((uint)buffer.ByteCapacity)))
                    {
                        WindowsError error = ErrorHelper.GetLastError();
                        switch (error)
                        {
                            case WindowsError.ERROR_HANDLE_EOF:
                                // No streams
                                return Enumerable.Empty<StreamInformation>();
                            case WindowsError.ERROR_MORE_DATA:
                                buffer.EnsureByteCapacity(buffer.ByteCapacity * 2);
                                break;
                            default:
                                throw ErrorHelper.GetIoExceptionForError(error);
                        }
                    }
                }

                var infos = new List<StreamInformation>();
                var reader = new NativeBufferReader(buffer);
                uint offset = 0;

                do
                {
                    reader.ByteOffset = offset;
                    offset = reader.ReadUint();
                    uint nameLength = reader.ReadUint();
                    infos.Add(new StreamInformation
                    {
                        Size = reader.ReadUlong(),
                        AllocationSize = reader.ReadUlong(),
                        Name = reader.ReadString((int)(nameLength / 2))
                    });
                } while (offset != 0);

                return infos;
            });
        }

        /// <summary>
        /// Synchronous wrapper for ReadFile.
        /// </summary>
        /// <param name="fileHandle">Handle to the file to read.</param>
        /// <param name="fileOffset">Offset into the file, or null for current.</param>
        /// <param name="numberOfBytes">Number of bytes to read.</param>
        /// <param name="buffer">Buffer to read bytes into.</param>
        /// <param name="bufferOffset">Buffer offset.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="nameof(numberOfBytes)"/> or <paramref name="nameof(bufferOffset)"/> would overrun the buffer.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buffer"/> or <paramref name="fileHandle"/> is null.</exception>
        /// <returns>The number of bytes read.</returns>
        public static uint ReadFile(SafeFileHandle fileHandle, byte[] buffer, uint numberOfBytes, ulong? fileOffset = null, uint bufferOffset = 0)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (bufferOffset >= buffer.Length) throw new ArgumentOutOfRangeException(nameof(bufferOffset));
            if (numberOfBytes > buffer.Length - bufferOffset) throw new ArgumentOutOfRangeException(nameof(numberOfBytes));

            unsafe
            {
                fixed (byte* pinnedBuffer = buffer)
                {
                    return ReadFile(fileHandle, pinnedBuffer, numberOfBytes, fileOffset, bufferOffset);
                }
            }
        }

        /// <summary>
        /// Synchronous wrapper for ReadFile.
        /// </summary>
        /// <param name="fileHandle">Handle to the file to read.</param>
        /// <param name="fileOffset">Offset into the file, or null for current.</param>
        /// <param name="numberOfBytes">Number of bytes to read.</param>
        /// <param name="buffer">Buffer to read bytes into.</param>
        /// <param name="bufferOffset">Buffer offset.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buffer"/> or <paramref name="fileHandle"/> is null.</exception>
        /// <returns>The number of bytes read.</returns>
        public static uint ReadFile(SafeFileHandle fileHandle, IntPtr buffer, uint numberOfBytes, ulong? fileOffset = null, uint bufferOffset = 0)
        {
            return ReadFile(fileHandle, buffer, numberOfBytes, fileOffset, bufferOffset);
        }

        /// <summary>
        /// Synchronous wrapper for ReadFile.
        /// </summary>
        /// <param name="fileHandle">Handle to the file to read.</param>
        /// <param name="fileOffset">Offset into the file, or null for current.</param>
        /// <param name="numberOfBytes">Number of bytes to read.</param>
        /// <param name="buffer">Buffer to read bytes into.</param>
        /// <param name="bufferOffset">Buffer offset.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buffer"/> or <paramref name="fileHandle"/> is null.</exception>
        /// <returns>The number of bytes read.</returns>
        unsafe public static uint ReadFile(SafeFileHandle fileHandle, byte* buffer, uint numberOfBytes, ulong? fileOffset = null, uint bufferOffset = 0)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (fileHandle == null) throw new ArgumentNullException("fileHandle");
            if (fileHandle.IsClosed | fileHandle.IsInvalid) throw new ArgumentException("fileHandle");

            uint numberOfBytesRead;
            buffer = buffer + bufferOffset;

            if (fileOffset.HasValue)
            {
                OVERLAPPED overlapped = new OVERLAPPED { Offset = fileOffset.Value };
                if (!Direct.ReadFile(fileHandle, buffer, numberOfBytes, &numberOfBytesRead, &overlapped))
                    throw ErrorHelper.GetIoExceptionForLastError();
            }
            else
            {
                if (!Direct.ReadFile(fileHandle, buffer, numberOfBytes, &numberOfBytesRead, null))
                    throw ErrorHelper.GetIoExceptionForLastError();
            }

            return numberOfBytesRead;
        }

        /// <summary>
        /// Synchronous wrapper for WriteFile.
        /// </summary>
        /// <param name="fileHandle">Handle to the file to write.</param>
        /// <param name="fileOffset">Offset into the file, or null for current.</param>
        /// <param name="numberOfBytes">Number of bytes to write.</param>
        /// <param name="buffer">Buffer to write bytes to.</param>
        /// <param name="bufferOffset">Buffer offset.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the <paramref name="nameof(numberOfBytes)"/> or <paramref name="nameof(bufferOffset)"/> would overrun the buffer.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buffer"/> or <paramref name="fileHandle"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buffer"/> or <paramref name="fileHandle"/> is null.</exception>
        /// <returns>The number of bytes written.</returns>
        public static uint WriteFile(SafeFileHandle fileHandle, byte[] buffer, uint? numberOfBytes = null, ulong? fileOffset = null, uint bufferOffset = 0)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (bufferOffset >= buffer.Length) throw new ArgumentOutOfRangeException(nameof(bufferOffset));

            uint actualBytes;
            if (numberOfBytes.HasValue)
            {
                if (numberOfBytes > buffer.Length - bufferOffset) throw new ArgumentOutOfRangeException(nameof(numberOfBytes));
                actualBytes = numberOfBytes.Value;
            }
            else
            {
                actualBytes = (uint)buffer.Length;
            }

            unsafe
            {
                fixed (byte* pinnedBuffer = buffer)
                {
                    return WriteFile(fileHandle, pinnedBuffer, actualBytes, fileOffset, bufferOffset);
                }
            }
        }

        /// <summary>
        /// Synchronous wrapper for WriteFile.
        /// </summary>
        /// <param name="fileHandle">Handle to the file to write.</param>
        /// <param name="fileOffset">Offset into the file, or null for current.</param>
        /// <param name="numberOfBytes">Number of bytes to write.</param>
        /// <param name="buffer">Buffer to write bytes to.</param>
        /// <param name="bufferOffset">Buffer offset.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buffer"/> or <paramref name="fileHandle"/> is null.</exception>
        /// <returns>The number of bytes written.</returns>
        public static uint WriteFile(SafeFileHandle fileHandle, IntPtr buffer, uint numberOfBytes, ulong? fileOffset = null, uint bufferOffset = 0)
        {
            return WriteFile(fileHandle, buffer, numberOfBytes, fileOffset, bufferOffset);
        }

        /// <summary>
        /// Synchronous wrapper for WriteFile.
        /// </summary>
        /// <param name="fileHandle">Handle to the file to write.</param>
        /// <param name="fileOffset">Offset into the file, or null for current.</param>
        /// <param name="numberOfBytes">Number of bytes to write.</param>
        /// <param name="buffer">Buffer to read bytes from.</param>
        /// <param name="bufferOffset">Buffer offset.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buffer"/> or <paramref name="fileHandle"/> is null.</exception>
        /// <returns>The number of bytes written.</returns>
        unsafe public static uint WriteFile(SafeFileHandle fileHandle, byte* buffer, uint numberOfBytes, ulong? fileOffset = null, uint bufferOffset = 0)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (fileHandle == null) throw new ArgumentNullException("fileHandle");
            if (fileHandle.IsClosed | fileHandle.IsInvalid) throw new ArgumentException("fileHandle");

            uint numberOfBytesWritten;
            buffer = buffer + bufferOffset;

            if (fileOffset.HasValue)
            {
                OVERLAPPED overlapped = new OVERLAPPED { Offset = fileOffset.Value };
                if (!Direct.WriteFile(fileHandle, buffer, numberOfBytes, &numberOfBytesWritten, &overlapped))
                    throw ErrorHelper.GetIoExceptionForLastError();
            }
            else
            {
                if (!Direct.WriteFile(fileHandle, buffer, numberOfBytes, &numberOfBytesWritten, null))
                    throw ErrorHelper.GetIoExceptionForLastError();
            }

            return numberOfBytesWritten;
        }

        /// <summary>
        /// Set the file pointer position for the given file.
        /// </summary>
        /// <param name="distance">Offset.</param>
        /// <param name="moveMethod">Start position.</param>
        /// <returns>The new pointer position.</returns>
        public static long SetFilePointer(SafeFileHandle fileHandle, long distance, MoveMethod moveMethod)
        {
            long position;

            if (!Direct.SetFilePointerEx(fileHandle, distance, out position, moveMethod))
                throw ErrorHelper.GetIoExceptionForLastError();

            return position;
        }

        /// <summary>
        /// Get the position of the pointer for the given file.
        /// </summary>
        public static long GetFilePointer(SafeFileHandle fileHandle)
        {
            return SetFilePointer(fileHandle, 0, MoveMethod.FILE_CURRENT);
        }

        /// <summary>
        /// Get the size of the given file.
        /// </summary>
        public static long GetFileSize(SafeFileHandle fileHandle)
        {
            long size;
            if (!Direct.GetFileSizeEx(fileHandle, out size))
                throw ErrorHelper.GetIoExceptionForLastError();

            return size;
        }

        /// <summary>
        /// Get the type of the given file handle.
        /// </summary>
        public static FileType GetFileType(SafeFileHandle fileHandle)
        {
            FileType fileType = Direct.GetFileType(fileHandle);
            if (fileType == FileType.FILE_TYPE_UNKNOWN)
            {
                WindowsError error = ErrorHelper.GetLastError();
                if (error != WindowsError.NO_ERROR)
                    throw ErrorHelper.GetIoExceptionForLastError();
            }

            return fileType;
        }
    }
}
