// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using WInterop.Authorization;
using WInterop.Buffers;
using WInterop.ErrorHandling;
using WInterop.FileManagement;
using WInterop.FileManagement.CopyFile2;
using WInterop.Handles;
using WInterop.Synchronization;
using WInterop.Utility;

namespace WInterop
{
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
                public static extern SafeFindHandle FindFirstFileExW(
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
                    SafeFindHandle hFindFile,
                    out WIN32_FIND_DATA lpFindFileData);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364413.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l1_1_0, SetLastError = true, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool FindClose(
                    IntPtr hFindFile);

                // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364953.aspx (kernel32)
                [DllImport(ApiSets.api_ms_win_core_file_l2_1_0, SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
                [return: MarshalAs(UnmanagedType.Bool)]
                public static extern bool GetFileInformationByHandleEx(
                    SafeFileHandle hFile,
                    FILE_INFO_BY_HANDLE_CLASS FileInformationClass,
                    IntPtr lpFileInformation,
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
                return BufferInvoke((buffer) => Direct.GetTempPathW(buffer.CharCapacity, buffer));
            }

            /// <summary>
            /// Get the fully resolved path name.
            /// </summary>
            public static string GetFullPathName(string path)
            {
                return BufferInvoke((buffer) => Direct.GetFullPathNameW(path, buffer.CharCapacity, buffer, IntPtr.Zero));
            }

            /// <summary>
            /// Get a temporary file name. Creates a 0 length file.
            /// </summary>
            /// <param name="path">The directory for the file.</param>
            /// <param name="prefix">Three character prefix for the filename.</param>
            public static string GetTempFileName(string path, string prefix)
            {
                return StringBufferCache.CachedBufferInvoke((buffer) =>
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
                // We could also potentially add logic to use CreateFile2 if we're at Win8 or greater. Version checking can only be done if
                // we are running as a normal desktop app.

#if !WINRT
                if (Utility.Environment.IsWindowsStoreApplication())
#endif
                {
                    return CreateFile2(path, desiredAccess, shareMode, creationDisposition, fileAttributes, fileFlags, securityQosFlags);
                }
#if !WINRT
                else
                {
                    return Desktop.CreateFile(path, desiredAccess, shareMode, creationDisposition, fileAttributes, fileFlags, securityQosFlags);
                }
#endif
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
                extended.lpSecurityAttributes = IntPtr.Zero;
                extended.hTemplateFile = IntPtr.Zero;

                SafeFileHandle handle = Direct.CreateFile2(
                    lpFileName: path,
                    dwDesiredAccess: desiredAccess,
                    dwShareMode: shareMode,
                    dwCreationDisposition: creationDisposition,
                    pCreateExParams: ref extended);

                if (handle.IsInvalid)
                    throw ErrorHelper.GetIoExceptionForLastError(path);

                return handle;
            }

            /// <summary>
            /// CopyFile wrapper that attempts to use CopyFile2 if running as Windows Store app.
            /// </summary>
            public static void CopyFile(string source, string destination)
            {
                // We could also potentially add logic to use CreateFile2 if we're at Win8 or greater. Version checking can only be done if
                // we are running as a normal desktop app.

#if !WINRT
                if (Utility.Environment.IsWindowsStoreApplication())
#endif
                {
                    CopyFile2(source, destination);
                }
#if !WINRT
                else
                {
                    Desktop.CopyFileEx(source, destination);
                }
#endif
            }

            /// <summary>
            /// CopyFile2 wrapper. Only available on Windows8 and above.
            /// </summary>
            public static void CopyFile2(string source, string destination)
            {
                unsafe
                {
                    int hr = Direct.CopyFile2(source, destination, null);
                    if (ErrorHandling.FAILED(hr))
                        throw ErrorHelper.GetIoExceptionForHResult(hr, source);
                }
            }

            /// <summary>
            /// Finds the first file in a file search. Does not take a trailing separator. Returns null if there are no matches.
            /// </summary>
            /// <param name="directoriesOnly">Attempts to filter to just directories where supported.</param>
            /// <param name="getAlternateName">Returns the alternate (short) file name in the FindResult.AlternateName field if it exists.</param>
            public static FindResult FindFirstFile(
                string path,
                bool directoriesOnly = false,
                bool getAlternateName = false)
            {
                WIN32_FIND_DATA findData;
                SafeFindHandle handle = Direct.FindFirstFileExW(
                    path,
                    getAlternateName ? FINDEX_INFO_LEVELS.FindExInfoStandard : FINDEX_INFO_LEVELS.FindExInfoBasic,
                    out findData,
                    // FINDEX_SEARCH_OPS.FindExSearchNameMatch is what FindFirstFile calls Ex wtih
                    directoriesOnly ? FINDEX_SEARCH_OPS.FindExSearchLimitToDirectories : FINDEX_SEARCH_OPS.FindExSearchNameMatch,
                    IntPtr.Zero,
                    FindFirstFileExFlags.FIND_FIRST_EX_LARGE_FETCH);

                if (handle.IsInvalid)
                {
                    uint error = (uint)Marshal.GetLastWin32Error();
                    if (error == WinErrors.ERROR_FILE_NOT_FOUND)
                        return null;
                    throw ErrorHelper.GetIoExceptionForLastError(path);
                }

                return new FindResult(handle, findData, path);
            }

            /// <summary>
            /// Finds the next file for a given search operation.
            /// </summary>
            public static FindResult FindNextFile(FindResult priorResult)
            {
                WIN32_FIND_DATA findData;
                if (!Direct.FindNextFileW(priorResult.FindHandle, out findData))
                {
                    uint error = (uint)Marshal.GetLastWin32Error();
                    if (error == WinErrors.ERROR_NO_MORE_FILES)
                        return null;
                    throw ErrorHelper.GetIoExceptionForLastError(priorResult.OriginalPath);
                }

                return new FindResult(priorResult.FindHandle, findData, priorResult.OriginalPath);
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
                return StringBufferCache.CachedBufferInvoke(Paths.MaxPath, (buffer) =>
                {
                    while (!Direct.GetFileInformationByHandleEx(fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileNameInfo, buffer.DangerousGetHandle(), checked((uint)buffer.ByteCapacity)))
                    {
                        uint error = (uint)Marshal.GetLastWin32Error();
                        if (error != WinErrors.ERROR_MORE_DATA)
                            throw ErrorHelper.GetIoExceptionForError(error);
                        buffer.EnsureByteCapacity(buffer.ByteCapacity * 2);
                    }

                    var reader = new NativeBufferReader(buffer);
                    return reader.ReadString((int)(reader.ReadUint() / 2));
                });
            }

            /// <summary>
            /// Get standard file info from the given file handle.
            /// </summary>
            public static FILE_STANDARD_INFO GetFileStandardInfoByHandle(SafeFileHandle fileHandle)
            {
                return StringBufferCache.CachedBufferInvoke((buffer) =>
                {
                    FILE_STANDARD_INFO info;
                    buffer.EnsureByteCapacity((ulong)Marshal.SizeOf<FILE_STANDARD_INFO>());

                    if (!Direct.GetFileInformationByHandleEx(fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileStandardInfo, buffer.DangerousGetHandle(), checked((uint)buffer.ByteCapacity)))
                        throw ErrorHelper.GetIoExceptionForLastError();

                    info = Marshal.PtrToStructure<FILE_STANDARD_INFO>(buffer.DangerousGetHandle());
                    return info;
                });
            }

            /// <summary>
            /// Get basic file info from the given file handle.
            /// </summary>
            public static FileBasicInfo GetFileBasicInfoByHandle(SafeFileHandle fileHandle)
            {
                return StringBufferCache.CachedBufferInvoke((buffer) =>
                {
                    FILE_BASIC_INFO info;
                    buffer.EnsureByteCapacity((ulong)Marshal.SizeOf<FILE_BASIC_INFO>());

                    if (!Direct.GetFileInformationByHandleEx(fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileBasicInfo, buffer.DangerousGetHandle(), checked((uint)buffer.ByteCapacity)))
                        throw ErrorHelper.GetIoExceptionForLastError();

                    info = Marshal.PtrToStructure<FILE_BASIC_INFO>(buffer.DangerousGetHandle());
                    return new FileBasicInfo(info);
                });
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

                // We'll ensure we have at least 100 characters worth in the buffer to start
                return StringBufferCache.CachedBufferInvoke(100, (buffer) =>
                {
                    while (!Direct.GetFileInformationByHandleEx(fileHandle, FILE_INFO_BY_HANDLE_CLASS.FileStreamInfo, buffer.DangerousGetHandle(), checked((uint)buffer.ByteCapacity)))
                    {
                        uint error = (uint)Marshal.GetLastWin32Error();
                        switch (error)
                        {
                            case WinErrors.ERROR_HANDLE_EOF:
                                // No streams
                                return Enumerable.Empty<StreamInformation>();
                            case WinErrors.ERROR_MORE_DATA:
                                buffer.EnsureByteCapacity(buffer.ByteCapacity * 2);
                                break;
                            default:
                                throw ErrorHelper.GetIoExceptionForError(error);
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
                    throw ErrorHelper.GetIoExceptionForLastError();

                return fileType;
            }
        }
    }
}
