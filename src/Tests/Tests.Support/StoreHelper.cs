// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;

namespace WInterop.Tests.Support
{
    public static class StoreHelper
    {
        public static void ValidateStoreGetsUnauthorizedAccess(Action action)
        {
            try
            {
                action();
                Utility.Environment.IsWindowsStoreApplication().Should().BeFalse();
            }
            catch (UnauthorizedAccessException)
            {
                Utility.Environment.IsWindowsStoreApplication().Should().BeTrue();
            }
        }
    }
}
