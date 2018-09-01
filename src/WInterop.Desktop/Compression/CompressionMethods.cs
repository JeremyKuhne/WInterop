// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.IO;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Storage;
using WInterop.Handles;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Compression
{
    public static partial class Compression
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365221.aspx
            [DllImport(Libraries.Lz32, ExactSpelling = true)]
            public static extern void LZClose(
                int hFile);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365223.aspx
            [DllImport(Libraries.Lz32, ExactSpelling = true)]
            public static extern int LZCopy(
                int hfSource,
                int hfDest);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365224.aspx
            [DllImport(Libraries.Lz32, ExactSpelling = true)]
            public static extern int LZInit(
                int hfSource);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365225.aspx
            [DllImport(Libraries.Lz32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern int LZOpenFileW(
                string lpFileName,
                ref OpenFileStruct lpReOpenBuf,
                OpenFileStyle wStyle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365226.aspx
            [DllImport(Libraries.Lz32, ExactSpelling = true)]
            public unsafe static extern int LZRead(
                int hFile,
                byte* lpBuffer,
                int cbRead);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365227.aspx
            [DllImport(Libraries.Lz32, ExactSpelling = true)]
            public static extern int LZSeek(
                int hFile,
                int lOffset,
                MoveMethod iOrigin);

            // Unfortunately GetExpandedNameW doesn't fail properly. It calls the A version
            // and accidentally ignores the errors returned, copying garbage into the
            // returned string.

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364941.aspx
            [DllImport(Libraries.Lz32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern int GetExpandedNameW(
                string lpszSource,
                SafeHandle lpszBuffer);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364941.aspx
            [DllImport(Libraries.Lz32, CharSet = CharSet.Ansi, ExactSpelling = true)]
            public static extern int GetExpandedNameA(
                string lpszSource,
                SafeHandle lpszBuffer);

            /// <summary>
            /// Undocumented API that uses CreateFile instead of OpenFile.
            /// </summary>
            /// <param name="lpFileName">File to open.</param>
            /// <param name="fdwCreate">If CreationDisposition.OpenExisting (OPEN_EXISTING), will call LZInit implicitly.</param>
            /// <param name="lpCompressedName">MAX_PATH (260 char) buffer for the uncompressed name of the file.</param>
            /// <returns>Handle or LZ error code (as LZOpenFileW).</returns>
            [DllImport(Libraries.Lz32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern int LZCreateFileW(
                string lpFileName,
                DesiredAccess dwAccess,
                ShareModes dwShareMode,
                CreationDisposition dwCreate,
                SafeHandle lpCompressedName);

            /// <summary>
            /// Matching close method for LZCreateFileW.
            /// </summary>
            [DllImport(Libraries.Lz32, ExactSpelling = true)]
            public static extern int LZCloseFile(
                int hFile);

            // TODO:
            // VerInstallFile handles both LZ and Cabinet compresed files.
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647462.aspx
        }

        /// <summary>
        /// Returns the expanded name of the file. Does not work for paths that are over 128
        /// characters long.
        /// </summary>
        public unsafe static string GetExpandedName(string path)
        {
            return BufferHelper.BufferInvoke((HeapBuffer buffer) =>
            {
                buffer.EnsureByteCapacity(Paths.MaxPath);
                ValidateLzResult(Imports.GetExpandedNameA(Paths.TrimTrailingSeparators(path), buffer), path);
                return BufferHelper.GetNullTerminatedAsciiString(buffer);
            });
        }

        /// <summary>
        /// Equivalent implementation of GetExpandedName that doesn't have a path length limit.
        /// </summary>
        /// <remarks>
        /// There are possibly quirks with ASCII handling that this function may not replicate. Files
        /// that end in a DBCS character get some special handling.
        /// </remarks>
        public unsafe static string GetExpandedNameEx(string path, bool filenameOnly = false)
        {
            // Need to end with underscore or be at least as long as the header.
            if (string.IsNullOrEmpty(path))
                return path;

            path = Paths.TrimTrailingSeparators(path);
            if (path[path.Length -1] != '_'
                || Storage.Storage.GetFileAttributesExtended(path).FileSize <= (ulong)sizeof(LzxHeader))
                return filenameOnly ? Paths.GetLastSegment(path) : path;

            char replacement;
            using (var file = Storage.Storage.CreateFile(path, CreationDisposition.OpenExisting, DesiredAccess.GenericRead, ShareModes.Read))
            {
                LzxHeader header = new LzxHeader();

                if (Storage.Storage.ReadFile(file, Structs.AsByteSpan(ref header)) < sizeof(LzxHeader))
                    return path;

                replacement = char.ToUpperInvariant((char)header.extensionChar);
            }

            if (filenameOnly)
                path = Paths.GetLastSegment(path);

            bool noExtension = path.Length == 1 || path[path.Length - 2] == '.';
            if (replacement == (char)0x00)
                return path.Substring(0, path.Length - (noExtension ? 2 : 1));

            return Strings.ReplaceChar(path, path.Length - 1, replacement);
        }

        public static LzHandle LzCreateFile(
            string path,
            out string uncompressedName,
            DesiredAccess access = DesiredAccess.GenericRead,
            ShareModes share = ShareModes.ReadWrite,
            CreationDisposition creation = CreationDisposition.OpenExisting)
        {
            string name = null;
            LzHandle handle = null;

            BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                buffer.EnsureCharCapacity(Paths.MaxPath);
                int result = ValidateLzResult(Imports.LZCreateFileW(path, access, share, creation, buffer), path);

                buffer.SetLengthToFirstNull();
                name = buffer.ToString();
                handle = new LzCreateHandle(result);
            });

            uncompressedName = name;
            return handle;
        }

        public static LzHandle LzCreateFile(
            string path,
            DesiredAccess access = DesiredAccess.GenericRead,
            ShareModes share = ShareModes.ReadWrite,
            CreationDisposition creation = CreationDisposition.OpenExisting)
        {
            return new LzCreateHandle(ValidateLzResult(
                Imports.LZCreateFileW(path, access, share, creation, EmptySafeHandle.Instance), path));
        }

        public unsafe static LzHandle LzOpenFile(
            string path,
            out string uncompressedName,
            OpenFileStyle openStyle = OpenFileStyle.Read | OpenFileStyle.ShareCompat)
        {
            OpenFileStruct ofs = new OpenFileStruct();
            int result = ValidateLzResult(Imports.LZOpenFileW(path, ref ofs, openStyle), path);
            uncompressedName = ofs.PathName;
            return new LzHandle(result);
        }

        public unsafe static LzHandle LzOpenFile(
            string path,
            OpenFileStyle openStyle = OpenFileStyle.Read | OpenFileStyle.ShareCompat)
        {
            OpenFileStruct ofs = new OpenFileStruct();
            return new LzHandle(ValidateLzResult(Imports.LZOpenFileW(path, ref ofs, openStyle), path));
        }

        private static int ValidateLzResult(int result, string path = null, bool throwOnError = true)
        {
            if (!throwOnError || result >= 0)
                return result;

            WindowsError error = Error.GetLastError();
            if (error != WindowsError.ERROR_SUCCESS)
                throw Error.GetIoExceptionForError(error, path);

            throw new LzException((LzError)result, path);
        }

        public static int LzSeek(LzHandle handle, int offset, MoveMethod origin)
        {
            return ValidateLzResult(Imports.LZSeek(handle, offset, origin));
        }

        public static unsafe int LzRead(LzHandle handle, byte[] buffer, int offset, int count)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            if (offset + count > buffer.Length) throw new ArgumentOutOfRangeException(nameof(count));

            fixed (byte* b = &buffer[offset])
            {
                return ValidateLzResult(Imports.LZRead(handle, b, count));
            }
        }

        public static unsafe int LzRead(LzHandle handle, byte* buffer, int offset, int count)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (offset < 0) throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));

            return ValidateLzResult(Imports.LZRead(handle, buffer + offset, count));
        }

        /// <summary>
        /// Copies a directory, expanding LZX compressed files if found.
        /// </summary>
        /// <param name="sourceDirectory">The source directory.</param>
        /// <param name="destinationDirectory">The destination directory, or null to put in an "Expanded" directory.</param>
        /// <param name="overwrite">True to overwrite files in the destination path.</param>
        /// <param name="throwOnBadCompression">If false and the compression is bad, source files will be copied normally.</param>
        public static void LzCopyDirectory(
            string sourceDirectory,
            string destinationDirectory = null,
            bool overwrite = false,
            bool throwOnBadCompression = false)
        {
            if (!Storage.Storage.DirectoryExists(sourceDirectory)) throw new DirectoryNotFoundException(sourceDirectory);

            if (destinationDirectory == null)
            {
                destinationDirectory = Paths.Combine(Paths.TrimLastSegment(sourceDirectory), "Expanded", Paths.GetLastSegment(sourceDirectory));
            }

            foreach (var file in Directory.EnumerateFiles(sourceDirectory, "*", SearchOption.AllDirectories))
            {
                string expandedName = GetExpandedNameEx(file, filenameOnly: true);
                string targetDirectory = 
                    Paths.TrimLastSegment(Paths.Combine(destinationDirectory, file.Substring(sourceDirectory.Length + 1)));
                Directory.CreateDirectory(targetDirectory);

                int result = LzCopyFile(file, Paths.Combine(targetDirectory, expandedName),
                    overwrite: overwrite, throwOnBadCompression: throwOnBadCompression);

                if (result < 0)
                {
                    // Bad source file perhaps, attempt a normal copy
                    Storage.Storage.CopyFile(file, Paths.Combine(targetDirectory, Paths.GetLastSegment(file)), overwrite: overwrite);
                }
            }
        }

        private static int ValidateLzCopyResult(int result, string source, string destination, bool throwOnBadCompression = false)
        {
            if (result >= 0)
                return result;

            switch ((LzError)result)
            {
                case LzError.Read:
                case LzError.UnknownAlgorithm:
                    if (throwOnBadCompression)
                        goto default;
                    else
                        return result;
                case LzError.BadOutHandle:
                case LzError.Write:
                    throw new LzException((LzError)result, destination);
                default:
                    throw new LzException((LzError)result, source);
            }
        }

        /// <summary>
        /// Copy the source to destination, expanding if LZX compressed.
        /// </summary>
        /// <param name="source">Source file.</param>
        /// <param name="destination">Destination file.</param>
        /// <param name="overwrite">True to overwrite destination.</param>
        /// <param name="useCreateFile">True to use LzCreateFile, otherwise uses LzOpenFile.</param>
        /// <param name="throwOnBadCompression">True to throw on bad compression, otherwise returns error code (less than 0).</param>
        /// <exception cref="FileExistsException">Thrown if the destination exists and <paramref name="overwrite"/> is false.</exception>
        /// <exception cref="LzException">Error copying file.</exception>
        /// <returns>
        /// Count of bytes copied or compression error if negative and <paramref name="throwOnBadCompression"/> is false.
        /// </returns>
        public static int LzCopyFile(
            string source,
            string destination,
            bool overwrite = false,
            bool throwOnBadCompression = true,
            bool useCreateFile = true)
        {
            if (!overwrite && Storage.Storage.PathExists(destination))
                throw Error.GetIoExceptionForError(WindowsError.ERROR_FILE_EXISTS, destination);

            using (LzHandle sourceHandle = useCreateFile ? LzCreateFile(source) : LzOpenFile(source))
            {
                LzHandle destinationHandle;
                if (useCreateFile)
                {
                    destinationHandle = LzCreateFile(destination, DesiredAccess.GenericReadWrite, ShareModes.Read,
                        overwrite ? CreationDisposition.CreateAlways : CreationDisposition.CreateNew);
                }
                else
                {
                    destinationHandle = LzOpenFile(destination, OpenFileStyle.ReadWrite | OpenFileStyle.ShareDenyWrite | OpenFileStyle.Create);
                }

                using (destinationHandle)
                {
                    return ValidateLzCopyResult(Imports.LZCopy(sourceHandle, destinationHandle), source, destination, throwOnBadCompression);
                }
            }
        }
    }
}
