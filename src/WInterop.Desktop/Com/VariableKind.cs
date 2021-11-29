// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  [VARKIND]
/// </summary>
public enum VariableKind
{
    /// <summary>
    ///  Structure or union member. [VAR_PERINSTANCE]
    /// </summary>
    PerInstance = 0,

    /// <summary>
    ///  AppObject CoClass. [VAR_STATIC]
    /// </summary>
    Static = PerInstance + 1,

    /// <summary>
    ///  Member of a module or enumeration. [VAR_CONST]
    /// </summary>
    Constant = Static + 1,

    /// <summary>
    ///  [VAR_DISPATCH]
    /// </summary>
    Dispatch = Constant + 1
}