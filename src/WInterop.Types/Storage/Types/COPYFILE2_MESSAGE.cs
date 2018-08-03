// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;

namespace WInterop.Storage
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/hh449413.aspx
    public struct COPYFILE2_MESSAGE
    {
        public COPYFILE2_MESSAGE_TYPE Type;
#pragma warning disable CS0169
        private uint dwPadding;
#pragma warning restore CS0169
        public MessageUnion Message;

        [StructLayout(LayoutKind.Explicit)]
        public struct MessageUnion
        {
            [FieldOffset(0)]
            public ChunkStarted ChunkStarted;

            [FieldOffset(0)]
            public ChunkFinished ChunkFinished;

            [FieldOffset(0)]
            public StreamStarted StreamStarted;

            [FieldOffset(0)]
            public StreamFinished StreamFinished;

            [FieldOffset(0)]
            public PollContinue PollContinue;

            [FieldOffset(0)]
            public Error Error;
        }

        /// <summary>
        /// Indicates a single chunk of a stream has started to be copied.
        /// </summary>
        public struct ChunkStarted
        {
            public uint dwStreamNumber;
#pragma warning disable CS0169
            private uint dwReserved;
#pragma warning restore CS0169
            public IntPtr hSourceFile;
            public IntPtr hDestinationFile;
            public ulong uliChunkNumber;
            public ulong uliChunkSize;
            public ulong uliStreamSize;
            public ulong uliTotalFileSize;
        }

        /// <summary>
        /// Indicates the copy of a single chunk of a stream has completed.
        /// </summary>
        public struct ChunkFinished
        {
            public uint dwStreamNumber;
#pragma warning disable CS0169
            private uint dwReserved;
#pragma warning restore CS0169
            public IntPtr hSourceFile;
            public IntPtr hDestinationFile;
            public ulong uliChunkNumber;
            public ulong uliChunkSize;
            public ulong uliStreamSize;
            public ulong uliStreamBytesTransferred;
            public ulong uliTotalFileSize;
            public ulong uliTotalBytesTransferred;
        }

        /// <summary>
        /// Indicates both source and destination handles for a stream have been opened and the copy of the stream is about to be started.
        /// </summary>
        public struct StreamStarted
        {
            public uint dwStreamNumber;
#pragma warning disable CS0169
            private uint dwReserved;
#pragma warning restore CS0169
            public IntPtr hSourceFile;
            public IntPtr hDestinationFile;
            public ulong uliStreamSize;
            public ulong uliTotalFileSize;
        }

        /// <summary>
        /// Indicates the copy operation for a stream have started to be completed, either successfully or due to a
        /// COPYFILE2_PROGRESS_STOP return from CopyFile2ProgressRoutine.
        /// </summary>
        public struct StreamFinished
        {
            public uint dwStreamNumber;
#pragma warning disable CS0169
            private uint dwReserved;
#pragma warning restore CS0169
            public IntPtr hSourceFile;
            public IntPtr hDestinationFile;
            public ulong uliStreamSize;
            public ulong uliStreamBytesTransferred;
            public ulong uliTotalFileSize;
            public ulong uliTotalBytesTransferred;
        }

        public struct PollContinue
        {
#pragma warning disable CS0169
            private uint dwReserved;
#pragma warning restore CS0169
        }

        /// <summary>
        /// Message sent when an error was encountered during the copy operation.
        /// </summary>
        public struct Error
        {
            public COPYFILE2_COPY_PHASE CopyPhase;
            public uint dwStreamNumber;
            public HRESULT hrFailure;
#pragma warning disable CS0169
            private uint dwReserved;
#pragma warning restore CS0169
            public ulong uliChunkNumber;
            public ulong uliStreamSize;
            public ulong uliStreamBytesTransferred;
            public ulong uliTotalFileSize;
            public ulong uliTotalBytesTransferred;
        }
    }
}
