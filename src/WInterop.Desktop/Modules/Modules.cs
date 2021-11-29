// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Modules.Native;
using WInterop.ProcessAndThreads;
using WInterop.Support.Buffers;

namespace WInterop.Modules;

/// <remarks>
///  This is an amalgamation of "Dynamic-Link Libraries" and "Process Status" APIs.
/// </remarks>
public static partial class Modules
{
    /// <summary>
    ///  Gets the module handle for the specified memory address without increasing the refcount.
    /// </summary>
    public static ModuleInstance GetModuleHandle(IntPtr address)
    {
        Error.ThrowLastErrorIfFalse(
            Imports.GetModuleHandleExW(
                GetModuleFlags.FromAddress | GetModuleFlags.UnchangedRefCount,
                address,
                out var handle));

        return new ModuleInstance(handle);
    }

    /// <summary>
    ///  Gets the module handle for the launching executable.
    /// </summary>
    public static ModuleInstance GetExeModuleHandle()
        => new(GetModuleHandleHelper(moduleName: null, GetModuleFlags.UnchangedRefCount));

    /// <summary>
    ///  Gets the specified module handle without increasing the ref count.
    /// </summary>
    public static ModuleInstance GetModuleHandle(string moduleName)
        => new(GetModuleHandleHelper(moduleName, GetModuleFlags.UnchangedRefCount));

    /// <summary>
    ///  Gets a module handle and pins the module so it can't be unloaded until the process exits.
    /// </summary>
    public static ModuleInstance GetModuleHandleAndPin(string moduleName)
        => new(GetModuleHandleHelper(moduleName, GetModuleFlags.Pin));

    /// <summary>
    ///  Gets a ref counted module handle for the specified module.
    /// </summary>
    public static ModuleInstance GetRefCountedModuleHandle(string moduleName)
    {
        // Module's reference count is incremented by 1
        return new RefCountedModuleInstance(GetModuleHandleHelper(moduleName, 0));
    }

    private static unsafe IntPtr GetModuleHandleHelper(string? moduleName, GetModuleFlags flags)
    {
        IntPtr GetHandle(IntPtr n, GetModuleFlags f)
        {
            Error.ThrowLastErrorIfFalse(
                Imports.GetModuleHandleExW(f, n, out var handle),
                moduleName);
            return handle;
        }

        if (moduleName is null)
            return GetHandle(IntPtr.Zero, flags);

        fixed (void* name = moduleName)
        {
            return GetHandle((IntPtr)name, flags);
        }
    }

    /// <summary>
    ///  Gets info for the given module in the given process.
    /// </summary>
    /// <param name="process">The process for the given module or null for the current process.</param>
    /// <remarks>External process handles must be opened with PROCESS_QUERY_INFORMATION|PROCESS_VM_READ</remarks>
    public static unsafe ModuleInfo GetModuleInfo(ModuleInstance module, SafeProcessHandle? process = null)
    {
        Error.ThrowLastErrorIfFalse(
            Imports.K32GetModuleInformation(process ?? Processes.GetCurrentProcess(), module, out var info, (uint)sizeof(ModuleInfo)));

        return info;
    }

    /// <summary>
    ///  Gets the file name (path) for the given module handle in the given process.
    /// </summary>
    /// <param name="process">The process for the given module or null for the current process.</param>
    /// <remarks>External process handles must be opened with PROCESS_QUERY_INFORMATION|PROCESS_VM_READ</remarks>
    public static unsafe string GetModuleFileName(ModuleInstance module, SafeProcessHandle? process = null)
    {
        return process is null
            ? PlatformInvoke.GrowableBufferInvoke(
                (ref ValueBuffer<char> buffer) =>
                {
                    fixed (char* b = buffer)
                    {
                        return Imports.GetModuleFileNameW(module, b, buffer.Length);
                    }
                },
                ReturnSizeSemantics.BufferTruncates
                    | ReturnSizeSemantics.LastErrorInsufficientBuffer
                    | ReturnSizeSemantics.SizeIncludesNullWhenTruncated)
            : PlatformInvoke.GrowableBufferInvoke(
                (ref ValueBuffer<char> buffer) =>
                {
                    fixed (char* b = buffer)
                    {
                        return Imports.K32GetModuleFileNameExW(process, module, b, buffer.Length);
                    }
                },
                ReturnSizeSemantics.BufferTruncates);
    }

    /// <summary>
    ///  Free the given library.
    /// </summary>
    public static void FreeLibrary(IntPtr handle)
        => Error.ThrowLastErrorIfFalse(Imports.FreeLibrary(handle));

    /// <summary>
    ///  Load the library at the given path.
    /// </summary>
    public static ModuleInstance LoadLibrary(string path, LoadLibraryFlags flags)
    {
        ModuleInstance handle = Imports.LoadLibraryExW(path, IntPtr.Zero, flags);
        if (handle.IsInvalid)
            Error.GetLastError().Throw(path);

        return handle;
    }

    /// <summary>
    ///  Get a delegate for the given native method.
    /// </summary>
    /// <remarks>
    ///  Here is a sample delegate definition:
    ///
    ///     [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    ///     private delegate int DoubleDelegate(int value);
    ///
    ///  And it's native signature:
    ///
    ///     extern "C" __declspec (dllexport) int Double(int);
    /// </remarks>
    public static TDelegate GetFunctionDelegate<TDelegate>(ModuleInstance library, string methodName)
    {
        IntPtr method = Imports.GetProcAddress(library, methodName);
        if (method == IntPtr.Zero)
            Error.GetLastError().Throw(methodName);

        return Marshal.GetDelegateForFunctionPointer<TDelegate>(method);
    }

    /// <summary>
    ///  Get a function pointer for the given native method.
    /// </summary>
    public static IntPtr GetFunctionAddress(ModuleInstance library, string methodName)
    {
        IntPtr method = Imports.GetProcAddress(library, methodName);
        if (method == IntPtr.Zero)
            Error.GetLastError().Throw(methodName);

        return method;
    }

    /// <summary>
    ///  Gets the module handles for the given process.
    /// </summary>
    /// <remarks>External process handles must be opened with PROCESS_QUERY_INFORMATION|PROCESS_VM_READ</remarks>
    /// <param name="process">The process to get modules for or null for the current process.</param>
    public static unsafe IEnumerable<ModuleInstance> GetProcessModules(SafeProcessHandle? process = null)
    {
        if (process == null) process = Processes.GetCurrentProcess();

        return BufferHelper.BufferInvoke<HeapBuffer, ModuleInstance[]>(buffer =>
        {
            uint sizeNeeded = (uint)buffer.ByteCapacity;

            do
            {
                buffer.EnsureByteCapacity(sizeNeeded);
                Error.ThrowLastErrorIfFalse(
                    Imports.K32EnumProcessModulesEx(
                        process,
                        buffer,
                        (uint)buffer.ByteCapacity,
                        out sizeNeeded,
                        ListModulesOptions.Default));
            } while (sizeNeeded > buffer.ByteCapacity);

            IntPtr* b = (IntPtr*)buffer.VoidPointer;
            ModuleInstance[] modules = new ModuleInstance[sizeNeeded / sizeof(IntPtr)];
            for (int i = 0; i < modules.Length; i++)
            {
                modules[i] = new ModuleInstance(*b++);
            }

            return modules;
        });
    }
}