// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.FileManagement.BufferWrappers;
using WInterop.FileManagement.Types;
using WInterop.Handles.Types;
using WInterop.SafeString.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.FileManagement
{
    public static partial class FileMethods
    {
        /// <summary>
        /// Get the long (non 8.3) path version of the given path.
        /// </summary>
        public static string GetLongPathName(string path)
        {
            var wrapper = new LongPathNameWrapper { Path = path };
            return BufferHelper.ApiInvoke(ref wrapper, path);
        }

        /// <summary>
        /// Get the short (8.3) path version of the given path.
        /// </summary>
        public static string GetShortPathName(string path)
        {
            var wrapper = new ShortPathNameWrapper { Path = path };
            return BufferHelper.ApiInvoke(ref wrapper, path);
        }

        /// <summary>
        /// Gets a canonical version of the given handle's path.
        /// </summary>
        public static string GetFinalPathNameByHandle(
            SafeFileHandle fileHandle,
            GetFinalPathNameByHandleFlags flags = GetFinalPathNameByHandleFlags.FILE_NAME_NORMALIZED | GetFinalPathNameByHandleFlags.VOLUME_NAME_DOS)
        {
            var wrapper = new FinalPathNameByHandleWrapper { FileHandle = fileHandle, Flags = flags };
            return BufferHelper.ApiInvoke(ref wrapper);
        }

        /// <summary>
        /// Gets a canonical version of the given file's path.
        /// </summary>
        /// <param name="resolveLinks">True to get the destination of links rather than the link itself.</param>
        public static string GetFinalPathName(string path, GetFinalPathNameByHandleFlags finalPathFlags, bool resolveLinks)
        {
            if (path == null) return null;

            // BackupSemantics is needed to get directory handles
            FileFlags flags = FileFlags.BackupSemantics;
            if (!resolveLinks) flags |= FileFlags.OpenReparsePoint;

            using (SafeFileHandle fileHandle = CreateFile(path, CreationDisposition.OpenExisting, 0, ShareMode.ReadWrite,
                FileAttributes.None, flags))
            {
                return GetFinalPathNameByHandle(fileHandle, finalPathFlags);
            }
        }

        /// <summary>
        /// Gets the file information for the given handle.
        /// </summary>
        public static BY_HANDLE_FILE_INFORMATION GetFileInformationByHandle(SafeFileHandle fileHandle)
        {
            if (!Imports.GetFileInformationByHandle(fileHandle, out BY_HANDLE_FILE_INFORMATION fileInformation))
                throw Errors.GetIoExceptionForLastError();

            return fileInformation;
        }

        /// <summary>
        /// Creates symbolic links.
        /// </summary>
        public static void CreateSymbolicLink(string symbolicLinkPath, string targetPath, bool targetIsDirectory = false)
        {
            if (!Imports.CreateSymbolicLinkW(symbolicLinkPath, targetPath,
                targetIsDirectory ? SYMBOLIC_LINK_FLAG.SYMBOLIC_LINK_FLAG_DIRECTORY : SYMBOLIC_LINK_FLAG.SYMBOLIC_LINK_FLAG_FILE))
                throw Errors.GetIoExceptionForLastError(symbolicLinkPath);
        }

        /// <summary>
        /// CreateFile wrapper. Desktop only. Prefer FileManagement.CreateFile() as it will handle all supported platforms.
        /// </summary>
        /// <remarks>Not available in Windows Store applications.</remarks>
        public unsafe static SafeFileHandle CreateFileW(
            string path,
            DesiredAccess desiredAccess,
            ShareMode shareMode,
            CreationDisposition creationDisposition,
            FileAttributes fileAttributes = FileAttributes.None,
            FileFlags fileFlags = FileFlags.None,
            SecurityQosFlags securityQosFlags = SecurityQosFlags.None)
        {
            uint flags = (uint)fileAttributes | (uint)fileFlags | (uint)securityQosFlags;

            SafeFileHandle handle = Imports.CreateFileW(path, desiredAccess, shareMode, null, creationDisposition, flags, IntPtr.Zero);
            if (handle.IsInvalid)
                throw Errors.GetIoExceptionForLastError(path);
            return handle;
        }

        /// <summary>
        /// NtCreateFile wrapper.
        /// </summary>
        public unsafe static SafeFileHandle CreateFileDirect(
            string path,
            CreateDisposition createDisposition,
            DesiredAccess desiredAccess = DesiredAccess.GenericReadWrite | DesiredAccess.Synchronize,
            ShareMode shareAccess = ShareMode.ReadWrite,
            FileAttributes fileAttributes = FileAttributes.None,
            CreateOptions createOptions = CreateOptions.SynchronousIoNonalert,
            ObjectAttributes objectAttributes = ObjectAttributes.CaseInsensitive)
        {
            return CreateFileRelative(path, null, createDisposition, desiredAccess, shareAccess,
                fileAttributes, createOptions, objectAttributes);
        }

        public unsafe static SafeFileHandle CreateFileRelative(
            string path,
            SafeFileHandle rootDirectory,
            CreateDisposition createDisposition,
            DesiredAccess desiredAccess = DesiredAccess.GenericReadWrite | DesiredAccess.Synchronize,
            ShareMode shareAccess = ShareMode.ReadWrite,
            FileAttributes fileAttributes = FileAttributes.None,
            CreateOptions createOptions = CreateOptions.SynchronousIoNonalert,
            ObjectAttributes objectAttributes = ObjectAttributes.CaseInsensitive)
        {
            bool refcounted = false;
            try
            {
                rootDirectory?.DangerousAddRef(ref refcounted);
                return new SafeFileHandle(
                    CreateFileRelative(path, rootDirectory?.DangerousGetHandle() ?? IntPtr.Zero, createDisposition, desiredAccess,
                        shareAccess, fileAttributes, createOptions, objectAttributes),
                    true);
            }
            finally
            {
                if (refcounted)
                    rootDirectory.DangerousRelease();
            }
        }

        public unsafe static IntPtr CreateFileRelative(
            string path,
            IntPtr rootDirectory,
            CreateDisposition createDisposition,
            DesiredAccess desiredAccess = DesiredAccess.GenericReadWrite | DesiredAccess.Synchronize,
            ShareMode shareAccess = ShareMode.ReadWrite,
            FileAttributes fileAttributes = FileAttributes.None,
            CreateOptions createOptions = CreateOptions.SynchronousIoNonalert,
            ObjectAttributes objectAttributes = ObjectAttributes.CaseInsensitive)
        {
            fixed (char* c = path)
            {
                UNICODE_STRING name = new UNICODE_STRING(c, path.Length);
                OBJECT_ATTRIBUTES attributes = new OBJECT_ATTRIBUTES(
                    &name,
                    objectAttributes,
                    rootDirectory,
                    null,
                    null);

                NTSTATUS status = Imports.NtCreateFile(
                    out IntPtr handle,
                    desiredAccess,
                    ref attributes,
                    out IO_STATUS_BLOCK statusBlock,
                    AllocationSize: null,
                    FileAttributes: fileAttributes,
                    ShareAccess: shareAccess,
                    CreateDisposition: createDisposition,
                    CreateOptions: createOptions,
                    EaBuffer: null,
                    EaLength: 0);

                if (status != NTSTATUS.STATUS_SUCCESS)
                    throw ErrorMethods.GetIoExceptionForNTStatus(status, path);

                return handle;
            }
        }

        /// <summary>
        /// CopyFileEx wrapper. Desktop only. Prefer FileManagement.CopyFile() as it will handle all supported platforms.
        /// </summary>
        /// <param name="overwrite">Overwrite an existing file if true.</param>
        public static void CopyFileEx(string source, string destination, bool overwrite = false)
        {
            bool cancel = false;

            if (!Imports.CopyFileExW(
                lpExistingFileName: source,
                lpNewFileName: destination,
                lpProgressRoutine: null,
                lpData: IntPtr.Zero,
                pbCancel: ref cancel,
                dwCopyFlags: overwrite ? 0 : CopyFileFlags.COPY_FILE_FAIL_IF_EXISTS))
            {
                throw Errors.GetIoExceptionForLastError(source);
            }
        }

        /// <summary>
        /// Gets file attributes for the given path.
        /// </summary>
        /// <remarks>Not available in Store apps, use FileMethods.GetFileInfo instead.</remarks>
        public static FileAttributes GetFileAttributes(string path)
        {
            FileAttributes attributes = Imports.GetFileAttributesW(path);
            if (attributes == FileAttributes.Invalid)
                throw Errors.GetIoExceptionForLastError(path);

            return attributes;
        }

        public static string GetFileName(SafeFileHandle fileHandle)
        {
            // https://msdn.microsoft.com/en-us/library/windows/hardware/ff545817.aspx

            //  typedef struct _FILE_NAME_INFORMATION
            //  {
            //      ULONG FileNameLength;
            //      WCHAR FileName[1];
            //  } FILE_NAME_INFORMATION, *PFILE_NAME_INFORMATION;

            return GetFileInformationString(fileHandle, FileInformationClass.FileNameInformation);
        }

        public static string GetVolumeName(SafeFileHandle fileHandle)
        {
            // Same basic structure as FILE_NAME_INFORMATION
            return GetFileInformationString(fileHandle, FileInformationClass.FileVolumeNameInformation);
        }

        /// <summary>
        /// This is the short name for the file/directory name, not the path. Available from WindowsStore.
        /// </summary>
        public static string GetShortName(SafeFileHandle fileHandle)
        {
            // Same basic structure as FILE_NAME_INFORMATION
            return GetFileInformationString(fileHandle, FileInformationClass.FileAlternateNameInformation);
        }

        private unsafe static string GetFileInformationString(SafeFileHandle fileHandle, FileInformationClass fileInformationClass)
        {
            return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                NTSTATUS status = NTSTATUS.STATUS_BUFFER_OVERFLOW;

                // Start with MAX_PATH
                uint byteLength = 260 * sizeof(char);

                FILE_NAME_INFORMATION* value = null;

                while (status == NTSTATUS.STATUS_BUFFER_OVERFLOW)
                {
                    // Add space for the FileNameLength
                    buffer.EnsureByteCapacity(byteLength + sizeof(uint));

                    unsafe
                    {
                        status = Imports.NtQueryInformationFile(
                            FileHandle: fileHandle,
                            IoStatusBlock: out _,
                            FileInformation: buffer.VoidPointer,
                            Length: checked((uint)buffer.ByteCapacity),
                            FileInformationClass: fileInformationClass);
                    }

                    if (status == NTSTATUS.STATUS_SUCCESS || status == NTSTATUS.STATUS_BUFFER_OVERFLOW)
                    {
                        value = (FILE_NAME_INFORMATION*)buffer.VoidPointer;
                        byteLength = value->FileNameLength;
                    }
                }

                if (status != NTSTATUS.STATUS_SUCCESS)
                    throw ErrorMethods.GetIoExceptionForNTStatus(status);

                return value->FileName.CreateString();
            });
        }

        unsafe private static void GetFileInformation(SafeFileHandle fileHandle, FileInformationClass fileInformationClass, void* value, uint size)
        {
            NTSTATUS status = Imports.NtQueryInformationFile(
                FileHandle: fileHandle,
                IoStatusBlock: out _,
                FileInformation: value,
                Length: size,
                FileInformationClass: fileInformationClass);

            if (status != NTSTATUS.STATUS_SUCCESS)
                throw ErrorMethods.GetIoExceptionForNTStatus(status);
        }

        /// <summary>
        /// Gets the file mode for the given handle.
        /// </summary>
        public static FileAccessMode GetFileMode(SafeFileHandle fileHandle)
        {
            FileAccessMode info;
            unsafe
            {
                GetFileInformation(fileHandle, FileInformationClass.FileModeInformation, &info, sizeof(FileAccessMode));
            }
            return info;
        }

        /// <summary>
        /// Return whether or not the given expression matches the given name. Takes standard
        /// Windows wildcards (*, ?, &lt;, &gt; &quot;).
        /// </summary>
        public unsafe static bool IsNameInExpression(string expression, string name, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(expression) || string.IsNullOrEmpty(name))
                return false;

            // If ignore case is set, the API will uppercase the name *if* an UpcaseTable
            // is not provided. It then flips to case-sensitive. In this state the expression
            // has to be uppercase to match as expected.

            fixed (char* e = ignoreCase ? expression.ToUpperInvariant() : expression)
            fixed (char* n = name)
            {
                UNICODE_STRING* eus = null;
                UNICODE_STRING* nus = null;

                if (e != null)
                {
                    var temp = new UNICODE_STRING(e, expression.Length);
                    eus = &temp;
                }
                if (n != null)
                {
                    var temp = new UNICODE_STRING(n, name.Length);
                    nus = &temp;
                }

                return Imports.RtlIsNameInExpression(eus, nus, ignoreCase, IntPtr.Zero);
            }
        }

        public unsafe static FileAccessRights GetRights(SafeFileHandle fileHandle)
        {
            FILE_ACCESS_INFORMATION access = new FILE_ACCESS_INFORMATION();
            NTSTATUS result = Imports.NtQueryInformationFile(fileHandle, out _,
                &access, (uint)sizeof(FILE_ACCESS_INFORMATION), FileInformationClass.FileAccessInformation);
            if (result != NTSTATUS.STATUS_SUCCESS)
                throw ErrorMethods.GetIoExceptionForNTStatus(result);
            return access.AccessFlags;
        }
    }
}
