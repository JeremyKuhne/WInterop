// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop.Com;

public unsafe struct TypeLibrary : IDisposable
{
    public ITypeLib* ITypeLib { get; private set; }

    public TypeLibrary(ITypeLib* handle) => ITypeLib = handle;

    public bool IsNull => ITypeLib is null;

    public uint GetTypeInfoCount() => ITypeLib->GetTypeInfoCount();

    public TypeInfo GetTypeInfo(uint index)
    {
        ITypeInfo* info;
        ITypeLib->GetTypeInfo(index, &info).ThrowIfFailed();
        return new(info);
    }

    public TypeKind GetTypeInfoType(uint index)
    {
        TypeKind kind;
        ITypeLib->GetTypeInfoType(index, (TYPEKIND*)&kind).ThrowIfFailed();
        return kind;
    }

    public TypeInfo GetTypeInfoOfGuid(Guid guid)
    {
        ITypeInfo* info;
        ITypeLib->GetTypeInfoOfGuid(&guid, &info).ThrowIfFailed();
        return new(info);
    }

    public TypeLibraryAttributes GetLibraryAttributes()
    {
        TLIBATTR* attr;
        ITypeLib->GetLibAttr(&attr).ThrowIfFailed();
        TypeLibraryAttributes attributes = *(TypeLibraryAttributes*)attr;
        ITypeLib->ReleaseTLibAttr(attr);
        return attributes;
    }

    public TypeCompilation GetTypeCompilation()
    {
        ITypeComp* comp;
        ITypeLib->GetTypeComp(&comp).ThrowIfFailed();
        return new(comp);
    }

    public void GetDocumentation(
        int index,
        out string name)
    {
        ushort* namep;
        ITypeLib->GetDocumentation(index, &namep, null, null, null).ThrowIfFailed();
        name = Strings.FromBSTRAndFree(namep);
    }

    public void GetDocumentation(
        int index,
        out string name,
        out string documentation)
    {
        ushort* namep;
        ushort* docs;
        ITypeLib->GetDocumentation(index, &namep, &docs, null, null).ThrowIfFailed();
        name = Strings.FromBSTRAndFree(namep);
        documentation = Strings.FromBSTRAndFree(docs);
    }

    public void GetDocumentation(
        int index,
        out string name,
        out string documentation,
        out uint helpContext,
        out string helpFile)
    {
        ushort* namep;
        ushort* docs;
        uint context;
        ushort* file;
        ITypeLib->GetDocumentation(index, &namep, &docs, &context, &file).ThrowIfFailed();
        name = Strings.FromBSTRAndFree(namep);
        documentation = Strings.FromBSTRAndFree(docs);
        helpContext = context;
        helpFile = Strings.FromBSTRAndFree(file);
    }

    public bool IsName(string name, out string foundName)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        Span<char> copy = stackalloc char[name.Length + 1];
        name.AsSpan().CopyTo(copy);
        BOOL isName;

        fixed (void* c = copy)
        {
            ITypeLib->IsName((ushort*)c, 0, &isName).ThrowIfFailed();
        }

        foundName = copy.SequenceEqual(name) ? name : copy[0..^1].ToString();

        return isName;
    }

    /// <summary>
    ///  Finds the first type with the given name. <see cref="TypeInfo.IsNull"/> will be true if the
    ///  type is not found.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <exception cref="ArgumentNullException"><paramref name="name"/> was null.</exception>
    public (MemberId Id, TypeInfo Info) FindName(string name)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        // The incoming string is recased so we need a copy.
        Span<char> copy = stackalloc char[name.Length + 1];
        name.AsSpan().CopyTo(copy);

        ITypeInfo* info;
        MemberId id;
        ushort found = 1;

        fixed (void* c = copy)
        {
            ITypeLib->FindName((ushort*)c, 0, &info, (int*)&id, &found).ThrowIfFailed();
        }

        return (id, new(info));
    }

    /// <summary>
    ///  Finds the first type with the given name. <see cref="TypeInfo.IsNull"/> will be true if the
    ///  type is not found.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <param name="foundName">The actual casing for the found name, if any.</param>
    /// <exception cref="ArgumentNullException"><paramref name="name"/> was null.</exception>
    public (MemberId Id, TypeInfo Info) FindName(string name, out string foundName)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));

        // The incoming string is recased so we need a copy.
        Span<char> copy = stackalloc char[name.Length + 1];
        name.AsSpan().CopyTo(copy);

        ITypeInfo* info;
        MemberId id;
        ushort found = 1;

        fixed (void* c = copy)
        {
            ITypeLib->FindName((ushort*)c, 0, &info, (int*)&id, &found).ThrowIfFailed();
        }

        foundName = copy.SequenceEqual(name) ? name : copy[0..^1].ToString();

        return (id, new(info));
    }

    /// <summary>
    ///  Find types with the given name up to the specified <paramref name="maxHits"/>.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <param name="maxHits">Maximum number of hits to return.</param>
    /// <param name="foundName">The actual casing for the found name, if any.</param>
    /// <exception cref="ArgumentNullException"><paramref name="name"/> was null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxHits"/> was zero.</exception>
    public IReadOnlyList<(MemberId Id, TypeInfo Info)> FindName(string name, ushort maxHits, out string foundName)
    {
        if (name is null)
            throw new ArgumentNullException(nameof(name));
        if (maxHits < 1)
            throw new ArgumentOutOfRangeException(nameof(maxHits));

        // The incoming string is recased so we need a copy.
        Span<char> copy = stackalloc char[name.Length + 1];
        name.AsSpan().CopyTo(copy);

        ITypeInfo*[] infos = new ITypeInfo*[maxHits];
        MemberId[] ids = new MemberId[maxHits];
        ushort found = maxHits;

        fixed (void* c = copy)
        fixed (ITypeInfo** i = infos)
        fixed (void* m = ids)
        {
            ITypeLib->FindName((ushort*)c, 0, i, (int*)m, &found).ThrowIfFailed();
        }

        (MemberId, TypeInfo)[] results = new (MemberId, TypeInfo)[found];
        for (int i = 0; i < found; i++)
        {
            results[i] = (ids[i], new(infos[i]));
        }

        foundName = copy.SequenceEqual(name) ? name : copy[0..^1].ToString();
        return results;
    }

    public void Dispose()
    {
        ITypeLib->Release();
        ITypeLib = null;
    }
}