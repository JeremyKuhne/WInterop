// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.Tracing;

namespace WInterop.Support.Collections
{
    /// <summary>
    /// Event logging for cache operations
    /// </summary>
    [EventSource(Name = "WInterop-Cache")]
    public sealed class CacheEventSource : EventSource
    {
        [Event(1, Level = EventLevel.Verbose)]
        public void ObjectAquired(string type) { WriteEvent(1, type); }

        [Event(2)]
        public void ObjectCreated(string type) { WriteEvent(2, type); }

        [Event(3, Level = EventLevel.Verbose)]
        public void ObjectReleased(string type) { WriteEvent(3, type); }

        [Event(4)]
        public void ObjectDestroyed(string type, string reason) { WriteEvent(4, type, reason); }

        public static CacheEventSource Log = new CacheEventSource();
    }
}
