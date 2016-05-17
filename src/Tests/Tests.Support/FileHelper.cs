// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WInterop.FileManagement;

namespace Tests.Support
{
    public static class FileHelper
    {
        public static void WriteAllText(string path, string text)
        {
            using (var stream = NativeMethods.CreateFileStream(path,
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
            using (var stream = NativeMethods.CreateFileStream(path,
                DesiredAccess.FILE_GENERIC_READ, ShareMode.FILE_SHARE_READWRITE, CreationDisposition.OPEN_EXISTING))
            {
                using (var reader = new System.IO.StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static bool FileExists(string path)
        {
            try
            {
                return (NativeMethods.GetFileAttributesEx(path).Attributes & FileAttributes.FILE_ATTRIBUTE_DIRECTORY) != FileAttributes.FILE_ATTRIBUTE_DIRECTORY;
            }
            catch
            {
                return false;
            }
        }

        public static bool DirectoryExists(string path)
        {
            try
            {
                return (NativeMethods.GetFileAttributesEx(path).Attributes & FileAttributes.FILE_ATTRIBUTE_DIRECTORY) == FileAttributes.FILE_ATTRIBUTE_DIRECTORY;
            }
            catch
            {
                return false;
            }
        }

    }
}
