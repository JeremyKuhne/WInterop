// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.WindowsStore;

namespace Tests.Support
{
    public static class StoreHelper
    {
        public static void ValidateStoreGetsUnauthorizedAccess(Action action)
        {
            try
            {
                action();
                if (WindowsStore.IsWindowsStoreApplication())
                    throw new InvalidOperationException("Should not succeed if Windows Store app");
            }
            catch (UnauthorizedAccessException)
            {
                if (!WindowsStore.IsWindowsStoreApplication())
                    throw new InvalidOperationException("Should succeed if not Windows Store app");
            }
        }
    }
}
