// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Devices;

public enum ControlCodeMethod
{
    // https://docs.microsoft.com/en-us/windows-hardware/drivers/kernel/buffer-descriptions-for-i-o-control-codes
    // https://docs.microsoft.com/en-us/windows-hardware/drivers/kernel/methods-for-accessing-data-buffers

    /// <summary>
    ///  Uses buffered IO. Typically used for transferring small amounts of data per request.
    ///  Improves overall physical memory usage, because the memory manager does not need
    ///  to lock down a full physical page for each transfer. Video, keyboard, mouse, serial,
    ///  and parallel drivers generally use this mode.
    ///  [METHOD_BUFFERED]
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/windows-hardware/drivers/kernel/using-buffered-i-o"/>
    Buffered = 0,

    /// <summary>
    ///  Uses direct I/O to pass data to the driver. Typically used for reading or writing large
    ///  amounts of data using DMA (direct memory access) or PIO (programmed IO).
    ///  [METHOD_IN_DIRECT]
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/windows-hardware/drivers/kernel/using-direct-i-o"/>
    InDirect = 1,

    /// <summary>
    ///  Uses direct I/O to pass data from the driver. Typically used for reading or writing large
    ///  amounts of data using DMA (direct memory access) or PIO (programmed IO).
    ///  [METHOD_OUT_DIRECT]
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/windows-hardware/drivers/kernel/using-direct-i-o"/>
    OutDirect = 2,

    /// <summary>
    ///  [METHOD_NEITHER]
    /// </summary>
    /// <see cref="https://docs.microsoft.com/en-us/windows-hardware/drivers/kernel/using-neither-buffered-nor-direct-i-o"/>
    Neither = 3
}