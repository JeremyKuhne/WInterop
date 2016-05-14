// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System.Collections.Generic;
using WInterop.FileManagement;
using WInterop.Tests.Support;
using WInterop.Utility;
using Xunit;

namespace WInterop.Tests.NativeMethodsTests
{
    public partial class HandlesTests
    {
        [Fact]
        public void CanCreateHandleToMountPointManager()
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                using (var mountPointManager = FileManagement.NativeMethods.CreateFile(
                    @"\\.\MountPointManager", 0, ShareMode.ReadWrite, CreationDisposition.OPEN_EXISTING, FileManagement.FileAttributes.FILE_ATTRIBUTE_NORMAL))
                {
                    mountPointManager.IsInvalid.Should().BeFalse();
                }
            });
        }
    }
}
