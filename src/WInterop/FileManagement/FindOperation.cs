// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;

namespace WInterop.FileManagement
{
    /// <summary>
    /// Encapsulates a find operation.
    /// </summary>
    public class FindOperation : IDisposable
    {
        private FindResult _lastGoodResult;
        private IntPtr _findHandle;

        public bool DirectoriesOnly { get; private set; }
        public bool GetAlternateName { get; private set; }

        public string OriginalPath { get; private set; }

        /// <summary>
        /// Encapsulates a find operation. Does not take a trailing separator.
        /// </summary>
        /// <param name="path">
        /// The search path. The path must not end in a directory separator. The final file/directory name (after the last
        /// directory separator) can contain wildcards, the full details can be found at
        /// <a href="https://msdn.microsoft.com/en-us/library/ff469270.aspx">[MS-FSA] 2.1.4.4 Algorithm for Determining if a FileName Is in an Expression</a>.
        /// </param>
        /// <param name="directoriesOnly">Attempts to filter to just directories where supported.</param>
        /// <param name="getAlternateName">Returns the alternate (short) file name in the FindResult.AlternateName field if it exists.</param>
        public FindOperation(
            string path,
            bool directoriesOnly = false,
            bool getAlternateName = false)
        {
            OriginalPath = path;
            DirectoriesOnly = directoriesOnly;
            GetAlternateName = getAlternateName;
        }

        /// <summary>
        /// Get the next match, or null if no more matches.
        /// </summary>
        public FindResult GetNextResult()
        {
            FindResult result;
            if (_lastGoodResult == null)
                result = FindFirstFile(OriginalPath, directoriesOnly: DirectoriesOnly, getAlternateName: GetAlternateName);
            else
                result = FindNextFile(_lastGoodResult);

            if (result != null)
            {
                _lastGoodResult = result;
                _findHandle = result.FindHandle;
            }

            return result;
        }

        private FindResult FindFirstFile(
            string path,
            bool directoriesOnly = false,
            bool getAlternateName = false)
        {
            WIN32_FIND_DATA findData;
            IntPtr handle = FileMethods.Direct.FindFirstFileExW(
                path,
                getAlternateName ? FINDEX_INFO_LEVELS.FindExInfoStandard : FINDEX_INFO_LEVELS.FindExInfoBasic,
                out findData,
                // FINDEX_SEARCH_OPS.FindExSearchNameMatch is what FindFirstFile calls Ex wtih
                directoriesOnly ? FINDEX_SEARCH_OPS.FindExSearchLimitToDirectories : FINDEX_SEARCH_OPS.FindExSearchNameMatch,
                IntPtr.Zero,
                FindFirstFileExFlags.FIND_FIRST_EX_LARGE_FETCH);

            if (handle == (IntPtr)(-1))
            {
                uint error = (uint)Marshal.GetLastWin32Error();
                if (error == WinErrors.ERROR_FILE_NOT_FOUND)
                    return null;

                throw ErrorHelper.GetIoExceptionForLastError(path);
            }

            return new FindResult(handle, findData, path);
        }

        private FindResult FindNextFile(FindResult priorResult)
        {
            WIN32_FIND_DATA findData;
            if (!FileMethods.Direct.FindNextFileW(priorResult.FindHandle, out findData))
            {
                uint error = (uint)Marshal.GetLastWin32Error();
                if (error == WinErrors.ERROR_NO_MORE_FILES)
                    return null;
                throw ErrorHelper.GetIoExceptionForLastError(priorResult.OriginalPath);
            }

            return new FindResult(priorResult.FindHandle, findData, priorResult.OriginalPath);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (_findHandle != IntPtr.Zero)
                FileMethods.Direct.FindClose(_findHandle);
        }

        ~FindOperation()
        {
            Debug.Fail("Should use FindOperation in a using statement to ensure open directory handles are closed.");
            Dispose(disposing: false);
        }
    }
}
