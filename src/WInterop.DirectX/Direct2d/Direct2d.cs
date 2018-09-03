// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Errors;
using WInterop.Direct2d.Unsafe;

namespace WInterop.Direct2d
{
    public static class Direct2d
    {
        public static IFactory CreateFactory(
            FactoryType factoryType = FactoryType.SingleThreaded,
            DebugLevel debugLevel = DebugLevel.None)
        {
            Imports.D2D1CreateFactory(
                factoryType, new Guid(InterfaceIds.IID_ID2D1Factory), debugLevel, out IFactory factory)
                .ThrowIfFailed();

            return factory;
        }
    }
}
