// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;

namespace WInterop.Com;

public unsafe struct StorageStats : IDisposable
{
    private STATSTG _stats;

    public ReadOnlySpan<char> Name => Strings.GetSpanFromNullTerminatedBuffer((char*)_stats.pwcsName);
    public StorageType StorageType => (StorageType)_stats.type;
    public ulong Size => _stats.cbSize.QuadPart;
    public DateTime Modified => _stats.mtime.ToDateTimeUTC();
    public DateTime Created => _stats.ctime.ToDateTimeUTC();
    public DateTime Accessed => _stats.atime.ToDateTimeUTC();
    public StorageMode Mode => (StorageMode)_stats.grfMode;
    public LockType LocksSupported => (LockType)_stats.grfLocksSupported;
    public Guid ClassId => _stats.clsid;
    public uint StateBits => _stats.grfStateBits;

    public void Dispose()
    {
        if (_stats.pwcsName is not null)
        {
            TerraFXWindows.CoTaskMemFree(_stats.pwcsName);
            _stats.pwcsName = null;
        }
    }
}