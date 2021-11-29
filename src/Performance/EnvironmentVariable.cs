using BenchmarkDotNet.Attributes;
using WInterop.Errors;
using WInterop.ProcessAndThreads;
using WInterop.ProcessAndThreads.Native;
using WInterop.Support.Buffers;

namespace Performance
{
    public class EnvironmentVariable
    {
        [Benchmark(Baseline = true)]
        public string GetTest()
        {
            return Processes.GetEnvironmentVariable("USERNAME");
        }

        [Benchmark]
        public unsafe string GetTestMoreDirect()
        {
            const uint MinBufferSize = 128;
            var buffer = StringBufferCache.Instance.Acquire(minCharCapacity: MinBufferSize);
            uint returnValue;

            // Ensure enough room for the output string
            fixed (char* c = "USERNAME")
            {
                while ((returnValue = ProcessAndThreadImports.GetEnvironmentVariableW(c, buffer.CharPointer, buffer.CharCapacity)) > buffer.CharCapacity)
                    buffer.EnsureCharCapacity(returnValue);
            }

            if (returnValue == 0)
                Error.ThrowIfLastErrorNot(WindowsError.ERROR_ENVVAR_NOT_FOUND);
            buffer.Length = returnValue;
            return StringBufferCache.Instance.ToStringAndRelease(buffer);
        }

        [Benchmark]
        public unsafe string GetTestValueBuffer()
        {
            Span<char> stack = stackalloc char[128];
            ValueBuffer<char> buffer = new ValueBuffer<char>(stack);

            fixed (char* n = "USERNAME")
            fixed (char* c = &buffer[0])
            {
                uint returnValue;
                while ((returnValue = ProcessAndThreadImports.GetEnvironmentVariableW(n, c, (uint)buffer.Length)) > buffer.Length)
                {
                    buffer.EnsureCapacity((int)returnValue);
                }

                if (returnValue == 0)
                {
                    Error.ThrowIfLastErrorNot(WindowsError.ERROR_ENVVAR_NOT_FOUND);
                    return string.Empty;
                }
                return new string(buffer.Span.Slice(0, (int)returnValue));
            }
        }

        [Benchmark]
        public unsafe string GetTestSuperDirect()
        {
            Span<char> buffer = stackalloc char[128];

            fixed (char* n = "USERNAME")
            fixed (char* c = buffer)
            {
                uint returnValue = ProcessAndThreadImports.GetEnvironmentVariableW(n, c, (uint)buffer.Length);
                if (returnValue == 0)
                    Error.ThrowIfLastErrorNot(WindowsError.ERROR_ENVVAR_NOT_FOUND);
                return new string(buffer.Slice(0, (int)returnValue));
            }
        }
    }
}
