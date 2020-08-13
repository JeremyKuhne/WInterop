// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Errors
{
    public class FileExistsException : WInteropIOException
    {
        private const WindowsError DefaultError = WindowsError.ERROR_FILE_EXISTS;

        public FileExistsException()
            : base(DefaultError) { }

        public FileExistsException(string? message, Exception? innerException = null)
            : this(message, DefaultError, innerException) { }

        public FileExistsException(string? message, WindowsError error, Exception? innerException = null)
            : base(message, error.ToHResult(), innerException) { }
    }
}
