// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Text;
using WInterop.Compression.Types;
using WInterop.ErrorHandling.Types;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using WInterop.Handles.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Compression
{
    public static partial class CompressionMethods
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
                ref OFSTRUCT lpReOpenBuf,
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

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa364941.aspx
            [DllImport(Libraries.Lz32, CharSet = CharSet.Unicode, ExactSpelling = true)]
            public static extern int GetExpandedNameW(
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
                ShareMode dwShareMode,
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

        public static LzHandle LzCreateFile(
            string fileName,
            out string uncompressedName,
            DesiredAccess access = DesiredAccess.GenericRead,
            ShareMode share = ShareMode.ReadWrite,
            CreationDisposition creation = CreationDisposition.OpenExisting)
        {
            string name = null;
            LzHandle handle = null;

            BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                buffer.EnsureCharCapacity(Paths.MaxPath);
                int result = ValidateLzResult(Imports.LZCreateFileW(fileName, access, share, creation, buffer));

                buffer.SetLengthToFirstNull();
                name = buffer.ToString();
                handle = new LzCreateHandle(result);
            });

            uncompressedName = name;
            return handle;
        }

        public static LzHandle LzCreateFile(
            string fileName,
            DesiredAccess access = DesiredAccess.GenericRead,
            ShareMode share = ShareMode.ReadWrite,
            CreationDisposition creation = CreationDisposition.OpenExisting)
        {
            return new LzCreateHandle(ValidateLzResult(Imports.LZCreateFileW(fileName, access, share, creation, EmptySafeHandle.Instance)));
        }

        public unsafe static LzHandle LzOpenFile(
            string fileName,
            out string uncompressedName,
            OpenFileStyle openStyle = OpenFileStyle.Read | OpenFileStyle.ShareCompat)
        {
            OFSTRUCT ofs = new OFSTRUCT();
            int result = ValidateLzResult(Imports.LZOpenFileW(fileName, ref ofs, openStyle));

            int length = 0;
            byte* start = ofs.szPathName;
            for (; *start != 0x00 && length < OFSTRUCT.OFS_MAXPATHNAME; start++, length++) ;

            uncompressedName = Encoding.ASCII.GetString(ofs.szPathName, length);
            return new LzHandle(result);
        }

        public unsafe static LzHandle LzOpenFile(
            string fileName,
            OpenFileStyle openStyle = OpenFileStyle.Read | OpenFileStyle.ShareCompat)
        {
            OFSTRUCT ofs = new OFSTRUCT();
            return new LzHandle(ValidateLzResult(Imports.LZOpenFileW(fileName, ref ofs, openStyle)));
        }

        private static int ValidateLzResult(int result)
        {
            if (result >= 0)
                return result;

            WindowsError error = Errors.GetLastError();
            if (error != WindowsError.ERROR_SUCCESS)
                throw Errors.GetIoExceptionForError(error);

            throw new WInteropIOException($"{(LzError)result}", HRESULT.E_FAIL, null);
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

        public static int CopyFile(string source, string destination, bool overwrite = false, bool useCreateFile = true)
        {
            if (!overwrite && FileMethods.PathExists(destination))
                throw Errors.GetIoExceptionForError(WindowsError.ERROR_FILE_EXISTS, destination);

            using (LzHandle sourceHandle = useCreateFile ? LzCreateFile(source) : LzOpenFile(source))
            {
                LzHandle destinationHandle;
                if (useCreateFile)
                {
                    destinationHandle = LzCreateFile(destination, DesiredAccess.GenericReadWrite, ShareMode.Read,
                        overwrite ? CreationDisposition.CreateAlways : CreationDisposition.CreateNew);
                }
                else
                {
                    destinationHandle = LzOpenFile(destination, OpenFileStyle.ReadWrite | OpenFileStyle.ShareDenyWrite | OpenFileStyle.Create);
                }

                return ValidateLzResult(Imports.LZCopy(sourceHandle, destinationHandle));
            }
        }
    }
}
