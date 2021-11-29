// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Security;

namespace WInterop.Registry;

// https://msdn.microsoft.com/en-us/library/windows/desktop/ms724878.aspx
// https://msdn.microsoft.com/en-us/library/windows/desktop/aa384129.aspx

[Flags]
public enum RegistryAccessRights : uint
{
    /// <summary>
    ///  [KEY_QUERY_VALUE]
    /// </summary>
    QueryValue = 0x0001,

    /// <summary>
    ///  [KEY_SET_VALUE]
    /// </summary>
    SetValue = 0x0002,

    /// <summary>
    ///  [KEY_CREATE_SUB_KEY]
    /// </summary>
    CreateSubkey = 0x0004,

    /// <summary>
    ///  [KEY_ENUMERATE_SUB_KEYS]
    /// </summary>
    EnumerateSubkeys = 0x0008,

    /// <summary>
    ///  [KEY_NOTIFY]
    /// </summary>
    Notify = 0x0010,

    /// <summary>
    ///  [KEY_CREATE_LINK]
    /// </summary>
    CreateLink = 0x0020,

    /// <summary>
    ///  Forces accessing the 32 bit view of the registry. [KEY_WOW64_32KEY]
    /// </summary>
    Wow6432Key = 0x0200,

    /// <summary>
    ///  Forces accessing the 64 bit view of the registry. [KEY_WOW64_64KEY]
    /// </summary>
    Wow6464Key = 0x0100,

    /// <summary>
    ///  [KEY_READ]
    /// </summary>
    Read =
        StandardAccessRights.Read
        | QueryValue
        | EnumerateSubkeys
        | Notify,

    /// <summary>
    ///  [KEY_WRITE]
    /// </summary>
    Write =
        StandardAccessRights.Write
        | SetValue
        | CreateSubkey,

    /// <summary>
    ///  [KEY_EXECUTE]
    /// </summary>
    Execute = Read,

    /// <summary>
    ///  [KEY_ALL_ACCESS]
    /// </summary>
    AllAccess = StandardAccessRights.Required
        | QueryValue
        | SetValue
        | CreateSubkey
        | EnumerateSubkeys
        | Notify
        | CreateLink,

    /// <summary>
    ///  The right to delete the object.
    /// </summary>
    Delete = StandardAccessRights.Delete,

    /// <summary>
    ///  The right to read the information in the object's security descriptor.
    ///  Doesn't include system access control list info (SACL).
    /// </summary>
    ReadControl = StandardAccessRights.ReadControl,

    /// <summary>
    ///  The right to modify the discretionary access control list (DACL) in the
    ///  object's security descriptor.
    /// </summary>
    WriteDac = StandardAccessRights.WriteDac,

    /// <summary>
    ///  The right to change the owner in the object's security descriptor.
    /// </summary>
    WriteOwner = StandardAccessRights.WriteOwner,

    /// <summary>
    ///  Internally maps to KEY_READ.
    /// </summary>
    GenericRead = GenericAccessRights.Read,

    /// <summary>
    ///  Internally maps to KEY_WRITE.
    /// </summary>
    GenericWrite = GenericAccessRights.Write,

    /// <summary>
    ///  Internally maps to KEY_EXECUTE | KEY_CREATE_LINK.
    /// </summary>
    GenericExecute = GenericAccessRights.Execute,

    /// <summary>
    ///  Internally maps to KEY_ALL_ACCESS.
    /// </summary>
    GenericAll = GenericAccessRights.All
}