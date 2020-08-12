// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications
{
    // https://docs.microsoft.com/windows/win32/api/winbase/ns-winbase-dcb
    public enum CommBaudRate : uint
    {
        Baud_110 = 110,
        Baud_300 = 300,
        Baud_600 = 600,
        Baud_1200 = 1200,
        Baud_2400 = 2400,
        Baud_4800 = 4800,
        Baud_9600 = 9600,
        Baud_14400 = 14400,
        Baud_19200 = 19200,
        Baud_38400 = 38400,
        Baud_56000 = 56000,
        Baud_57600 = 57600,
        Baud_115200 = 115200,
        Baud_128000 = 128000,
        Baud_256000 = 256000
    }
}