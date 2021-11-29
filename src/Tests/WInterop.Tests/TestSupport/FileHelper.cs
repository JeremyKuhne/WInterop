// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IO.Enumeration;
using WInterop.Storage;
using WInterop.Support;

namespace Tests.Support;

public static class FileHelper
{
    public static void WriteAllBytes(string path, byte[] data)
    {
        using var stream = Storage.CreateFileStream(path,
            DesiredAccess.GenericWrite, ShareModes.ReadWrite, CreationDisposition.OpenAlways);
        using var writer = new System.IO.BinaryWriter(stream);
        writer.Write(data);
    }

    public static void WriteAllText(string path, string text)
    {
        using var stream = Storage.CreateFileStream(path,
            DesiredAccess.GenericWrite, ShareModes.ReadWrite, CreationDisposition.OpenAlways);
        using var writer = new System.IO.StreamWriter(stream);
        writer.Write(text);
    }

    public static string ReadAllText(string path)
    {
        using var stream = Storage.CreateFileStream(path,
            DesiredAccess.GenericRead, ShareModes.ReadWrite, CreationDisposition.OpenExisting);
        using var reader = new System.IO.StreamReader(stream);
        return reader.ReadToEnd();
    }

    public static string CreateDirectoryRecursive(string path)
    {
        if (!Storage.PathExists(path))
        {
            int lastSeparator = path.LastIndexOfAny(new char[] { Paths.DirectorySeparator, Paths.AltDirectorySeparator });
            CreateDirectoryRecursive(path[..lastSeparator]);
            Storage.CreateDirectory(path);
        }

        return path;
    }

    private class MakeWritableEnumerator : FileSystemEnumerator<string>
    {
        public MakeWritableEnumerator(string directory)
            : base(directory, new EnumerationOptions
            {
                RecurseSubdirectories = true
            })
        { }

        protected override bool ShouldIncludeEntry(ref FileSystemEntry entry)
        {
            if (entry.Attributes.HasFlag(FileAttributes.ReadOnly))
            {
                // Make it writable
                Storage.SetFileAttributes(
                    entry.ToFullPath(),
                    (AllFileAttributes)(entry.Attributes & ~FileAttributes.ReadOnly));
            }

            return false;
        }

        protected override string TransformEntry(ref FileSystemEntry entry)
            => string.Empty;
    }

    public static void DeleteDirectoryRecursive(string path)
    {
        using var enumerator = new MakeWritableEnumerator(path);
        while (enumerator.MoveNext()) { }

        Directory.Delete(path, recursive: true);
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
        return path[..length];
    }
}
