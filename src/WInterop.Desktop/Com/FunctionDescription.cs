// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

public unsafe struct FunctionDescription : IDisposable
{
    public FUNCDESC* FUNCDESC { get; private set; }
    private ITypeInfo* _owner;

    public FunctionDescription(FUNCDESC* desc, ITypeInfo* owner)
    {
        FUNCDESC = desc;
        _owner = owner;
    }

    public readonly MemberId MemberId => FUNCDESC->memid;
    public readonly FunctionKind FunctionKind => (FunctionKind)FUNCDESC->funckind;
    public readonly InvokeKind InvokeKind => (InvokeKind)FUNCDESC->invkind;
    public readonly CallConvention CallConvention => (CallConvention)FUNCDESC->callconv;
    public readonly short ParameterCount => FUNCDESC->cParams;
    public readonly short OptionalParameterCount => FUNCDESC->cParamsOpt;
    public readonly short VTableOffset => FUNCDESC->oVft;
    public readonly short ReturnCodeCount => FUNCDESC->cScodes;
    public readonly VariantType ReturnType => (VariantType)FUNCDESC->elemdescFunc.tdesc.vt;
    public readonly FunctionFlags Flags => (FunctionFlags)FUNCDESC->wFuncFlags;

    /// <summary>
    ///  Parameter descriptions.
    /// </summary>
    /// <remarks>
    ///  Not valid after <see cref="FunctionDescription"/> is disposed.
    /// </remarks>
    public readonly ReadOnlySpan<ParameterDescription> Parameters => new(FUNCDESC->lprgelemdescParam, ParameterCount);

    public void Dispose()
    {
        if (_owner is not null)
        {
            _owner->ReleaseFuncDesc(FUNCDESC);
        }

        _owner = null;
        FUNCDESC = null;
    }
}