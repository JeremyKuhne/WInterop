// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/dd378447.aspx">KNOWN_FOLDER_FLAG</a> enumeration.
    /// </summary>
    [Flags]
    public enum KnownFolderFlags : uint
    {
        Default = 0x00000000,

        /// <summary>
        ///  Build a simple IDList (PIDL) This value can be used when you want to retrieve the file system path but do not specify this value if you are
        ///  retrieving the localized display name of the folder because it might not resolve correctly.
        /// </summary>
        SimpleIdList = 0x00000100,

        /// <summary>
        ///  Gets the folder's default path independent of the current location of its parent. KF_FLAG_DEFAULT_PATH must also be set.
        /// </summary>
        NotParentRelative = 0x00000200,

        /// <summary>
        ///  Gets the default path for a known folder. If this flag is not set, the function retrieves the current—and possibly redirected—path of the folder.
        ///  The execution of this flag includes a verification of the folder's existence unless KF_FLAG_DONT_VERIFY is set.
        /// </summary>
        DefaultPath = 0x00000400,

        /// <summary>
        ///  Initializes the folder using its Desktop.ini settings. If the folder cannot be initialized, the function returns a failure code and
        ///  no path is returned. This flag should always be combined with KF_FLAG_CREATE.
        ///
        ///  If the folder is located on a network, the function might take a longer time to execute.
        /// </summary>
        Init = 0x00000800,

        /// <summary>
        ///  Gets the true system path for the folder, free of any aliased placeholders such as %USERPROFILE%, returned by SHGetKnownFolderIDList
        ///  and IKnownFolder::GetIDList. This flag has no effect on paths returned by SHGetKnownFolderPath and IKnownFolder::GetPath. By default,
        ///  known folder retrieval functions and methods return the aliased path if an alias exists.
        /// </summary>
        NoAlias = 0x00001000,

        /// <summary>
        ///  Stores the full path in the registry without using environment strings. If this flag is not set, portions of the path may be
        ///  represented by environment strings such as %USERPROFILE%.
        /// </summary>
        DontUnexpand = 0x00002000,

        /// <summary>
        ///  Do not verify the folder's existence before attempting to retrieve the path or IDList. If this flag is not set, an attempt is
        ///  made to verify that the folder is truly present at the path. If that verification fails due to the folder being absent or
        ///  inaccessible, the function returns a failure code and no path is returned.
        ///
        ///  If the folder is located on a network, the function might take a longer time to execute. Setting this flag can reduce that lag time.
        /// </summary>
        DontVerify = 0x00004000,

        /// <summary>
        ///  Forces the creation of the specified folder if that folder does not already exist. The security provisions predefined for that folder
        ///  are applied. If the folder does not exist and cannot be created, the function returns a failure code and no path is returned.
        /// </summary>
        Create = 0x00008000,

        /// <summary>
        ///  When running inside an app container, or when providing an app container token, this flag prevents redirection to app container
        ///  folders. Instead, it retrieves the path that would be returned where it not running inside an app container.
        /// </summary>
        NoAppcontainerRedirection = 0x00010000,

        /// <summary>
        ///  Return only aliased PIDLs. Do not use the file system path.
        /// </summary>
        AliasOnly = 0x80000000
    }
}