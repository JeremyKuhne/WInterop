// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.SystemInformation.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms724950.aspx
    public struct SYSTEMTIME
    {
        public ushort wYear;
        public Month wMonth;
        public DayOfWeek wDayOfWeek;
        public ushort wDay;
        public ushort wHour;
        public ushort wMinute;
        public ushort wSecond;
        public ushort wMilliseconds;
    }
}
