// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell;

[Flags]
public enum BrowseInfoFlags : uint
{
    /// <summary>
    ///  Return only file system directories.
    ///  [BIF_RETURNONLYFSDIRS]
    /// </summary>
    ReturnOnlyFileSystemDirectories = 0x00000001,

    /// <summary>
    ///  Do not include network folders below the domain level in the dialog box's tree view control.
    ///  [BIF_DONTGOBELOWDOMAIN]
    /// </summary>
    DontGoBelowDomain = 0x00000002,

    /// <summary>
    ///  Include a status area in the dialog box. The callback function can set the status text by
    ///  sending messages to the dialog box. This flag is not supported when BIF_NEWDIALOGSTYLE is specified.
    ///  [BIF_STATUSTEXT]
    /// </summary>
    StatusText = 0x00000004,

    /// <summary>
    ///  Only return file system ancestors. An ancestor is a subfolder that is beneath the root
    ///  folder in the namespace hierarchy. [BIF_RETURNFSANCESTORS]
    /// </summary>
    ReturnFileSystemAncestors = 0x00000008,

    /// <summary>
    ///  Add editbox to the dialog. [BIF_EDITBOX]
    /// </summary>
    EditBox = 0x00000010,

    /// <summary>
    ///  If the user types an invalid name into the edit box, the browse dialog box calls the
    ///  application's BrowseCallbackProc with the BFFM_VALIDATEFAILED message.
    ///
    ///  Used with <see cref="EditBox"/>. [BIF_VALIDATE]
    /// </summary>
    Validate = 0x00000020,

    /// <summary>
    ///  Use the new dialog layout with the ability to resize. [BIF_NEWDIALOGSTYLE]
    /// </summary>
    /// <remarks>
    ///  COM must not be initialized with COINIT_MULTITHREADED.
    /// </remarks>
    NewDialogStyle = 0x00000040,

    /// <summary>
    ///  Allow URLs to be displayed or entered. Requires <see cref="EditBox"/>, <see cref="NewDialogStyle"/>,
    ///  and <see cref="BrowseIncludeFiles"/>.  [BIF_BROWSEINCLUDEURLS]
    /// </summary>
    BrowseIncludeUrls = 0x00000080,

    /// <summary>
    ///  Adds a usgae hint to the dialog. Requires <see cref="NewDialogStyle"/> and
    ///  overrides <see cref="EditBox"/>. [BIF_UAHINT]
    /// </summary>
    UaHint = 0x00000100,

    /// <summary>
    ///  Don't show new folder button. Only applies to <see cref="NewDialogStyle"/>. [BIF_NONEWFOLDERBUTTON]
    /// </summary>
    NoNewFolderButton = 0x00000200,

    /// <summary>
    ///  Return shortcut PIDLS rather than the target. [BIF_NOTRANSLATETARGETS]
    /// </summary>
    NoTranslateTargets = 0x00000400,

    /// <summary>
    ///  Browse for computers. [BIF_BROWSEFORCOMPUTER]
    /// </summary>
    BrowseForComputer = 0x00001000,

    /// <summary>
    ///  Brows for printers. [BIF_BROWSEFORPRINTER]
    /// </summary>
    BrowseForPrinter = 0x00002000,

    /// <summary>
    ///  Display files as well as folders. [BIF_BROWSEINCLUDEFILES]
    /// </summary>
    BrowseIncludeFiles = 0x00004000,

    /// <summary>
    ///  Displays sharable resources on remote systems. Requires <see cref="NewDialogStyle"/>.
    ///  [BIF_SHAREABLE]
    /// </summary>
    Shareable = 0x00008000,

    /// <summary>
    ///  Can browse libraries and zip files. [BIF_BROWSEFILEJUNCTIONS]
    /// </summary>
    BrowseFileJunctions = 0x00010000
}