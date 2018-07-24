// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.GdiPlus
{
    public class GdiPlusSession : IDisposable
    {
        private UIntPtr _token;

        public GdiPlusSession()
        {
            _token = GdiPlusMethods.Startup();
        }

        public void Dispose()
        {
            GdiPlusMethods.Shutdown(_token);
            _token = UIntPtr.Zero;
        }
    }
}
