// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Native;

public unsafe struct BP_PAINTPARAMS
{
    public uint cbSize;
    public BufferedPaintFlags dwFlags;
    public Rect* prcExclude;
    public BlendFunction* pBlendFunction;
}