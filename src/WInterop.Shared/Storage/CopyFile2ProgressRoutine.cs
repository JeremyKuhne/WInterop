// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/hh449407.aspx">CopyFile2ProgressRoutine</a> callback used by CopyFile2.
    /// </summary>
    public delegate COPYFILE2_MESSAGE_ACTION CopyFile2ProgressRoutine(
        IntPtr pMessage,
        IntPtr pvCallbackContext
        );
}
