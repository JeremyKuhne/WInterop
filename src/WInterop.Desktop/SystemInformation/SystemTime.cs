// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.SystemInformation
{
    /// <summary>
    ///  [SYSTEMTIME]
    /// </summary>
    /// <docs>https://msdn.microsoft.com/en-us/library/windows/desktop/ms724950.aspx</docs>
    public readonly struct SystemTime
    {
        public readonly ushort Year;
        public readonly Month Month;
        public readonly DayOfWeek DayOfWeek;
        public readonly ushort Day;
        public readonly ushort Hour;
        public readonly ushort Minute;
        public readonly ushort Second;
        public readonly ushort Milliseconds;
    }
}