// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using WInterop.ErrorHandling.Types;
using WInterop.FileManagement.Types;
using WInterop.Support;

namespace WInterop.FileManagement
{
    /// <summary>
    /// Encapsulates a find operation.
    /// </summary>
    public class FindOperation : IEnumerable<FindResult>
    {
        public bool GetAlternateName { get; private set; }
        public string Directory { get; private set; }
        public string Filter { get; private set; }

        /// <summary>
        /// Encapsulates a find operation. Will strip trailing separator as FindFile will not take it.
        /// </summary>
        /// <param name="directory">The directory to search in.</param>
        /// <param name="filter">
        /// The filter. Can contain wildcards, full details can be found at
        /// <a href="https://msdn.microsoft.com/en-us/library/ff469270.aspx">[MS-FSA] 2.1.4.4 Algorithm for Determining if a FileName Is in an Expression</a>.
        /// </param>
        /// <param name="getAlternateName">Returns the alternate (short) file name in the FindResult.AlternateName field if it exists.</param>
        public FindOperation(
            string directory,
            string filter = "*",
            bool getAlternateName = false)
        {
            Directory = directory;
            Filter = filter;
            GetAlternateName = getAlternateName;
        }

        public IEnumerator<FindResult> GetEnumerator() => new FindEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class FindEnumerator : IEnumerator<FindResult>
        {
            private IntPtr _findHandle;
            private bool _lastEntryFound;
            private FindOperation _operation;
            private string _searchPath;

            public FindEnumerator(FindOperation operation)
            {
                _operation = operation;
                Reset();
            }

            public FindResult Current { get; private set; }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_lastEntryFound) return false;

                Current = _findHandle == IntPtr.Zero
                    ? FindFirstFile()
                    : FindNextFile();

                if (Current != null)
                {
                    return true;
                }
                else
                {
                    // Agressively close the handle
                    _lastEntryFound = true;
                    CloseHandle();
                    return false;
                }
            }

            private FindResult FindFirstFile()
            {
                _findHandle = FileMethods.Imports.FindFirstFileExW(
                    _searchPath,
                    _operation.GetAlternateName ? FINDEX_INFO_LEVELS.FindExInfoStandard : FINDEX_INFO_LEVELS.FindExInfoBasic,
                    out WIN32_FIND_DATA findData,
                    // FindExSearchNameMatch (0) is what FindFirstFile calls Ex with. This value has no impact on
                    // the actual behavior of the API other than it checks it to make sure that it is < 2.
                    0,
                    IntPtr.Zero,
                    FindFirstFileExFlags.FIND_FIRST_EX_LARGE_FETCH);

                if (_findHandle == (IntPtr)(-1))
                {
                    _findHandle = IntPtr.Zero;
                    WindowsError error = Errors.GetLastError();
                    if (error == WindowsError.ERROR_FILE_NOT_FOUND)
                        return null;

                    throw Errors.GetIoExceptionForLastError(_operation.Directory);
                }

                return new FindResult(ref findData, _operation.Directory);
            }

            private FindResult FindNextFile()
            {
                if (!FileMethods.Imports.FindNextFileW(_findHandle, out WIN32_FIND_DATA findData))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_NO_MORE_FILES, _operation.Directory);
                    return null;
                }

                return new FindResult(ref findData, _operation.Directory);
            }

            public void Reset()
            {
                _findHandle = IntPtr.Zero;
                _lastEntryFound = false;

                if (_searchPath == null)
                {
                    _searchPath = string.IsNullOrEmpty(_operation.Filter) ? _operation.Directory : Paths.Combine(_operation.Directory, _operation.Filter);

                    // Find first file does not like trailing separators so we'll cull it
                    _searchPath = Paths.TrimTrailingSeparators(_searchPath);
                }

                // There is one weird special case. If we're passed a legacy root volume (e.g. C:\) then removing the
                // trailing separator will make the path drive relative, leading to whatever the current directory is
                // on that particular drive (randomness). (System32 for some odd reason in my tests.)
                //
                // You can't find a volume on it's own anyway, so we'll exit out in this case. For C: without a
                // trailing slash it is legitimate to try and find whatever that matches. Note that we also don't need
                // to bother checking the first character, as anything else there would be invalid.
                if ((_searchPath.Length == 2 && _searchPath[1] == ':')   // C:
                    || (_searchPath.Length == 6 && _searchPath[5] == ':' && Paths.IsExtendedDosDevicePath(_searchPath))) // \\?\C:
                {
                    _lastEntryFound = true;
                }
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            protected void Dispose(bool disposing) => CloseHandle();

            private void CloseHandle()
            {
                if (_findHandle != IntPtr.Zero)
                {
                    FileMethods.Imports.FindClose(_findHandle);
                    _findHandle = IntPtr.Zero;
                }
            }

            ~FindEnumerator()
            {
                Dispose(disposing: false);
            }
        }
    }
}
