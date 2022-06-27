// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Runtime.InteropServices;
using Tests.Support;
using WInterop.Modules;
using WInterop.Storage;
using WInterop.Windows;
using ModuleTypes = WInterop.Modules;

namespace ModuleTests;

public class Basic
{
    internal const string NativeTestLibrary = "NativeTestLibrary.dll";

    private string GetNativeTestLibraryLocation()
    {
        string path = Path.Join(Path.GetDirectoryName(typeof(Basic).Assembly.Location), NativeTestLibrary);
        if (!Storage.FileExists(path))
        {
            throw new FileNotFoundException(path);
        }
        return path;
    }

    [Fact]
    public void LoadAsResource()
    {
        using var handle = Modules.LoadLibrary(
            GetNativeTestLibraryLocation(),
            LoadLibraryFlags.LoadLibraryAsImageResource | LoadLibraryFlags.LoadLibraryAsDatafile);

        handle.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void LoadAsResourceFromLongPath()
    {
        using var cleaner = new TestFileCleaner();

        string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);

        FileHelper.CreateDirectoryRecursive(longPath);
        string longPathLibrary = Path.Join(longPath, "LoadAsResourceFromLongPath.dll");
        Storage.CopyFile(GetNativeTestLibraryLocation(), longPathLibrary);

        using var handle = Modules.LoadLibrary(
            longPathLibrary,
            LoadLibraryFlags.LoadLibraryAsImageResource | LoadLibraryFlags.LoadLibraryAsDatafile);

        handle.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void LoadString()
    {
        using var handle = Modules.LoadLibrary(
            GetNativeTestLibraryLocation(),
            LoadLibraryFlags.LoadLibraryAsImageResource | LoadLibraryFlags.LoadLibraryAsDatafile);

        string resource = Windows.LoadString(handle, 101);
        resource.Should().Be("Test");
    }

    [Fact]
    public void LoadStringFromLongPath()
    {
        using var cleaner = new TestFileCleaner();

        string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
        FileHelper.CreateDirectoryRecursive(longPath);
        string longPathLibrary = Path.Join(longPath, "LoadStringFromLongPath.dll");
        Storage.CopyFile(GetNativeTestLibraryLocation(), longPathLibrary);

        using var handle = Modules.LoadLibrary(
            longPathLibrary,
            LoadLibraryFlags.LoadLibraryAsImageResource | LoadLibraryFlags.LoadLibraryAsDatafile);

        string resource = Windows.LoadString(handle, 101);
        resource.Should().Be("Test");
    }

    [Fact]
    public void LoadAsBinary()
    {
        using var handle = Modules.LoadLibrary(GetNativeTestLibraryLocation(), 0);

        handle.IsInvalid.Should().BeFalse();
    }

    [Fact]
    public void LoadAsBinaryFromLongPath()
    {
        using var cleaner = new TestFileCleaner();

        string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
        FileHelper.CreateDirectoryRecursive(longPath);
        string longPathLibrary = Path.Join(longPath, "LoadAsBinaryFromLongPath.dll");
        Storage.CopyFile(GetNativeTestLibraryLocation(), longPathLibrary);

        using var handle = Modules.LoadLibrary(longPathLibrary, LoadLibraryFlags.LoadWithAlteredSearchPath);
        handle.IsInvalid.Should().BeFalse();
    }

    [Fact(Skip = "For exploring richedit handles, not needed for normal run.")]
    public void LoadRichEdit()
    {
        using var firstHandle = Modules.LoadLibrary("riched32.dll", default);
        using var secondHandle = Modules.LoadLibrary("riched20.dll", default);
        using var thirdHandle = Modules.LoadLibrary("MsftEdit.dll", default);
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int DoubleDelegate(int value);

    [Fact]
    public void LoadFunction()
    {
        using var handle = Modules.LoadLibrary(
            GetNativeTestLibraryLocation(),
            LoadLibraryFlags.LoadWithAlteredSearchPath);

        handle.IsInvalid.Should().BeFalse();
        var doubler = Modules.GetFunctionDelegate<DoubleDelegate>(handle, "Double");
        doubler(2).Should().Be(4);
    }

    [Fact]
    public unsafe void LoadFunctionPointer()
    {
        using var handle = Modules.LoadLibrary(
            GetNativeTestLibraryLocation(),
            LoadLibraryFlags.LoadWithAlteredSearchPath);

        handle.IsInvalid.Should().BeFalse();
        var doubler = (delegate* unmanaged[Cdecl]<int, int>)Modules.GetFunctionAddress(handle, "Double");
        doubler(3).Should().Be(6);
    }

    [Fact]
    public void LoadFunctionFromLongPath()
    {
        using var cleaner = new TestFileCleaner();

        string longPath = @"\\?\" + PathGenerator.CreatePathOfLength(cleaner.TempFolder, 500);
        FileHelper.CreateDirectoryRecursive(longPath);
        string longPathLibrary = Path.Join(longPath, "LoadFunctionFromLongPath.dll");
        Storage.CopyFile(GetNativeTestLibraryLocation(), longPathLibrary);

        using var handle = Modules.LoadLibrary(longPathLibrary, 0);

        handle.IsInvalid.Should().BeFalse();
        var doubler = Modules.GetFunctionDelegate<DoubleDelegate>(handle, "Double");
        doubler(2).Should().Be(4);
    }

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int DoubleDelegateStandard(int value);

    [Fact]
    public void LoadFunctionStandard()
    {
        using var handle = Modules.LoadLibrary(
            GetNativeTestLibraryLocation(),
            LoadLibraryFlags.LoadWithAlteredSearchPath);

        handle.IsInvalid.Should().BeFalse();
        var doubler = Modules.GetFunctionDelegate<DoubleDelegateStandard>(handle, "DoubleStdCall");
        doubler(2).Should().Be(4);
    }

    [DllImport(NativeTestLibrary, ExactSpelling = true)]
    public static extern int DoubleStdCall(int value);

    [Fact]
    public void StdCallViaDllImport()
    {
        DoubleStdCall(2).Should().Be(4);
    }

    [DllImport(NativeTestLibrary, EntryPoint = "IntPointerCheck")]
    public static extern IntPtr CheckIntAsArray(int[] values, int count);

    [Fact]
    public unsafe void AsIntArrayInvoke()
    {
        int[] values = { 3, 4 };

        fixed (int* v = values)
        {
            IntPtr current = (IntPtr)v;
            IntPtr result = CheckIntAsArray(values, values.Length);
            values[0].Should().Be(6);

            // Simple array declaration pins, so we get the same value back
            current.Should().Be(result);
        }
    }

    [DllImport(NativeTestLibrary, EntryPoint = "IntPointerCheck")]
    public static extern IntPtr CheckIntAsRef(ref int values, int count);

    [Fact]
    public unsafe void AsRefIntInvoke()
    {
        int[] values = { 5, 6 };

        fixed (int* v = values)
        {
            IntPtr current = (IntPtr)v;

            ReadOnlySpan<int> span = new(values);
            IntPtr result = CheckIntAsRef(ref MemoryMarshal.GetReference(span), values.Length);

            // By ref doesn't copy, so we get the same result back
            current.Should().Be(result);
            values[0].Should().Be(10);

            // Same goes if we take ref[0]
            result = CheckIntAsRef(ref values[0], values.Length);
            current.Should().Be(result);
        }
    }

    [Fact]
    public unsafe void EmptyArrayBehavior()
    {
        int[] values = new int[0];

        fixed (int* v = values)
        {
            // Fixing an empty array gives null
            IntPtr current = (IntPtr)v;
            current.Should().Be(IntPtr.Zero);

            Action action = () => CheckIntAsRef(ref values[0], 0);
            action.Should().Throw<IndexOutOfRangeException>();

            // While you can't fix an empty array, you can GetReference for a wrapping span
            ReadOnlySpan<int> span = new(values);
            IntPtr result = CheckIntAsRef(ref MemoryMarshal.GetReference(span), 0);
            result.Should().NotBe(IntPtr.Zero);
        }
    }

    [DllImport(NativeTestLibrary, EntryPoint = "StructPointerCheck")]
    public static extern IntPtr CheckStructAsArray(Point[] points, int count);

    [Fact]
    public unsafe void AsArrayInvoke()
    {
        Point[] points = { new Point(1, 2), new Point(3, 4) };

        fixed (void* p = &points[0])
        {
            IntPtr current = (IntPtr)p;
            IntPtr result = CheckStructAsArray(points, points.Length);

            // Simple struct array declaration creates a copy in
            // (even though it is blittable)
            current.Should().NotBe(result);
            points[0].X.Should().Be(1);
        }
    }

    [DllImport(NativeTestLibrary, EntryPoint = "StructPointerCheck")]
    public static extern IntPtr CheckStructAsInOutArray([In, Out] Point[] points, int count);

    [Fact]
    public unsafe void AsInOutArrayInvoke()
    {
        Point[] points = { new Point(1, 2), new Point(3, 4) };

        fixed (void* p = &points[0])
        {
            IntPtr current = (IntPtr)p;
            IntPtr result = CheckStructAsInOutArray(points, points.Length);

            // Simple struct array declaration creates a copy both ways
            // (even though it is blittable)
            current.Should().NotBe(result);
            points[0].X.Should().Be(3);
        }
    }

    [DllImport(NativeTestLibrary, EntryPoint = "StructPointerCheck")]
    public static extern unsafe IntPtr CheckStructAsPointerArray(Point* points, int count);

    [Fact]
    public unsafe void AsPointerArrayInvoke()
    {
        Point[] points = { new Point(1, 2), new Point(3, 4) };

        fixed (Point* p = &points[0])
        {
            IntPtr current = (IntPtr)p;
            IntPtr result = CheckStructAsPointerArray(p, points.Length);

            // No copy for simple struct array when we send a pointer, modification
            // is as expected
            current.Should().Be(result);
            points[0].X.Should().Be(3);
        }
    }

    [DllImport(NativeTestLibrary, EntryPoint = "StructPointerCheck")]
    public static extern unsafe IntPtr CheckStructAsRef(ref Point points, int count);

    [Fact]
    public unsafe void AsSingleRefInvoke()
    {
        Point point = new(4, 6);
        Point* p = &point;

        IntPtr current = (IntPtr)p;
        IntPtr result = CheckStructAsRef(ref point, 1);

        // Should not have gotten a copy
        current.Should().Be(result);
    }

    [Fact]
    public void GetEntryModuleFileName()
    {
        string path = Modules.GetModuleFileName(ModuleTypes.ModuleInstance.Null);
        path.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void GetEntryModuleHandleAndFileName()
    {
        var module = Modules.GetModuleHandle(null);
        module.Should().NotBe(ModuleTypes.ModuleInstance.Null);
        string pathByHandle = Modules.GetModuleFileName(module);
        pathByHandle.Should().NotBeNullOrWhiteSpace();
        string pathByDefault = Modules.GetModuleFileName(ModuleTypes.ModuleInstance.Null);

        // Strangely the path is cased differently when getting the module file name through
        // GetModuleFileNameEx, but not GetModuleFileName.
        pathByHandle.Should().BeEquivalentTo(pathByDefault);
    }

    [Fact]
    public void GetEntryModuleInfo()
    {
        var module = Modules.GetModuleHandle(null);
        module.Should().NotBe(ModuleTypes.ModuleInstance.Null);
        var info = Modules.GetModuleInfo(module);
        info.BaseOfDll.Should().Be(module.DangerousGetHandle());
    }

    [Fact]
    public void GetProcessModules()
    {
        var modules = Modules.GetProcessModules();
        modules.Should().NotBeEmpty();
    }

    [Fact]
    public void GetProcessModulePaths()
    {
        var modules = Modules.GetProcessModules();
        var moduleNames = (from module in modules select Modules.GetModuleFileName(module)).ToArray();
        moduleNames.Should().NotBeEmpty();
        moduleNames.Length.Should().Be(modules.Count());
    }

    [DllImport(NativeTestLibrary, EntryPoint = "ReturnPoint")]
    public static extern unsafe Point ReturnPoint(int x, int y);

    [DllImport(NativeTestLibrary, EntryPoint = "ReturnPointFloat")]
    public static extern unsafe PointF ReturnPointFloat(float x, float y);

    [Fact]
    public void ReturnPointInvoke()
    {
        Point point = ReturnPoint(4, 2);
        point.X.Should().Be(4);
        point.Y.Should().Be(2);
    }

    [Fact]
    public void ReturnPointFloatInvoke()
    {
        PointF point = ReturnPointFloat(4.5f, 1.6f);
        point.X.Should().Be(4.5f);
        point.Y.Should().Be(1.6f);
    }
}
