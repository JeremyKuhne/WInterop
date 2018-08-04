// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Compression.Types
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate IntPtr FNFDINOTIFY(
        FdiNotificationType fdint,
        FDINOTIFICATION pfdin);
}
