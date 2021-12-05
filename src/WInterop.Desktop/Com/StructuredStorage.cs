// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.Storage;

namespace WInterop.Com;

public unsafe struct StructuredStorage : IDisposable
{
    public IStorage* IStorage { get; }

    public StructuredStorage(IStorage* storage) => IStorage = storage;

    public bool IsNull => IStorage is null;

    public Stream CreateStream(string name, StorageMode mode = StorageMode.Default)
    {
        fixed (void* n = name)
        {
            IStream* stream;
            IStorage->CreateStream((ushort*)n, (uint)mode, 0, 0, &stream).ThrowIfFailed();
            return new(stream);
        }
    }

    public Stream OpenStream(string name, StorageMode mode = StorageMode.Default)
    {
        fixed (void* n = name)
        {
            IStream* stream;
            IStorage->OpenStream((ushort*)n, null, (uint)mode, 0, &stream).ThrowIfFailed();
            return new(stream);
        }
    }

    public StructuredStorage CreateStorage(string name, StorageMode mode = StorageMode.Default)
    {
        fixed (void* n = name)
        {
            IStorage* storage;
            IStorage->CreateStorage((ushort*)n, (uint)mode, 0, 0, &storage).ThrowIfFailed();
            return new(storage);
        }
    }

    public StructuredStorage OpenStorage(string name, StorageMode mode = StorageMode.Default)
    {
        fixed (void* n = name)
        {
            IStorage* storage;
            IStorage->OpenStorage((ushort*)n, null, (uint)mode, null, 0, &storage).ThrowIfFailed();
            return new(storage);
        }
    }

    public void CopyTo(StructuredStorage destination)
    {
        // TODO: Create overrides for exclusions (Guids, string names)
        IStorage->CopyTo(0, null, null, destination.IStorage).ThrowIfFailed();
    }

    public void MoveElementTo(string name, StructuredStorage destination, string newName, StorageMove move = StorageMove.Copy)
    {
        fixed (void* s = name)
        fixed (void* d = newName)
        {
            IStorage->MoveElementTo((ushort*)s, destination.IStorage, (ushort*)d, (uint)move).ThrowIfFailed();
        }
    }

    public void Commit(StorageCommit commit = StorageCommit.Default)
    {
        IStorage->Commit((uint)commit).ThrowIfFailed();
    }

    public void Revert() => IStorage->Revert().ThrowIfFailed();

    public StorageEnumerator Enumerate()
    {
        IEnumSTATSTG* enumerator;
        IStorage->EnumElements(0, null, 0, &enumerator).ThrowIfFailed();
        return new(enumerator);
    }

    public void DestroyElement(string name)
    {
        fixed (void* n = name)
        {
            IStorage->DestroyElement((ushort*)n).ThrowIfFailed();
        }
    }

    public void RenameElement(string oldName, string newName)
    {
        fixed (void* o = oldName)
        fixed (void* n = newName)
        {
            IStorage->RenameElement((ushort*)o, (ushort*)n).ThrowIfFailed();
        }
    }

    public void SetElementTimes(string name, DateTime? creation, DateTime? access, DateTime? modified)
    {
        FileTime c = new(creation ?? default);
        FileTime a = new(access ?? default);
        FileTime m = new(modified ?? default);

        fixed (void* n = name)
        {
            IStorage->SetElementTimes(
                (ushort*)n,
                creation.HasValue ? (FILETIME*)&c : null,
                access.HasValue ? (FILETIME*)&a : null,
                modified.HasValue ? (FILETIME*)&m : null).ThrowIfFailed();
        }
    }

    public void SetClass(Guid clsid) => IStorage->SetClass(&clsid).ThrowIfFailed();

    public void SetStateBits(uint stateBits, uint mask)
        => IStorage->SetStateBits(stateBits, mask).ThrowIfFailed();

    public StorageStats Stat(StatFlag flag = StatFlag.Default)
    {
        StorageStats stats;
        IStorage->Stat((STATSTG*)&stats, (uint)flag).ThrowIfFailed();
        return stats;
    }

    public void Dispose() => IStorage->Release();
}