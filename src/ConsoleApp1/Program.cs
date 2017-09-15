using System;
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
            string filter = @"*.txt";
            Stopwatch watch = new Stopwatch();
            watch.Start();
            string[] files = null;
            for (int i = 0; i < 10; i++)
            {
                GC.Collect();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                watch.Restart();

                files = System.IO.Directory.GetFileSystemEntries(path, filter, System.IO.SearchOption.AllDirectories);
                // files = new DirectFindOperation<FindResult>(path, true, filter).Select(v => Paths.Combine(v.Directory, v.FileName)).ToArray();
                //files = new DirectFindOperation<string>(path, true, filter).ToArray();

                watch.Stop();
                Console.WriteLine($"{files.Length} files, {watch.ElapsedMilliseconds} time");
                Console.ReadLine();
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
