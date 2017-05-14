// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Support;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using Xunit;

namespace Tests
{
    public partial class HandlesTests
    {
        [Fact]
        public void CanCreateHandleToMountPointManager()
        {
            StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
            {
                using (var mountPointManager = FileMethods.CreateFile(
                    @"\\.\MountPointManager", 0, ShareMode.ReadWrite, CreationDisposition.OpenExisting, FileAttributes.FILE_ATTRIBUTE_NORMAL))
                {
                    mountPointManager.IsInvalid.Should().BeFalse();
                }
            });
        }
    }
}
