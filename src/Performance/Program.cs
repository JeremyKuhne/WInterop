﻿using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;

namespace Performance
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = DefaultConfig.Instance
                .AddJob(
                    Job.Default.WithToolchain(
                        CsProjCoreToolchain.From(
                            new NetCoreAppSettings(
                                targetFrameworkMoniker: "net6.0-windows",  // the key to make it work
                                runtimeFrameworkVersion: null,
                                name: "6.0")))
                                .AsDefault());

            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
        }
    }
}
