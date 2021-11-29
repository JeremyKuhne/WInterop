// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.Direct2d
{
    public static class Direct2d
    {
        public unsafe static Factory CreateFactory(
            FactoryType factoryType = FactoryType.SingleThreaded,
            DebugLevel debugLevel = DebugLevel.None)
        {
            ID2D1Factory* factory;
            HResult result = TerraFX.Interop.DirectX.DirectX.D2D1CreateFactory<ID2D1Factory>(
                (D2D1_FACTORY_TYPE)factoryType,
                (D2D1_FACTORY_OPTIONS*)&debugLevel,
                (void**)&factory).ToHResult();

            result.ThrowIfFailed();
            return new(factory);
        }
    }
}
