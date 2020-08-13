// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using WInterop.Com;
using WInterop.Errors;
using WInterop.Handles;
using WInterop.Registry;
using WInterop.Security;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.Shell
{
    public static partial class ShellMethods
    {
        public static IPropertyDescriptionList GetPropertyDescriptionListFromString(string value)
        {
            Imports.PSGetPropertyDescriptionListFromString(value, new Guid(InterfaceIds.IID_IPropertyDescriptionList), out IPropertyDescriptionList list)
                .ThrowIfFailed();
            return list;
        }

        public static RegistryKeyHandle AssocQueryKey(AssociationFlags flags, AssociationKey key, string association, string extraInfo)
        {
            Imports.AssocQueryKeyW(flags, key, association, extraInfo, out RegistryKeyHandle handle)
                .ThrowIfFailed();
            return handle;
        }

        public static string AssocQueryString(AssociationFlags flags, AssociationString @string, string association, string extraInfo)
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                flags |= AssociationFlags.NoTruncate;

                HResult result;
                uint count = buffer.CharCapacity;
                while ((result = Imports.AssocQueryStringW(flags, @string, association, extraInfo, buffer, ref count)) == HResult.E_POINTER)
                {
                    buffer.EnsureCharCapacity(count);
                }

                result.ThrowIfFailed(association);

                // Count includes the null
                buffer.Length = count - 1;
                return buffer.ToString();
            });
        }

        /// <summary>
        ///  Get the path for the given known folder Guid.
        /// </summary>
        public static string GetKnownFolderPath(Guid folderIdentifier, KnownFolderFlags flags = KnownFolderFlags.Default)
        {
            Imports.SHGetKnownFolderPath(ref folderIdentifier, flags, EmptySafeHandle.Instance, out string path)
                .ThrowIfFailed();

            return path;
        }

        /// <summary>
        ///  Get the Shell item id for the given known folder Guid.
        /// </summary>
        public static ItemIdList GetKnownFolderId(Guid folderIdentifier, KnownFolderFlags flags = KnownFolderFlags.Default)
        {
            Imports.SHGetKnownFolderIDList(ref folderIdentifier, flags, EmptySafeHandle.Instance, out ItemIdList id)
                .ThrowIfFailed();

            return id;
        }

        /// <summary>
        ///  Get the name for a given Shell item ID.
        /// </summary>
        public static string GetNameFromId(ItemIdList id, ShellItemDisplayNames form = ShellItemDisplayNames.NormalDisplay)
        {
            Imports.SHGetNameFromIDList(id, form, out string name).ThrowIfFailed();
            return name;
        }

        /// <summary>
        ///  Get the IKnownFolderManager.
        /// </summary>
        public static IKnownFolderManager GetKnownFolderManager()
        {
            return (IKnownFolderManager)(Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid(ClassIds.CLSID_KnownFolderManager)))
                ?? throw new InvalidOperationException());
        }

        /// <summary>
        ///  Get the Guid identifiers for all known folders.
        /// </summary>
        public static unsafe IEnumerable<Guid> GetKnownFolderIds()
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
        ///  Collapses common path segments into the equivalent environment string.
        ///  Returns null if unsuccessful.
        /// </summary>
        public static string? UnexpandEnvironmentStrings(string path)
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
        ///  Expands environment variables for the given user token. If the token is
        ///  null, returns the system variables.
        /// </summary>
        public static string ExpandEnvironmentVariablesForUser(AccessToken token, string value)
        {
            return BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                while (!Imports.ExpandEnvironmentStringsForUserW(token, value, buffer, buffer.CharCapacity))
                {
                    Error.ThrowIfLastErrorNot(WindowsError.ERROR_INSUFFICIENT_BUFFER);
                    buffer.EnsureCharCapacity(buffer.CharCapacity * 2);
                }

                buffer.SetLengthToFirstNull();
                return buffer.ToString();
            });
        }
    }
}
