// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Runtime.InteropServices;
using WInterop.DynamicLinkLibrary;
using WInterop.DynamicLinkLibrary.Desktop;
using WInterop.FileManagement;
using WInterop.Strings;
using WInterop.Tests.Support;
using WInterop.Utility;
using Xunit;

namespace DesktopTests.DllTests
{
    public class Methods
    {
        internal const string NativeTestLibrary = "NativeTestLibrary.dll";

        private string GetNativeTestLibraryLocation()
        {
            string path = Paths.Combine(System.IO.Path.GetDirectoryName((new Uri(typeof(Methods).Assembly.CodeBase)).LocalPath), NativeTestLibrary);
            if (!FileMethods.FileExists(path))
            {
                throw new System.IO.FileNotFoundException(path);
            }
            return path;
        }

        [Fact]
        public void LoadAsResource()
        {
            using (var handle = DllDesktopMethods.LoadLibrary(GetNativeTestLibraryLocation(),
                LoadLibraryFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE | LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE))
            {
                handle.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void LoadAsResourceFromLongPath()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);

                FileHelper.CreateDirectoryRecursive(longPath);
                string longPathLibrary = Paths.Combine(longPath, "LoadAsResourceFromLongPath.dll");
                FileMethods.CopyFile(GetNativeTestLibraryLocation(), longPathLibrary);

                using (var handle = DllDesktopMethods.LoadLibrary(longPathLibrary,
                    LoadLibraryFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE | LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE))
                {
                    handle.IsInvalid.Should().BeFalse();
                }
            }
        }

        [Fact]
        public void LoadString()
        {
            using (var handle = DllDesktopMethods.LoadLibrary(GetNativeTestLibraryLocation(),
                LoadLibraryFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE | LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE))
            {
                string resource = StringDesktopMethods.LoadString(handle, 101);
                resource.Should().Be("Test");
            }
        }

        [Fact]
        public void LoadStringFromLongPath()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                FileHelper.CreateDirectoryRecursive(longPath);
                string longPathLibrary = Paths.Combine(longPath, "LoadStringFromLongPath.dll");
                FileMethods.CopyFile(GetNativeTestLibraryLocation(), longPathLibrary);

                using (var handle = DllDesktopMethods.LoadLibrary(longPathLibrary,
                    LoadLibraryFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE | LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE))
                {
                    string resource = StringDesktopMethods.LoadString(handle, 101);
                    resource.Should().Be("Test");
                }
            }
        }

        [Fact]
        public void LoadAsBinary()
        {
            using (var handle = DllDesktopMethods.LoadLibrary(GetNativeTestLibraryLocation(), 0))
            {
                handle.IsInvalid.Should().BeFalse();
            }
        }

        [Fact]
        public void LoadAsBinaryFromLongPath()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                FileHelper.CreateDirectoryRecursive(longPath);
                string longPathLibrary = Paths.Combine(longPath, "LoadAsBinaryFromLongPath.dll");
                FileMethods.CopyFile(GetNativeTestLibraryLocation(), longPathLibrary);

                using (var handle = DllDesktopMethods.LoadLibrary(longPathLibrary, LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH))
                {
                    handle.IsInvalid.Should().BeFalse();
                }
            }
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int DoubleDelegate(int value);

        [Fact]
        public void LoadFunction()
        {
            using (var handle = DllDesktopMethods.LoadLibrary(GetNativeTestLibraryLocation(), LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH))
            {
                var doubler = DllDesktopMethods.GetFunctionDelegate<DoubleDelegate>(handle, "Double");
                doubler(2).Should().Be(4);
            }
        }

        [Fact]
        public void LoadFunctionFromLongPath()
        {
            using (var cleaner = new TestFileCleaner())
            {
                string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
                FileHelper.CreateDirectoryRecursive(longPath);
                string longPathLibrary = Paths.Combine(longPath, "LoadFunctionFromLongPath.dll");
                FileMethods.CopyFile(GetNativeTestLibraryLocation(), longPathLibrary);

                using (var handle = DllDesktopMethods.LoadLibrary(longPathLibrary, 0))
                {
                    var doubler = DllDesktopMethods.GetFunctionDelegate<DoubleDelegate>(handle, "Double");
                    doubler(2).Should().Be(4);
                }
            }
        }
    }
}
