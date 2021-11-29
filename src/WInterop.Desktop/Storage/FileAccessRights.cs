// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Security;

namespace WInterop.Storage;

// File Access Rights Constants
// https://msdn.microsoft.com/en-us/library/windows/desktop/gg258116.aspx

/// <summary>
///  File specific access rights.
/// </summary>
[Flags]
public enum FileAccessRights : uint
{
    /// <summary>
    ///  The right to read data from the file. [FILE_READ_DATA]
    /// </summary>
    ReadData = 0x0001,

    /// <summary>
    ///  The right to write data to the file. [FILE_WRITE_DATA]
    /// </summary>
    WriteData = 0x0002,

    /// <summary>
    ///  The right to append data to the file. WriteData is needed
    ///  to overwrite existing data. [FILE_APPEND_DATA]
    /// </summary>
    AppendData = 0x0004,

    /// <summary>
    ///  The right to read extended attributes. [FILE_READ_EA]
    /// </summary>
    ReadExtendedAttributes = 0x0008,

    /// <summary>
    ///  The right to write extended attributes. [FILE_WRITE_EA]
    /// </summary>
    WriteExtendedAttributes = 0x0010,

    /// <summary>
    ///  The right to execute the file. [FILE_EXECUTE]
    /// </summary>
    Execute = 0x0020,

    /// <summary>
    ///  The right to read attributes. [FILE_READ_ATTRIBUTES]
    /// </summary>
    ReadAttributes = 0x0080,

    /// <summary>
    ///  The right to write attributes. [FILE_WRITE_ATTRIBUTES]
    /// </summary>
    WriteAttributes = 0x0100,

    /// <summary>
    ///  The right to delete the object. [DELETE]
    /// </summary>
    Delete = StandardAccessRights.Delete,

    /// <summary>
    ///  The right to read the information in the object's security descriptor.
    ///  Doesn't include system access control list info (SACL). [READ_CONTROL]
    /// </summary>
    ReadControl = StandardAccessRights.ReadControl,

    /// <summary>
    ///  The right to modify the discretionary access control list (DACL) in the
    ///  object's security descriptor. [WRITE_DAC]
    /// </summary>
    WriteDac = StandardAccessRights.WriteDac,

    /// <summary>
    ///  The right to change the owner in the object's security descriptor. [WRITE_OWNER]
    /// </summary>
    WriteOwner = StandardAccessRights.WriteOwner,

    /// <summary>
    ///  The right to use the object for synchronization. Enables a thread to wait
    ///  until the object is in the signaled state. [SYNCHRONIZE]
    /// </summary>
    Synchronize = StandardAccessRights.Synchronize,

    /// <summary>
    ///  Maps internally to ReadAttributes | ReadData | ReadExtendedAttributes | StandardRightsRead | Synchronize.
    ///  [FILE_GENERIC_READ]
    /// </summary>
    /// <remarks>
    ///  This is effectively equivalent to System.Security.AccessControl.FileSystemRights.Read,
    ///  although the rights are mapped out (modulo <see cref="Synchronize"/>.
    /// </remarks>
    GenericRead = GenericAccessRights.Read,

    /// <summary>
    ///  Maps internally to AppendData | WriteAttributes | WriteData | WriteExtendedAttributes | StandardRightsWrite | Synchronize.
    ///  [FILE_GENERIC_WRITE]
    /// </summary>
    GenericWrite = GenericAccessRights.Write,

    /// <summary>
    ///  Maps internally to Execute | ReadAttributes | StandardRightsExecute | Synchronize.
    ///  [FILE_GENERIC_EXECUTE]
    /// </summary>
    GenericExecute = GenericAccessRights.Execute,

    /// <summary>
    ///  Not technically part of the SDK, for convenience.
    /// </summary>
    GenericReadWrite = GenericRead | GenericWrite
}