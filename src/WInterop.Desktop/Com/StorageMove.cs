// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  Options for <see cref="IStorage.MoveElementTo(string, IStorage, string, StorageMove)"/>
/// </summary>
public enum StorageMove : uint
{
    /// <summary>
    ///  [STGMOVE_MOVE]
    /// </summary>
    Move = 0,

    /// <summary>
    ///  [STGMOVE_COPY]
    /// </summary>
    Copy = 1,

    /// <summary>
    ///  Not implemented. [STGMOVE_SHALLOWCOPY]
    /// </summary>
    ShallowCopy = 2
}