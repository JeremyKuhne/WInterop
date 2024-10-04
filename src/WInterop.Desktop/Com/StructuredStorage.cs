// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.Storage;

namespace WInterop.Com;

public readonly unsafe struct StructuredStorage : IDisposable
{
    public IStorage* IStorage { get; }

    public StructuredStorage(IStorage* storage) => IStorage = storage;

    public readonly bool IsNull => IStorage is null;

    public readonly Stream CreateStream(string name, StorageMode mode = StorageMode.Default)
    {
        fixed (void* n = name)
        {
            IStream* stream;
            IStorage->CreateStream((char*)n, (uint)mode, 0, 0, &stream).ThrowIfFailed();
            return new(stream);
        }
    }

    public readonly Stream OpenStream(string name, StorageMode mode = StorageMode.Default)
    {
        fixed (void* n = name)
        {
            IStream* stream;
            IStorage->OpenStream((char*)n, null, (uint)mode, 0, &stream).ThrowIfFailed();
            return new(stream);
        }
    }

    public readonly StructuredStorage CreateStorage(string name, StorageMode mode = StorageMode.Default)
    {
        fixed (void* n = name)
        {
            IStorage* storage;
            IStorage->CreateStorage((char*)n, (uint)mode, 0, 0, &storage).ThrowIfFailed();
            return new(storage);
        }
    }

    public readonly StructuredStorage OpenStorage(string name, StorageMode mode = StorageMode.Default)
    {
        fixed (void* n = name)
        {
            IStorage* storage;
            IStorage->OpenStorage((char*)n, null, (uint)mode, null, 0, &storage).ThrowIfFailed();
            return new(storage);
        }
    }

    public readonly void CopyTo(StructuredStorage destination)
    {
        // TODO: Create overrides for exclusions (Guids, string names)
        IStorage->CopyTo(0, null, null, destination.IStorage).ThrowIfFailed();
    }

    public readonly void MoveElementTo(string name, StructuredStorage destination, string newName, StorageMove move = StorageMove.Copy)
    {
        fixed (void* s = name)
        fixed (void* d = newName)
        {
            IStorage->MoveElementTo((char*)s, destination.IStorage, (char*)d, (uint)move).ThrowIfFailed();
        }
    }

    public readonly void Commit(StorageCommit commit = StorageCommit.Default)
    {
        IStorage->Commit((uint)commit).ThrowIfFailed();
    }

    public readonly void Revert() => IStorage->Revert().ThrowIfFailed();

    public readonly StorageEnumerator Enumerate()
    {
        IEnumSTATSTG* enumerator;
        IStorage->EnumElements(0, null, 0, &enumerator).ThrowIfFailed();
        return new(enumerator);
    }

    public readonly void DestroyElement(string name)
    {
        fixed (void* n = name)
        {
            IStorage->DestroyElement((char*)n).ThrowIfFailed();
        }
    }

    public readonly void RenameElement(string oldName, string newName)
    {
        fixed (void* o = oldName)
        fixed (void* n = newName)
        {
            IStorage->RenameElement((char*)o, (char*)n).ThrowIfFailed();
        }
    }

    public readonly void SetElementTimes(string name, DateTime? creation, DateTime? access, DateTime? modified)
    {
        FileTime c = new(creation ?? default);
        FileTime a = new(access ?? default);
        FileTime m = new(modified ?? default);

        fixed (void* n = name)
        {
            IStorage->SetElementTimes(
                (char*)n,
                creation.HasValue ? (FILETIME*)&c : null,
                access.HasValue ? (FILETIME*)&a : null,
                modified.HasValue ? (FILETIME*)&m : null).ThrowIfFailed();
        }
    }

    public readonly void SetClass(Guid clsid) => IStorage->SetClass(&clsid).ThrowIfFailed();

    public readonly void SetStateBits(uint stateBits, uint mask)
        => IStorage->SetStateBits(stateBits, mask).ThrowIfFailed();

    public readonly StorageStats Stat(StatFlag flag = StatFlag.Default)
    {
        StorageStats stats;
        IStorage->Stat((STATSTG*)&stats, (uint)flag).ThrowIfFailed();
        return stats;
    }

    public readonly void Dispose() => IStorage->Release();
}