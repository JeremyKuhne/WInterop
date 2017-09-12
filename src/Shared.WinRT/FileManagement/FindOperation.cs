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
        public string OriginalPath { get; private set; }

        /// <summary>
        /// Encapsulates a find operation. Will strip trailing separator as FindFile will not take it.
        /// </summary>
        /// <param name="path">
        /// The search path. The path must not end in a directory separator. The final file/directory name (after the last
        /// directory separator) can contain wildcards, the full details can be found at
        /// <a href="https://msdn.microsoft.com/en-us/library/ff469270.aspx">[MS-FSA] 2.1.4.4 Algorithm for Determining if a FileName Is in an Expression</a>.
        /// </param>
        /// <param name="getAlternateName">Returns the alternate (short) file name in the FindResult.AlternateName field if it exists.</param>
        public FindOperation(
            string path,
            bool getAlternateName = false)
        {
            if (Paths.EndsInDirectorySeparator(path))
            {
                // Find first file does not like trailing separators so we'll cull it
                path = Paths.TrimTrailingSeparators(path);
            }

            OriginalPath = path;
            GetAlternateName = getAlternateName;
        }

        public IEnumerator<FindResult> GetEnumerator() => new FindEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class FindEnumerator : IEnumerator<FindResult>
        {
            private IntPtr _findHandle;
            private bool _lastEntryFound;
            private FindOperation _operation;

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
                    _operation.OriginalPath,
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

                    throw Errors.GetIoExceptionForLastError(_operation.OriginalPath);
                }

                return new FindResult(ref findData, _operation.OriginalPath);
            }

            private FindResult FindNextFile()
            {
                if (!FileMethods.Imports.FindNextFileW(_findHandle, out WIN32_FIND_DATA findData))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_NO_MORE_FILES, _operation.OriginalPath);
                    return null;
                }

                return new FindResult(ref findData, _operation.OriginalPath);
            }

            public void Reset()
            {
                _findHandle = IntPtr.Zero;
                _lastEntryFound = false;

                // There is one weird special case. If we're passed a legacy root volume (e.g. C:\) then removing the
                // trailing separator will make the path drive relative, leading to whatever the current directory is
                // on that particular drive (randomness). (System32 for some odd reason in my tests.)
                //
                // You can't find a volume on it's own anyway, so we'll exit out in this case. For C: without a
                // trailing slash it is legitimate to try and find whatever that matches. Note that we also don't need
                // to bother checking the first character, as anything else there would be invalid.
                if ((_operation.OriginalPath.Length == 2 && _operation.OriginalPath[1] == ':')   // C:
                    || (_operation.OriginalPath.Length == 6 && _operation.OriginalPath[5] == ':' && Paths.IsExtendedDosDevicePath(_operation.OriginalPath))) // \\?\C:
                {
                    _lastEntryFound = true;
                }
                else
                {
                    _lastEntryFound = false;
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
