// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com
{
    /// <summary>
    ///  Provides information about the parent undo unit. [UASFLAGS]
    /// </summary>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/ne-ocidl-taguasflags"/>
    [Flags]
    public enum UndoStateFlags : uint
    {
        UAS_NORMAL = 0,
        UAS_BLOCKED = 0x1,
        UAS_NOPARENTENABLE = 0x2,
        UAS_MASK = 0x3
    }
}