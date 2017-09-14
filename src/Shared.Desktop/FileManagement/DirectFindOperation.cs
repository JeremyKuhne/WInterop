// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using WInterop.DirectoryManagement;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.FileManagement.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.FileManagement
{
    /// <summary>
    /// Encapsulates a find operation.
    /// </summary>
    public class DirectFindOperation : IEnumerable<FindResult>
    {
        public string Directory { get; private set; }
        public string Filter { get; private set; }
        public bool Recursive { get; private set; }

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
            string filter = "*")
        {
            Directory = directory;
            Filter = filter;
            Recursive = recursive;
        }

        public IEnumerator<FindResult> GetEnumerator()
        {
            SafeFileHandle handle = DirectoryMethods.CreateDirectoryHandle(Directory);
            return new FindEnumerator(handle, Directory, Recursive,
                new MultiFilter(NormalDirectoryFilter.Instance, new DosFilter(Filter, ignoreCase: true)));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private unsafe class FindEnumerator : IEnumerator<FindResult>
        {
            private HeapBuffer _buffer;
            private SafeFileHandle _directory;
            private string _path;
            private IDirectFindFilter _filter;
            private bool _lastEntryFound;
            private FILE_FULL_DIR_INFORMATION* _current;
            private Queue<Tuple<SafeFileHandle, string>> _pending;

            public FindEnumerator(SafeFileHandle directory, string path, bool recursive, IDirectFindFilter filter)
            {
                _directory = directory;
                _path = path;
                _filter = filter;
                _buffer = HeapBuffer.Cache.Acquire(4096);
                if (recursive)
                    _pending = new Queue<Tuple<SafeFileHandle, string>>();
            }

            public FindResult Current => new FindResult(_current, _path);

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_lastEntryFound) return false;

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
                        _pending.Enqueue(Tuple.Create(
                            DirectoryMethods.CreateDirectoryHandle(_directory, fileName),
                            Paths.Combine(_path, fileName)));
                    }
                } while (info != null && !_filter.Match(info));

                return info != null;
            }

            private unsafe void FindNextFile()
            {
                FILE_FULL_DIR_INFORMATION* info = _current;
                if (info != null && info->NextEntryOffset != 0)
                {
                    // We're already in a buffer and have another entry
                    _current = (FILE_FULL_DIR_INFORMATION*)((byte*)info + info->NextEntryOffset);
                    return;
                }

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
                            _directory.Dispose();
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

                _current = (FILE_FULL_DIR_INFORMATION*)_buffer.VoidPointer;
            }

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
                HeapBuffer buffer = Interlocked.Exchange(ref _buffer, null);
                if (buffer != null)
                    HeapBuffer.Cache.Release((StringBuffer)buffer);
                _directory?.Dispose();
            }

            ~FindEnumerator()
            {
                Dispose(disposing: false);
            }
        }
    }
}
