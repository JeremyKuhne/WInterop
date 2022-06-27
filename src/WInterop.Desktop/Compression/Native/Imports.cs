// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Storage;

namespace WInterop.Compression.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class Imports
{
    /// <summary>
    ///  Undocumented API that uses CreateFile instead of OpenFile.
    /// </summary>
    /// <param name="lpFileName">File to open.</param>
    /// <param name="fdwCreate">If CreationDisposition.OpenExisting (OPEN_EXISTING), will call LZInit implicitly.</param>
    /// <param name="lpCompressedName">MAX_PATH (260 char) buffer for the uncompressed name of the file.</param>
    /// <returns>Handle or LZ error code (as LZOpenFileW).</returns>
    [DllImport(Libraries.Lz32, CharSet = CharSet.Unicode, ExactSpelling = true)]
    public static extern unsafe int LZCreateFileW(
        string lpFileName,
        DesiredAccess dwAccess,
        ShareModes dwShareMode,
        CreationDisposition dwCreate,
        char* lpCompressedName);

    /// <summary>
    ///  Matching close method for LZCreateFileW.
    /// </summary>
    [DllImport(Libraries.Lz32, ExactSpelling = true)]
    public static extern int LZCloseFile(
        int hFile);

    // TODO:
    // VerInstallFile handles both LZ and Cabinet compresed files.
    // https://docs.microsoft.com/en-us/windows/win32/api/winver/nf-winver-verinstallfilew
}