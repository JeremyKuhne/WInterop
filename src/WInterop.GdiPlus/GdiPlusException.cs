// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.GdiPlus
{
    public class GdiPlusException : WInteropIOException
    {
        public GdiPlusException(GpStatus status) : base(status.ToString())
        {
            Status = status;
        }

        public GpStatus Status { get; private set; }
    }
}