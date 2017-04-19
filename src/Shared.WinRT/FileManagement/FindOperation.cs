// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.FileManagement.DataTypes;
using WInterop.Support;

namespace WInterop.FileManagement
{
    /// <summary>
    /// Encapsulates a find operation.
    /// </summary>
    public class FindOperation : IDisposable
    {
        private IntPtr _findHandle;
        private bool _lastEntryFound;

        public bool DirectoriesOnly { get; private set; }
        public bool GetAlternateName { get; private set; }

        public string OriginalPath { get; private set; }

        /// <summary>
        /// Encapsulates a find operation. Will strip trailing separator as FindFile will not take it.
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
            if (Paths.EndsInDirectorySeparator(path))
            {
                // Find first file does not like trailing separators so we'll cull it
                //
                // There is one weird special case. If we're passed a legacy root volume (e.g. C:\) then removing the
                // trailing separator will make the path drive relative, leading to whatever the current directory is
                // on that particular drive (randomness). (System32 for some odd reason in my tests.)
                //
                // You can't find a volume on it's own anyway, so we'll exit out in this case. For C: without a
                // trailing slash it is legitimate to try and find whatever that matches. Note that we also don't need
                // to bother checking the first character, as anything else there would be invalid.

                path = Paths.RemoveTrailingSeparators(path);
                if ((path.Length == 2 && path[1] == ':')   // C:
                    || (path.Length == 6 && path[5] == ':' && Paths.IsExtendedDosDevicePath(path))) // \\?\C:
                {
                    _lastEntryFound = true;
                }
            }

            OriginalPath = path;
            DirectoriesOnly = directoriesOnly;
            GetAlternateName = getAlternateName;
        }

        /// <summary>
        /// Get the next match, or null if no more matches.
        /// </summary>
        public FindResult GetNextResult()
        {
            if (_lastEntryFound) return null;

            FindResult result = null;
            if (_findHandle == IntPtr.Zero)
                result = FindFirstFile();
            else
                result = FindNextFile();

            if (result == null)
            {
                // Agressively close the handle
                _lastEntryFound = true;
                CloseHandle();
            }

            return result;
        }

        private FindResult FindFirstFile()
        {
            WIN32_FIND_DATA findData;
            _findHandle = FileMethods.Direct.FindFirstFileExW(
                OriginalPath,
                GetAlternateName ? FINDEX_INFO_LEVELS.FindExInfoStandard : FINDEX_INFO_LEVELS.FindExInfoBasic,
                out findData,
                // FINDEX_SEARCH_OPS.FindExSearchNameMatch is what FindFirstFile calls Ex wtih
                DirectoriesOnly ? FINDEX_SEARCH_OPS.FindExSearchLimitToDirectories : FINDEX_SEARCH_OPS.FindExSearchNameMatch,
                IntPtr.Zero,
                FindFirstFileExFlags.FIND_FIRST_EX_LARGE_FETCH);

            if (_findHandle == (IntPtr)(-1))
            {
                _findHandle = IntPtr.Zero;
                WindowsError error = Errors.GetLastError();
                if (error == WindowsError.ERROR_FILE_NOT_FOUND)
                    return null;

                throw Errors.GetIoExceptionForLastError(OriginalPath);
            }

            return new FindResult(findData, OriginalPath);
        }

        private FindResult FindNextFile()
        {
            WIN32_FIND_DATA findData;
            if (!FileMethods.Direct.FindNextFileW(_findHandle, out findData))
            {
                WindowsError error = Errors.GetLastError();
                if (error == WindowsError.ERROR_NO_MORE_FILES)
                    return null;
                throw Errors.GetIoExceptionForLastError(OriginalPath);
            }

            return new FindResult(findData, OriginalPath);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            CloseHandle();
        }

        private void CloseHandle()
        {
            if (_findHandle != IntPtr.Zero)
            {
                FileMethods.Direct.FindClose(_findHandle);
                _findHandle = IntPtr.Zero;
            }
        }

        ~FindOperation()
        {
            Debug.Fail("Should use FindOperation in a using statement to ensure open directory handles are closed.");
            Dispose(disposing: false);
        }
    }
}
