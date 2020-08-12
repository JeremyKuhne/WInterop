// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Storage;

namespace WInterop.Compression.Native
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
        public static unsafe extern int LZRead(
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
        public static unsafe extern int LZCreateFileW(
            string lpFileName,
            DesiredAccess dwAccess,
            ShareModes dwShareMode,
            CreationDisposition dwCreate,
            char* lpCompressedName);

        /// <summary>
        /// Matching close method for LZCreateFileW.
        /// </summary>
        [DllImport(Libraries.Lz32, ExactSpelling = true)]
        public static extern int LZCloseFile(
            int hFile);

        // TODO:
        // VerInstallFile handles both LZ and Cabinet compresed files.
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms647462.aspx

        // https://msdn.microsoft.com/en-us/library/bb432271.aspx
        [DllImport(Libraries.Cabinet, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr FDICreate(
            FNALLOC pfnalloc,
            FNFREE pfnfree,
            FNOPEN pfnopen,
            FNREAD pfnread,
            FNWRITE pfnwrite,
            FNCLOSE pfnclose,
            FNSEEK pfnseek,
            int cpuType,
            ref ExtractResult perf);

        // https://msdn.microsoft.com/en-us/library/bb432272.aspx
        [DllImport(Libraries.Cabinet, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern bool FDIDestroy(
            IntPtr hfdi);

        // https://msdn.microsoft.com/en-us/library/bb432273.aspx
        [DllImport(Libraries.Cabinet, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern bool FDIIsCabinet(
            FdiHandle hfdi,
            IntPtr hf,
            ref CabinetInfo pfdici);

        // https://msdn.microsoft.com/en-us/library/bb432270.aspx
        [DllImport(Libraries.Cabinet, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true, CharSet = CharSet.Ansi)]
        public static extern bool FDICopy(
            FdiHandle hfdi,
            string pszCabinet,
            string pszCabPath,
            int flags,
            FNFDINOTIFY pfnfdin,
            IntPtr pfnfdid,
            IntPtr pvUser);
    }
}
