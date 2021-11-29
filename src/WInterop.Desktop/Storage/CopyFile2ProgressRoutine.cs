// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage;

/// <summary>
///  CopyFile2ProgressRoutine callback used by CopyFile2.
/// </summary>
/// <docs>https://docs.microsoft.com/en-us/windows/win32/api/winbase/nc-winbase-pcopyfile2_progress_routine</docs>
public delegate CopyFile2MessageAction CopyFile2ProgressRoutine(
    IntPtr message,
    IntPtr callbackContext);