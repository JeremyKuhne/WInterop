// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa363854.aspx">CopyProgressRoutine</a> callback used by CopyFileEx.
    /// </summary>
    public delegate CopyProgressResult CopyProgressRoutine(
         long totalFileSize,
         long totalBytesTransferred,
         long streamSize,
         long streamBytesTransferred,
         uint streamNumber,
         CopyProgressCallbackReason callbackReason,
         IntPtr sourceFile,
         IntPtr destinationFile,
         IntPtr data);
}
