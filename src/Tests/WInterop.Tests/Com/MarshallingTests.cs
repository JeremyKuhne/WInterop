// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Runtime.InteropServices;
using Tests.Support;
using WInterop;
using WInterop.Com;
using WInterop.Errors;
using WInterop.Security.Native;
using WInterop.Storage;
using Xunit;

namespace ComTests
{
    public class MarshallingTests
    {
        [Fact]
        public unsafe void Unknown()
        {
            using var cleaner = new TestFileCleaner();
            IUnknown* unknown = (IUnknown*)CreateStorage(cleaner.GetTestPath(), InterfaceIds.IID_IStorage);

            uint refCount = unknown->AddRef();
            refCount.Should().Be(2);
            refCount = unknown->Release();
            refCount.Should().Be(1);

            Guid riid = InterfaceIds.IID_IStorage;
            IUnknown* queried;
            HResult result = unknown->QueryInterface(&riid, (void**)&queried);
            result.Should().Be(HResult.S_OK);

            refCount = queried->AddRef();

            // 1 + QueryInteface + AddRef
            refCount.Should().Be(3);

            unknown->Release().Should().Be(2);
            queried->Release().Should().Be(1);
            unknown->Release().Should().Be(0);
        }

        public static unsafe void* CreateStorage(
            string path,
            Guid riid,
            StorageMode mode = StorageMode.ReadWrite | StorageMode.Create | StorageMode.ShareExclusive,
            StorageFormat format = StorageFormat.DocFile)
        {
            STGOPTIONS options = new STGOPTIONS
            {
                usVersion = 1,

                // If possible, we want the larger 4096 sector size
                ulSectorSize = (mode & StorageMode.Simple) != 0 ? 512u : 4096
            };

            void* created = default;

            StgCreateStorageEx(
                path,
                mode,
                format,
                0,
                format == StorageFormat.DocFile ? &options : null,
                null,
                ref riid,
                &created).ThrowIfFailed(path);

            return created;
        }

        // https://docs.microsoft.com/windows/win32/api/coml2api/nf-coml2api-stgcreatestorageex
        [DllImport(Libraries.Ole32, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static unsafe extern HResult StgCreateStorageEx(
            string pwcsName,
            StorageMode grfMode,
            StorageFormat stgfmt,
            FileFlags grfAttrs,
            STGOPTIONS* pStgOptions,
            SECURITY_DESCRIPTOR** pSecurityDescriptor,
            ref Guid riid,
            void** ppObjectOpen);
    }
}
