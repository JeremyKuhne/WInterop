// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Text;
using WInterop.Compression.Native;
using WInterop.Errors;
using WInterop.Storage;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Compression;

public static partial class Compression
{
    // https://interoperability.blob.core.windows.net/files/Archive_Exchange/%5bMS-CAB%5d.pdf
    // "https://docs.microsoft.com/previous-versions/bb417343(v=msdn.10)"

    /// <summary>
    ///  Returns the expanded name of the file. Does not work for paths that are over 128
    ///  characters long.
    /// </summary>
    public static unsafe string GetExpandedName(string path)
    {
        return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
        {
            buffer.EnsureByteCapacity(Paths.MaxPath);

            fixed (byte* p = Strings.ToNullTerminatedAsciiString(Paths.TrimTrailingSeparators(path)))
            {
                // Unfortunately GetExpandedNameW doesn't fail properly. It calls the A version
                // and accidentally ignores the errors returned, copying garbage into the
                // returned string.

                ValidateLzResult(TerraFXWindows.GetExpandedNameA((sbyte*)p, (sbyte*)buffer.BytePointer), path);
                return Strings.FromNullTerminatedAsciiString(buffer);
            }
        });
    }

    /// <summary>
    ///  Equivalent implementation of GetExpandedName that doesn't have a path length limit.
    /// </summary>
    /// <remarks>
    ///  There are possibly quirks with ASCII handling that this function may not replicate. Files
    ///  that end in a DBCS character get some special handling.
    /// </remarks>
    public static unsafe string GetExpandedNameEx(string path, bool filenameOnly = false)
    {
        // Need to end with underscore or be at least as long as the header.
        if (string.IsNullOrEmpty(path))
        {
            return path;
        }

        path = Paths.TrimTrailingSeparators(path);
        if (path[^1] != '_'
            || Storage.Storage.GetFileAttributesExtended(path).FileSize <= (ulong)sizeof(LzxHeader))
        {
            return filenameOnly ? Paths.GetLastSegment(path) : path;
        }

        char replacement;
        using (var file = Storage.Storage.CreateFile(
            path,
            CreationDisposition.OpenExisting,
            DesiredAccess.GenericRead,
            ShareModes.Read))
        {
            LzxHeader header = default;

            if (Storage.Storage.ReadFile(file, Structs.AsByteSpan(ref header)) < sizeof(LzxHeader))
            {
                return path;
            }

            replacement = char.ToUpperInvariant((char)header.ExtensionChar);
        }

        if (filenameOnly)
        {
            path = Paths.GetLastSegment(path);
        }

        bool noExtension = path.Length == 1 || path[^2] == '.';

        return replacement == (char)0x00
            ? path[..^(noExtension ? 2 : 1)]
            : Strings.ReplaceChar(path, path.Length - 1, replacement);
    }

    public static unsafe LzHandle LzCreateFile(
        string path,
        out string uncompressedName,
        DesiredAccess access = DesiredAccess.GenericRead,
        ShareModes share = ShareModes.ReadWrite,
        CreationDisposition creation = CreationDisposition.OpenExisting)
    {
        var (handle, name) = ValueBuffer<char>.Invoke(
            (ref ValueBuffer<char> buffer) =>
            {
                fixed (char* b = buffer)
                {
                    int result = ValidateLzResult(Imports.LZCreateFileW(path, access, share, creation, b), path);
                    return (handle: new LzCreateHandle(result), name: buffer.Span.SliceAtNull().ToString());
                }
            },
        stackBufferSize: Paths.MaxPath);

        uncompressedName = name;
        return handle;
    }

    public static unsafe LzHandle LzCreateFile(
        string path,
        DesiredAccess access = DesiredAccess.GenericRead,
        ShareModes share = ShareModes.ReadWrite,
        CreationDisposition creation = CreationDisposition.OpenExisting)
    {
        return new LzCreateHandle(ValidateLzResult(
            Imports.LZCreateFileW(path, access, share, creation, null), path));
    }

    public static unsafe LzHandle LzOpenFile(
        string path,
        out string uncompressedName,
        OpenFileStyle openStyle = OpenFileStyle.Read | OpenFileStyle.ShareCompat)
    {
        OFSTRUCT ofs = default;

        fixed (char* p = path)
        {
            int result = ValidateLzResult(
                TerraFXWindows.LZOpenFileW((ushort*)p, &ofs, (ushort)openStyle),
                path);

            // Note that DOS error ids match Windows errors
            const int OFS_MAXPATHNAME = 128;
            uncompressedName = Strings.FromNullTerminatedAsciiString(new(ofs.szPathName, OFS_MAXPATHNAME));
            return new LzHandle(result);
        }
    }

    public static unsafe LzHandle LzOpenFile(
        string path,
        OpenFileStyle openStyle = OpenFileStyle.Read | OpenFileStyle.ShareCompat)
    {
        OFSTRUCT ofs = default;
        fixed (char* p = path)
        {
            return new LzHandle(ValidateLzResult(
                TerraFXWindows.LZOpenFileW((ushort*)p, &ofs, (ushort)openStyle),
                path));
        }
    }

    private static int ValidateLzResult(int result, string? path = null, bool throwOnError = true)
        => !throwOnError || result >= 0 ? result : throw new LzException((LzError)result, path);

    public static int LzSeek(LzHandle handle, int offset, MoveMethod origin)
        => ValidateLzResult(TerraFXWindows.LZSeek(handle, offset, (int)origin));

    public static unsafe int LzRead(LzHandle handle, Span<byte> buffer, int offset, int count)
    {
        if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
        if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
        if (offset + count > buffer.Length) throw new ArgumentOutOfRangeException(nameof(count));

        fixed (byte* b = &buffer[offset])
        {
            return ValidateLzResult(TerraFXWindows.LZRead(handle, (sbyte*)b, count));
        }
    }

    /// <summary>
    ///  Copies a directory, expanding LZX compressed files if found.
    /// </summary>
    /// <param name="sourceDirectory">The source directory.</param>
    /// <param name="destinationDirectory">The destination directory, or null to put in an "Expanded" directory.</param>
    /// <param name="overwrite">True to overwrite files in the destination path.</param>
    /// <param name="throwOnBadCompression">If false and the compression is bad, source files will be copied normally.</param>
    public static void LzCopyDirectory(
        string sourceDirectory,
        string? destinationDirectory = null,
        bool overwrite = false,
        bool throwOnBadCompression = false)
    {
        if (!Storage.Storage.DirectoryExists(sourceDirectory))
            throw new DirectoryNotFoundException(sourceDirectory);

        destinationDirectory ??= Path.Join(
            Paths.TrimLastSegment(sourceDirectory),
            "Expanded",
            Paths.GetLastSegment(sourceDirectory));

        foreach (var file in Directory.EnumerateFiles(sourceDirectory, "*", SearchOption.AllDirectories))
        {
            string expandedName = GetExpandedNameEx(file, filenameOnly: true);
            string targetDirectory =
                Paths.TrimLastSegment(Path.Join(destinationDirectory, file[(sourceDirectory.Length + 1)..]));
            Directory.CreateDirectory(targetDirectory);

            int result = LzCopyFile(
                file,
                Path.Join(targetDirectory, expandedName),
                overwrite,
                throwOnBadCompression: throwOnBadCompression);

            if (result < 0)
            {
                // Bad source file perhaps, attempt a normal copy
                Storage.Storage.CopyFile(
                    file,
                    Path.Join(targetDirectory, Paths.GetLastSegment(file)),
                    overwrite: overwrite);
            }
        }
    }

    private static int ValidateLzCopyResult(int result, string source, string destination, bool throwOnBadCompression = false)
    {
        if (result >= 0)
        {
            return result;
        }

        switch ((LzError)result)
        {
            case LzError.Read:
            case LzError.UnknownAlgorithm:
                if (throwOnBadCompression)
                {
                    goto default;
                }
                else
                {
                    return result;
                }

            case LzError.BadOutHandle:
            case LzError.Write:
                throw new LzException((LzError)result, destination);
            default:
                throw new LzException((LzError)result, source);
        }
    }

    /// <summary>
    ///  Copy the source to destination, expanding if LZX compressed.
    /// </summary>
    /// <param name="source">Source file.</param>
    /// <param name="destination">Destination file.</param>
    /// <param name="overwrite">True to overwrite destination.</param>
    /// <param name="useCreateFile">True to use LzCreateFile, otherwise uses LzOpenFile.</param>
    /// <param name="throwOnBadCompression">True to throw on bad compression, otherwise returns error code (less than 0).</param>
    /// <exception cref="FileExistsException">Thrown if the destination exists and <paramref name="overwrite"/> is false.</exception>
    /// <exception cref="LzException">Error copying file.</exception>
    /// <returns>
    ///  Count of bytes copied or compression error if negative and <paramref name="throwOnBadCompression"/> is false.
    /// </returns>
    public static int LzCopyFile(
        string source,
        string destination,
        bool overwrite = false,
        bool throwOnBadCompression = true,
        bool useCreateFile = true)
    {
        if (!overwrite && Storage.Storage.PathExists(destination))
            WindowsError.ERROR_FILE_EXISTS.Throw(destination);

        using LzHandle sourceHandle = useCreateFile ? LzCreateFile(source) : LzOpenFile(source);
        LzHandle destinationHandle;
        if (useCreateFile)
        {
            destinationHandle = LzCreateFile(
                destination,
                DesiredAccess.GenericReadWrite,
                ShareModes.Read,
                overwrite ? CreationDisposition.CreateAlways : CreationDisposition.CreateNew);
        }
        else
        {
            destinationHandle = LzOpenFile(
                destination,
                OpenFileStyle.ReadWrite | OpenFileStyle.ShareDenyWrite | OpenFileStyle.Create);
        }

        using (destinationHandle)
        {
            return ValidateLzCopyResult(
                TerraFXWindows.LZCopy(sourceHandle, destinationHandle),
                source,
                destination,
                throwOnBadCompression);
        }
    }
}