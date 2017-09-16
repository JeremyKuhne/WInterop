// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// #define WIN32HANDLE
// #define WIN32FIND
#define USEINTPTR

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using WInterop.DirectoryManagement;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.FileManagement.Types;
using WInterop.Handles;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.FileManagement
{
    /// <summary>
    /// Encapsulates a find operation.
    /// </summary>
    public class DirectFindOperation<T> : IEnumerable<T>
    {
        private string _directory;
        private bool _recursive;
        private IDirectFindTransform<T> _transform;
        private IDirectFindFilter _filter;

        /// <summary>
        /// Encapsulates a find operation. Will strip trailing separator as FindFile will not take it.
        /// </summary>
        /// <param name="directory">The directory to search in.</param>
        /// <param name="filter">
        /// The filter. Can contain wildcards, full details can be found at
        /// <a href="https://msdn.microsoft.com/en-us/library/ff469270.aspx">[MS-FSA] 2.1.4.4 Algorithm for Determining if a FileName Is in an Expression</a>.
        /// </param>
        /// <param name="getAlternateName">Returns the alternate (short) file name in the FindResult.AlternateName field if it exists.</param>
        public DirectFindOperation(
            string directory,
            bool recursive = false,
            string filter = "*",
            IDirectFindTransform<T> transform = null)
        {
            _directory = directory;
            _recursive = recursive;
            if (transform == null)
            {
                if (typeof(T) == typeof(string))
                    transform = (IDirectFindTransform<T>)DirectFindTransforms.ToFullPath.Instance;
                else if (typeof(T) == typeof(FindResult))
                    transform = (IDirectFindTransform<T>)DirectFindTransforms.ToFindResult.Instance;
            }
            _transform = transform;
            _filter = new MultiFilter(NormalDirectoryFilter.Instance, new DosFilter(filter, ignoreCase: true));
        }

        public IEnumerator<T> GetEnumerator()
        {
            SafeFileHandle handle = DirectoryMethods.CreateDirectoryHandle(_directory);
#if USEINTPTR
            IntPtr phandle = handle.DangerousGetHandle();
            handle.SetHandleAsInvalid();
            handle.Dispose();
            return new FindEnumerator(phandle, this);
#else
            return new FindEnumerator(handle, this);
#endif
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private unsafe class FindEnumerator : CriticalFinalizerObject, IEnumerator<T>
        {
#if WIN32FIND
            private IntPtr _findHandle;
            private WIN32_FIND_DATA _current;
#else
            private FILE_FULL_DIR_INFORMATION* _current;
            private HeapBuffer _buffer;
#endif

#if USEINTPTR
            private IntPtr _directory;
#else
            private SafeFileHandle _directory;
#endif
            private string _path;
            private bool _lastEntryFound;
#if USEINTPTR
            private Queue<ValueTuple<IntPtr, string>> _pending;
#else
            private Queue<ValueTuple<SafeFileHandle, string>> _pending;
#endif
            private DirectFindOperation<T> _findOperation;

#if USEINTPTR
            public FindEnumerator(IntPtr directory, DirectFindOperation<T> findOperation)
#else
            public FindEnumerator(SafeFileHandle directory, DirectFindOperation<T> findOperation)
#endif
            {
                // Set the handle first to ensure we always dispose of it
                _directory = directory;
                _path = findOperation._directory;
#if !WIN32FIND
                _buffer = HeapBuffer.Cache.Acquire(4096);
#endif
                _findOperation = findOperation;
                if (findOperation._recursive)
#if USEINTPTR
                    _pending = new Queue<ValueTuple<IntPtr, string>>();
#else
                    _pending = new Queue<ValueTuple<SafeFileHandle, string>>();
#endif
            }

#if WIN32FIND
            public T Current => _findOperation._transform.TransformResult(ref _current, _path);
#else
            public T Current => _findOperation._transform.TransformResult(_current, _path);
#endif

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_lastEntryFound) return false;

#if WIN32FIND
                do
                {
                    FindNextFile();
                    if (!_lastEntryFound && (_current.dwFileAttributes & FileAttributes.Directory) != 0
                            && NormalDirectoryFilter.Instance.Match(ref _current))
                    {
                        // Stash directory to recurse into
                        string fileName = _current.cFileName.Value;
                        string subDirectory = string.Concat(_path, "\\", fileName);
                        _pending.Enqueue(ValueTuple.Create(
                            DirectoryMethods.CreateDirectoryHandle(subDirectory),
                            // DirectoryMethods.CreateDirectoryHandle(_directory, fileName),
                            subDirectory));

                    }
                } while (!_lastEntryFound && !_findOperation._filter.Match(ref _current));
#else
                FILE_FULL_DIR_INFORMATION* info;

                do
                {
                    FindNextFile();
                    info = _current;
                    if (_pending != null && info != null && (info->FileAttributes & FileAttributes.Directory) != 0
                        && NormalDirectoryFilter.NotSpecialDirectory(info))
                    {
                        // Stash directory to recurse into
                        string fileName = info->FileName.GetValue(info->FileNameLength);
                        string subDirectory = string.Concat(_path, "\\", fileName);
                        _pending.Enqueue(ValueTuple.Create(
                            // DirectoryMethods.CreateDirectoryHandle(subDirectory),
                            DirectoryMethods.CreateDirectoryHandle(_directory, fileName),
                            subDirectory));
                    }
                } while (info != null && !_findOperation._filter.Match(info));
#endif

                return !_lastEntryFound;
            }

#if WIN32FIND
            private unsafe void FindNextFile()
            {
                if (_findHandle == IntPtr.Zero)
                {
                    _findHandle = FileMethods.Imports.FindFirstFileExW(
                        string.Concat(_path, "\\", "*"),
                        FINDEX_INFO_LEVELS.FindExInfoBasic,
                        out _current,
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
                        {
                            if (_pending == null || _pending.Count == 0)
                            {
                                _lastEntryFound = true;
                            }
                            else
                            {
                                // Grab the next directory to parse
                                var next = _pending.Dequeue();
                                _directory.Dispose();
                                _directory = next.Item1;
                                _path = next.Item2;
                                FindNextFile();
                            }
                            return;
                        }

                        throw Errors.GetIoExceptionForLastError(_path);
                    }
                }
                else
                {
                    // Existing
                    if (!FileMethods.Imports.FindNextFileW(_findHandle, out _current))
                    {
                        Errors.ThrowIfLastErrorNot(WindowsError.ERROR_NO_MORE_FILES, _path);

                        FileMethods.Imports.FindClose(_findHandle);
                        _findHandle = IntPtr.Zero;
                        if (_pending == null || _pending.Count == 0)
                        {
                            _lastEntryFound = true;
                        }
                        else
                        {
                            // Grab the next directory to parse
                            var next = _pending.Dequeue();
                            _directory.Dispose();
                            _directory = next.Item1;
                            _path = next.Item2;
                            FindNextFile();
                        }
                    }
                }
            }
#else
            private unsafe void FindNextFile()
            {
                FILE_FULL_DIR_INFORMATION* info = _current;
                if (info != null && info->NextEntryOffset != 0)
                {
                    // We're already in a buffer and have another entry
                    _current = (FILE_FULL_DIR_INFORMATION*)((byte*)info + info->NextEntryOffset);
                    return;
                }
#if WIN32HANDLE
                if (!FileMethods.Imports.GetFileInformationByHandleEx(
                    _directory,
                    FILE_INFO_BY_HANDLE_CLASS.FileFullDirectoryInfo,
                    _buffer.VoidPointer,
                    (uint)_buffer.ByteCapacity))
                {
                    WindowsError error = Errors.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_NO_MORE_FILES:
                            _current = null;
                            if (_pending == null || _pending.Count == 0)
                            {
                                _lastEntryFound = true;
                            }
                            else
                            {
                                // Grab the next directory to parse
                                var next = _pending.Dequeue();
                                _directory.Dispose();
                                _directory = next.Item1;
                                _path = next.Item2;
                                FindNextFile();
                            }
                            return;
                        default:
                            throw Errors.GetIoExceptionForError(error);
                    }
                }
#else
                NTSTATUS status = FileMethods.Imports.NtQueryDirectoryFile(
                    _directory,
                    IntPtr.Zero,
                    null,
                    IntPtr.Zero,
                    out IO_STATUS_BLOCK statusBlock,
                    _buffer.VoidPointer,
                    (uint)_buffer.ByteCapacity,
                    FILE_INFORMATION_CLASS.FileFullDirectoryInformation,
                    false,
                    null,
                    false);

                switch (status)
                {
                    case NTSTATUS.STATUS_NO_MORE_FILES:
                        _current = null;
                        if (_pending == null || _pending.Count == 0)
                        {
                            _lastEntryFound = true;
                        }
                        else
                        {
                            // Grab the next directory to parse
                            var next = _pending.Dequeue();
#if USEINTPTR
                            HandleMethods.CloseHandle(_directory);
#else
                            _directory.Dispose();
#endif
                            _directory = next.Item1;
                            _path = next.Item2;
                            FindNextFile();
                        }
                        return;
                    case NTSTATUS.STATUS_SUCCESS:
                        Debug.Assert(statusBlock.Information.ToInt64() != 0);
                        break;
                    default:
                        throw ErrorMethods.GetIoExceptionForNTStatus(status);
                }
#endif

                            _current = (FILE_FULL_DIR_INFORMATION*)_buffer.VoidPointer;
            }
#endif

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public void Dispose()
            {
                Dispose(disposing: true);
                GC.SuppressFinalize(this);
            }

            protected void Dispose(bool disposing)
            {
#if !WIN32FIND
                HeapBuffer buffer = Interlocked.Exchange(ref _buffer, null);
                if (buffer != null)
                    HeapBuffer.Cache.Release((StringBuffer)buffer);

                var queue = Interlocked.Exchange(ref _pending, null);
                if (queue != null)
                {
                    while (queue.Count > 0)
#if USEINTPTR
                        HandleMethods.CloseHandle(queue.Dequeue().Item1);
#else
                        queue.Dequeue().Item1?.Dispose();
#endif
                }
#else
                FileMethods.Imports.FindClose(_findHandle);
#endif

#if USEINTPTR
                HandleMethods.CloseHandle(_directory);
#else
                _directory?.Dispose();
#endif
            }

            ~FindEnumerator()
            {
                Dispose(disposing: false);
            }
        }
    }
}
