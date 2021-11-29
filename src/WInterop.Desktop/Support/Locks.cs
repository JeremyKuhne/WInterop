// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support;

public static class Locks
{
    public readonly ref struct ReaderWriterLockSlimScope
    {
        private readonly ReaderWriterLockSlim _lock;
        private readonly Type _type;

        public ReaderWriterLockSlimScope(ReaderWriterLockSlim @lock, Type type)
        {
            _lock = @lock;
            _type = type;
            switch (type)
            {
                case Type.Read:
                    _lock.EnterReadLock();
                    break;
                case Type.Write:
                    _lock.EnterWriteLock();
                    break;
                case Type.UpgradableRead:
                    _lock.EnterUpgradeableReadLock();
                    break;
            }
        }

        public void Dispose()
        {
            switch (_type)
            {
                case Type.Read:
                    _lock.ExitReadLock();
                    break;
                case Type.Write:
                    _lock.ExitWriteLock();
                    break;
                case Type.UpgradableRead:
                    _lock.ExitUpgradeableReadLock();
                    break;
            }
        }
    }

    public enum Type
    {
        Read,
        Write,
        UpgradableRead
    }

    public static ReaderWriterLockSlimScope Lock(this ReaderWriterLockSlim @lock, Type type)
        => new(@lock, type);
}