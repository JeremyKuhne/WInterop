// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles.Types;

namespace WInterop.GdiPlus
{
    public class GpBrush : HandleZeroOrMinusOneIsInvalid
    {
        public GpBrush()
            : base(ownsHandle: true) { }

        protected override bool ReleaseHandle()
        {
            return GdiPlusMethods.Imports.GdipDeleteBrush(handle) == GpStatus.Ok;
        }
    }
}