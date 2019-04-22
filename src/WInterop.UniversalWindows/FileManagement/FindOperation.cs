// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using WInterop.Errors;
using WInterop.Storage.Native;
using WInterop.Support;

namespace WInterop.Storage
{
    public partial class FindOperation<T> : IEnumerable<T>
    {
        private unsafe partial class FindEnumerator : CriticalFinalizerObject, IEnumerator<T>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private IntPtr CreateDirectoryHandle(string fileName, string subDirectory)
            {
                SafeFileHandle safeHandle = Storage.CreateDirectoryHandle(subDirectory);

                // Ideally we'd never wrap in a SafeFileHandle, but for now this is reasonable.
                IntPtr handle = safeHandle.DangerousGetHandle();
                safeHandle.SetHandleAsInvalid();
                return handle;
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            private void GetData()
            {
                if (!Imports.GetFileInformationByHandleEx(
                   _directory,
                   FileInfoClass.FileFullDirectoryInfo,
                   _buffer.VoidPointer,
                   (uint)_buffer.ByteCapacity))
                {
                    WindowsError error = Error.GetLastError();
                    switch (error)
                    {
                        case WindowsError.ERROR_NO_MORE_FILES:
                            NoMoreFiles();
                            return;
                        default:
                            throw error.GetException();
                    }
                }
            }
        }
    }
}
