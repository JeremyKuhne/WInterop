﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Tests.Support;
using WInterop.Storage;

namespace HandlesTests;

public partial class Methods
{
    [Fact]
    public void CanCreateHandleToMountPointManager()
    {
        StoreHelper.ValidateStoreGetsUnauthorizedAccess(() =>
        {
            using var mountPointManager = Storage.CreateFile(
                @"\\.\MountPointManager", CreationDisposition.OpenExisting, 0, ShareModes.ReadWrite, AllFileAttributes.Normal);
            mountPointManager.IsInvalid.Should().BeFalse();
        });
    }
}
