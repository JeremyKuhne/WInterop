// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.


using System;
using System.IO;
using System.Threading;

namespace Tests.Support
{
    /// <summary>
    /// This will lock until disposed to assist in tests that change the current directory.
    /// Current directory is process wide- try to avoid changing the directory at all, or use this wrapper.
    /// </summary>
    public class TempCurrentDirectory : IDisposable
    {
        private string _priorDirectory;
        private static object _tempDirectoryLock = new object();

        public TempCurrentDirectory(string directory = null)
        {
            Monitor.Enter(_tempDirectoryLock);
            _priorDirectory = Environment.CurrentDirectory;

            if (directory != null)
                Environment.CurrentDirectory = directory;
        }

        public void Dispose()
        {
            if (Environment.CurrentDirectory != _priorDirectory)
            {
                if (Directory.Exists(_priorDirectory))
                    Environment.CurrentDirectory = _priorDirectory;
            }

            Monitor.Exit(_tempDirectoryLock);
        }
    }
}
