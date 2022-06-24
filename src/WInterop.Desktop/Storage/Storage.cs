// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System.Diagnostics;
using WInterop.Errors;
using WInterop.Handles;
using WInterop.Security;
using WInterop.Storage.Native;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Storage;

public static partial class Storage
{
    /// <summary>
    ///  Get the short (8.3) path version of the given path.
    /// </summary>
    public static unsafe string GetShortPathName(string path)
    {
        static uint Fix(string path, in ValueBuffer<char> buffer)
        {
            fixed (char* p = path)
            fixed (char* c = buffer)
            {
                return TerraFXWindows.GetShortPathNameW((ushort*)p, (ushort*)c, buffer.Length);
            }
        }

        using var buffer = new ValueBuffer<char>(Paths.MaxPath);
        uint result;
        while ((result = Fix(path, buffer)) > buffer.Length)
        {
            buffer.EnsureCapacity((int)result);
        }

        Error.ThrowLastErrorIfZero(result);
        return buffer.Span[.. (int)result].ToString();
    }

    /// <summary>
    ///  Gets file information for the given handle.
    /// </summary>
    public static unsafe ByHandleFileInformation GetFileInformationByHandle(SafeFileHandle fileHandle)
    {
        ByHandleFileInformation fileInformation;
        Error.ThrowLastErrorIfFalse(
            TerraFXWindows.GetFileInformationByHandle(
                fileHandle.ToHANDLE(),
                (BY_HANDLE_FILE_INFORMATION*)&fileInformation));

        return fileInformation;
    }

    /// <summary>
    ///  Creates symbolic links.
    /// </summary>
    public static unsafe void CreateSymbolicLink(string symbolicLinkPath, string targetPath, bool targetIsDirectory = false)
    {
        fixed (char* slp = symbolicLinkPath)
        fixed (char* tp = targetPath)
        {
            Error.ThrowLastErrorIfFalse((ByteBoolean)TerraFXWindows.CreateSymbolicLinkW(
                (ushort*)slp,
                (ushort*)tp,
                targetIsDirectory ? (uint)SymbolicLinkFlag.Directory : (uint)SymbolicLinkFlag.File));
        }
    }

    /// <summary>
    ///  CreateFile wrapper. Desktop only. Prefer File.CreateFile() as it will handle all supported platforms.
    /// </summary>
    /// <remarks>Not available in Windows Store applications.</remarks>
    public static unsafe SafeFileHandle CreateFileW(
        string path,
        DesiredAccess desiredAccess,
        ShareModes shareMode,
        CreationDisposition creationDisposition,
        AllFileAttributes fileAttributes = AllFileAttributes.None,
        FileFlags fileFlags = FileFlags.None,
        SecurityQosFlags securityQosFlags = SecurityQosFlags.None)
    {
        uint flags = (uint)fileAttributes | (uint)fileFlags | (uint)securityQosFlags;

        HANDLE handle;
        fixed (char* p = path)
        {
            handle = TerraFXWindows.CreateFileW(
                (ushort*)p,
                (uint)desiredAccess,
                (uint)shareMode,
                lpSecurityAttributes: null,
                (uint)creationDisposition,
                flags,
                hTemplateFile: HANDLE.NULL);

            if (handle == HANDLE.INVALID_VALUE)
            {
                Error.ThrowLastError(path);
            }
        }

        return new(handle, ownsHandle: true);
    }

    /// <summary>
    ///  NtCreateFile wrapper.
    /// </summary>
    public static unsafe SafeFileHandle CreateFileDirect(
        string path,
        CreateDisposition createDisposition,
        DesiredAccess desiredAccess = DesiredAccess.GenericReadWrite | DesiredAccess.Synchronize,
        ShareModes shareAccess = ShareModes.ReadWrite,
        AllFileAttributes fileAttributes = AllFileAttributes.None,
        CreateOptions createOptions = CreateOptions.SynchronousIoNonalert,
        ObjectAttributes objectAttributes = ObjectAttributes.CaseInsensitive)
    {
        return CreateFileRelative(
            path,
            rootDirectory: null,
            createDisposition,
            desiredAccess,
            shareAccess,
            fileAttributes,
            createOptions,
            objectAttributes);
    }

    /// <summary>
    ///  NtCreateFile wrapper.
    /// </summary>
    public static unsafe SafeFileHandle CreateFileDirect(
        ReadOnlySpan<char> path,
        CreateDisposition createDisposition,
        DesiredAccess desiredAccess = DesiredAccess.GenericReadWrite | DesiredAccess.Synchronize,
        ShareModes shareAccess = ShareModes.ReadWrite,
        AllFileAttributes fileAttributes = AllFileAttributes.None,
        CreateOptions createOptions = CreateOptions.SynchronousIoNonalert,
        ObjectAttributes objectAttributes = ObjectAttributes.CaseInsensitive)
    {
        return CreateFileRelative(
            path,
            rootDirectory: null,
            createDisposition,
            desiredAccess,
            shareAccess,
            fileAttributes,
            createOptions,
            objectAttributes);
    }

    /// <summary>
    ///  Create a file handle using an optional base directory handle.
    /// </summary>
    /// <param name="path">
    ///  Full path (or relative path if <paramref name="rootDirectory"/> is specified.
    /// </param>
    /// <param name="rootDirectory">
    ///  Optional handle to the directory the path is relative to.
    /// </param>
    public static unsafe SafeFileHandle CreateFileRelative(
        ReadOnlySpan<char> path,
        SafeFileHandle? rootDirectory,
        CreateDisposition createDisposition,
        DesiredAccess desiredAccess = DesiredAccess.GenericReadWrite | DesiredAccess.Synchronize,
        ShareModes shareAccess = ShareModes.ReadWrite,
        AllFileAttributes fileAttributes = AllFileAttributes.None,
        CreateOptions createOptions = CreateOptions.SynchronousIoNonalert,
        ObjectAttributes objectAttributes = ObjectAttributes.CaseInsensitive)
    {
        using var rootDirectoryHandle = new UnwrapHandle(rootDirectory);
        return new SafeFileHandle(
            CreateFileRelative(
                path,
                rootDirectoryHandle,
                createDisposition,
                desiredAccess,
                shareAccess,
                fileAttributes,
                createOptions,
                objectAttributes),
            true);
    }

    /// <summary>
    ///  Create a file handle using an optional base directory handle.
    /// </summary>
    /// <param name="path">
    ///  Full path (or relative path if <paramref name="rootDirectory"/> is specified.
    /// </param>
    /// <param name="rootDirectory">
    ///  Optional handle to the directory the path is relative to.
    /// </param>
    public static unsafe IntPtr CreateFileRelative(
        ReadOnlySpan<char> path,
        IntPtr rootDirectory,
        CreateDisposition createDisposition,
        DesiredAccess desiredAccess = DesiredAccess.GenericReadWrite | DesiredAccess.Synchronize,
        ShareModes shareAccess = ShareModes.ReadWrite,
        AllFileAttributes fileAttributes = AllFileAttributes.None,
        CreateOptions createOptions = CreateOptions.SynchronousIoNonalert,
        ObjectAttributes objectAttributes = ObjectAttributes.CaseInsensitive)
    {
        fixed (char* c = path)
        {
            var name = new SafeString.Native.UNICODE_STRING(c, path.Length);
            var attributes = new Handles.Native.OBJECT_ATTRIBUTES(
                &name,
                objectAttributes,
                rootDirectory,
                securityDescriptor: null,
                securityQualityOfService: null);
            StorageImports.NtCreateFile(
                out IntPtr handle,
                desiredAccess,
                ref attributes,
                IoStatusBlock: out _,
                AllocationSize: null,
                FileAttributes: fileAttributes,
                ShareAccess: shareAccess,
                CreateDisposition: createDisposition,
                CreateOptions: createOptions,
                EaBuffer: null,
                EaLength: 0)
                .ThrowIfFailed(path);

            return handle;
        }
    }

    /// <summary>
    ///  Wrapper to create a directory within another directory
    /// </summary>
    public static SafeFileHandle CreateDirectory(SafeFileHandle rootDirectory, string name)
    {
        return CreateFileRelative(
            name,
            rootDirectory,
            CreateDisposition.Create,
            DesiredAccess.ListDirectory | DesiredAccess.Synchronize,
            ShareModes.ReadWrite | ShareModes.Delete,
            AllFileAttributes.None,
            CreateOptions.SynchronousIoNonalert | CreateOptions.DirectoryFile | CreateOptions.OpenForBackupIntent | CreateOptions.OpenReparsePoint);
    }

    /// <summary>
    ///  Creates a directory handle from an existing directory handle.
    /// </summary>
    public static SafeFileHandle CreateDirectoryHandle(SafeFileHandle rootDirectory, string subdirectoryPath)
    {
        return CreateFileRelative(
            subdirectoryPath,
            rootDirectory,
            CreateDisposition.Open,
            DesiredAccess.ListDirectory | DesiredAccess.Synchronize,
            ShareModes.ReadWrite | ShareModes.Delete,
            AllFileAttributes.None,
            CreateOptions.SynchronousIoNonalert | CreateOptions.DirectoryFile | CreateOptions.OpenForBackupIntent | CreateOptions.OpenReparsePoint);
    }

    /// <summary>
    ///  Creates a raw directory handle from an existing directory handle.
    /// </summary>
    public static IntPtr CreateDirectoryHandle(IntPtr rootDirectory, string subdirectoryPath)
    {
        return CreateFileRelative(
            subdirectoryPath,
            rootDirectory,
            CreateDisposition.Open,
            DesiredAccess.ListDirectory | DesiredAccess.Synchronize,
            ShareModes.ReadWrite | ShareModes.Delete,
            AllFileAttributes.None,
            CreateOptions.SynchronousIoNonalert | CreateOptions.DirectoryFile | CreateOptions.OpenForBackupIntent | CreateOptions.OpenReparsePoint);
    }

    /// <summary>
    ///  CopyFileEx wrapper. Desktop only. Prefer File.CopyFile() as it will handle all supported platforms.
    /// </summary>
    /// <param name="overwrite">Overwrite an existing file if true.</param>
    public static unsafe void CopyFileEx(string source, string destination, bool overwrite = false)
    {
        BOOL cancel = false;

        fixed (char* s = source)
        fixed (char* d = destination)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.CopyFileExW(
                    lpExistingFileName: (ushort*)s,
                    lpNewFileName: (ushort*)d,
                    lpProgressRoutine: null,
                    lpData: null,
                    pbCancel: &cancel,
                    dwCopyFlags: overwrite ? 0 : (uint)CopyFileFlags.FailIfExists),
                source);
        }
    }

    public static string GetFileName(SafeFileHandle fileHandle)
    {
        // https://docs.microsoft.com/windows-hardware/drivers/ddi/ntddk/ns-ntddk-_file_name_information

        // typedef struct _FILE_NAME_INFORMATION
        // {
        //     ULONG FileNameLength;
        //     WCHAR FileName[1];
        // } FILE_NAME_INFORMATION, *PFILE_NAME_INFORMATION;

        return GetFileInformationString(fileHandle, FileInformationClass.FileNameInformation);
    }

    public static string GetVolumeName(SafeFileHandle fileHandle)
    {
        // Same basic structure as FILE_NAME_INFORMATION
        return GetFileInformationString(fileHandle, FileInformationClass.FileVolumeNameInformation);
    }

    /// <summary>
    ///  This is the short name for the file/directory name, not the path. Available from WindowsStore.
    /// </summary>
    public static string GetShortName(SafeFileHandle fileHandle)
    {
        // Same basic structure as FILE_NAME_INFORMATION
        return GetFileInformationString(fileHandle, FileInformationClass.FileAlternateNameInformation);
    }

    private static unsafe string GetFileInformationString(
        SafeFileHandle fileHandle,
        FileInformationClass fileInformationClass)
    {
        return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
        {
            NTStatus status = NTStatus.STATUS_BUFFER_OVERFLOW;

                // Start with MAX_PATH
                uint byteLength = 260 * sizeof(char);

            FILE_NAME_INFORMATION* value = null;

            while (status == NTStatus.STATUS_BUFFER_OVERFLOW)
            {
                    // Add space for the FileNameLength
                    buffer.EnsureByteCapacity(byteLength + sizeof(uint));

                status = StorageImports.NtQueryInformationFile(
                    fileHandle,
                    IoStatusBlock: out _,
                    FileInformation: buffer.VoidPointer,
                    Length: checked((uint)buffer.ByteCapacity),
                    fileInformationClass);

                if (status == NTStatus.STATUS_SUCCESS || status == NTStatus.STATUS_BUFFER_OVERFLOW)
                {
                    value = (FILE_NAME_INFORMATION*)buffer.VoidPointer;
                    byteLength = value->FileNameLength;
                }
            }

            status.ThrowIfFailed();

            return value->FileName.CreateString();
        });
    }

    private static unsafe void GetFileInformation(
        SafeFileHandle fileHandle,
        FileInformationClass fileInformationClass,
        void* value,
        uint size)
    {
        StorageImports.NtQueryInformationFile(
            FileHandle: fileHandle,
            IoStatusBlock: out _,
            FileInformation: value,
            Length: size,
            FileInformationClass: fileInformationClass)
            .ThrowIfFailed();
    }

    /// <summary>
    ///  Gets the file mode for the given handle.
    /// </summary>
    public static unsafe FileAccessModes GetFileMode(SafeFileHandle fileHandle)
    {
        FileAccessModes info;
        GetFileInformation(fileHandle, FileInformationClass.FileModeInformation, &info, sizeof(FileAccessModes));
        return info;
    }

    /// <summary>
    ///  Return whether or not the given expression matches the given name. Takes standard Windows wildcards
    ///  (*, ?, &lt;, &gt; &quot;).
    /// </summary>
    public static unsafe bool IsNameInExpression(string expression, string name, bool ignoreCase)
    {
        if (string.IsNullOrEmpty(expression) || string.IsNullOrEmpty(name))
            return false;

        // If ignore case is set, the API will uppercase the name *if* an UpcaseTable
        // is not provided. It then flips to case-sensitive. In this state the expression
        // has to be uppercase to match as expected.

        fixed (char* e = ignoreCase ? expression.ToUpperInvariant() : expression)
        {
            fixed (char* n = name)
            {
                SafeString.Native.UNICODE_STRING* eus = null;
                SafeString.Native.UNICODE_STRING* nus = null;

                if (e != null)
                {
                    var temp = new SafeString.Native.UNICODE_STRING(e, expression.Length);
                    eus = &temp;
                }

                if (n != null)
                {
                    var temp = new SafeString.Native.UNICODE_STRING(n, name.Length);
                    nus = &temp;
                }

                return StorageImports.RtlIsNameInExpression(eus, nus, ignoreCase, IntPtr.Zero);
            }
        }
    }

    /// <summary>
    ///  Get the access rights applied to the given file handle.
    /// </summary>
    public static unsafe FileAccessRights GetRights(SafeFileHandle fileHandle)
    {
        FileAccessInformation access = default;
        StorageImports.NtQueryInformationFile(
            fileHandle,
            IoStatusBlock: out _,
            &access,
            (uint)sizeof(FileAccessInformation),
            FileInformationClass.FileAccessInformation).ThrowIfFailed();
        return access.AccessFlags;
    }

    /// <summary>
    ///  Get the ids for all processes that have a handle to this file system object.
    ///  Does not include the current process.
    /// </summary>
    public static unsafe IEnumerable<UIntPtr> GetProcessIds(SafeFileHandle fileHandle)
    {
        return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
        {
            NTStatus status = NTStatus.STATUS_INFO_LENGTH_MISMATCH;

            while (status == NTStatus.STATUS_INFO_LENGTH_MISMATCH)
            {
                status = StorageImports.NtQueryInformationFile(
                    fileHandle,
                    out Native.IO_STATUS_BLOCK statusBlock,
                    buffer.VoidPointer,
                    (uint)buffer.ByteCapacity,
                    FileInformationClass.FileProcessIdsUsingFileInformation);

                switch (status)
                {
                    case NTStatus.STATUS_SUCCESS:
                        break;
                    case NTStatus.STATUS_INFO_LENGTH_MISMATCH:
                            // Not a big enough buffer
                            buffer.EnsureByteCapacity((ulong)statusBlock.Information);
                        break;
                    default:
                        throw status.GetException();
                }
            }

            return ((FILE_PROCESS_IDS_USING_FILE_INFORMATION*)buffer.VoidPointer)->ProcessIdList.ToArray();
        });
    }

    /// <summary>
    ///  Returns the mapping for the specified DOS device name or the full list of DOS device names if passed null.
    /// </summary>
    /// <remarks>
    ///  This will look up the symbolic link target from the dos device namespace (\??\) when a name is specified.
    ///  It performs the equivalent of NtOpenDirectoryObject, NtOpenSymbolicLinkObject, then NtQuerySymbolicLinkObject.
    /// </remarks>
    public static unsafe IEnumerable<string> QueryDosDevice(string deviceName)
    {
        if (deviceName is not null)
        {
            deviceName = Paths.TrimTrailingSeparators(deviceName);
        }

        // Null will return everything defined- this list is quite large so set a higher initial allocation

        using var bufferScope = StringBuffer.Cache.AcquireScoped(deviceName is null ? 16384u : 256);
        var buffer = bufferScope.Buffer;

        uint result = 0;

        fixed (char* n = deviceName)
        {
            // QueryDosDevicePrivate takes the buffer count in TCHARs, which is 2 bytes for Unicode (WCHAR)
            while ((result = TerraFXWindows.QueryDosDeviceW((ushort*)n, buffer.UShortPointer, buffer.CharCapacity)) == 0)
            {
                WindowsError error = Error.GetLastError();
                switch (error)
                {
                    case WindowsError.ERROR_INSUFFICIENT_BUFFER:
                        buffer.EnsureCharCapacity(buffer.CharCapacity * 2);
                        break;
                    default:
                        throw error.GetException(deviceName);
                }
            }
        }

        // This API returns a szArray, which is terminated by two nulls
        buffer.Length = checked(result - 2);
        return buffer.Split('\0');
    }

    /// <summary>
    ///  Gets a set of strings for the defined logical drives in the system.
    /// </summary>
    public static unsafe IEnumerable<string> GetLogicalDriveStrings()
    {
        return BufferHelper.BufferInvoke((StringBuffer buffer) =>
        {
            uint result = 0;

            // GetLogicalDriveStringsPrivate takes the buffer count in TCHARs, which is 2 bytes for Unicode (WCHAR)
            while ((result = TerraFXWindows.GetLogicalDriveStringsW(buffer.CharCapacity, buffer.UShortPointer))
                > buffer.CharCapacity)
            {
                buffer.EnsureCharCapacity(result);
            }

            if (result == 0)
            {
                Error.ThrowLastError();
            }

            buffer.Length = result;
            return buffer.Split('\0', removeEmptyStrings: true);
        });
    }

    /// <summary>
    ///  Returns the path of the volume mount point for the specified path.
    /// </summary>
    public static unsafe string GetVolumePathName(string path)
    {
        return BufferHelper.BufferInvoke((StringBuffer buffer) =>
        {
            fixed (char* p = path)
            {
                while (!TerraFXWindows.GetVolumePathNameW((ushort*)p, buffer.UShortPointer, buffer.CharCapacity))
                {
                    WindowsError error = Error.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_FILENAME_EXCED_RANGE:
                            buffer.EnsureCharCapacity(buffer.CharCapacity * 2);
                            break;
                        default:
                            throw error.GetException(path);
                    }
                }
            }

            buffer.SetLengthToFirstNull();
            return buffer.ToString();
        });
    }

    /// <summary>
    ///  Returns the list of all paths where the given volume name is mounted.
    /// </summary>
    /// <param name="volumeName">
    ///  A volume GUID path for the volume (\\?\Volume{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}\).
    /// </param>
    public static unsafe IEnumerable<string> GetVolumePathNamesForVolumeName(string volumeName)
    {
        return BufferHelper.BufferInvoke((StringBuffer buffer) =>
        {
            uint returnLength = 0;

            fixed (char* v = volumeName)
            // GetLogicalDriveStringsPrivate takes the buffer count in TCHARs, which is 2 bytes for Unicode (WCHAR)
            while (!TerraFXWindows.GetVolumePathNamesForVolumeNameW((ushort*)v, buffer.UShortPointer, buffer.CharCapacity, &returnLength))
            {
                WindowsError error = Error.GetLastError();
                switch (error)
                {
                    case WindowsError.ERROR_MORE_DATA:
                        buffer.EnsureCharCapacity(returnLength);
                        break;
                    default:
                        throw error.GetException(volumeName);
                }
            }

            Debug.Assert(returnLength != 2, "this should never happen can't have a string array without at least 3 chars");

            // If the return length is 1 there were no mount points. The buffer should be '\0'.
            if (returnLength < 3)
            return Enumerable.Empty<string>();

            // The return length will be the entire length of the buffer, including the final string's
            // null and the string list's second null. Example: "Foo\0Bar\0\0" would be 9.
            buffer.Length = returnLength - 2;
            return buffer.Split('\0');
        });
    }

    /// <summary>
    ///  Gets the GUID format (\\?\Volume{xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx}\) of the given volume mount
    ///  point path.
    /// </summary>
    public static unsafe string GetVolumeNameForVolumeMountPoint(string volumeMountPoint)
    {
        // MSDN claims 50 is the largest size
        Span<char> buffer = stackalloc char[50];

        fixed (char* v = Paths.AddTrailingSeparator(volumeMountPoint))
        fixed (char* b = buffer)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.GetVolumeNameForVolumeMountPointW(
                    (ushort*)v,
                    (ushort*)b,
                    (uint)buffer.Length));
        }

        return buffer.SliceAtNull().ToString();
    }

    /// <summary>
    ///  Get all volume names.
    /// </summary>
    public static IEnumerable<string> GetVolumes() => new VolumeNamesEnumerable();

    /// <summary>
    ///  Get all of the folder mount points for the given volume. Requires admin access.
    /// </summary>
    /// <remarks>
    ///  This API seems busted/flaky. Use GetVolumePathNamesForVolumeName() instead to get all mount points
    ///  (folders *and* drive letter mounts) without requiring admin access.
    /// </remarks>
    /// <param name="volumeName">Volume name in the form "\\?\Volume{guid}\"</param>
    public static IEnumerable<string> GetVolumeMountPoints(string volumeName)
        => new VolumeMountPointsEnumerable(volumeName);

    public static IEnumerable<BackupStreamInformation> GetAlternateStreamInformation(string path)
    {
        List<BackupStreamInformation> streams = new();
        using (var fileHandle = CreateFile(
            path: path.AsSpan(),
            // To look at metadata we don't need read or write access
            desiredAccess: 0,
            shareMode: ShareModes.ReadWrite,
            creationDisposition: CreationDisposition.OpenExisting,
            fileAttributes: AllFileAttributes.None,
            fileFlags: FileFlags.BackupSemantics))
        {
            using BackupReader reader = new(fileHandle);
            BackupStreamInformation? info;
            while ((info = reader.GetNextInfo()).HasValue)
            {
                if (info.Value.StreamType == BackupStreamType.AlternateData)
                {
                    streams.Add(new BackupStreamInformation { Name = info.Value.Name, Size = info.Value.Size });
                }
            }
        }

        return streams;
    }

    public static unsafe void EncryptFile(string path)
    {
        fixed (char* p = path)
        {
            Error.ThrowLastErrorIfFalse(TerraFXWindows.EncryptFileW((ushort*)p), path);
        }
    }

    public static unsafe void DecryptFile(string path)
    {
        fixed (char* p = path)
        {
            Error.ThrowLastErrorIfFalse(TerraFXWindows.DecryptFileW((ushort*)p, 0), path);
        }
    }

    public static unsafe IEnumerable<SecurityIdentifier> QueryUsersOnEncryptedFile(string path)
    {
        ENCRYPTION_CERTIFICATE_HASH_LIST* hashList;
        StorageImports.QueryUsersOnEncryptedFile(path, &hashList).ThrowIfFailed(path);
        if (hashList->nCert_Hash == 0)
        {
            StorageImports.FreeEncryptionCertificateHashList(hashList);
            return Enumerable.Empty<SecurityIdentifier>();
        }

        ENCRYPTION_CERTIFICATE_HASH* users = *hashList->pUsers;
        SecurityIdentifier[] sids = new SecurityIdentifier[hashList->nCert_Hash];
        for (int i = 0; i < sids.Length; i++)
        {
            sids[0] = new SecurityIdentifier(users->pUserSid);
            users++;
        }

        StorageImports.FreeEncryptionCertificateHashList(hashList);
        return sids;
    }

    public static unsafe bool RemoveUser(ref SecurityIdentifier sid, string path)
    {
        ENCRYPTION_CERTIFICATE_HASH_LIST* hashList;
        StorageImports.QueryUsersOnEncryptedFile(path, &hashList).ThrowIfFailed(path);

        try
        {
            ENCRYPTION_CERTIFICATE_HASH* users = *hashList->pUsers;
            for (int i = 0; i < hashList->nCert_Hash; i++)
            {
                if (sid.Equals(users->pUserSid))
                {
                    var removeList = new ENCRYPTION_CERTIFICATE_HASH_LIST
                    {
                        nCert_Hash = 1,
                        pUsers = &users
                    };

                    StorageImports.RemoveUsersFromEncryptedFile(path, &removeList).ThrowIfFailed(path);
                    return true;
                }

                users++;
            }

            return false;
        }
        finally
        {
            StorageImports.FreeEncryptionCertificateHashList(hashList);
        }
    }

    // Asynchronous Disk I/O Appears as Synchronous on Windows
    // https://support.microsoft.com/en-us/kb/156932
    //
    // NTFS File Attributes
    // https://blogs.technet.microsoft.com/askcore/2010/08/25/ntfs-file-attributes/

    /// <summary>
    ///  Get the temporary directory path.
    /// </summary>
    public static unsafe string GetTempPath()
    {
        return PlatformInvoke.GrowableBufferInvoke(
            (ref ValueBuffer<char> buffer) =>
            {
                fixed (char* b = buffer)
                {
                    return TerraFXWindows.GetTempPathW(buffer.Length, (ushort*)b);
                }
            });
    }

    /// <summary>
    ///  Get the fully resolved path name.
    /// </summary>
    public static unsafe string GetFullPathName(string path)
    {
        return PlatformInvoke.GrowableBufferInvoke(
            (ref ValueBuffer<char> buffer) =>
            {
                fixed (char* p = path)
                fixed (char* b = buffer)
                {
                    return TerraFXWindows.GetFullPathNameW((ushort*)p, buffer.Length, (ushort*)b, null);
                }
            },
            detail: path);
    }

    /// <summary>
    ///  Gets a canonical version of the given handle's path.
    /// </summary>
    public static unsafe string GetFinalPathNameByHandle(
        SafeFileHandle fileHandle,
        GetFinalPathNameByHandleFlags flags = GetFinalPathNameByHandleFlags.FileNameNormalized | GetFinalPathNameByHandleFlags.VolumeNameDos)
    {
        return PlatformInvoke.GrowableBufferInvoke(
            (ref ValueBuffer<char> buffer) =>
            {
                fixed (char* b = buffer)
                {
                    return TerraFXWindows.GetFinalPathNameByHandleW(
                        fileHandle.ToHANDLE(),
                        (ushort*)b,
                        buffer.Length,
                        (uint)flags);
                }
            });
    }

    /// <summary>
    ///  Gets a canonical version of the given file's path.
    /// </summary>
    /// <param name="resolveLinks">True to get the destination of links rather than the link itself.</param>
    public static string GetFinalPathName(StringSpan path, GetFinalPathNameByHandleFlags finalPathFlags, bool resolveLinks)
    {
        if (path.IsEmpty) return string.Empty;

        // BackupSemantics is needed to get directory handles
        FileFlags flags = FileFlags.BackupSemantics;
        if (!resolveLinks)
            flags |= FileFlags.OpenReparsePoint;

        using SafeFileHandle fileHandle = CreateFile(
            path,
            CreationDisposition.OpenExisting,
            0,
            ShareModes.ReadWrite,
            AllFileAttributes.None,
            flags);

        return GetFinalPathNameByHandle(fileHandle, finalPathFlags);
    }

    /// <summary>
    ///  Get the long (non 8.3) path version of the given path.
    /// </summary>
    public static unsafe string GetLongPathName(string path)
    {
        return PlatformInvoke.GrowableBufferInvoke(
            (ref ValueBuffer<char> buffer) =>
            {
                fixed (char* p = path)
                fixed (char* b = buffer)
                {
                    return TerraFXWindows.GetLongPathNameW((ushort*)p, (ushort*)b, buffer.Length);
                }
            },
            detail: path);
    }

    /// <summary>
    ///  Get a temporary file name. Creates a 0 length file.
    /// </summary>
    /// <param name="path">The directory for the file.</param>
    /// <param name="prefix">Three character prefix for the filename.</param>
    public static unsafe string GetTempFileName(string path, string prefix)
    {
        return BufferHelper.BufferInvoke((StringBuffer buffer) =>
        {
            fixed (char* p = path)
            fixed (char* pre = prefix)
            {
                buffer.EnsureCharCapacity(Paths.MaxPath);
                Error.ThrowLastErrorIfZero(
                    TerraFXWindows.GetTempFileNameW(
                        lpPathName: (ushort*)p,
                        lpPrefixString: (ushort*)pre,
                        uUnique: 0,
                        lpTempFileName: buffer.UShortPointer),
                    path);
            }

            buffer.SetLengthToFirstNull();
            return buffer.ToString();
        });
    }

    /// <summary>
    ///  Delete the given file.
    /// </summary>
    public static unsafe void DeleteFile(string path)
    {
        fixed (char* p = path)
        {
            Error.ThrowLastErrorIfFalse(TerraFXWindows.DeleteFileW((ushort*)p), path);
        }
    }

    /// <summary>
    ///  Wrapper that allows getting a file stream using System.IO defines.
    /// </summary>
    public static System.IO.Stream CreateFileStream(
        StringSpan path,
        FileAccess fileAccess,
        FileShare fileShare,
        FileMode fileMode,
        FileAttributes fileAttributes = 0,
        FileFlags fileFlags = FileFlags.None,
        SecurityQosFlags securityFlags = SecurityQosFlags.None)
    {
        return CreateFileStream(
            path: path,
            desiredAccess: Conversion.FileAccessToDesiredAccess(fileAccess),
            shareMode: Conversion.FileShareToShareMode(fileShare),
            creationDisposition: Conversion.FileModeToCreationDisposition(fileMode),
            fileAttributes: (AllFileAttributes)fileAttributes,
            fileFlags: fileFlags,
            securityQosFlags: securityFlags);
    }

    /// <summary>
    ///  Get a stream for the specified file.
    /// </summary>
    public static Stream CreateFileStream(
        StringSpan path,
        DesiredAccess desiredAccess,
        ShareModes shareMode,
        CreationDisposition creationDisposition,
        AllFileAttributes fileAttributes = AllFileAttributes.None,
        FileFlags fileFlags = FileFlags.None,
        SecurityQosFlags securityQosFlags = SecurityQosFlags.None)
    {
        var fileHandle = CreateFile(path, creationDisposition, desiredAccess, shareMode, fileAttributes, fileFlags, securityQosFlags);

        // FileStream will own the lifetime of the handle
        return new FileStream(
            handle: fileHandle,
            access: Conversion.DesiredAccessToFileAccess(desiredAccess),
            bufferSize: 4096,
            isAsync: (fileFlags & FileFlags.Overlapped) != 0);
    }

    private delegate SafeFileHandle CreateFileDelegate(
        StringSpan path,
        DesiredAccess desiredAccess,
        ShareModes shareMode,
        CreationDisposition creationDisposition,
        AllFileAttributes fileAttributes,
        FileFlags fileFlags,
        SecurityQosFlags securityQosFlags);

    private static CreateFileDelegate? s_createFileDelegate;

    /// <summary>
    ///  Wrapper that allows using System.IO defines where available. Calls CreateFile2 if available.
    /// </summary>
    public static SafeFileHandle CreateFileSystemIo(
        StringSpan path,
        FileAccess fileAccess,
        FileShare fileShare,
        FileMode fileMode,
        FileAttributes fileAttributes = 0,
        FileFlags fileFlags = FileFlags.None,
        SecurityQosFlags securityFlags = SecurityQosFlags.None)
    {
        return CreateFile(
            path: path,
            desiredAccess: Conversion.FileAccessToDesiredAccess(fileAccess),
            shareMode: Conversion.FileShareToShareMode(fileShare),
            creationDisposition: Conversion.FileModeToCreationDisposition(fileMode),
            fileAttributes: (AllFileAttributes)fileAttributes,
            fileFlags: fileFlags,
            securityQosFlags: securityFlags);
    }

    /// <summary>
    ///  CreateFile wrapper that attempts to use CreateFile2 if running as Windows Store app.
    /// </summary>
    public static SafeFileHandle CreateFile(
        StringSpan path,
        CreationDisposition creationDisposition,
        DesiredAccess desiredAccess = DesiredAccess.GenericReadWrite,
        ShareModes shareMode = ShareModes.ReadWrite,
        AllFileAttributes fileAttributes = AllFileAttributes.None,
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
                    $"WInterop.Storage.Desktop.NativeMethods, {Delegates.DesktopLibrary}",
                    "CreateFileW");
            }
        }

        return s_createFileDelegate(path, desiredAccess, shareMode, creationDisposition, fileAttributes, fileFlags, securityQosFlags);
    }

    /// <summary>
    ///  CreateFile2 wrapper. Only available on Windows 8 and above.
    /// </summary>
    public static unsafe SafeFileHandle CreateFile2(
        StringSpan path,
        DesiredAccess desiredAccess,
        ShareModes shareMode,
        CreationDisposition creationDisposition,
        AllFileAttributes fileAttributes = AllFileAttributes.None,
        FileFlags fileFlags = FileFlags.None,
        SecurityQosFlags securityQosFlags = SecurityQosFlags.None)
    {
        CREATEFILE2_EXTENDED_PARAMETERS extended = new()
        {
            dwSize = (uint)sizeof(CREATEFILE2_EXTENDED_PARAMETERS),
            dwFileAttributes = (uint)fileAttributes,
            dwFileFlags = (uint)fileFlags,
            dwSecurityQosFlags = (uint)securityQosFlags
        };

        path = path.NullTerminate();
        HANDLE handle;

        fixed (char* p = path)
        {
            handle = TerraFXWindows.CreateFile2(
                lpFileName: (ushort*)p,
                dwDesiredAccess: (uint)desiredAccess,
                dwShareMode: (uint)shareMode,
                dwCreationDisposition: (uint)creationDisposition,
                pCreateExParams: &extended);
        }

        if (handle == HANDLE.INVALID_VALUE)
        {
            Error.GetLastError().Throw(path.ToString());
        }

        return new(handle, ownsHandle: true);
    }

    private delegate void CopyFileDelegate(
        string source,
        string destination,
        bool overwrite);

    private static CopyFileDelegate? s_copyFileDelegate;

    /// <summary>
    ///  CopyFile wrapper that attempts to use CopyFile2 if running as Windows Store app.
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
                    $"WInterop.Storage.Desktop.NativeMethods, {Delegates.DesktopLibrary}",
                    "CopyFileEx");
            }
        }

        s_copyFileDelegate(source, destination, overwrite);
    }

    /// <summary>
    ///  CopyFile2 wrapper. Only available on Windows8 and above.
    /// </summary>
    public static unsafe void CopyFile2(string source, string destination, bool overwrite = false)
    {
        BOOL cancel;
        COPYFILE2_EXTENDED_PARAMETERS parameters = new()
        {
            dwSize = (uint)sizeof(COPYFILE2_EXTENDED_PARAMETERS),
            pfCancel = &cancel,
            dwCopyFlags = overwrite ? 0u : (uint)CopyFileFlags.FailIfExists
        };

        fixed (char* s = source)
        fixed (char* d = destination)
        {
            TerraFXWindows.CopyFile2((ushort*)s, (ushort*)d, &parameters).ThrowIfFailed();
        }
    }

    /// <summary>
    ///  Gets file attributes for the given path.
    /// </summary>
    public static unsafe AllFileAttributes GetFileAttributes(string path)
    {
        AllFileAttributes attributes;

        fixed (char* p = path)
        {
            attributes = (AllFileAttributes)TerraFXWindows.GetFileAttributesW((ushort*)p);
        }

        if (attributes == AllFileAttributes.Invalid)
        {
            Error.ThrowLastError(path);
        }

        return attributes;
    }

    /// <summary>
    ///  Gets the file attributes for the given path.
    /// </summary>
    public static unsafe Win32FileAttributeData GetFileAttributesExtended(string path)
    {
        Win32FileAttributeData data = default;

        fixed (char* p = path)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.GetFileAttributesExW(
                    (ushort*)p,
                    GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard,
                    &data),
                path);
        }

        return data;
    }

    /// <summary>
    ///  Simple wrapper to check if a given path exists.
    /// </summary>
    /// <exception cref="UnauthorizedAccessException">
    ///  Thrown if there aren't rights to get attributes on the given path.
    /// </exception>
    public static bool PathExists(string path) => TryGetFileInfo(path).HasValue;

    /// <summary>
    ///  Simple wrapper to check if a given path exists and is a file.
    /// </summary>
    /// <exception cref="UnauthorizedAccessException">
    ///  Thrown if there aren't rights to get attributes on the given path.
    ///  </exception>
    public static bool FileExists(string path)
    {
        Win32FileAttributeData? data = TryGetFileInfo(path);
        return data.HasValue && !data.Value.FileAttributes.HasFlag(AllFileAttributes.Directory);
    }

    /// <summary>
    ///  Simple wrapper to check if a given path exists and is a directory.
    /// </summary>
    /// <exception cref="UnauthorizedAccessException">
    ///  Thrown if there aren't rights to get attributes on the given path.
    /// </exception>
    public static bool DirectoryExists(string path)
    {
        Win32FileAttributeData? data = TryGetFileInfo(path);
        return data.HasValue && data.Value.FileAttributes.HasFlag(AllFileAttributes.Directory);
    }

    /// <summary>
    ///  Tries to get file info, returns null if the given path doesn't exist.
    /// </summary>
    /// <exception cref="UnauthorizedAccessException">
    ///  Thrown if there aren't rights to get attributes on the given path.
    /// </exception>
    public static unsafe Win32FileAttributeData? TryGetFileInfo(string path)
    {
        Win32FileAttributeData data = default;

        fixed (char* p = path)
        {
            if (TerraFXWindows.GetFileAttributesExW(
                (ushort*)p,
                GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard,
                &data))
            {
                return data;
            }
        }

        WindowsError error = Error.GetLastError();
        return error switch
        {
            WindowsError.ERROR_ACCESS_DENIED or WindowsError.ERROR_NETWORK_ACCESS_DENIED
                => throw error.GetException(path),
            _ => null,
        };
    }

    /// <summary>
    ///  Sets the file attributes for the given path.
    /// </summary>
    public static unsafe void SetFileAttributes(string path, AllFileAttributes attributes)
    {
        fixed (char* p = path)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.SetFileAttributesW((ushort*)p, (uint)attributes),
                path);
        }
    }

    /// <summary>
    ///  Flush file buffers.
    /// </summary>
    public static void FlushFileBuffers(SafeFileHandle fileHandle)
        => Error.ThrowLastErrorIfFalse(TerraFXWindows.FlushFileBuffers(fileHandle.ToHANDLE()));

    /// <summary>
    ///  Gets the file name from the given handle. This uses GetFileInformationByHandleEx, which does not give back
    ///  the volume name for the path- but is available from Windows Store apps.
    /// </summary>
    /// <remarks>
    ///  The exact data that is returned is somewhat complicated and is described in the documentation for
    ///  ZwQueryInformationFile.
    /// </remarks>
    public static unsafe string GetFileNameLegacy(SafeFileHandle fileHandle)
    {
        return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
        {
            while (!TerraFXWindows.GetFileInformationByHandleEx(
                fileHandle.ToHANDLE(),
                FILE_INFO_BY_HANDLE_CLASS.FileNameInfo,
                buffer.VoidPointer,
                checked((uint)buffer.ByteCapacity)))
            {
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_MORE_DATA);
                buffer.EnsureByteCapacity(buffer.ByteCapacity * 2);
            }

            return ((FILE_NAME_INFORMATION*)buffer.VoidPointer)->FileName.CreateString();
        });
    }

    /// <summary>
    ///  Get standard file info from the given file handle.
    /// </summary>
    public static unsafe FileStandardInformation GetFileStandardInformation(SafeFileHandle fileHandle)
    {
        FileStandardInformation info;
        Error.ThrowLastErrorIfFalse(
            TerraFXWindows.GetFileInformationByHandleEx(
                fileHandle.ToHANDLE(),
                FILE_INFO_BY_HANDLE_CLASS.FileStandardInfo,
                &info,
                (uint)sizeof(FileStandardInformation)));
        return info;
    }

    /// <summary>
    ///  Get basic file info from the given file handle.
    /// </summary>
    public static unsafe FileBasicInformation GetFileBasicInformation(SafeFileHandle fileHandle)
    {
        FileBasicInformation info;
        Error.ThrowLastErrorIfFalse(
            TerraFXWindows.GetFileInformationByHandleEx(
                fileHandle.ToHANDLE(),
                FILE_INFO_BY_HANDLE_CLASS.FileBasicInfo,
                &info,
                (uint)sizeof(FileBasicInformation)));
        return info;
    }

    /// <summary>
    ///  Get the list of data streams for the given handle.
    /// </summary>
    public static unsafe IEnumerable<StreamInformation> GetStreamInformation(SafeFileHandle fileHandle)
    {
        return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
        {
            while (!TerraFXWindows.GetFileInformationByHandleEx(
                fileHandle.ToHANDLE(),
                FILE_INFO_BY_HANDLE_CLASS.FileStreamInfo,
                buffer.VoidPointer,
                checked((uint)buffer.ByteCapacity)))
            {
                WindowsError error = Error.GetLastError();
                switch (error)
                {
                    case WindowsError.ERROR_HANDLE_EOF:
                            // No streams
                            return Enumerable.Empty<StreamInformation>();
                    case WindowsError.ERROR_MORE_DATA:
                        buffer.EnsureByteCapacity(buffer.ByteCapacity * 2);
                        break;
                    default:
                        error.Throw();
                        break;
                }
            }

            return StreamInformation.Create((FILE_STREAM_INFORMATION*)buffer.VoidPointer);
        });
    }

    /// <summary>
    ///  Synchronous wrapper for ReadFile.
    /// </summary>
    /// <param name="fileHandle">Handle to the file to read.</param>
    /// <param name="buffer">Buffer to read bytes into.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="fileHandle"/> is null.</exception>
    /// <returns>The number of bytes read.</returns>
    public static unsafe uint ReadFile(SafeFileHandle fileHandle, Span<byte> buffer, ulong? fileOffset = null)
    {
        if (fileHandle is null) throw new ArgumentNullException(nameof(fileHandle));
        if (fileHandle.IsClosed | fileHandle.IsInvalid) throw new ArgumentException(message: null, nameof(fileHandle));

        uint numberOfBytesRead;

        fixed (byte* b = buffer)
        {
            if (fileOffset.HasValue)
            {
                ulong offset = fileOffset.Value;
                OVERLAPPED overlapped = default;
                overlapped.Offset = offset.LowWord();
                overlapped.OffsetHigh = offset.HighWord();
                Error.ThrowLastErrorIfFalse(
                    TerraFXWindows.ReadFile(
                        fileHandle.ToHANDLE(),
                        b,
                        (uint)buffer.Length,
                        &numberOfBytesRead,
                        &overlapped));
            }
            else
            {
                Error.ThrowLastErrorIfFalse(
                    TerraFXWindows.ReadFile(
                        fileHandle.ToHANDLE(),
                        b,
                        (uint)buffer.Length,
                        &numberOfBytesRead,
                        null));
            }
        }

        return numberOfBytesRead;
    }

    /// <summary>
    ///  Synchronous wrapper for WriteFile.
    /// </summary>
    /// <param name="fileHandle">Handle to the file to write.</param>
    /// <param name="data">Data to write.</param>
    /// <param name="fileOffset">Offset into the file, or null for current.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="fileHandle"/> is null.</exception>
    /// <returns>The number of bytes written.</returns>
    public static unsafe uint WriteFile(SafeFileHandle fileHandle, Span<byte> data, ulong? fileOffset = null)
    {
        if (fileHandle is null) throw new ArgumentNullException(nameof(fileHandle));
        if (fileHandle.IsClosed | fileHandle.IsInvalid) throw new ArgumentException(null, nameof(fileHandle));

        uint numberOfBytesWritten;

        fixed (byte* d = data)
        {
            if (fileOffset.HasValue)
            {
                ulong offset = fileOffset.Value;
                OVERLAPPED overlapped = default;
                overlapped.Offset = offset.LowWord();
                overlapped.OffsetHigh = offset.HighWord();

                Error.ThrowLastErrorIfFalse(
                    TerraFXWindows.WriteFile(
                        fileHandle.ToHANDLE(),
                        d,
                        (uint)data.Length,
                        &numberOfBytesWritten,
                        &overlapped));
            }
            else
            {
                Error.ThrowLastErrorIfFalse(
                    TerraFXWindows.WriteFile(
                        fileHandle.ToHANDLE(),
                        d,
                        (uint)data.Length,
                        &numberOfBytesWritten,
                        null));
            }
        }

        return numberOfBytesWritten;
    }

    /// <summary>
    ///  Set the file pointer position for the given file.
    /// </summary>
    /// <param name="distance">Offset.</param>
    /// <param name="moveMethod">Start position.</param>
    /// <returns>The new pointer position.</returns>
    public static unsafe long SetFilePointer(SafeFileHandle fileHandle, long distance, MoveMethod moveMethod)
    {
        LARGE_INTEGER position;

        Error.ThrowLastErrorIfFalse(
            TerraFXWindows.SetFilePointerEx(
                fileHandle.ToHANDLE(),
                distance.ToLARGE_INTEGER(),
                &position,
                (uint)moveMethod));

        return position.QuadPart;
    }

    /// <summary>
    ///  Get the position of the pointer for the given file.
    /// </summary>
    public static long GetFilePointer(SafeFileHandle fileHandle)
        => SetFilePointer(fileHandle, 0, MoveMethod.Current);

    /// <summary>
    ///  Get the size of the given file.
    /// </summary>
    public static unsafe long GetFileSize(SafeFileHandle fileHandle)
    {
        LARGE_INTEGER size;
        Error.ThrowLastErrorIfFalse(
            TerraFXWindows.GetFileSizeEx(fileHandle.ToHANDLE(), &size));

        return size.QuadPart;
    }

    /// <summary>
    ///  Get the type of the given file handle.
    /// </summary>
    public static FileType GetFileType(SafeFileHandle fileHandle)
    {
        FileType fileType = (FileType)TerraFXWindows.GetFileType(fileHandle.ToHANDLE());
        if (fileType == FileType.Unknown)
            Error.ThrowIfLastErrorNot(WindowsError.NO_ERROR);

        return fileType;
    }

    /// <summary>
    ///  Gets the filenames in the specified directory. Includes "." and "..".
    /// </summary>
    public static unsafe IEnumerable<string> GetDirectoryFilenames(SafeFileHandle directoryHandle)
    {
        List<string> filenames = new();
        GetFullDirectoryInfoHelper(directoryHandle, buffer =>
        {
            FILE_FULL_DIR_INFORMATION* info = (FILE_FULL_DIR_INFORMATION*)buffer.BytePointer;
            do
            {
                filenames.Add(info->FileName.CreateString());
                info = FILE_FULL_DIR_INFORMATION.GetNextInfo(info);
            } while (info is not null);
        });
        return filenames;
    }

    /// <summary>
    ///  Gets all of the info for files within the given directory handle.
    /// </summary>
    public static unsafe IEnumerable<FullFileInformation> GetDirectoryInformation(SafeFileHandle directoryHandle)
    {
        List<FullFileInformation> infos = new();
        GetFullDirectoryInfoHelper(directoryHandle, buffer =>
        {
            FILE_FULL_DIR_INFORMATION* info = (FILE_FULL_DIR_INFORMATION*)buffer.BytePointer;
            do
            {
                infos.Add(new FullFileInformation(info));
                info = FILE_FULL_DIR_INFORMATION.GetNextInfo(info);
            } while (info is not null);
        });
        return infos;
    }

    private static unsafe void GetFullDirectoryInfoHelper(SafeFileHandle directoryHandle, Action<HeapBuffer> action)
    {
        BufferHelper.BufferInvoke((HeapBuffer buffer) =>
        {
            // Make sure we have at least enough for the normal "." and ".."
            buffer.EnsureByteCapacity((ulong)sizeof(FILE_FULL_DIR_INFORMATION) * 2);

            do
            {
                while (!TerraFXWindows.GetFileInformationByHandleEx(
                    directoryHandle.ToHANDLE(),
                    FILE_INFO_BY_HANDLE_CLASS.FileFullDirectoryInfo,
                    buffer.VoidPointer,
                    (uint)buffer.ByteCapacity))
                {
                    var error = Error.GetLastError();
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
                            throw error.GetException();
                    }
                }

                action(buffer);
            } while (true);
        });
    }

    /// <summary>
    ///  Returns true if the given tag is owned by Microsoft.
    /// </summary>
    /// <docs>https://docs.microsoft.com/windows-hardware/drivers/ddi/ntifs/nf-ntifs-isreparsetagmicrosoft</docs>
    public static bool IsReparseTagMicrosoft(ReparseTag reparseTag) => ((uint)reparseTag & 0x80000000) != 0;

    /// <summary>
    ///  Returns true if the given tag is a name surrogate.
    /// </summary>
    /// <docs>https://docs.microsoft.com/windows-hardware/drivers/ddi/ntifs/nf-ntifs-isreparsetagnamesurrogate</docs>
    public static bool IsReparseTagNameSurrogate(ReparseTag reparseTag) => ((uint)reparseTag & 0x20000000) != 0;

    /// <summary>
    ///  Returns true if the reparse point can have children.
    /// </summary>
    public static bool IsReparseTagDirectory(ReparseTag reparseTag) => ((uint)reparseTag & 0x10000000) != 0;

    /// <summary>
    ///  Get the owner SID for the given handle.
    /// </summary>
    public static unsafe SecurityIdentifier GetOwner(this SafeFileHandle handle)
        => Security.Security.GetOwner(handle, ObjectType.File);

    /// <summary>
    ///  Get the primary group SID for the given handle.
    /// </summary>
    public static unsafe SecurityIdentifier GetPrimaryGroup(this SafeFileHandle handle)
        => Security.Security.GetPrimaryGroup(handle, ObjectType.File);

    /// <summary>
    ///  Get the discretionary access control list for the given handle.
    /// </summary>
    public static unsafe SecurityDescriptor GetAccessControlList(this SafeFileHandle handle)
        => Security.Security.GetAccessControlList(handle, ObjectType.File);

    public static unsafe void ChangeAccess(
        this SafeFileHandle handle,
        in SecurityIdentifier sid,
        FileAccessRights rights,
        AccessMode access,
        Inheritance inheritance = Inheritance.NoInheritance)
    {
        using SecurityDescriptor sd = GetAccessControlList(handle);
        sd.SetAclAccess(in sid, rights, access, inheritance);
    }

    /// <summary>
    ///  Remove the given directory.
    /// </summary>
    public static unsafe void RemoveDirectory(string path)
    {
        fixed (char* p = path)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.RemoveDirectoryW((ushort*)p),
                path);
        }
    }

    /// <summary>
    ///  Create the given directory.
    /// </summary>
    public static unsafe void CreateDirectory(string path)
    {
        fixed (char* p = path)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.CreateDirectoryW((ushort*)p, null),
                path);
        }
    }

    /// <summary>
    ///  Simple wrapper to allow creating a file handle for an existing directory.
    /// </summary>
    public static SafeFileHandle CreateDirectoryHandle(StringSpan directoryPath, DesiredAccess desiredAccess = DesiredAccess.ListDirectory)
    {
        return CreateFile(
            directoryPath,
            CreationDisposition.OpenExisting,
            desiredAccess,
            ShareModes.ReadWrite | ShareModes.Delete,
            AllFileAttributes.None,
            FileFlags.BackupSemantics);
    }

    /// <summary>
    ///  Get the current directory.
    /// </summary>
    public static unsafe string GetCurrentDirectory()
    {
        static uint Fix(ValueBuffer<char> buffer)
        {
            fixed (char* b = buffer)
            {
                return TerraFXWindows.GetCurrentDirectoryW(buffer.Length, (ushort*)b);
            }
        }

        using var buffer = new ValueBuffer<char>(Paths.MaxPath);
        uint result;
        while ((result = Fix(buffer)) > buffer.Length)
        {
            buffer.EnsureCapacity((int)result);
        }

        Error.ThrowLastErrorIfZero(result);
        return buffer.Span[.. (int)result].ToString();
    }

    /// <summary>
    ///  Set the current directory.
    /// </summary>
    public static unsafe void SetCurrentDirectory(StringSpan path)
    {
        path = path.NullTerminate();

        fixed (char* p = path)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.SetCurrentDirectoryW((ushort*)p),
                path.ToString());
        }
    }

    /// <summary>
    ///  Get the drive type for the given root path.
    /// </summary>
    public static unsafe DriveType GetDriveType(string? rootPath)
    {
        rootPath = Paths.AddTrailingSeparator(rootPath);
        fixed (char* r = rootPath)
        {
            return (DriveType)TerraFXWindows.GetDriveTypeW((ushort*)r);
        }
    }

    /// <summary>
    ///  Gets volume information for the given volume root path.
    /// </summary>
    /// <param name="rootPath">
    ///  The root path for the volume or UNC share. If null will use the root of the current directory.
    /// </param>
    public static unsafe VolumeInformation GetVolumeInformation(string? rootPath)
    {
        Span<char> volumeName = stackalloc char[Paths.MaxPath + 1];
        Span<char> fileSystemName = stackalloc char[Paths.MaxPath + 1];
        rootPath = Paths.AddTrailingSeparator(rootPath);

        fixed (char* r = rootPath)
        fixed (char* v = volumeName)
        fixed (char* f = fileSystemName)
        {
            uint serialNumber;
            uint maxComponentLength;
            FileSystemFeature flags;

            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.GetVolumeInformationW(
                    (ushort*)r,
                    (ushort*)v,
                    (uint)volumeName.Length,
                    &serialNumber,
                    &maxComponentLength,
                    (uint*)&flags,
                    (ushort*)f,
                    (uint)fileSystemName.Length),
                rootPath);

            return new VolumeInformation
            {
                RootPathName = rootPath,
                VolumeName = volumeName.SliceAtNull().ToString(),
                VolumeSerialNumber = serialNumber,
                MaximumComponentLength = maxComponentLength,
                FileSystemFlags = flags,
                FileSystemName = fileSystemName.ToString()
            };
        }
    }

    /// <summary>
    ///  Gets free space for the given drive.
    /// </summary>
    /// <param name="rootPath">Optional drive root, otherwise will use current directory's drive.</param>
    public static unsafe DiskFreeSpace GetDiskFreeSpace(string? rootPath)
    {
        DiskFreeSpace freeSpace;
        rootPath = Paths.AddTrailingSeparator(rootPath);

        fixed (char* r = rootPath)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.GetDiskFreeSpaceW(
                    lpRootPathName: (ushort*)r,
                    lpSectorsPerCluster: &freeSpace.SectorsPerCluster,
                    lpBytesPerSector: &freeSpace.BytesPerSector,
                    lpNumberOfFreeClusters: &freeSpace.NumberOfFreeClusters,
                    lpTotalNumberOfClusters: &freeSpace.TotalNumberOfClusters),
                rootPath);
        }

        return freeSpace;
    }

    public static unsafe ExtendedDiskFreeSpace GetDiskFreeSpaceExtended(string directory)
    {
        ExtendedDiskFreeSpace freeSpace;

        fixed (char* d = directory)
        {
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.GetDiskFreeSpaceExW(
                    lpDirectoryName: (ushort*)d,
                    lpFreeBytesAvailableToCaller: (ULARGE_INTEGER*)&freeSpace.FreeBytesAvailable,
                    lpTotalNumberOfBytes: (ULARGE_INTEGER*)&freeSpace.TotalNumberOfBytes,
                    lpTotalNumberOfFreeBytes: (ULARGE_INTEGER*)&freeSpace.TotalNumberOfFreeBytes),
                directory);
        }

        return freeSpace;
    }

    /// <summary>
    ///  Helper for identifying the special invalid attributes.
    /// </summary>
    public static bool AreInvalid(this AllFileAttributes attributes) => attributes == AllFileAttributes.Invalid;

    /// <summary>
    ///  Gets the flags for available drive letters.
    /// </summary>
    public static LogicalDrives GetLogicalDrives()
    {
        LogicalDrives drives = (LogicalDrives)TerraFXWindows.GetLogicalDrives();
        if (drives == 0)
        {
            Error.GetLastError().Throw();
        }

        return drives;
    }

    /// <summary>
    ///  Convert the given drive letter into a logical drive bit flag. Returns default if impossible.
    /// </summary>
    public static LogicalDrives GetLogicalDrive(char letter)
    {
        letter = char.ToUpperInvariant(letter);
        return letter < 'A' || letter > 'Z' ? default : (LogicalDrives)(1 << (letter - 'A'));
    }

    /// <summary>
    ///  Return the next available drive letter after the given drive letter. Returns default if no additional
    ///  valid letters can be found.
    /// </summary>
    public static char GetNextAvailableDrive(char after)
    {
        after = char.ToUpperInvariant(after);
        if (after < 'A' || after > 'Y')
        {
            return default;
        }

        uint drives = (uint)GetLogicalDrives();
        for (int i = after - 'A' + 1; i < 26; i++)
        {
            if (((1 << i) & drives) > 0)
            {
                return (char)('A' + i);
            }
        }

        return default;
    }

    // https://docs.microsoft.com/windows/win32/fileio/adding-users-to-an-encrypted-file
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
}