// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GraphicsInfrastructure
{
    /// <summary>
    /// [DXGI_SAMPLE_DESC]
    /// </summary>
    public readonly struct SampleDescriptor
    {
        public readonly uint Count;
        public readonly uint Quality;

        public SampleDescriptor(uint count = 0, uint quality = 0)
        {
            Count = count;
            Quality = quality;
        }
    }
}
