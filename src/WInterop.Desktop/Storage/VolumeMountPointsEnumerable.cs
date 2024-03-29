﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections;
using System.Diagnostics;
using WInterop.Errors;
using WInterop.Support.Buffers;

namespace WInterop.Storage;

/// <summary>
///  Encapsulates a finding volume mount points.
/// </summary>
/// <remarks>
///  Can't fully validate this yet. It only returns mount points for folders-
///  on the one drive I have set up this way it always returns FILE_NOT_FOUND.
///  Drives without folder mounts return NO_MORE_FILES as expected.
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
        private FindVolumeMountPointHandle? _findHandle;
        private readonly string _volumeName;
        private bool _lastEntryFound;
        private StringBuffer? _buffer;

        public VolumeMountPointsEnumerator(string volumeName)
        {
            _buffer = StringBuffer.Cache.Acquire();
            _volumeName = volumeName;
            Current = string.Empty;
            Reset();
        }

        public string Current { get; private set; }

        object? IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_lastEntryFound) return false;

            if (_buffer == null)
                throw new ObjectDisposedException(nameof(VolumeMountPointsEnumerable));

            Current = _findHandle is null
                ? FindFirstVolume()
                : FindNextVolume();

            return Current != null;
        }

        private unsafe string FindFirstVolume()
        {
            if (_buffer is null)
                throw new ObjectDisposedException(nameof(VolumeMountPointsEnumerable));

            // Need at least some length on initial call or we'll get ERROR_INVALID_PARAMETER
            _buffer.EnsureCharCapacity(400);

            HANDLE handle;
            fixed (void* n = _volumeName)
            {
                handle = TerraFXWindows.FindFirstVolumeMountPointW(
                    (ushort*)n,
                    _buffer.UShortPointer,
                    _buffer.CharCapacity);
            }

            if (handle == HANDLE.INVALID_VALUE)
            {
                WindowsError error = Error.GetLastError();
                switch (error)
                {
                    // Not positive on this case as I haven't been able to get this API
                    // to fully work correctly yet.
                    case WindowsError.ERROR_MORE_DATA:
                        _buffer.EnsureCharCapacity(_buffer.CharCapacity + 64);
                        return FindFirstVolume();
                    case WindowsError.ERROR_NO_MORE_FILES:
                        _lastEntryFound = true;
                        return string.Empty;
                    default:
                        throw error.GetException();
                }
            }

            _findHandle = new(handle);
            _buffer.SetLengthToFirstNull();
            return _buffer.ToString();
        }

        private unsafe string FindNextVolume()
        {
            Debug.Assert(_findHandle is not null);

            if (_buffer is null)
                throw new ObjectDisposedException(nameof(VolumeMountPointsEnumerable));

            if (!TerraFXWindows.FindNextVolumeMountPointW(_findHandle, _buffer.UShortPointer, _buffer.CharCapacity))
            {
                WindowsError error = Error.GetLastError();
                switch (error)
                {
                    // Not positive on this case as I haven't been able to get this API
                    // to fully work correctly yet.
                    case WindowsError.ERROR_MORE_DATA:
                        _buffer.EnsureCharCapacity(_buffer.CharCapacity + 64);
                        return FindNextVolume();
                    case WindowsError.ERROR_NO_MORE_FILES:
                        _lastEntryFound = true;
                        return string.Empty;
                    default:
                        throw error.GetException();
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

            if (disposing)
            {
                StringBuffer? buffer = Interlocked.Exchange(ref _buffer, null);
                if (buffer != null)
                    StringBuffer.Cache.Release(buffer);
            }
        }

        private void CloseHandle()
        {
            _findHandle?.Dispose();
        }

        ~VolumeMountPointsEnumerator()
        {
            Dispose(disposing: false);
        }
    }
}