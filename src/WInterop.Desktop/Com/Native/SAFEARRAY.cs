// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com.Native;

public unsafe struct SAFEARRAY
{
    public ushort cDims;
    public FADF fFeatures;
    public uint cbElements;
    public uint cLocks;
    public void* pvData;

    public SafeArrayBound _rgsabound;

    public ReadOnlySpan<SafeArrayBound> rgsabound => TrailingArray<SafeArrayBound>.GetBuffer(in _rgsabound, cDims);
}