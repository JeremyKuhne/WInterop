// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Buffers;
using WInterop.DynamicLinkLibrary.DataTypes;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Handles.DataTypes;

namespace WInterop.DynamicLinkLibrary
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static class DllDesktopMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms684179.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern SafeModuleHandle LoadLibraryExW(
                string lpFileName,
                IntPtr hReservedNull,
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
                ModuleHandle hModule,
                [MarshalAs(UnmanagedType.LPStr)] string methodName);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683200.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            unsafe public static extern bool GetModuleHandleExW(
                GetModuleFlags dwFlags,
                IntPtr lpModuleName,
                out ModuleHandle moduleHandle);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/ms683197.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern uint GetModuleFileNameW(
                ModuleHandle hModule,
                SafeHandle lpFileName,
                uint nSize);
        }

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

        public static SafeModuleHandle GetRefCountedModuleHandle(string moduleName)
        {
            ModuleHandle handle = GetModuleHandleHelper(moduleName, 0);
            return new SafeModuleHandle(handle.HMODULE, ownsHandle: true);
        }

        private unsafe static ModuleHandle GetModuleHandleHelper(string moduleName, GetModuleFlags flags)
        {
            Func<IntPtr, GetModuleFlags, ModuleHandle> getHandle = (IntPtr n, GetModuleFlags f) =>
            {
                ModuleHandle handle;
                if (!Direct.GetModuleHandleExW(f, n, out handle))
                    throw ErrorHelper.GetIoExceptionForLastError();
                return handle;
            };

            if (moduleName == null)
                return getHandle(IntPtr.Zero, flags);

            fixed (void* name = moduleName)
            {
                return getHandle((IntPtr)name, flags);
            }
        }

        public static string GetModuleFileName(ModuleHandle moduleHandle)
        {
            return StringBufferCache.CachedBufferInvoke(100, (buffer) =>
            {
                realloc:
                uint result = Direct.GetModuleFileNameW(moduleHandle, buffer, buffer.CharCapacity);

                if (result == 0 || result >= buffer.CharCapacity)
                {
                    WindowsError error = ErrorHelper.GetLastError();
                    if (error != WindowsError.ERROR_INSUFFICIENT_BUFFER)
                        throw ErrorHelper.GetIoExceptionForError(error);

                    buffer.EnsureCharCapacity(buffer.CharCapacity * 2);
                    goto realloc;
                }

                buffer.Length = result;
                return buffer.ToString();
            });
        }

        /// <summary>
        /// Free the given library.
        /// </summary>
        public static void FreeLibrary(IntPtr handle)
        {
            if (!Direct.FreeLibrary(handle))
                throw ErrorHelper.GetIoExceptionForLastError();
        }

        /// <summary>
        /// Load the library at the given path.
        /// </summary>
        public static SafeModuleHandle LoadLibrary(string path, LoadLibraryFlags flags)
        {
            SafeModuleHandle handle = Direct.LoadLibraryExW(path, IntPtr.Zero, flags);
            if (handle.IsInvalid)
                throw ErrorHelper.GetIoExceptionForLastError(path);

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
        public static DelegateType GetFunctionDelegate<DelegateType>(ModuleHandle library, string methodName)
        {
            IntPtr method = Direct.GetProcAddress(library, methodName);
            if (method == IntPtr.Zero)
                throw ErrorHelper.GetIoExceptionForLastError(methodName);

            return Marshal.GetDelegateForFunctionPointer<DelegateType>(method);
        }
    }
}
