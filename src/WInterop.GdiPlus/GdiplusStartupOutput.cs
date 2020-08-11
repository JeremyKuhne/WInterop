// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.GdiPlus
{
    // https://msdn.microsoft.com/en-us/library/ms534068.aspx
    public struct GdiPlusStartupOutput
    {
        public IntPtr NotificationHook;
        public IntPtr NotificationUnhook;
    }
}
