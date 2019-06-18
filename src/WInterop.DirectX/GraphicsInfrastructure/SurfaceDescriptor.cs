// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GraphicsInfrastructure
{
    public readonly struct SurfaceDescriptor
    {
        public readonly uint Width;
        public readonly uint Height;
        public readonly Format Format;
        public readonly SampleDescriptor SampleDescription;
    }
}
