using System;
using BenchmarkDotNet;
using BenchmarkDotNet.Running;

namespace Performance
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<EnvironmentVariable>();
        }
    }
}
