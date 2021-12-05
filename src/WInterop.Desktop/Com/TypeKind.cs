// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  [TYPEKIND]
/// </summary>
public enum TypeKind : uint
{
    /// <summary>
    ///  [TKIND_ENUM]
    /// </summary>
    Enum = TYPEKIND.TKIND_ENUM,

    /// <summary>
    ///  [TKIND_RECORD]
    /// </summary>
    Record = TYPEKIND.TKIND_RECORD,

    /// <summary>
    ///  [TKIND_MODULE]
    /// </summary>
    Module = TYPEKIND.TKIND_MODULE,

    /// <summary>
    ///  [TKIND_INTERFACE]
    /// </summary>
    Interface = TYPEKIND.TKIND_INTERFACE,

    /// <summary>
    ///  [TKIND_DISPATCH]
    /// </summary>
    Dispatch = TYPEKIND.TKIND_DISPATCH,

    /// <summary>
    ///  [TKIND_COCLASS]
    /// </summary>
    CoClass = TYPEKIND.TKIND_COCLASS,

    /// <summary>
    ///  [TKIND_ALIAS]
    /// </summary>
    Alias = TYPEKIND.TKIND_ALIAS,

    /// <summary>
    ///  [TKIND_UNION]
    /// </summary>
    Union = TYPEKIND.TKIND_UNION
}