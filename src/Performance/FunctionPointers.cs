using BenchmarkDotNet.Attributes;
using System;
using System.IO;
using System.Runtime.InteropServices;
using WInterop;
using WInterop.Com;
using WInterop.Errors;
using WInterop.Security.Native;
using WInterop.Storage;

namespace Performance
{
    [DisassemblyDiagnoser]
    [MemoryDiagnoser]
    public unsafe class FunctionPointers
    {
        private IUnknown* _punk;
        private string _path;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _path = Path.Join(Path.GetTempPath(), Path.GetRandomFileName());
            _punk = (IUnknown*)CreateStorage(_path, InterfaceIds.IID_IStorage);
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            while (_punk->Release() > 0) ;

            try
            {
                File.Delete(_path);
            }
            catch
            {
            }
        }

        // The direct code is smaller as it doesn't have a null check and throw. Otherwise the generated code is
        // pretty much the same.
        // 
        // |         Method |      Mean |     Error |    StdDev | Ratio | Code Size |
        // |--------------- |----------:|----------:|----------:|------:|----------:|
        // |         AddRef |  9.683 ns | 0.0662 ns | 0.0587 ns |  1.00 |     136 B |
        // | AddRef_Marshal | 10.083 ns | 0.0864 ns | 0.0766 ns |  1.04 |     207 B |

        [Benchmark(Baseline = true)]
        public uint AddRef()
        {
            return _punk->AddRef();
        }

        [Benchmark]
        public uint AddRef_Marshal()
        {
            return (uint)Marshal.AddRef((IntPtr)_punk);
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
