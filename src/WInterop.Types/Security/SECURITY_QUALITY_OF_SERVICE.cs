// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa379574.aspx
    public struct SECURITY_QUALITY_OF_SERVICE
    {
        public uint Length;
        public ImpersonationLevel ImpersonationLevel;
        public SECURITY_CONTEXT_TRACKING_MODE ContextTrackingMode;
        public BOOLEAN EffectiveOnly;
    }
}
