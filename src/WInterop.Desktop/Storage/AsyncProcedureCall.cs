// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Storage.Native;

namespace WInterop.Storage;

/// <summary>
///  Callback for [IO_APC_ROUTINE]. Defined in wdm.h.
/// </summary>
public delegate void AsyncProcedureCall(
    IntPtr apcContext,
    ref IO_STATUS_BLOCK ioStatusBlock,
    uint reserved);