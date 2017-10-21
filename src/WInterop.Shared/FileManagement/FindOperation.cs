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
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Threading;
using WInterop.DirectoryManagement;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.Types;
using WInterop.FileManagement.Types;
using WInterop.Handles;
using WInterop.Support.Buffers;

namespace WInterop.FileManagement
{
    public partial class FindOperation<T> : IEnumerable<T>
    {
        private string _directory;
        private bool _recursive;
        private IFindTransform<T> _transform;
        private IFindFilter _filter;

        /// <summary>
        /// Encapsulates a find operation. Will strip trailing separator as FindFile will not take it.
        /// </summary>
        /// <param name="directory">The directory to search in.</param>
        /// <param name="nameFilter">
        /// The filter. Can contain wildcards, full details can be found at
        /// <a href="https://msdn.microsoft.com/en-us/library/ff469270.aspx">[MS-FSA] 2.1.4.4 Algorithm for Determining if a FileName Is in an Expression</a>.
        /// </param>
        /// <param name="getAlternateName">Returns the alternate (short) file name in the FindResult.AlternateName field if it exists.</param>
        public FindOperation(
            string directory,
            string nameFilter = "*",
            bool recursive = false,
            IFindTransform<T> findTransform = null,
            IFindFilter findFilter = null)
        {
            _directory = directory;
            _recursive = recursive;
            if (findTransform == null)
            {
                if (typeof(T) == typeof(string))
                    findTransform = (IFindTransform<T>)FindTransforms.ToFullPath.Instance;
                else if (typeof(T) == typeof(FindResult))
                    findTransform = (IFindTransform<T>)FindTransforms.ToFindResult.Instance;
                else
                    throw new ArgumentException(nameof(findTransform), $"No default filter for {typeof(T)}");
            }
            _transform = findTransform;
            _filter = findFilter ?? new FindFilters.Multiple(FindFilters.NormalDirectory.Instance, new FindFilters.DosMatch(nameFilter, ignoreCase: true));
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            SafeFileHandle handle = DirectoryMethods.CreateDirectoryHandle(_directory);
            IntPtr phandle = handle.DangerousGetHandle();
            handle.SetHandleAsInvalid();
            handle.Dispose();
            return new FindEnumerator(phandle, this);
        }

        private unsafe partial class FindEnumerator : CriticalFinalizerObject, IEnumerator<T>
        {
            private RawFindData _current = new RawFindData();
            private FILE_FULL_DIR_INFORMATION* _info;
            private HeapBuffer _buffer;
            private IntPtr _directory;
            private string _path;
            private bool _lastEntryFound;
            private Queue<ValueTuple<IntPtr, string>> _pending;
            private FindOperation<T> _findOperation;

            public FindEnumerator(IntPtr directory, FindOperation<T> findOperation)
            {
                // Set the handle first to ensure we always dispose of it
                _directory = directory;
                _path = findOperation._directory;
                _buffer = StringBuffer.Cache.Acquire(4096);
                _findOperation = findOperation;
                if (findOperation._recursive)
                    _pending = new Queue<ValueTuple<IntPtr, string>>();
            }

            public T Current => _findOperation._transform.TransformResult(ref _current);

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_lastEntryFound) return false;

                do
                {
                    FindNextFile();
                    if (!_lastEntryFound && _info != null)
                    {
                        _current.FileName = _info->FileName;
                        _current.Directory = _path;
                        _current.FileAttributes = _info->FileAttributes;
                        _current.RawCreationTimeUtc = _info->CreationTime.TicksSince1601;
                        _current.RawLastAccessTimeUtc = _info->CreationTime.TicksSince1601;
                        _current.RawLastWriteTimeUtc = _info->CreationTime.TicksSince1601;
                        _current.FileSize = _info->FileNameLength;

                        if (_pending != null && (_current.FileAttributes & FileAttributes.Directory) != 0
                            && FindFilters.NormalDirectory.NotSpecialDirectory(ref _current))
                        {
                            // Stash directory to recurse into
                            string fileName = _current.FileName.CreateString();
                            string subDirectory = string.Concat(_path, "\\", fileName);
                            _pending.Enqueue(ValueTuple.Create(
                                CreateDirectoryHandle(fileName, subDirectory),
                                subDirectory));
                        }
                    }
                } while (!_lastEntryFound && !_findOperation._filter.Match(ref _current));

                return !_lastEntryFound;
            }

            private unsafe void FindNextFile()
            {
                FILE_FULL_DIR_INFORMATION* info = _info;
                if (info != null && info->NextEntryOffset != 0)
                {
                    // We're already in a buffer and have another entry
                    _info = (FILE_FULL_DIR_INFORMATION*)((byte*)info + info->NextEntryOffset);
                    return;
                }

                GetData();

                _info = (FILE_FULL_DIR_INFORMATION*)_buffer.VoidPointer;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void NoMoreFiles()
            {
                _info = null;
                if (_pending == null || _pending.Count == 0)
                {
                    _lastEntryFound = true;
                }
                else
                {
                    // Grab the next directory to parse
                    var next = _pending.Dequeue();
                    HandleMethods.CloseHandle(_directory);
                    _directory = next.Item1;
                    _path = next.Item2;
                    FindNextFile();
                }
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
                    StringBuffer.Cache.Release((StringBuffer)buffer);

                var queue = Interlocked.Exchange(ref _pending, null);
                if (queue != null)
                {
                    while (queue.Count > 0)
                        HandleMethods.CloseHandle(queue.Dequeue().Item1);
                }

                HandleMethods.CloseHandle(_directory);
            }

            ~FindEnumerator()
            {
                Dispose(disposing: false);
            }
        }
    }
}
