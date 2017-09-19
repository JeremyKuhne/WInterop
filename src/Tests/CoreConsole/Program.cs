#define INTERACTIVE

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;
using WInterop.Support;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\NetFxDev1";
            string filter = @"*";
            const int iterations = 50;
            Stopwatch watch = new Stopwatch();
            string[] files = null;
            List<long> times = new List<long>(iterations);

            for (int i = 0; i < iterations; i++)
            {
#if INTERACTIVE
                GC.Collect();
                GC.Collect();
                GC.WaitForPendingFinalizers();
#endif

                watch.Restart();

                // files = System.IO.Directory.GetFileSystemEntries(path, filter, System.IO.SearchOption.AllDirectories);
                // files = new DirectFindOperation<FindResult>(path, true, filter).Select(v => string.Concat(v.Directory, "\\", v.FileName)).ToArray();
                files = new FindOperation<string>(path, true, filter).ToArray();

                watch.Stop();
                Console.WriteLine($"{files.Length} files, {watch.ElapsedMilliseconds} time");
                if (i > 0) times.Add(watch.ElapsedMilliseconds);
            }
            Console.WriteLine(iterations < 2 ? "Done" : $"Done- {(long)times.Average()} average");
#if INTERACTIVE
            Console.ReadLine();
#endif
        }
    }
}
