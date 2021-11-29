// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage;

/// <summary>
///  Contains information about the remote end of a named pipe. [FILE_PIPE_REMOTE_INFORMATION]
/// </summary>
/// <remarks>
/// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff728847.aspx"/>
/// <see cref="https://msdn.microsoft.com/en-us/library/cc232120.aspx"/>
/// </remarks>
public readonly struct PipeRemoteInformation
{
    /// <summary>
    ///  The maximum amount of time, in 100-nanosecond intervals, that elapses before
    ///  transmission of data from the client machine to the server.
    /// </summary>
    public readonly long CollectDataTime;

    /// <summary>
    ///  The maximum size, in bytes, of data that will be collected on the client machine
    ///  before transmission to the server.
    /// </summary>
    public readonly uint MaximumCollectionCount;
}