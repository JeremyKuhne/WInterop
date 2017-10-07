// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using WInterop.FileManagement;
using WInterop.FileManagement.Types;

namespace Tests.Support
{
    public class TestFileCleaner : IDisposable
    {
        protected const string WinteropFlagFileName = @"%WinteropFlagFile%";
        protected ConcurrentBag<string> _filesToClean = new ConcurrentBag<string>();
        private Stream _flagFile;
        private static string s_rootTempFolder;
        private static object s_CleanLock;

        static TestFileCleaner()
        {
            s_CleanLock = new object();
            s_rootTempFolder = Path.Combine(Path.GetTempPath(), "WinteropTests");
            CleanOrphanedTempFolders();
        }

        /// <param name="tempRootDirectoryName">The subdirectory to use for temp files "MyApp"</param>
        public TestFileCleaner()
        {
            TempFolder = Path.Combine(s_rootTempFolder, Path.GetRandomFileName());
            string flagFile = Path.Combine(TempFolder, WinteropFlagFileName);

            lock (s_CleanLock)
            {
                // Make sure we fully lock the directory before allowing cleaning
                FileHelper.CreateDirectoryRecursive(TempFolder);

                // Create a flag file and leave it open- this way we can track and clean abandoned (crashed/terminated) processes
                _flagFile = FileMethods.CreateFileStream(flagFile,
                    DesiredAccess.GenericReadWrite, 0, CreationDisposition.CreateNew);

                var writer = new StreamWriter(_flagFile);
                writer.WriteLine("Temporary Flag File");
                writer.Flush();
            }
        }

        /// <summary>
        /// The isolated root folder for this instance of TestFileCleaner.
        /// </summary>
        public string TempFolder { [DebuggerStepThrough] get; private set; }

        public void TrackFile(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                _filesToClean.Add(path);
            }
        }

        private static void CleanOrphanedTempFolders()
        {
            // Clean up orphaned temp folders
            DirectoryInfo rootInfo = new DirectoryInfo(s_rootTempFolder);

            if (rootInfo != null)
            {
                try
                {
                    var flagFiles =
                        from directory in rootInfo.EnumerateDirectories()
                        from file in directory.EnumerateFiles(WinteropFlagFileName)
                        select new { Directory = directory.FullName, File = file.FullName };

                    foreach (var flagFile in flagFiles.ToArray())
                    {
                        try
                        {
                            // If we can't delete the flag file (open handle) we'll throw and move on
                            FileMethods.DeleteFile(@"\\?\" + flagFile.File);

                            FileHelper.DeleteDirectoryRecursive(@"\\?\" + flagFile.Directory);
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                catch (Exception)
                {
                    // Ignoring orphan cleanup errors as the DotNet file service chokes on long paths
                }
            }
        }

        public string GetTestPath(string basePath = null)
        {
            return Path.Combine(basePath ?? TempFolder, Path.GetRandomFileName());
        }

        public string CreateTestFile(string content, string basePath = null)
        {
            string testFile = GetTestPath(basePath);
            using (var stream = FileMethods.CreateFileStream(testFile,
                DesiredAccess.GenericReadWrite, ShareModes.ReadWrite, CreationDisposition.CreateNew))
            {
                using (var writer = new StreamWriter(stream))
                {
                    writer.Write(content);
                }
            }

            return testFile;
        }

        public string CreateTestFile(byte[] content, string basePath = null)
        {
            string testFile = GetTestPath(basePath);
            using (var stream = FileMethods.CreateFileStream(testFile,
                DesiredAccess.GenericReadWrite, ShareModes.ReadWrite, CreationDisposition.CreateNew))
            {
                using (var writer = new BinaryWriter(stream))
                {
                    writer.Write(content);
                }
            }

            return testFile;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        ~TestFileCleaner()
        {
            Dispose(disposing: false);
        }

        protected virtual void Dispose(bool disposing)
        {
            CleanOwnFiles();
        }

        private void CleanOwnFiles()
        {
            lock (s_CleanLock)
            {
                _flagFile.Dispose();
                _flagFile = null;

                // Delete our own temp folder
                try
                {
                    FileHelper.DeleteDirectoryRecursive(@"\\?\" + TempFolder);
                }
                catch (Exception)
                {
                }

                // Clean any loose files we're tracking
                foreach (string file in _filesToClean.Distinct(StringComparer.OrdinalIgnoreCase))
                {
                    if (string.IsNullOrWhiteSpace(file)) { continue; }

                    try
                    {
                        FileMethods.DeleteFile(@"\\?\" + file);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
