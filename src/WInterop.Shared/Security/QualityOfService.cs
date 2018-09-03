// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    /// Used to support client impersonation. Client specifies this to a server to allow
    /// it to impersonate the client. [SECURITY_QUALITY_OF_SERVICE]
    /// </summary>
    /// <remarks>
    /// <see cref="https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ns-winnt-_security_quality_of_service"/>
    /// </remarks>
    public struct QualityOfService
    {
        public uint Length;
        public ImpersonationLevel ImpersonationLevel;
        public ContextTrackingMode ContextTrackingMode;
        public Boolean8 EffectiveOnly;
    }
}
