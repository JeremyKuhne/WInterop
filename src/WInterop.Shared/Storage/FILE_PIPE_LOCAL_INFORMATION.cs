// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    /// Used to get information on a named pipe that is associated with the end of the pipe
    /// that is being queried.
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff728846.aspx"/>
    /// <see cref="https://msdn.microsoft.com/en-us/library/cc232083.aspx"/>
    /// </remarks>
    public struct FILE_PIPE_LOCAL_INFORMATION
    {
        /// <summary>
        /// Type of the named pipe (stream or message).
        /// </summary>
        public PipeType NamedPipeType;

        /// <summary>
        /// The pipe configuration (inbound, outbound, or full-duplex).
        /// </summary>
        public PipeConfiguration NamedPipeConfiguration;

        /// <summary>
        /// The maximum number of instances that can be createed for
        /// the pipe. Specified by the first instance.
        /// </summary>
        public uint MaximumInstances;

        /// <summary>
        /// The current number of instances.
        /// </summary>
        public uint CurrentInstances;

        /// <summary>
        /// Inbound quota, in bytes.
        /// </summary>
        public uint InboundQuota;

        /// <summary>
        /// Amount of data available, in bytes, to be read.
        /// </summary>
        public uint ReadDataAvailable;

        /// <summary>
        /// Outbound quota, in bytes.
        /// </summary>
        public uint OutboundQuota;

        /// <summary>
        /// Write quota, in bytes.
        /// </summary>
        public uint WriteQuotaAvailable;

        /// <summary>
        /// Current state of the pipe.
        /// </summary>
        public PipeState NamedPipeState;

        /// <summary>
        /// Which end of the pipe.
        /// </summary>
        public PipeEnd NamedPipeEnd;
    }
}
