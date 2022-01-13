// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace WInterop.Com;

public unsafe struct ParameterDescription
{
    private ELEMDESC _desc;

    public TypeDescription ParameterType => Unsafe.As<TYPEDESC, TypeDescription>(ref _desc.tdesc);
    public ParameterFlags Flags => (ParameterFlags)_desc.paramdesc.wParamFlags;
}