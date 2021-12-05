// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace WInterop.Com;

public unsafe struct ArrayDescription
{
    private readonly ARRAYDESC* _desc;
    public ArrayDescription(ARRAYDESC* desc) => _desc = desc;

    public TypeDescription ElementType
        => _desc is null ? default : Unsafe.As<TYPEDESC, TypeDescription>(ref _desc->tdescElem);
    public uint Dimensions
        => _desc is null ? 0u : _desc->cDims;
    public ReadOnlySpan<SafeArrayBound> Bounds
        => new(&_desc->rgbounds.e0, (int)Dimensions);
}