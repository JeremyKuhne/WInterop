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
using WInterop.Modules.Types;
using ModuleTypes = WInterop.Modules.Types;
using WInterop.FileManagement;
using WInterop.Resources;
using WInterop.Support;
using Xunit;
using WInterop.Gdi.Types;

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

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate int DoubleDelegateStandard(int value);

        [Fact]
        public void LoadFunctionStandard()
        {
            using (var handle = ModuleMethods.LoadLibrary(GetNativeTestLibraryLocation(), LoadLibraryFlags.LOAD_WITH_ALTERED_SEARCH_PATH))
            {
                handle.IsInvalid.Should().BeFalse();
                var doubler = ModuleMethods.GetFunctionDelegate<DoubleDelegateStandard>(handle, "DoubleStdCall");
                doubler(2).Should().Be(4);
            }
        }

        [DllImport(NativeTestLibrary, ExactSpelling = true)]
        public static extern int DoubleStdCall(int value);

        [Fact]
        public void StdCallViaDllImport()
        {
            DoubleStdCall(2).Should().Be(4);
        }

        [DllImport(NativeTestLibrary, EntryPoint = "IntPointerCheck")]
        public static extern IntPtr CheckIntAsArray(int[] values);

        [Fact]
        public unsafe void AsIntArrayInvoke()
        {
            int[] values = { 3, 4 };

            fixed (int* v = values)
            {
                IntPtr current = (IntPtr)v;
                IntPtr result = CheckIntAsArray(values);

                // Simple array declaration creates a copy in
                current.Should().Be(result);
            }
        }

        [DllImport(NativeTestLibrary, EntryPoint = "StructPointerCheck")]
        public static extern IntPtr CheckStructAsArray(POINT[] points);

        [Fact]
        public unsafe void AsArrayInvoke()
        {
            POINT[] points = { new POINT(1, 2), new POINT(3, 4) };

            fixed(void* p = &points[0])
            {
                IntPtr current = (IntPtr)p;
                IntPtr result = CheckStructAsArray(points);

                // Simple struct array declaration creates a copy in
                // (even though it is blittable)
                current.Should().NotBe(result);
                points[0].x.Should().Be(1);
            }
        }

        [DllImport(NativeTestLibrary, EntryPoint = "StructPointerCheck")]
        public static extern IntPtr CheckStructAsInOutArray([In, Out]POINT[] points);

        [Fact]
        public unsafe void AsInOutArrayInvoke()
        {
            POINT[] points = { new POINT(1, 2), new POINT(3, 4) };

            fixed (void* p = &points[0])
            {
                IntPtr current = (IntPtr)p;
                IntPtr result = CheckStructAsInOutArray(points);

                // Simple struct array declaration creates a copy both ways
                // (even though it is blittable)
                current.Should().NotBe(result);
                points[0].x.Should().Be(3);
            }
        }

        [DllImport(NativeTestLibrary, EntryPoint = "StructPointerCheck")]
        public unsafe static extern IntPtr CheckStructAsPointerArray(POINT* points);

        [Fact]
        public unsafe void AsPointerArrayInvoke()
        {
            POINT[] points = { new POINT(1, 2), new POINT(3, 4) };

            fixed (POINT* p = &points[0])
            {
                IntPtr current = (IntPtr)p;
                IntPtr result = CheckStructAsPointerArray(p);

                // No copy for simple struct array when we send a pointer, modification
                // is as expected
                current.Should().Be(result);
                points[0].x.Should().Be(3);
            }
        }

        [Fact]
        public void GetEntryModuleFileName()
        {
            string path = ModuleMethods.GetModuleFileName(ModuleTypes.SafeModuleHandle.NullModuleHandle);
            path.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void GetEntryModuleHandleAndFileName()
        {
            var module = ModuleMethods.GetModuleHandle(null);
            module.Should().NotBe(ModuleTypes.SafeModuleHandle.NullModuleHandle);
            string pathByHandle = ModuleMethods.GetModuleFileName(module);
            pathByHandle.Should().NotBeNullOrWhiteSpace();
            string pathByDefault = ModuleMethods.GetModuleFileName(ModuleTypes.SafeModuleHandle.NullModuleHandle);
            // Strangely the path is cased differently when getting the module file name through GetModuleFileNameEx, but not GetModuleFileName.
            pathByHandle.Should().BeEquivalentTo(pathByDefault);
        }

        [Fact]
        public void GetEntryModuleInfo()
        {
            var module = ModuleMethods.GetModuleHandle(null);
            module.Should().NotBe(ModuleTypes.SafeModuleHandle.NullModuleHandle);
            var info = ModuleMethods.GetModuleInfo(module);
            info.lpBaseOfDll.Should().Be(module.DangerousGetHandle());
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
