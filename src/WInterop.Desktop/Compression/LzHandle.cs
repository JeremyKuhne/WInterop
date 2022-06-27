// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Compression;

public class LzHandle : IDisposable
{
    // The handles used by the LZ methods are not IntPtr like most handles.

    public LzHandle(int handle) => RawHandle = handle;

    public int RawHandle { get; private set; }

    protected virtual void Dispose(bool disposing) => TerraFXWindows.LZClose(RawHandle);

    ~LzHandle() => Dispose(disposing: false);

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public static implicit operator int(LzHandle handle) => handle.RawHandle;
}