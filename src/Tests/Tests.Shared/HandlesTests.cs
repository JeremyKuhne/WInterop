// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using Tests.Support;
using WInterop.Storage;
using WInterop.Storage.Types;
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
                using (var mountPointManager = StorageMethods.CreateFile(
                    @"\\.\MountPointManager", CreationDisposition.OpenExisting, 0, ShareModes.ReadWrite, FileAttributes.Normal))
                {
                    mountPointManager.IsInvalid.Should().BeFalse();
                }
            });
        }
    }
}
