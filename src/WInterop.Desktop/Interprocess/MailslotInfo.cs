// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Interprocess
{
    public struct MailslotInfo
    {
        /// <summary>
        ///  Size, in bytes of the next message or uint.MaxValue if there is no message.
        /// </summary>
        public uint NextSize;

        /// <summary>
        ///  Count of remaining messages.
        /// </summary>
        public uint MessageCount;

        /// <summary>
        ///  Maximum message size for the mailslot or 0 for unlimited.
        /// </summary>
        /// <remarks>
        ///  This can be larger than the requested max from CreateMailslot.
        /// </remarks>
        public uint MaxMessageSize;

        /// <summary>
        ///  Time a read request will wait for a message.
        /// </summary>
        public uint ReadTimeout;
    }
}