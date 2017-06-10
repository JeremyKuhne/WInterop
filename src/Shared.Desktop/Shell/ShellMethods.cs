﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Com.Types;
using WInterop.ErrorHandling.Types;
using WInterop.Handles.Types;
using WInterop.Shell.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Shell
{
    public static partial class ShellMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762188.aspx
            [DllImport(Libraries.Shell32, CharSet = CharSet.Unicode, SetLastError = false, ExactSpelling = true)]
            public static extern HRESULT SHGetKnownFolderPath(
                [MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
                KNOWN_FOLDER_FLAG dwFlags,
                SafeHandle hToken,
                out string ppszPath);

            [DllImport(Libraries.Shell32, SetLastError = false, ExactSpelling = true)]
            public static extern HRESULT SHGetKnownFolderIDList(
                [MarshalAs(UnmanagedType.LPStruct)] Guid rfid,
                KNOWN_FOLDER_FLAG dwFlags,
                SafeHandle hToken,
                out ItemIdList ppidl);

            [DllImport(Libraries.Shell32, CharSet = CharSet.Unicode, SetLastError = false, ExactSpelling = true)]
            public static extern HRESULT SHGetNameFromIDList(
                ItemIdList pidl,
                SIGDN sigdnName,
                out string ppszName);

            [DllImport(Libraries.Shlwapi, CharSet = CharSet.Unicode, SetLastError = false, ExactSpelling = true)]
            public static extern bool PathUnExpandEnvStringsW(
                string pszPath,
                SafeHandle pszBuf,
                uint cchBuf);


            // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762275.aspx
            [DllImport(Libraries.Userenv, CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]
            public static extern bool ExpandEnvironmentStringsForUserW(
                TokenHandle hToken,
                string lpSrc,
                SafeHandle lpDst,
                uint dwSize);
        }

        /// <summary>
        /// Get the path for the given known folder Guid.
        /// </summary>
        public static string GetKnownFolderPath(Guid folderIdentifier, KNOWN_FOLDER_FLAG flags = KNOWN_FOLDER_FLAG.KF_FLAG_DEFAULT)
        {
            HRESULT hr = Imports.SHGetKnownFolderPath(folderIdentifier, flags, EmptySafeHandle.Instance, out string path);
            if (hr != HRESULT.S_OK)
                throw Errors.GetIoExceptionForHResult(hr, folderIdentifier.ToString());

            return path;
        }

        /// <summary>
        /// Get the Shell item id for the given known folder Guid.
        /// </summary>
        public static ItemIdList GetKnownFolderId(Guid folderIdentifier, KNOWN_FOLDER_FLAG flags = KNOWN_FOLDER_FLAG.KF_FLAG_DEFAULT)
        {
            HRESULT hr = Imports.SHGetKnownFolderIDList(folderIdentifier, flags, EmptySafeHandle.Instance, out ItemIdList id);
            if (hr != HRESULT.S_OK)
                throw Errors.GetIoExceptionForHResult(hr, folderIdentifier.ToString());

            return id;
        }

        /// <summary>
        /// Get the name for a given Shell item ID.
        /// </summary>
        public static string GetNameFromId(ItemIdList id, SIGDN form = SIGDN.NORMALDISPLAY)
        {
            HRESULT hr = Imports.SHGetNameFromIDList(id, form, out string name);
            if (hr != HRESULT.S_OK)
                throw Errors.GetIoExceptionForHResult(hr);

            return name;
        }

        /// <summary>
        /// Get the IKnownFolderManager.
        /// </summary>
        public static IKnownFolderManager GetKnownFolderManager()
        {
            return (IKnownFolderManager)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid(ClassIds.CLSID_KnownFolderManager)));
        }

        /// <summary>
        /// Get the Guid identifiers for all known folders.
        /// </summary>
        public unsafe static IEnumerable<Guid> GetKnownFolderIds()
        {
            List<Guid> ids = new List<Guid>();

            IKnownFolderManager manager = GetKnownFolderManager();
            uint count = manager.GetFolderIds(out SafeComHandle buffer);

            using (buffer)
            {
                Guid* g = (Guid*)buffer.DangerousGetHandle();
                for (int i = 0; i < count; i++)
                    ids.Add(*g++);
            }

            return ids;
        }

        /// <summary>
        /// Collapses common path segments into the equivalent environment string.
        /// Returns null if unsuccessful.
        /// </summary>
        public static string UnexpandEnvironmentStrings(string path)
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                buffer.EnsureCharCapacity(Paths.MaxPath);
                if (!Imports.PathUnExpandEnvStringsW(path, buffer, buffer.CharCapacity))
                    return null;

                buffer.SetLengthToFirstNull();
                return buffer.ToString();
            });
        }

        /// <summary>
        /// Expands environment variables for the given user token. If the token is
        /// null, returns the system variables.
        /// </summary>
        public static string ExpandEnvironmentVariablesForUser(TokenHandle token, string value)
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                while (!Imports.ExpandEnvironmentStringsForUserW(token, value, buffer, buffer.CharCapacity))
                {
                    Errors.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    buffer.EnsureCharCapacity(buffer.CharCapacity * 2);
                }

                buffer.SetLengthToFirstNull();
                return buffer.ToString();
            });
        }
    }
}
