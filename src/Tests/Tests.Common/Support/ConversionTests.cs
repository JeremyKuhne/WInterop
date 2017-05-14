// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Runtime.InteropServices.ComTypes;
using WInterop.FileManagement.Types;
using WInterop.Support;
using Xunit;

namespace Tests.Support
{
    public class ConversionsTests
    {
        [Theory,
            InlineData(0, 0, 0x0701cdd41453c000),    // January 1, 1601
            InlineData(0, 1, 0x0701cdd41453c001),
            InlineData(0, -1, 0x0701cdd51453bfff)    // If we don't cast to uint this would throw an exception
            ]
        public void FromFileTimeTest(int high, int low, long expectedTicks)
        {
            FILETIME fileTime;
            fileTime.dwHighDateTime = high;
            fileTime.dwLowDateTime = low;

            DateTime dt = Conversion.FileTimeToDateTime(fileTime);
            dt.Ticks.Should().Be(expectedTicks);
        }

        [Theory,
            InlineData(System.IO.FileMode.Open, CreationDisposition.OpenExisting),
            InlineData(System.IO.FileMode.Append, CreationDisposition.OpenAlways),
            InlineData(System.IO.FileMode.OpenOrCreate, CreationDisposition.OpenAlways),
            InlineData(System.IO.FileMode.Create, CreationDisposition.CreateAlways),
            InlineData(System.IO.FileMode.CreateNew, CreationDisposition.CreateNew),
            InlineData(System.IO.FileMode.Truncate, CreationDisposition.TruncateExisting)
            ]
        public void FromFileModeTest(System.IO.FileMode mode, CreationDisposition expected)
        {
            Conversion.FileModeToCreationDisposition(mode).Should().Be(expected);
        }

        [Theory,
            InlineData(0.0d, 599264352000000000),     // [12/30/1899 12:00:00 AM]
            InlineData(1.0d, 599265216000000000),     // [12/31/1899 12:00:00 AM]
            InlineData(-1.0d, 599263488000000000),    // [12/29/1899 12:00:00 AM]
            InlineData(-0.5d, 599264784000000000),    // [12/30/1899 12:00:00 PM]
            InlineData(0.5d, 599264784000000000)      // [12/30/1899 12:00:00 PM]
            ]
        public void OleTimeToDateTime(double oleTime, long expectedTicks)
        {
            DateTime expected = new DateTime(expectedTicks);
            DateTime result = Conversion.VariantDateToDateTime(oleTime);
            result.Should().Be(expected);
        }
    }
}