﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Resources.Types
{
    public class SharedCursorHandle : CursorHandle
    {
        public SharedCursorHandle() : base(ownsHandle: false) { }

        protected override bool ReleaseHandle()
        {
            // Shared handles shouldn't be destroyed
            return true;
        }
    }
}
