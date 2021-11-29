// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  [FUNCKIND]
/// </summary>
public enum FunctionKind
{
    /// <summary>
    ///  [FUNC_VIRTUAL]
    /// </summary>
    Virtual = 0,

    /// <summary>
    ///  [FUNC_PUREVIRTUAL]
    /// </summary>
    PureVirtual = Virtual + 1,

    /// <summary>
    ///  [FUNC_NONVIRTUAL]
    /// </summary>
    NonVirtual = PureVirtual + 1,

    /// <summary>
    ///  [FUNC_STATIC]
    /// </summary>
    Static = NonVirtual + 1,

    /// <summary>
    ///  [FUNC_DISPATCH]
    /// </summary>
    Dispatch = Static + 1
}