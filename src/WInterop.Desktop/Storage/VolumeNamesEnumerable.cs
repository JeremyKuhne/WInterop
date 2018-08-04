// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using WInterop.Errors;
using WInterop.Storage.Native;
using WInterop.Support.Buffers;

namespace WInterop.Storage
{
    /// <summary>
    /// Encapsulates a finding volume names.
    /// </summary>
    public class VolumeNamesEnumerable : IEnumerable<string>
    {
        public IEnumerator<string> GetEnumerator() => new VolumeNamesEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class VolumeNamesEnumerator : IEnumerator<string>
        {
            private FindVolumeHandle _findHandle;
            private bool _lastEntryFound;
            private StringBuffer _buffer;

            public VolumeNamesEnumerator()
            {
                _buffer = StringBuffer.Cache.Acquire();
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
                _findHandle = Imports.FindFirstVolumeW(
                    _buffer,
                    _buffer.CharCapacity);

                if (_findHandle.IsInvalid)
                {
                    WindowsError error = Error.GetLastError();
                    if (error == WindowsError.ERROR_FILENAME_EXCED_RANGE)
                    {
                        _buffer.EnsureCharCapacity(_buffer.CharCapacity + 64);
                        return FindFirstVolume();
                    }

                    throw Error.GetIoExceptionForError(error);
                }

                _buffer.SetLengthToFirstNull();
                return _buffer.ToString();
            }

            private string FindNextVolume()
            {
                if (!Imports.FindNextVolumeW(_findHandle, _buffer, _buffer.CharCapacity))
                {
                    WindowsError error = Error.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_FILENAME_EXCED_RANGE:
                            _buffer.EnsureCharCapacity(_buffer.CharCapacity + 64);
                            return FindNextVolume();
                        case WindowsError.ERROR_NO_MORE_FILES:
                            _lastEntryFound = true;
                            return null;
                        default:
                            throw Error.GetIoExceptionForError(error);
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

            ~VolumeNamesEnumerator()
            {
                Dispose(disposing: false);
            }
        }
    }
}
