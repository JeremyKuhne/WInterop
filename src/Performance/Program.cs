using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Running;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<EnvironmentVariable>();
        }
    }
}
