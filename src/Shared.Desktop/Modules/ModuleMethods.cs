// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling;
using WInterop.Modules.DataTypes;
using WInterop.ProcessAndThreads;
using WInterop.ProcessAndThreads.DataTypes;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Modules
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    /// <remarks>
    /// This is an amalgamation of "Dynamic-Link Libraries" and "Process Status" APIs.
    /// </remarks>
    public static partial class ModuleMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static partial class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684179.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern SafeModuleHandle LoadLibraryExW(
                string lpFileName,
                IntPtr hFile,
                LoadLibraryFlags dwFlags);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683152.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool FreeLibrary(
                IntPtr hModule);

            // This API is only available in ANSI
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683212.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, BestFitMapping = false)]
            public static extern IntPtr GetProcAddress(
                SafeModuleHandle hModule,
                [MarshalAs(UnmanagedType.LPStr)] string methodName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683200.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            unsafe public static extern bool GetModuleHandleExW(
                GetModuleFlags dwFlags,
                IntPtr lpModuleName,
                out ModuleHandle moduleHandle);

            // The non-ex version is more performant for the current process.
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683197.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetModuleFileNameW(
                ModuleHandle hModule,
                SafeHandle lpFileName,
                uint nSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683198.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern uint K32GetModuleFileNameExW(
                SafeProcessHandle hProcess,
                SafeModuleHandle hModule,
                SafeHandle lpFileName,
                uint nSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683201.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool K32GetModuleInformation(
                SafeProcessHandle hProcess,
                SafeModuleHandle hModule,
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
            [return: MarshalAs(UnmanagedType.Bool)]
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
        public static ModuleHandle GetModuleHandle(IntPtr address)
        {
            if (!Direct.GetModuleHandleExW(
                GetModuleFlags.GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GetModuleFlags.GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
                address,
                out var handle))
                throw Errors.GetIoExceptionForLastError();

            return handle;
        }

        /// <summary>
        /// Gets the specified module handle without increasing the ref count.
        /// </summary>
        public static ModuleHandle GetModuleHandle(string moduleName)
        {
            return GetModuleHandleHelper(moduleName, GetModuleFlags.GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT);
        }

        /// <summary>
        /// Gets a module handle and pins the module so it can't be unloaded until the process exits.
        /// </summary>
        public static ModuleHandle GetModuleHandleAndPin(string moduleName)
        {
            return GetModuleHandleHelper(moduleName, GetModuleFlags.GET_MODULE_HANDLE_EX_FLAG_PIN);
        }

        /// <summary>
        /// Gets a ref counted module handle for the specified module.
        /// </summary>
        public static SafeModuleHandle GetRefCountedModuleHandle(string moduleName)
        {
            ModuleHandle handle = GetModuleHandleHelper(moduleName, 0);
            return new SafeModuleHandle(handle.HMODULE, ownsHandle: true);
        }

        private unsafe static ModuleHandle GetModuleHandleHelper(string moduleName, GetModuleFlags flags)
        {
            Func<IntPtr, GetModuleFlags, ModuleHandle> getHandle = (IntPtr n, GetModuleFlags f) =>
            {
                if (!Direct.GetModuleHandleExW(f, n, out var handle))
                    throw Errors.GetIoExceptionForLastError();
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
        public unsafe static MODULEINFO GetModuleInfo(SafeModuleHandle module, SafeProcessHandle process = null)
        {
            if (process == null) process = ProcessMethods.GetCurrentProcess();

            if (!Direct.K32GetModuleInformation(process, module, out var info, (uint)sizeof(MODULEINFO)))
                throw Errors.GetIoExceptionForLastError();

            return info;
        }

        /// <summary>
        /// Gets the file name (path) for the given module handle in the given process.
        /// </summary>
        /// <param name="process">The process for the given module or null for the current process.</param>
        /// <remarks>External process handles must be opened with PROCESS_QUERY_INFORMATION|PROCESS_VM_READ</remarks>
        public static string GetModuleFileName(SafeModuleHandle module, SafeProcessHandle process = null)
        {
            if (process == null)
                return BufferHelper.CachedTruncatingApiInvoke((buffer) => Direct.GetModuleFileNameW(module, buffer, buffer.CharCapacity));
            else
                return BufferHelper.CachedTruncatingApiInvoke((buffer) => Direct.K32GetModuleFileNameExW(process, module, buffer, buffer.CharCapacity));
        }

        /// <summary>
        /// Free the given library.
        /// </summary>
        public static void FreeLibrary(IntPtr handle)
        {
            if (!Direct.FreeLibrary(handle))
                throw Errors.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Load the library at the given path.
        /// </summary>
        public static SafeModuleHandle LoadLibrary(string path, LoadLibraryFlags flags)
        {
            SafeModuleHandle handle = Direct.LoadLibraryExW(path, IntPtr.Zero, flags);
            if (handle.IsInvalid)
                throw Errors.GetIoExceptionForLastError(path);

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
        public static DelegateType GetFunctionDelegate<DelegateType>(SafeModuleHandle library, string methodName)
        {
            IntPtr method = Direct.GetProcAddress(library, methodName);
            if (method == IntPtr.Zero)
                throw Errors.GetIoExceptionForLastError(methodName);

            return Marshal.GetDelegateForFunctionPointer<DelegateType>(method);
        }

        /// <summary>
        /// Gets the module handles for the given process.
        /// </summary>
        /// <remarks>External process handles must be opened with PROCESS_QUERY_INFORMATION|PROCESS_VM_READ</remarks>
        /// <param name="process">The process to get modules for or null for the current process.</param>
        public unsafe static IEnumerable<ModuleHandle> GetProcessModules(SafeProcessHandle process = null)
        {
            if (process == null) process = ProcessMethods.GetCurrentProcess();

            List<ModuleHandle> modules = new List<ModuleHandle>();
            BufferHelper.CachedInvoke<HeapBuffer>(buffer =>
            {
                uint sizeNeeded = (uint)buffer.ByteCapacity;

                do
                {
                    buffer.EnsureByteCapacity(sizeNeeded);
                    if (!Direct.K32EnumProcessModulesEx(process, buffer, (uint)buffer.ByteCapacity,
                        out sizeNeeded, ListModulesOptions.LIST_MODULES_DEFAULT))
                        throw Errors.GetIoExceptionForLastError();
                } while (sizeNeeded > buffer.ByteCapacity);

                CheckedReader reader = new CheckedReader(buffer);
                for (int i = 0; i < sizeNeeded; i += sizeof(ModuleHandle))
                {
                    modules.Add(reader.ReadStruct<ModuleHandle>());
                }
            });

            return modules;
        }
    }
}
