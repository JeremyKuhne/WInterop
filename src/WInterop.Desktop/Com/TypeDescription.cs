// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

public unsafe struct TypeDescription
{
    private readonly TYPEDESC _desc;
    public VariantType Type => (VariantType)_desc.vt;

    public RefTypeHandle TypeHandle
        => Type is VariantType.UserDefined ? (RefTypeHandle)_desc.hreftype : default;

    public TypeDescription PointerType
        => Type is VariantType.Pointer || Type is VariantType.SafeArray
            ? *(TypeDescription*)_desc.lptdesc
            : default;

    public ArrayDescription ArrayDescription
        => Type is VariantType.CArray
            ? new(_desc.lpadesc)
            : default;
}