// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Direct2d;

namespace WInterop.GraphicsInfrastructure
{
    /// <summary>
    /// [DXGI_SWAP_CHAIN_DESC1]
    /// </summary>
    public struct SwapChainDescriptor1
    {
        public uint Width;
        public uint Height;
        public Format Format;
        public BOOL Stereo;
        public SampleDescriptor SampleDescriptor;
        public Usage BufferUsage;
        public uint BufferCount;
        public Scaling Scaling;
        public SwapEffect SwapEffect;
        public AlphaMode AlphaMode;
        public uint Flags;
    }
}
