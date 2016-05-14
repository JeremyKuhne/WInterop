// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Linq;
using WInterop.FileManagement;
using WInterop.Tests.Support;
using Xunit;

namespace WInterop.DesktopTests.NativeMethodTests
{
    public partial class FileManagementTests
    {
        [Fact]
        public void GetShortPathBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            FileManagement.Desktop.NativeMethods.GetShortPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetLongPathBasic()
        {
            string tempPath = FileManagement.NativeMethods.GetTempPath();
            FileManagement.Desktop.NativeMethods.GetLongPathName(tempPath).Should().NotBeNullOrWhiteSpace();
        }
    }
}
