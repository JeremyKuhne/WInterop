// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  Options for <see cref="StructuredStorage.MoveElementTo(string, StructuredStorage, string, StorageMove)"/>
/// </summary>
public enum StorageMove : uint
{
    /// <summary>
    ///  [STGMOVE_MOVE]
    /// </summary>
    Move = STGMOVE.STGMOVE_MOVE,

    /// <summary>
    ///  [STGMOVE_COPY]
    /// </summary>
    Copy = STGMOVE.STGMOVE_COPY,

    /// <summary>
    ///  Not implemented. [STGMOVE_SHALLOWCOPY]
    /// </summary>
    ShallowCopy = STGMOVE.STGMOVE_SHALLOWCOPY
}