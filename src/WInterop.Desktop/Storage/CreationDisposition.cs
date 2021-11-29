// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage;

/// <summary>
///  Called FileMode in .NET System.IO.
/// </summary>
/// <remarks>
///  FileMode.Append is a .NET construct- it is OPEN_ALWAYS with FileIOPermissionAccess.Append.
/// </remarks>
public enum CreationDisposition : uint
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/aa363858.aspx

    /// <summary>
    ///  Create or fail if exists. [CREATE_NEW]
    /// </summary>
    /// <remarks>
    ///  The equivalent of <see cref="System.IO.FileMode.CreateNew"/>
    /// </remarks>
    CreateNew = 1,

    /// <summary>
    ///  Create or overwrite. [CREATE_ALWAYS]
    /// </summary>
    /// <remarks>
    ///  The equivalent of <see cref="System.IO.FileMode.Create"/>
    /// </remarks>
    CreateAlways = 2,

    /// <summary>
    ///  Opens if exists, fails otherwise. [OPEN_EXISTING]
    /// </summary>
    /// <remarks>
    ///  The equivalent of <see cref="System.IO.FileMode.Open"/>
    /// </remarks>
    OpenExisting = 3,

    /// <summary>
    ///  Open if exists, creates otherwise. [OPEN_ALWAYS]
    /// </summary>
    /// <remarks>
    ///  The equivalent of <see cref="System.IO.FileMode.OpenOrCreate"/>
    /// </remarks>
    OpenAlways = 4,

    /// <summary>
    ///  Opens if exists and sets the size to zero. Fails if the file does not exist. [TRUNCATE_EXISTING]
    /// </summary>
    /// <remarks>
    ///  The equivalent of <see cref="System.IO.FileMode.Truncate"/>
    /// </remarks>
    TruncateExisting = 5,
}