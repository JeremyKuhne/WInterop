// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using Tests.Support;
using WInterop.Modules;
using WInterop.Modules.DataTypes;
using ModuleTypes = WInterop.Modules.DataTypes;
using WInterop.FileManagement;
using WInterop.Resources;
using WInterop.Support;
using Xunit;

namespace DesktopTests.ModuleTests
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
            using (var handle = ModuleMethods.LoadLibrary(GetNativeTestLibraryLocation(),
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

                using (var handle = ModuleMethods.LoadLibrary(longPathLibrary,
                    LoadLibraryFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE | LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE))
                {
                    handle.IsInvalid.Should().BeFalse();
                }
            }
        }

        [Fact]
        public void LoadString()
        {
            using (var handle = ModuleMethods.LoadLibrary(GetNativeTestLibraryLocation(),
                LoadLibraryFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE | LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE))
            {
                string resource = ResourceMethods.LoadString(handle, 101);
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

                using (var handle = ModuleMethods.LoadLibrary(longPathLibrary,
                    LoadLibraryFlags.LOAD_LIBRARY_AS_IMAGE_RESOURCE | LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE))
                {
                    string resource = ResourceMethods.LoadString(handle, 101);
                    resource.Should().Be("Test");
                }
            }
        }

        [Fact]
        public void LoadAsBinary()
        {
            using (var handle = ModuleMethods.LoadLibrary(GetNativeTestLibraryLocation(), 0))
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

                using (var handle = ModuleMethods.LoadLibrary(longPathLibrary, LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH))
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
            using (var handle = ModuleMethods.LoadLibrary(GetNativeTestLibraryLocation(), LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH))
            {
                handle.IsInvalid.Should().BeFalse();
                var doubler = ModuleMethods.GetFunctionDelegate<DoubleDelegate>(handle, "Double");
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

                using (var handle = ModuleMethods.LoadLibrary(longPathLibrary, 0))
                {
                    handle.IsInvalid.Should().BeFalse();
                    var doubler = ModuleMethods.GetFunctionDelegate<DoubleDelegate>(handle, "Double");
                    doubler(2).Should().Be(4);
                }
            }
        }

        [Fact]
        public void GetEntryModuleFileName()
        {
            string path = ModuleMethods.GetModuleFileName(ModuleTypes.ModuleHandle.NullModuleHandle);
            path.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetEntryModuleHandleAndFileName()
        {
            var module = ModuleMethods.GetModuleHandle(null);
            module.Should().NotBe(ModuleTypes.ModuleHandle.NullModuleHandle);
            string pathByHandle = ModuleMethods.GetModuleFileName(module);
            pathByHandle.Should().NotBeNullOrWhiteSpace();
            string pathByDefault = ModuleMethods.GetModuleFileName(ModuleTypes.ModuleHandle.NullModuleHandle);
            // Strangely the path is cased differently when getting the module file name through GetModuleFileNameEx, but not GetModuleFileName.
            pathByHandle.Should().BeEquivalentTo(pathByDefault);
        }

        [Fact]
        public void GetEntryModuleInfo()
        {
            var module = ModuleMethods.GetModuleHandle(null);
            module.Should().NotBe(ModuleTypes.ModuleHandle.NullModuleHandle);
            var info = ModuleMethods.GetModuleInfo(module);
            info.lpBaseOfDll.Should().Be(module.HMODULE);
        }

        [Fact]
        public void GetProcessModules()
        {
            var modules = ModuleMethods.GetProcessModules();
            modules.Should().NotBeEmpty();
        }

        [Fact]
        public void GetProcessModulePaths()
        {
            var modules = ModuleMethods.GetProcessModules();
            var moduleNames = (from module in modules select ModuleMethods.GetModuleFileName(module)).ToArray();
            moduleNames.Should().NotBeEmpty();
            moduleNames.Length.Should().Be(modules.Count());
        }
    }
}
