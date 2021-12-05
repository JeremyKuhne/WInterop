// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop.Storage;

/// <summary>
///  100-nanosecond intervals (ticks) since January 1, 1601 (UTC).
///  [FILETIME]
/// </summary>
/// <docs>
///  https://docs.microsoft.com/windows/win32/api/minwinbase/ns-minwinbase-filetime
///  Why can’t you treat a FILETIME as an __int64?
///  https://devblogs.microsoft.com/oldnewthing/?p=38053
///  Converting from a UTC-based SYSTEMTIME directly to a local-time-based SYSTEMTIME
///  https://devblogs.microsoft.com/oldnewthing/20140307-00/?p=1573
/// </docs>
public struct FileTime
{
    private readonly FILETIME _fileTime;

    public FileTime(uint low, uint high)
    {
        _fileTime = new()
        {
            dwLowDateTime = low,
            dwHighDateTime = high
        };
    }

    public FileTime(DateTime time)
    {
        ulong filetime = (ulong)time.ToFileTimeUtc();
        _fileTime = new()
        {
            dwLowDateTime = filetime.LowWord(),
            dwHighDateTime = filetime.HighWord()
        };
    }

    public ulong FileDateTime => Conversion.HighLowToLong(_fileTime.dwHighDateTime, _fileTime.dwLowDateTime);

    public DateTime ToDateTimeUtc() => DateTime.FromFileTimeUtc((long)FileDateTime);
}