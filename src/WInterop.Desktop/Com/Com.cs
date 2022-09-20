// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Com.Native;
using WInterop.Errors;
using WInterop.Globalization;

namespace WInterop.Com;

public static unsafe partial class Com
{
    public static StructuredStorage CreateStorage(
        string path,
        StorageMode mode = StorageMode.ReadWrite | StorageMode.Create | StorageMode.ShareExclusive,
        StorageFormat format = StorageFormat.DocFile)
    {
        return new((IStorage*)CreateStorageInternal(
            path,
            InterfaceIds.IID_IStorage,
            mode,
            format));
    }

    private static void* CreateStorageInternal(
        string path,
        Guid riid,
        StorageMode mode = StorageMode.ReadWrite | StorageMode.Create | StorageMode.ShareExclusive,
        StorageFormat format = StorageFormat.DocFile)
    {
        STGOPTIONS options = new()
        {
            usVersion = 1,

            // If possible, we want the larger 4096 sector size
            ulSectorSize = (mode & StorageMode.Simple) != 0 ? 512u : 4096
        };

        void* created;
        fixed (void* p = path)
        {
            TerraFXWindows.StgCreateStorageEx(
                (ushort*)p,
                (uint)mode,
                (uint)format,
                0,
                format == StorageFormat.DocFile ? &options : null,
                null,
                &riid,
                &created).ThrowIfFailed(path);
        }

        return created;
    }

    public static StructuredStorage OpenStorage(
        string path,
        StorageMode mode = StorageMode.ReadWrite | StorageMode.ShareExclusive,
        StorageFormat format = StorageFormat.Any)
    {
        return new((IStorage*)OpenStorageInternal(
            path,
            InterfaceIds.IID_IStorage,
            mode,
            format));
    }

    private static void* OpenStorageInternal(
        string path,
        Guid riid,
        StorageMode mode = StorageMode.ReadWrite | StorageMode.ShareExclusive,
        StorageFormat format = StorageFormat.Any)
    {
        STGOPTIONS options = new()
        {
            // Must have version set before using
            usVersion = 1
        };

        void* created;
        fixed (void* p = path)
        {
            TerraFXWindows.StgOpenStorageEx(
                (ushort*)p,
                (uint)mode,
                (uint)format,
                0,
                format == StorageFormat.DocFile ? &options : null,
                null,
                &riid,
                &created).ThrowIfFailed(path);
        }

        return created;
    }

    public static bool IsStorageFile(string path)
    {
        fixed (void* p = path)
        {
            return TerraFXWindows.StgIsStorageFile((ushort*)p).ToHResult() == HResult.S_OK;
        }
    }

    public static TypeLibrary LoadRegisteredTypeLibrary(Guid guid, ushort majorVersion, ushort minorVersion = 0, LocaleId locale = default)
    {
        ITypeLib* library;
        TerraFXWindows.LoadRegTypeLib(&guid, majorVersion, minorVersion, locale.RawValue, &library).ThrowIfFailed();
        return new(library);
    }

    public static unsafe uint Release(IntPtr pUnk)
        => pUnk == IntPtr.Zero ? throw new ArgumentNullException(nameof(pUnk)) : ((IUnknown*)pUnk)->Release();
}