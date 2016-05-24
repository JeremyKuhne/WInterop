// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Runtime.InteropServices.ComTypes;
using WInterop.FileManagement.DataTypes;
using WInterop.Utility;
using Xunit;

namespace WInterop.UtilityTests
{
    public class ConversionsTests
    {
        [Theory
            InlineData(0, 0, 0x0701cdd41453c000)    // January 1, 1601
            InlineData(0, 1, 0x0701cdd41453c001)
            InlineData(0, -1, 0x0701cdd51453bfff)   // If we don't cast to uint this would throw an exception
            ]
        public void FromFileTimeTest(int high, int low, long expectedTicks)
        {
            FILETIME fileTime;
            fileTime.dwHighDateTime = high;
            fileTime.dwLowDateTime = low;

            DateTime dt = Conversion.FileTimeToDateTime(fileTime);
            dt.Ticks.Should().Be(expectedTicks);
        }

        [Theory
            InlineData(System.IO.FileMode.Open, CreationDisposition.OPEN_EXISTING)
            InlineData(System.IO.FileMode.Append, CreationDisposition.OPEN_ALWAYS)
            InlineData(System.IO.FileMode.OpenOrCreate, CreationDisposition.OPEN_ALWAYS)
            InlineData(System.IO.FileMode.Create, CreationDisposition.CREATE_ALWAYS)
            InlineData(System.IO.FileMode.CreateNew, CreationDisposition.CREATE_NEW)
            InlineData(System.IO.FileMode.Truncate, CreationDisposition.TRUNCATE_EXISTING)
            ]
        public void FromFileModeTest(System.IO.FileMode mode, CreationDisposition expected)
        {
            Conversion.FileModeToCreationDisposition(mode).Should().Be(expected);
        }
    }
}