// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;

namespace Tests.Support
{
    public static class StoreHelper
    {
        public static void ValidateStoreGetsUnauthorizedAccess(Action action)
        {
            try
            {
                action();
                WInterop.Support.Environment.IsWindowsStoreApplication().Should().BeFalse();
            }
            catch (UnauthorizedAccessException)
            {
                WInterop.Support.Environment.IsWindowsStoreApplication().Should().BeTrue();
            }
        }
    }
}
