// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Shell;

/// <summary>
///  [ASSOCKEY]
/// </summary>
// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762474.aspx
public enum AssociationKey
{
    /// <summary>
    ///  A key that is passsed to ShellExecuteEx through a SHELLEXECUTEINFO structure.
    ///  [ASSOCKEY_SHELLEXECCLASS]
    /// </summary>
    ShellExecClass = 1,

    /// <summary>
    ///  An Application key for the file type. [ASSOCKEY_APP]
    /// </summary>
    App,

    /// <summary>
    ///  A ProgID or class key. [ASSOCKEY_CLASS]
    /// </summary>
    Class,

    /// <summary>
    ///  A BaseClass value. [ASSOCKEY_BASECLASS]
    /// </summary>
    BaseClass
}