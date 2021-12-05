// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using WInterop.Globalization;

namespace WInterop.Com;

public unsafe struct TypeAttributes : IDisposable
{
    private TYPEATTR* _attr;
    private ITypeInfo* _owner;

    public TypeAttributes(TYPEATTR* attr, ITypeInfo* owner)
    {
        _attr = attr;
        _owner = owner;
    }

    public Guid Guid => _attr->guid;
    public LocaleId Locale => (LocaleId)_attr->lcid;
    public MemberId Constructor => _attr->memidConstructor;
    public MemberId Destructor => _attr->memidDestructor;
    public string Schema => new((char*)_attr->lpstrSchema);
    public TypeKind TypeKind => (TypeKind)_attr->typekind;
    public ushort FunctionCount => _attr->cFuncs;
    public ushort VariableCount => _attr->cVars;
    public ushort InterfaceCount => _attr->cImplTypes;
    public ushort VTableSize => _attr->cbSizeVft;
    public ushort Alignment => _attr->cbAlignment;
    public uint InstanceSize => _attr->cbSizeInstance;
    public TypeFlags TypeFlags => (TypeFlags)_attr->wTypeFlags;
    public IdlFlag IdlFlags => (IdlFlag)_attr->idldescType.wIDLFlags;
    public ushort MajorVersion => _attr->wMajorVerNum;
    public ushort MinorVersion => _attr->wMinorVerNum;
    public TypeDescription Alias => Unsafe.As<TYPEDESC, TypeDescription>(ref _attr->tdescAlias);

    public void Dispose()
    {
        if (_owner is not null)
        {
            _owner->ReleaseTypeAttr(_attr);
        }

        _owner = null;
        _attr = null;
    }
}