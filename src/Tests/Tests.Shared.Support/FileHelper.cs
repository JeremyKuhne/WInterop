// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling;
using WInterop.Storage;
using WInterop.Storage.Types;
using WInterop.Support;

namespace Tests.Support
{
    public static class FileHelper
    {
        public static void WriteAllBytes(string path, byte[] data)
        {
            using (var stream = StorageMethods.CreateFileStream(path,
                DesiredAccess.GenericWrite, ShareModes.ReadWrite, CreationDisposition.OpenAlways))
            {
                using (var writer = new System.IO.BinaryWriter(stream))
                {
                    writer.Write(data);
                }
            }
        }

        public static void WriteAllText(string path, string text)
        {
            using (var stream = StorageMethods.CreateFileStream(path,
                DesiredAccess.GenericWrite, ShareModes.ReadWrite, CreationDisposition.OpenAlways))
            {
                using (var writer = new System.IO.StreamWriter(stream))
                {
                    writer.Write(text);
                }
            }
        }

        public static string ReadAllText(string path)
        {
            using (var stream = StorageMethods.CreateFileStream(path,
                DesiredAccess.GenericRead, ShareModes.ReadWrite, CreationDisposition.OpenExisting))
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static string CreateDirectoryRecursive(string path)
        {
            if (!StorageMethods.PathExists(path))
            {
                int lastSeparator = path.LastIndexOfAny(new char[] { Paths.DirectorySeparator, Paths.AltDirectorySeparator });
                CreateDirectoryRecursive(path.Substring(0, lastSeparator));
                StorageMethods.CreateDirectory(path);
            }

            return path;
        }

        public static void DeleteDirectoryRecursive(string path)
        {
            var data = StorageMethods.TryGetFileInfo(path);
            if (!data.HasValue)
            {
                // Nothing found
                throw Errors.GetIoExceptionForError(WindowsError.ERROR_PATH_NOT_FOUND, path);
            }

            if ((data.Value.dwFileAttributes & FileAttributes.Directory) != FileAttributes.Directory)
            {
                // Not a directory, a file
                throw Errors.GetIoExceptionForError(WindowsError.ERROR_FILE_EXISTS, path);
            }

            if ((data.Value.dwFileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                // Make it writable
                StorageMethods.SetFileAttributes(path, data.Value.dwFileAttributes & ~FileAttributes.ReadOnly);
            }

            // Reparse points don't need to be empty to be deleted. Deleting will simply disconnect the reparse point, which is what we want.
            if ((data.Value.dwFileAttributes & FileAttributes.ReparsePoint) != FileAttributes.ReparsePoint)
            {
                foreach (FindResult findResult in new FindOperation<FindResult>(path))
                {
                    if ((findResult.Attributes & FileAttributes.Directory) == FileAttributes.Directory)
                        DeleteDirectoryRecursive(Paths.Combine(path, findResult.FileName));
                    else
                        StorageMethods.DeleteFile(Paths.Combine(path, findResult.FileName));
                }
            }

            // We've either emptied or we're a reparse point, delete the directory
            StorageMethods.RemoveDirectory(path);
        }

        public static void EnsurePathDirectoryExists(string path)
        {
            CreateDirectoryRecursive(TrimLastSegment(path));
        }

        public static string TrimLastSegment(string path)
        {
            int length = path.Length;
            while (((length > 0)
                && (path[--length] != Paths.DirectorySeparator))
                && (path[length] != Paths.AltDirectorySeparator)) { }
            return path.Substring(0, length);
        }
    }
}
