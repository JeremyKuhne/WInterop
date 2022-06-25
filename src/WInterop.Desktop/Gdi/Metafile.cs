// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Gdi;

public sealed class Metafile : IDisposable
{
    private readonly HENHMETAFILE _hemf;

    public const uint EMFPlusSignature = 0x2B464D45; // '+' 'F' 'M' 'E'

    public Metafile(HENHMETAFILE hemf) => _hemf = hemf;

    public static implicit operator HENHMETAFILE(Metafile metafile) => metafile._hemf;

    [UnmanagedCallersOnly]
    private static unsafe int EnumCallback(HDC hdc, HANDLETABLE* lpht, ENHMETARECORD* lpmr, int nHandles, LPARAM data)
    {
        GCHandle handle = GCHandle.FromIntPtr(data);
        var callback = (EnumerateMetafileCallback)handle.Target!;
        MetafileRecord record = new(lpmr);
        return (BOOL)callback(ref record);
    }

    public unsafe void Enumerate(EnumerateMetafileCallback callback)
    {
        GCHandle handle = GCHandle.Alloc(callback);
        TerraFXWindows.EnumEnhMetaFile(
            default,
            _hemf,
            &EnumCallback,
            (void*)(IntPtr)handle,
            null);

        if (handle.IsAllocated)
        {
            handle.Free();
        }
    }

    public void Dispose()
    {
        TerraFXWindows.DeleteEnhMetaFile(_hemf);
    }
}