// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;

namespace WInterop.Com;

public unsafe struct VariableDescription : IDisposable
{
    private ITypeInfo* _info;
    private VARDESC* _desc;

    public VariableDescription(VARDESC* handle, ITypeInfo* info)
    {
        _desc = handle;
        _info = info;
    }

    public VariableKind Kind => (VariableKind)_desc->varkind;
    public MemberId MemberId => _desc->memid;
    public VariableFlags Flags => (VariableFlags)_desc->wVarFlags;
    public TypeDescription VariableType => Unsafe.As<TYPEDESC, TypeDescription>(ref _desc->elemdescVar.tdesc);

    public void Dispose()
    {
        if (_info is not null)
        {
            _info->ReleaseVarDesc(_desc);
        }

        _info = null;
        _desc = null;
    }
}