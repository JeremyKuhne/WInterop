using System;
using System.Diagnostics;
using System.Linq;
using WInterop.FileManagement;
using WInterop.Support;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\X";
            Stopwatch watch = new Stopwatch();
            watch.Start();
            string[] files = null;
            for (int i = 0; i < 10; i++)
            {
                // files = System.IO.Directory.GetFileSystemEntries(path, "*.jpg", System.IO.SearchOption.AllDirectories);
                files = new DirectFindOperation(path, true, "*.jpg").Select(v => Paths.Combine(v.Directory, v.FileName)).ToArray();
            }
            watch.Stop();
            Console.WriteLine($"{files.Length} files, {watch.ElapsedMilliseconds} time");
        }
    }
}
