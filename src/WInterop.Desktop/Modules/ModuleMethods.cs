// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Modules.BufferWrappers;
using WInterop.ProcessAndThreads;
using WInterop.Support.Buffers;

namespace WInterop.Modules
{
    /// <remarks>
    /// This is an amalgamation of "Dynamic-Link Libraries" and "Process Status" APIs.
    /// </remarks>
    public static partial class ModuleMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684179.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern RefCountedModuleInstance LoadLibraryExW(
                string lpFileName,
                IntPtr hFile,
                LoadLibraryFlags dwFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683152.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool FreeLibrary(
                IntPtr hModule);

            // This API is only available in ANSI
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683212.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, BestFitMapping = false)]
            public static extern IntPtr GetProcAddress(
                ModuleInstance hModule,
                [MarshalAs(UnmanagedType.LPStr)] string methodName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683200.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern bool GetModuleHandleExW(
                GetModuleFlags dwFlags,
                IntPtr lpModuleName,
                out IntPtr moduleHandle);

            // The non-ex version is more performant for the current process.
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683197.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetModuleFileNameW(
                ModuleInstance hModule,
                SafeHandle lpFileName,
                uint nSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683198.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern uint K32GetModuleFileNameExW(
                SafeProcessHandle hProcess,
                ModuleInstance hModule,
                SafeHandle lpFileName,
                uint nSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683201.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool K32GetModuleInformation(
                SafeProcessHandle hProcess,
                ModuleInstance hModule,
                out MODULEINFO lpmodinfo,
                uint cb);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683195.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern uint K32GetMappedFileNameW(
                SafeProcessHandle hProcess,
                IntPtr lpv,
                SafeHandle lpFilename,
                uint nSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms682633.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool K32EnumProcessModulesEx(
                SafeProcessHandle hProcess,
                SafeHandle lphModule,
                uint cb,
                out uint lpcbNeeded,
                ListModulesOptions dwFilterFlag);
        }

        /// <summary>
        /// Gets the module handle for the specified memory address without increasing the refcount.
        /// </summary>
        public static ModuleInstance GetModuleHandle(IntPtr address)
        {
            if (!Imports.GetModuleHandleExW(
                GetModuleFlags.GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GetModuleFlags.GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
                address,
                out var handle))
                throw Error.GetIoExceptionForLastError();

            return new ModuleInstance(handle);
        }

        /// <summary>
        /// Gets the specified module handle without increasing the ref count.
        /// </summary>
        public static ModuleInstance GetModuleHandle(string moduleName)
        {
            return new ModuleInstance(GetModuleHandleHelper(moduleName, GetModuleFlags.GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT));
        }

        /// <summary>
        /// Gets a module handle and pins the module so it can't be unloaded until the process exits.
        /// </summary>
        public static ModuleInstance GetModuleHandleAndPin(string moduleName)
        {
            return new ModuleInstance(GetModuleHandleHelper(moduleName, GetModuleFlags.GET_MODULE_HANDLE_EX_FLAG_PIN));
        }

        /// <summary>
        /// Gets a ref counted module handle for the specified module.
        /// </summary>
        public static ModuleInstance GetRefCountedModuleHandle(string moduleName)
        {
            // Module's reference count is incremented by 1
            return new RefCountedModuleInstance(GetModuleHandleHelper(moduleName, 0));
        }

        private unsafe static IntPtr GetModuleHandleHelper(string moduleName, GetModuleFlags flags)
        {
            Func<IntPtr, GetModuleFlags, IntPtr> getHandle = (IntPtr n, GetModuleFlags f) =>
            {
                if (!Imports.GetModuleHandleExW(f, n, out var handle))
                    throw Error.GetIoExceptionForLastError();
                return handle;
            };

            if (moduleName == null)
                return getHandle(IntPtr.Zero, flags);

            fixed (void* name = moduleName)
            {
                return getHandle((IntPtr)name, flags);
            }
        }

        /// <summary>
        /// Gets info for the given module in the given process.
        /// </summary>
        /// <param name="process">The process for the given module or null for the current process.</param>
        /// <remarks>External process handles must be opened with PROCESS_QUERY_INFORMATION|PROCESS_VM_READ</remarks>
        public unsafe static MODULEINFO GetModuleInfo(ModuleInstance module, SafeProcessHandle process = null)
        {
            if (process == null) process = Processes.GetCurrentProcess();

            if (!Imports.K32GetModuleInformation(process, module, out var info, (uint)sizeof(MODULEINFO)))
                throw Error.GetIoExceptionForLastError();

            return info;
        }

        /// <summary>
        /// Gets the file name (path) for the given module handle in the given process.
        /// </summary>
        /// <param name="process">The process for the given module or null for the current process.</param>
        /// <remarks>External process handles must be opened with PROCESS_QUERY_INFORMATION|PROCESS_VM_READ</remarks>
        public static string GetModuleFileName(ModuleInstance module, SafeProcessHandle process = null)
        {
            var wrapper = new ModuleFileNameWrapper { Module = module, Process = process };

            return BufferHelper.TruncatingApiInvoke(ref wrapper);
        }

        /// <summary>
        /// Free the given library.
        /// </summary>
        public static void FreeLibrary(IntPtr handle)
        {
            if (!Imports.FreeLibrary(handle))
                throw Error.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Load the library at the given path.
        /// </summary>
        public static ModuleInstance LoadLibrary(string path, LoadLibraryFlags flags)
        {
            ModuleInstance handle = Imports.LoadLibraryExW(path, IntPtr.Zero, flags);
            if (handle.IsInvalid)
                throw Error.GetIoExceptionForLastError(path);

            return handle;
        }

        /// <summary>
        /// Get a delegate for the given native method
        /// </summary>
        /// <remarks>
        /// Here is a sample delegate definition:
        ///
        ///     [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        ///     private delegate int DoubleDelegate(int value);
        ///
        /// And it's native signature:
        /// 
        ///     extern "C" __declspec (dllexport) int Double(int);
        /// </remarks>
        public static DelegateType GetFunctionDelegate<DelegateType>(ModuleInstance library, string methodName)
        {
            IntPtr method = Imports.GetProcAddress(library, methodName);
            if (method == IntPtr.Zero)
                throw Error.GetIoExceptionForLastError(methodName);

            return Marshal.GetDelegateForFunctionPointer<DelegateType>(method);
        }

        /// <summary>
        /// Gets the module handles for the given process.
        /// </summary>
        /// <remarks>External process handles must be opened with PROCESS_QUERY_INFORMATION|PROCESS_VM_READ</remarks>
        /// <param name="process">The process to get modules for or null for the current process.</param>
        public unsafe static IEnumerable<ModuleInstance> GetProcessModules(SafeProcessHandle process = null)
        {
            if (process == null) process = Processes.GetCurrentProcess();

            return BufferHelper.BufferInvoke<HeapBuffer, ModuleInstance[]>(buffer =>
            {
                uint sizeNeeded = (uint)buffer.ByteCapacity;

                do
                {
                    buffer.EnsureByteCapacity(sizeNeeded);
                    if (!Imports.K32EnumProcessModulesEx(process, buffer, (uint)buffer.ByteCapacity,
                        out sizeNeeded, ListModulesOptions.LIST_MODULES_DEFAULT))
                        throw Error.GetIoExceptionForLastError();
                } while (sizeNeeded > buffer.ByteCapacity);

                IntPtr* b = (IntPtr*)buffer.VoidPointer;
                ModuleInstance[] modules = new ModuleInstance[sizeNeeded / sizeof(IntPtr)];
                for (int i = 0; i < modules.Length; i++)
                {
                    modules[i] = (new ModuleInstance(*b++));
                }

                return modules;
            });
        }
    }
}
