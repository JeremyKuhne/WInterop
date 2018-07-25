﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using WInterop.Desktop.Communications.Types;
using WInterop.Registry;
using WInterop.Registry.Types;
using WInterop.Storage;
using WInterop.Storage.Types;
using WInterop.Support;
using WInterop.Synchronization.Types;
using WInterop.Windows;

namespace WInterop.Desktop.Communications
{
    public static partial class CommunicationsMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363260.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetCommState(
                SafeFileHandle hFile,
                ref DCB lpDCB);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363436.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool SetCommState(
                SafeFileHandle hFile,
                ref DCB lpDCB);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363143.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool BuildCommDCBW(
                string lpDef,
                out DCB lpDCB);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363259.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetCommProperties(
                SafeFileHandle hFile,
                out COMMPROP lpCommProp);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363262.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetDefaultCommConfigW(
                string lpszName,
                ref COMMCONFIG lpCC,
                ref uint lpdwSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363256.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool GetCommConfig(
                SafeFileHandle hCommDev,
                ref COMMCONFIG lpCC,
                ref uint lpdwSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363187.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool CommConfigDialogW(
                string lpszName,
                WindowHandle hWnd,
                ref COMMCONFIG lpCC);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363473.aspx
            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public static extern bool TransmitCommChar(
                SafeFileHandle hFile,
                sbyte cChar);

            [DllImport(Libraries.Kernel32, SetLastError = true, ExactSpelling = true)]
            public unsafe static extern bool WaitCommEvent(
                SafeFileHandle hFile,
                out EventMask lpEvtMask,
                OVERLAPPED* lpOverlapped);
        }

        public unsafe static DCB GetCommState(SafeFileHandle fileHandle)
        {
            DCB dcb = new DCB()
            {
                DCBlength = (uint)sizeof(DCB)
            };

            if (!Imports.GetCommState(fileHandle, ref dcb))
                throw Errors.GetIoExceptionForLastError();

            return dcb;
        }

        public unsafe static void SetCommState(SafeFileHandle fileHandle, ref DCB dcb)
        {
            dcb.DCBlength = (uint)sizeof(DCB);

            if (!Imports.GetCommState(fileHandle, ref dcb))
                throw Errors.GetIoExceptionForLastError();
        }

        public unsafe static DCB BuildCommDCB(string definition)
        {
            if (!Imports.BuildCommDCBW(definition, out DCB dcb))
                throw Errors.GetIoExceptionForLastError();

            return dcb;
        }

        public static COMMPROP GetCommProperties(SafeFileHandle fileHandle)
        {
            if (!Imports.GetCommProperties(fileHandle, out COMMPROP properties))
                throw Errors.GetIoExceptionForLastError();

            return properties;
        }

        public unsafe static COMMCONFIG GetCommConfig(SafeFileHandle fileHandle)
        {
            COMMCONFIG config = new COMMCONFIG();
            uint size = (uint)sizeof(COMMCONFIG);

            if (!Imports.GetCommConfig(fileHandle, ref config, ref size))
                throw Errors.GetIoExceptionForLastError();

            return config;
        }

        /// <summary>
        /// Get the default config values for the given com port.
        /// </summary>
        /// <param name="port">Simple name only (COM1, not \\.\COM1)</param>
        public unsafe static COMMCONFIG GetDefaultCommConfig(string port)
        {
            COMMCONFIG config = new COMMCONFIG();
            uint size = (uint)sizeof(COMMCONFIG);

            if (!Imports.GetDefaultCommConfigW(port, ref config, ref size))
                throw Errors.GetIoExceptionForLastError();

            return config;
        }

        /// <summary>
        /// Pops the COM port configuration dialog and returns the selected settings.
        /// </summary>
        /// <exception cref="OperationCanceledException">Thrown if the dialog is cancelled.</exception>
        public unsafe static COMMCONFIG CommConfigDialog(string port, WindowHandle parent)
        {
            COMMCONFIG config = GetDefaultCommConfig(port);

            if (!Imports.CommConfigDialogW(port, parent, ref config))
                throw Errors.GetIoExceptionForLastError();

            return config;
        }

        /// <summary>
        /// Simple helper for CreateFile call that sets the expected values when opening a COM port.
        /// </summary>
        public static SafeFileHandle CreateComFileHandle(
            string path,
            FileAttributes fileAttributes = FileAttributes.None,
            FileFlags fileFlags = FileFlags.None)
        {
            return StorageMethods.CreateFile(
                path,
                CreationDisposition.OpenExisting,
                DesiredAccess.GenericReadWrite,
                0,
                fileAttributes,
                fileFlags);
        }

        public static IEnumerable<string> GetAvailableComPorts()
        {
            using (var key = RegistryMethods.OpenKey(
                RegistryKeyHandle.HKEY_LOCAL_MACHINE, @"HARDWARE\DEVICEMAP\SERIALCOMM"))
            {
                return RegistryMethods.GetValueDataDirect(key, RegistryValueType.REG_SZ).OfType<string>().ToArray();
            }
        }
    }
}
