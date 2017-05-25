// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles.Types;

namespace WInterop.GdiPlus
{
    public class GpPen : HandleZeroOrMinusOneIsInvalid
    {
        public GpPen()
            : base(ownsHandle: true) { }

        protected override bool ReleaseHandle()
        {
            return GdiPlusMethods.Imports.GdipDeletePen(handle) == GpStatus.Ok;
        }
    }
}