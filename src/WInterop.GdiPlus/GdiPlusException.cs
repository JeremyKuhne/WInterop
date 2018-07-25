// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ErrorHandling;

namespace WInterop.GdiPlus
{
    public class GdiPlusException : WInteropIOException
    {
        public GdiPlusException(GpStatus status) : base()
        {
            Status = status;
        }

        public GpStatus Status { get; private set; }
    }
}