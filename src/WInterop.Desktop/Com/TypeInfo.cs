// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop.Com;

/// <summary>
///  Wrapper for easy access to <see cref="TerraFX.Interop.Windows.ITypeInfo"/>.
/// </summary>
public unsafe struct TypeInfo : IDisposable
{
    public ITypeInfo* ITypeInfo { get; private set; }

    public TypeInfo(ITypeInfo* handle) => ITypeInfo = handle;

    public bool IsNull => ITypeInfo is null;

    public void* AddressOfMember(MemberId memberId, InvokeKind invokeKind)
    {
        void* address;
        ITypeInfo->AddressOfMember(memberId, (INVOKEKIND)invokeKind, &address).ThrowIfFailed();
        return address;
    }

    /// <summary>
    ///  Create a new instance of a type that describes a component object class (coclass).
    /// </summary>
    public void* CreateInstance(void* outerUnknown, Guid riid)
    {
        void* instance;
        ITypeInfo->CreateInstance((IUnknown*)outerUnknown, &riid, &instance).ThrowIfFailed();
        return instance;
    }

    public (TypeLibrary Library, uint Index) GetContainingTypeLibrary()
    {
        ITypeLib* library;
        uint index;

        ITypeInfo->GetContainingTypeLib(&library, &index).ThrowIfFailed();
        return (new(library), index);
    }

    public void GetDocumentation(
        MemberId memberId,
        out string name)
    {
        ushort* namep;
        ITypeInfo->GetDocumentation(memberId, &namep, null, null, null).ThrowIfFailed();
        name = Strings.FromBSTRAndFree(namep);
    }

    public void GetDocumentation(
        MemberId memberId,
        out string name,
        out string documentation)
    {
        ushort* namep;
        ushort* docs;
        ITypeInfo->GetDocumentation(memberId, &namep, &docs, null, null).ThrowIfFailed();
        name = Strings.FromBSTRAndFree(namep);
        documentation = Strings.FromBSTRAndFree(docs);
    }

    public void GetDocumentation(
        MemberId memberId,
        out string name,
        out string documentation,
        out uint helpContext,
        out string helpFile)
    {
        ushort* namep;
        ushort* docs;
        uint context;
        ushort* file;
        ITypeInfo->GetDocumentation(memberId, &namep, &docs, &context, &file).ThrowIfFailed();
        name = Strings.FromBSTRAndFree(namep);
        documentation = Strings.FromBSTRAndFree(docs);
        helpContext = context;
        helpFile = Strings.FromBSTRAndFree(file);
    }

    public VariableDescription GetVariableDescription(uint variableIndex)
    {
        VARDESC* description;
        ITypeInfo->GetVarDesc(variableIndex, &description)
            .ThrowIfFailed($"Failed to get description for variable index: {variableIndex}");

        return new(description, ITypeInfo);
    }

    public FunctionDescription GetFunctionDescription(uint index)
    {
        FUNCDESC* description;
        ITypeInfo->GetFuncDesc(index, &description).ThrowIfFailed();
        return new(description, ITypeInfo);
    }

    public string? GetMemberName(MemberId memberId)
    {
        ushort* buffer;
        uint count = 1;
        ITypeInfo->GetNames(memberId, &buffer, count, &count).ThrowIfFailed();
        return Strings.FromBSTRAndFree(buffer);
    }

    public IReadOnlyList<string> GetMemberNames(MemberId memberId, uint maxNames)
    {
        if (maxNames < 1)
            throw new ArgumentOutOfRangeException(nameof(maxNames));

        ushort*[] buffer = new ushort*[maxNames];
        uint count = maxNames;

        fixed (ushort** b = buffer)
        {
            ITypeInfo->GetNames(memberId, b, count, &count)
                .ThrowIfFailed($"Failed to get names for member id: {memberId}");
        }

        string[] results = new string[count];
        for (int i = 0; i < count; i++)
        {
            results[i] = Strings.FromBSTRAndFree(buffer[i]);
        }

        return results;
    }

    /// <summary>
    ///  Get the names for the given <paramref name="functionIndex"/>. The first name is the
    ///  name of the function, the rest (if any) are parameter names.
    /// </summary>
    public IReadOnlyList<string> GetFunctionNames(uint functionIndex)
    {
        FUNCDESC* description;
        ITypeInfo->GetFuncDesc(functionIndex, &description)
            .ThrowIfFailed($"Failed to get description for function index: {functionIndex}");

        uint count = (uint)description->cParams + 1;
        MemberId id = description->memid;
        ITypeInfo->ReleaseFuncDesc(description);
        return GetMemberNames(id, count);
    }

    public TypeAttributes GetTypeAttributes()
    {
        TYPEATTR* attr;
        ITypeInfo->GetTypeAttr(&attr).ThrowIfFailed();
        return new(attr, ITypeInfo);
    }

    public TypeInfo GetRefTypeInfo(RefTypeHandle handle)
    {
        ITypeInfo* info;
        ITypeInfo->GetRefTypeInfo(handle, &info).ThrowIfFailed();
        return new(info);
    }

    // TODO: Implement other wrappers

    public void Dispose()
    {
        if (ITypeInfo is not null)
        {
            ITypeInfo->Release();
        }

        ITypeInfo = null;
    }
}