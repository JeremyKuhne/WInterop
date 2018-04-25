﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WInterop.Authorization;
using WInterop.Authorization.Types;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.FileManagement.BufferWrappers;
using WInterop.FileManagement.Types;
using WInterop.MemoryManagement;
using WInterop.Support;
using WInterop.Support.Buffers;
using WInterop.Synchronization.Types;

namespace WInterop.FileManagement
{
    public static partial class FileMethods
    {
        // Asynchronous Disk I/O Appears as Synchronous on Windows
        // https://support.microsoft.com/en-us/kb/156932
        //
        // NTFS File Attributes
        // https://blogs.technet.microsoft.com/askcore/2010/08/25/ntfs-file-attributes/

        /// <summary>
        /// Get the temporary directory path.
        /// </summary>
        public static string GetTempPath()
        {
            var wrapper = new TempPathWrapper();
            return BufferHelper.ApiInvoke(ref wrapper);
        }

        /// <summary>
        /// Get the fully resolved path name.
        /// </summary>
        public static string GetFullPathName(string path)
        {
            var wrapper = new FullPathNameWrapper { Path = path };
            return BufferHelper.ApiInvoke(ref wrapper, path);
        }

        /// <summary>
        /// Get a temporary file name. Creates a 0 length file.
        /// </summary>
        /// <param name="path">The directory for the file.</param>
        /// <param name="prefix">Three character prefix for the filename.</param>
        public static string GetTempFileName(string path, string prefix)
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                buffer.EnsureCharCapacity(Paths.MaxPath);
                uint result = Imports.GetTempFileNameW(
                    lpPathName: path,
                    lpPrefixString: prefix,
                    uUnique: 0,
                    lpTempFileName: buffer);

                if (result == 0) throw Errors.GetIoExceptionForLastError(path);

                buffer.SetLengthToFirstNull();
                return buffer.ToString();
            });
        }

        /// <summary>
        /// Delete the given file.
        /// </summary>
        public static void DeleteFile(string path)
        {
            if (!Imports.DeleteFileW(path))
                throw Errors.GetIoExceptionForLastError(path);
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
            FileFlags fileFlags = FileFlags.None,
            SecurityQosFlags securityFlags = SecurityQosFlags.None)
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
            ShareModes shareMode,
            CreationDisposition creationDisposition,
            FileAttributes fileAttributes = FileAttributes.None,
            FileFlags fileFlags = FileFlags.None,
            SecurityQosFlags securityQosFlags = SecurityQosFlags.None)
        {
            var fileHandle = CreateFile(path, creationDisposition, desiredAccess, shareMode, fileAttributes, fileFlags, securityQosFlags);

            // FileStream will own the lifetime of the handle
            return new System.IO.FileStream(
                handle: fileHandle,
                access: Conversion.DesiredAccessToFileAccess(desiredAccess),
                bufferSize: 4096,
                isAsync: (fileFlags & FileFlags.Overlapped) != 0);
        }

        private delegate SafeFileHandle CreateFileDelegate(
            string path,
            DesiredAccess desiredAccess,
            ShareModes shareMode,
            CreationDisposition creationDisposition,
            FileAttributes fileAttributes,
            FileFlags fileFlags,
            SecurityQosFlags securityQosFlags);

        private static CreateFileDelegate s_createFileDelegate;

        /// <summary>
        /// Wrapper that allows using System.IO defines where available. Calls CreateFile2 if available.
        /// </summary>
        public static SafeFileHandle CreateFileSystemIo(
            string path,
            System.IO.FileAccess fileAccess,
            System.IO.FileShare fileShare,
            System.IO.FileMode fileMode,
            System.IO.FileAttributes fileAttributes = 0,
            FileFlags fileFlags = FileFlags.None,
            SecurityQosFlags securityFlags = SecurityQosFlags.None)
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
            CreationDisposition creationDisposition,
            DesiredAccess desiredAccess = DesiredAccess.GenericReadWrite,
            ShareModes shareMode = ShareModes.ReadWrite,
            FileAttributes fileAttributes = FileAttributes.None,
            FileFlags fileFlags = FileFlags.None,
            SecurityQosFlags securityQosFlags = SecurityQosFlags.None)
        {
            // Prefer CreateFile2, falling back to CreateFileEx if we can
            if (s_createFileDelegate == null)
            {
                s_createFileDelegate = CreateFile2;
                try
                {
                    return s_createFileDelegate(path, desiredAccess, shareMode, creationDisposition, fileAttributes, fileFlags, securityQosFlags);
                }
                catch (EntryPointNotFoundException)
                {
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
        public static unsafe SafeFileHandle CreateFile2(
            string path,
            DesiredAccess desiredAccess,
            ShareModes shareMode,
            CreationDisposition creationDisposition,
            FileAttributes fileAttributes = FileAttributes.None,
            FileFlags fileFlags = FileFlags.None,
            SecurityQosFlags securityQosFlags = SecurityQosFlags.None)
        {
            CREATEFILE2_EXTENDED_PARAMETERS extended = new CREATEFILE2_EXTENDED_PARAMETERS()
            {
                dwSize = (uint)sizeof(CREATEFILE2_EXTENDED_PARAMETERS),
                dwFileAttributes = fileAttributes,
                dwFileFlags = fileFlags,
                dwSecurityQosFlags = securityQosFlags
            };

            SafeFileHandle handle = Imports.CreateFile2(
                lpFileName: path,
                dwDesiredAccess: desiredAccess,
                dwShareMode: shareMode,
                dwCreationDisposition: creationDisposition,
                pCreateExParams: ref extended);

            if (handle.IsInvalid)
                throw Errors.GetIoExceptionForLastError(path);

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
                catch (EntryPointNotFoundException)
                {
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
                COPYFILE2_EXTENDED_PARAMETERS parameters = new COPYFILE2_EXTENDED_PARAMETERS()
                {
                    dwSize = (uint)sizeof(COPYFILE2_EXTENDED_PARAMETERS),
                    pfCanel = &cancel,
                    dwCopyFlags = overwrite ? 0 : CopyFileFlags.COPY_FILE_FAIL_IF_EXISTS
                };

                HRESULT hr = Imports.CopyFile2(source, destination, &parameters);
                if (ErrorMacros.FAILED(hr))
                    throw Errors.GetIoExceptionForHResult(hr, source);
            }
        }

        /// <summary>
        /// Creates a wrapper for finding files.
        /// </summary>
        /// <param name="directory">The directory to search in.</param>
        /// <param name="recursive">True to find files recursively.</param>
        /// <param name="nameFilter">
        /// The filename filter. Can contain wildcards, full details can be found at
        /// <a href="https://msdn.microsoft.com/en-us/library/ff469270.aspx">[MS-FSA] 2.1.4.4 Algorithm for Determining if a FileName Is in an Expression</a>.
        /// If custom <paramref name="findFilter"/> is specified this parameter is ignored.
        /// </param>
        /// <param name="findFilter">Custom filter, if default behavior isn't desired. (Which is no "." or "..", and use the filter string.)</param>
        /// <param name="findTransform">Custom transform, if the default transform isn't desired.</param>
        public static FindOperation<T> CreateFindOperation<T>(
            string directory,
            string nameFilter = "*",
            bool recursive = false,
            IFindTransform<T> findTransform = null,
            IFindFilter findFilter = null)
        {
            return new FindOperation<T>(directory, nameFilter, recursive, findTransform, findFilter);
        }

        /// <summary>
        /// Gets the file attributes for the given path.
        /// </summary>
        public static WIN32_FILE_ATTRIBUTE_DATA GetFileAttributesEx(string path)
        {
            if (!Imports.GetFileAttributesExW(path, GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, out WIN32_FILE_ATTRIBUTE_DATA data))
                throw Errors.GetIoExceptionForLastError(path);

            return data;
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
            return data.HasValue && (data.Value.dwFileAttributes & FileAttributes.Directory) != FileAttributes.Directory;
        }

        /// <summary>
        /// Simple wrapper to check if a given path exists and is a directory.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException">Thrown if there aren't rights to get attributes on the given path.</exception>
        public static bool DirectoryExists(string path)
        {
            var data = TryGetFileInfo(path);
            return data.HasValue && (data.Value.dwFileAttributes & FileAttributes.Directory) == FileAttributes.Directory;
        }

        /// <summary>
        /// Tries to get file info, returns null if the given path doesn't exist.
        /// </summary>
        /// <exception cref="UnauthorizedAccessException">Thrown if there aren't rights to get attributes on the given path.</exception>
        public static WIN32_FILE_ATTRIBUTE_DATA? TryGetFileInfo(string path)
        {
            if (!Imports.GetFileAttributesExW(path, GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, out WIN32_FILE_ATTRIBUTE_DATA data))
            {
                WindowsError error = Errors.GetLastError();
                switch (error)
                {
                    case WindowsError.ERROR_ACCESS_DENIED:
                    case WindowsError.ERROR_NETWORK_ACCESS_DENIED:
                        throw Errors.GetIoExceptionForError(error, path);
                    case WindowsError.ERROR_PATH_NOT_FOUND:
                    default:
                        return null;
                }
            }

            return data;
        }

        /// <summary>
        /// Sets the file attributes for the given path.
        /// </summary>
        public static void SetFileAttributes(string path, FileAttributes attributes)
        {
            if (!Imports.SetFileAttributesW(path, attributes))
                throw Errors.GetIoExceptionForLastError(path);
        }

        /// <summary>
        /// Flush file buffers.
        /// </summary>
        public static void FlushFileBuffers(SafeFileHandle fileHandle)
        {
            if (!Imports.FlushFileBuffers(fileHandle))
                throw Errors.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Gets the file name from the given handle. This uses GetFileInformationByHandleEx, which does not give back the volume
        /// name for the path- but is available from Windows Store apps.
        /// </summary>
        /// <remarks>
        /// The exact data that is returned is somewhat complicated and is described in the documentation for ZwQueryInformationFile.
        /// </remarks>
        public unsafe static string GetFileNameLegacy(SafeFileHandle fileHandle)
        {
            return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                unsafe
                {
                    while (!Imports.GetFileInformationByHandleEx(
                        fileHandle,
                        FileInfoClass.FileNameInfo,
                        buffer.VoidPointer,
                        checked((uint)buffer.ByteCapacity)))
                    {
                        WindowsError error = Errors.GetLastError();
                        if (error != WindowsError.ERROR_MORE_DATA)
                            throw Errors.GetIoExceptionForError(error);
                        buffer.EnsureByteCapacity(buffer.ByteCapacity * 2);
                    }
                }

                return ((FILE_NAME_INFORMATION*)buffer.VoidPointer)->FileName.CreateString();
            });
        }

        /// <summary>
        /// Get standard file info from the given file handle.
        /// </summary>
        public static FileStandardInfo GetFileStandardInformation(SafeFileHandle fileHandle)
        {
            FILE_STANDARD_INFORMATION info;
            unsafe
            {
                if (!Imports.GetFileInformationByHandleEx(
                    fileHandle,
                    FileInfoClass.FileStandardInfo,
                    &info,
                    (uint)sizeof(FILE_STANDARD_INFORMATION)))
                    throw Errors.GetIoExceptionForLastError();
            }

            return new FileStandardInfo(info);
        }

        /// <summary>
        /// Get basic file info from the given file handle.
        /// </summary>
        public static FileBasicInfo GetFileBasicInformation(SafeFileHandle fileHandle)
        {
            FILE_BASIC_INFORMATION info;
            unsafe
            {
                if (!Imports.GetFileInformationByHandleEx(
                    fileHandle,
                    FileInfoClass.FileBasicInfo,
                    &info,
                    (uint)sizeof(FILE_BASIC_INFORMATION)))
                    throw Errors.GetIoExceptionForLastError();
            }
            return new FileBasicInfo(info);
        }

        /// <summary>
        /// Get the list of data streams for the given handle.
        /// </summary>
        public unsafe static IEnumerable<StreamInformation> GetStreamInformation(SafeFileHandle fileHandle)
        {
            return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                unsafe
                {
                    while (!Imports.GetFileInformationByHandleEx(fileHandle, FileInfoClass.FileStreamInfo,
                        buffer.VoidPointer, checked((uint)buffer.ByteCapacity)))
                    {
                        WindowsError error = Errors.GetLastError();
                        switch (error)
                        {
                            case WindowsError.ERROR_HANDLE_EOF:
                                // No streams
                                return Enumerable.Empty<StreamInformation>();
                            case WindowsError.ERROR_MORE_DATA:
                                buffer.EnsureByteCapacity(buffer.ByteCapacity * 2);
                                break;
                            default:
                                throw Errors.GetIoExceptionForError(error);
                        }
                    }
                }

                return StreamInformation.Create((FILE_STREAM_INFORMATION*)buffer.VoidPointer);
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
        public unsafe static uint ReadFile(SafeFileHandle fileHandle, byte* buffer, uint numberOfBytes, ulong? fileOffset = null, uint bufferOffset = 0)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (fileHandle == null) throw new ArgumentNullException("fileHandle");
            if (fileHandle.IsClosed | fileHandle.IsInvalid) throw new ArgumentException("fileHandle");

            uint numberOfBytesRead;
            buffer = buffer + bufferOffset;

            if (fileOffset.HasValue)
            {
                OVERLAPPED overlapped = new OVERLAPPED { Offset = fileOffset.Value };
                if (!Imports.ReadFile(fileHandle, buffer, numberOfBytes, &numberOfBytesRead, &overlapped))
                    throw Errors.GetIoExceptionForLastError();
            }
            else
            {
                if (!Imports.ReadFile(fileHandle, buffer, numberOfBytes, &numberOfBytesRead, null))
                    throw Errors.GetIoExceptionForLastError();
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
        public unsafe static uint WriteFile(SafeFileHandle fileHandle, byte* buffer, uint numberOfBytes, ulong? fileOffset = null, uint bufferOffset = 0)
        {
            if (buffer == null) throw new ArgumentNullException("buffer");
            if (fileHandle == null) throw new ArgumentNullException("fileHandle");
            if (fileHandle.IsClosed | fileHandle.IsInvalid) throw new ArgumentException("fileHandle");

            uint numberOfBytesWritten;
            buffer = buffer + bufferOffset;

            if (fileOffset.HasValue)
            {
                OVERLAPPED overlapped = new OVERLAPPED { Offset = fileOffset.Value };
                if (!Imports.WriteFile(fileHandle, buffer, numberOfBytes, &numberOfBytesWritten, &overlapped))
                    throw Errors.GetIoExceptionForLastError();
            }
            else
            {
                if (!Imports.WriteFile(fileHandle, buffer, numberOfBytes, &numberOfBytesWritten, null))
                    throw Errors.GetIoExceptionForLastError();
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
            if (!Imports.SetFilePointerEx(fileHandle, distance, out long position, moveMethod))
                throw Errors.GetIoExceptionForLastError();

            return position;
        }

        /// <summary>
        /// Get the position of the pointer for the given file.
        /// </summary>
        public static long GetFilePointer(SafeFileHandle fileHandle)
        {
            return SetFilePointer(fileHandle, 0, MoveMethod.Current);
        }

        /// <summary>
        /// Get the size of the given file.
        /// </summary>
        public static long GetFileSize(SafeFileHandle fileHandle)
        {
            if (!Imports.GetFileSizeEx(fileHandle, out long size))
                throw Errors.GetIoExceptionForLastError();

            return size;
        }

        /// <summary>
        /// Get the type of the given file handle.
        /// </summary>
        public static FileType GetFileType(SafeFileHandle fileHandle)
        {
            FileType fileType = Imports.GetFileType(fileHandle);
            if (fileType == FileType.Unknown)
                Errors.ThrowIfLastErrorNot(WindowsError.NO_ERROR);

            return fileType;
        }

        /// <summary>
        /// Gets the filenames in the specified directory. Includes "." and "..".
        /// </summary>
        public unsafe static IEnumerable<string> GetDirectoryFilenames(SafeFileHandle directoryHandle)
        {
            List<string> filenames = new List<string>();
            GetFullDirectoryInfoHelper(directoryHandle, buffer =>
            {
                FILE_FULL_DIR_INFORMATION* info = (FILE_FULL_DIR_INFORMATION*)buffer.BytePointer;
                do
                {
                    filenames.Add(info->FileName.CreateString());
                    info = FILE_FULL_DIR_INFORMATION.GetNextInfo(info);
                } while (info != null);
            });
            return filenames;
        }

        /// <summary>
        /// Gets all of the info for files within the given directory handle.
        /// </summary>
        public unsafe static IEnumerable<FullFileInformation> GetDirectoryInformation(SafeFileHandle directoryHandle)
        {
            List<FullFileInformation> infos = new List<FullFileInformation>();
            GetFullDirectoryInfoHelper(directoryHandle, buffer =>
            {
                FILE_FULL_DIR_INFORMATION* info = (FILE_FULL_DIR_INFORMATION*)buffer.BytePointer;
                do
                {
                    infos.Add(new FullFileInformation(info));
                    info = FILE_FULL_DIR_INFORMATION.GetNextInfo(info);
                } while (info != null);
            });
            return infos;
        }

        private unsafe static void GetFullDirectoryInfoHelper(SafeFileHandle directoryHandle, Action<HeapBuffer> action)
        {
            BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                // Make sure we have at least enough for the normal "." and ".."
                buffer.EnsureByteCapacity((ulong)sizeof(FILE_FULL_DIR_INFORMATION) * 2);

                do
                {
                    while (!Imports.GetFileInformationByHandleEx(
                        directoryHandle,
                        FileInfoClass.FileFullDirectoryInfo,
                        buffer.VoidPointer,
                        (uint)buffer.ByteCapacity))
                    {
                        var error = Errors.GetLastError();
                        switch (error)
                        {
                            case WindowsError.ERROR_BAD_LENGTH:
                                // Not enough space for the struct data
                                Debug.Fail("Should have properly set a minimum buffer");
                                goto case WindowsError.ERROR_MORE_DATA;
                            case WindowsError.ERROR_MORE_DATA:
                                // Buffer isn't big enough for a filename
                                buffer.EnsureByteCapacity(buffer.ByteCapacity + 512);
                                break;
                            case WindowsError.ERROR_NO_MORE_FILES:
                                // Nothing left to get
                                return;
                            default:
                                throw Errors.GetIoExceptionForError(error);
                        }
                    }

                    action(buffer);
                } while (true);
            });
        }

        /// <summary>
        /// Returns true if the given tag is owned by Microsoft.
        /// </summary>
        public static bool IsReparseTagMicrosoft(ReparseTag reparseTag)
        {
            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff549452.aspx
            return ((uint)reparseTag & 0x80000000) != 0;
        }

        /// <summary>
        /// Returns true if the given tag is a name surrogate.
        /// </summary>
        public static bool IsReparseTagNameSurrogate(ReparseTag reparseTag)
        {
            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff549462.aspx
            return ((uint)reparseTag & 0x20000000) != 0;
        }

        /// <summary>
        /// Returns true if the reparse point can have children.
        /// </summary>
        public static bool IsReparseTagDirectory(ReparseTag reparseTag)
        {
            return ((uint)reparseTag & 0x10000000) != 0;
        }

        /// <summary>
        /// Get the owner SID for the given handle.
        /// </summary>
        public unsafe static void QueryOwner(SafeFileHandle handle, out SID sid)
        {
            SID* sidp;
            SECURITY_DESCRIPTOR* descriptor;

            WindowsError result = AuthorizationMethods.Imports.GetSecurityInfo(
                handle,
                SecurityObjectType.File,
                SecurityInformation.Owner,
                ppsidOwner: &sidp,
                ppSecurityDescriptor: &descriptor);

            if (result != WindowsError.ERROR_SUCCESS)
                throw Errors.GetIoExceptionForError(result);

            SID.CopyFromNative(sidp, out sid);
            MemoryMethods.LocalFree((IntPtr)(descriptor));
        }
    }
}
