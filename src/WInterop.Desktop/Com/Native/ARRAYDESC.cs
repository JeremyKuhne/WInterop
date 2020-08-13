// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com.Native
{
    public ref struct ARRAYDESC
    {
        public TYPEDESC tdescElem;
        public ushort cDims;
        private SafeArrayBound _rgbounds;
        public ReadOnlySpan<SafeArrayBound> rgbounds => TrailingArray<SafeArrayBound>.GetBuffer(ref _rgbounds, cDims);
    }
}
