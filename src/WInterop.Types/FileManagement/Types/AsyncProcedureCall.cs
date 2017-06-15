// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using WInterop.Handles.Types;

namespace WInterop.FileManagement.Types
{
    /// <summary>
    /// Callback for [IO_APC_ROUTINE]. Defined in wdm.h.
    /// </summary>
    /// <param name="ApcContext"></param>
    /// <param name="IoStatusBlock"></param>
    /// <param name="reserved"></param>
    public delegate void AsyncProcedureCall(
        IntPtr ApcContext,
        ref IO_STATUS_BLOCK IoStatusBlock,
        uint reserved);
}
