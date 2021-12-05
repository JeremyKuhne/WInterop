using BenchmarkDotNet.Attributes;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;
using WInterop.Com;

namespace Performance;

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
        _punk = (IUnknown*)Com.CreateStorage(_path).IStorage;
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
}
