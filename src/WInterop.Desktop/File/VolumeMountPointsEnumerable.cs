// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using WInterop.ErrorHandling.Types;
using WInterop.File.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.File
{
    /// <summary>
    /// Encapsulates a finding volume mount points.
    /// </summary>
    /// <remarks>
    /// Can't fully validate this yet. It only returns mount points for folders-
    /// on the one drive I have set up this way it always returns FILE_NOT_FOUND.
    /// Drives without folder mounts return NO_MORE_FILES as expected.
    /// </remarks>
    public class VolumeMountPointsEnumerable : IEnumerable<string>
    {
        public string VolumeName { get; private set; }

        public VolumeMountPointsEnumerable(string volumeName)
        {
            VolumeName = volumeName;
        }

        public IEnumerator<string> GetEnumerator() => new VolumeMountPointsEnumerator(VolumeName);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class VolumeMountPointsEnumerator : IEnumerator<string>
        {
            private FindVolumeMountPointHandle _findHandle;
            private string _volumeName;
            private bool _lastEntryFound;
            private StringBuffer _buffer;

            public VolumeMountPointsEnumerator(string volumeName)
            {
                _buffer = StringBuffer.Cache.Acquire();
                _volumeName = volumeName;
                Reset();
            }

            public string Current { get; private set; }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_lastEntryFound) return false;

                Current = _findHandle == null
                    ? FindFirstVolume()
                    : FindNextVolume();

                return Current != null;
            }

            private string FindFirstVolume()
            {
                // Need at least some length on initial call or we'll get ERROR_INVALID_PARAMETER
                _buffer.EnsureCharCapacity(400);

                _findHandle = FileMethods.Imports.FindFirstVolumeMountPointW(
                    _volumeName,
                    _buffer,
                    _buffer.CharCapacity);

                if (_findHandle.IsInvalid)
                {
                    WindowsError error = Errors.GetLastError();
                    switch (error)
                    {
                        // Not positive on this case as I haven't been able to get this API
                        // to fully work correctly yet.
                        case WindowsError.ERROR_MORE_DATA:
                            _buffer.EnsureCharCapacity(_buffer.CharCapacity + 64);
                            return FindFirstVolume();
                        case WindowsError.ERROR_NO_MORE_FILES:
                            _lastEntryFound = true;
                            return null;
                        default:
                            throw Errors.GetIoExceptionForError(error);
                    }
                }

                _buffer.SetLengthToFirstNull();
                return _buffer.ToString();
            }

            private string FindNextVolume()
            {
                if (!FileMethods.Imports.FindNextVolumeMountPointW(_findHandle, _buffer, _buffer.CharCapacity))
                {
                    WindowsError error = Errors.GetLastError();
                    switch (error)
                    {
                        // Not positive on this case as I haven't been able to get this API
                        // to fully work correctly yet.
                        case WindowsError.ERROR_MORE_DATA:
                            _buffer.EnsureCharCapacity(_buffer.CharCapacity + 64);
                            return FindNextVolume();
                        case WindowsError.ERROR_NO_MORE_FILES:
                            _lastEntryFound = true;
                            return null;
                        default:
                            throw Errors.GetIoExceptionForError(error);
                    }
                }

                _buffer.SetLengthToFirstNull();
                return _buffer.ToString();
            }

            public void Reset()
            {
                CloseHandle();
                _lastEntryFound = false;
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            protected void Dispose(bool disposing)
            {
                CloseHandle();
                StringBuffer buffer = Interlocked.Exchange(ref _buffer, null);
                if (buffer != null)
                    StringBuffer.Cache.Release(buffer);
            }

            private void CloseHandle()
            {
                _findHandle?.Dispose();
                _findHandle = null;
            }

            ~VolumeMountPointsEnumerator()
            {
                Dispose(disposing: false);
            }
        }
    }
}
