// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.FileManagement;
using WInterop.Utility;

namespace WInterop.Tests.Support
{
    public static class FileHelper
    {
        public static void WriteAllText(string path, string text)
        {
            using (var stream = FileMethods.CreateFileStream(path,
                DesiredAccess.FILE_GENERIC_WRITE, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_ALWAYS))
            {
                using (var writer = new System.IO.StreamWriter(stream))
                {
                    writer.Write(text);
                }
            }
        }

        public static string ReadAllText(string path)
        {
            using (var stream = FileMethods.CreateFileStream(path,
                DesiredAccess.GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static void CreateDirectoryRecursive(string path)
        {
            if (!FileMethods.PathExists(path))
            {
                int lastSeparator = path.LastIndexOfAny(new char[] { Paths.DirectorySeparator, Paths.AltDirectorySeparator });
                CreateDirectoryRecursive(path.Substring(0, lastSeparator));
                DirectoryManagement.DirectoryMethods.CreateDirectory(path);
            }
        }

        public static void DeleteDirectoryRecursive(string path)
        {
            var data = FileMethods.TryGetFileInfo(path);
            if (!data.HasValue)
            {
                // Nothing found
                throw ErrorHandling.ErrorHelper.GetIoExceptionForError(ErrorHandling.WinErrors.ERROR_PATH_NOT_FOUND, path);
            }

            if ((data.Value.Attributes & FileAttributes.FILE_ATTRIBUTE_DIRECTORY) != FileAttributes.FILE_ATTRIBUTE_DIRECTORY)
            {
                // Not a directory, a file
                throw ErrorHandling.ErrorHelper.GetIoExceptionForError(ErrorHandling.WinErrors.ERROR_FILE_EXISTS, path);
            }

            if ((data.Value.Attributes & FileAttributes.FILE_ATTRIBUTE_READONLY) == FileAttributes.FILE_ATTRIBUTE_READONLY)
            {
                // Make it writable
                FileMethods.SetFileAttributes(path, data.Value.Attributes & ~FileAttributes.FILE_ATTRIBUTE_READONLY);
            }

            // Reparse points don't need to be empty to be deleted. Deleting will simply disconnect the reparse point, which is what we want.
            if ((data.Value.Attributes & FileAttributes.FILE_ATTRIBUTE_REPARSE_POINT) != FileAttributes.FILE_ATTRIBUTE_REPARSE_POINT)
            {
                using (FindOperation findOperation = new FindOperation(Paths.Combine(path, "*")))
                {
                    FindResult findResult;
                    while ((findResult = findOperation.GetNextResult()) != null)
                    {
                        if (findResult.FileName != "." && findResult.FileName != "..")
                        {
                            bool isDirectory = (findResult.Attributes & FileAttributes.FILE_ATTRIBUTE_DIRECTORY) == FileAttributes.FILE_ATTRIBUTE_DIRECTORY;

                            if (isDirectory)
                                DeleteDirectoryRecursive(Paths.Combine(path, findResult.FileName));
                            else
                                FileMethods.DeleteFile(Paths.Combine(path, findResult.FileName));
                        }
                    }
                }
            }

            // We've either emptied or we're a reparse point, delete the directory
            DirectoryManagement.DirectoryMethods.RemoveDirectory(path);
        }
    }
}
