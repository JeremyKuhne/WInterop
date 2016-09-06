// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Com.DataTypes;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Handles.DataTypes;
using WInterop.Shell.DataTypes;
using WInterop.Support.Buffers;

namespace WInterop.Shell
{
    /// <summary>
    /// These methods are only available from Windows desktop apps. Windows store apps cannot access them.
    /// </summary>
    public static class ShellDesktopMethods
    {
        /// <summary>
        /// Direct P/Invokes aren't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        /// <remarks>
        /// By keeping the names exactly as they are defined we can reduce string count and make the initial P/Invoke call slightly faster.
        /// </remarks>
        public static class Direct
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
        }

        public static string GetKnownFolderPath(Guid folderIdentifier, KNOWN_FOLDER_FLAG flags = KNOWN_FOLDER_FLAG.KF_FLAG_DEFAULT)
        {
            string path;
            HRESULT hr = Direct.SHGetKnownFolderPath(folderIdentifier, flags, EmptySafeHandle.Instance, out path);
            if (hr != HRESULT.S_OK)
                throw ErrorHelper.GetIoExceptionForHResult(hr, folderIdentifier.ToString());

            return path;
        }

        public static ItemIdList GetKnownFolderId(Guid folderIdentifier, KNOWN_FOLDER_FLAG flags = KNOWN_FOLDER_FLAG.KF_FLAG_DEFAULT)
        {
            ItemIdList id;
            HRESULT hr = Direct.SHGetKnownFolderIDList(folderIdentifier, flags, EmptySafeHandle.Instance, out id);
            if (hr != HRESULT.S_OK)
                throw ErrorHelper.GetIoExceptionForHResult(hr, folderIdentifier.ToString());

            return id;
        }

        public static string GetNameFromId(ItemIdList id, SIGDN form = SIGDN.NORMALDISPLAY)
        {
            string name;
            HRESULT hr = Direct.SHGetNameFromIDList(id, form, out name);
            if (hr != HRESULT.S_OK)
                throw ErrorHelper.GetIoExceptionForHResult(hr);

            return name;
        }

        public static IKnownFolderManager GetKnownFolderManager()
        {
            return (IKnownFolderManager)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid(ClassIds.CLSID_KnownFolderManager)));
        }

        public static IEnumerable<Guid> GetKnownFolderIds()
        {
            List<Guid> ids = new List<Guid>();

            IKnownFolderManager manager = GetKnownFolderManager();
            SafeComHandle buffer;
            uint count = manager.GetFolderIds(out buffer);
            using (buffer)
            {
                Reader reader = new Reader(buffer);
                for (int i = 0; i < count; i++)
                    ids.Add(reader.ReadStruct<Guid>());
            }

            return ids;
        }
    }
}
