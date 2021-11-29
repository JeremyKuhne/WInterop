// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Com;

/// <summary>
///  Locking type for <see cref="IStream.LockRegion(ulong, ulong, uint)"/> and
///  ILockBytes.LockRegion. <see cref="https://docs.microsoft.com/windows/win32/api/objidl/ne-objidl-locktype"/>
/// </summary>
[Flags]
public enum LockType : uint
{
    /// <summary>
    ///  [LOCK_WRITE]
    /// </summary>
    Write = 1,

    /// <summary>
    ///  [LOCK_EXCLUSIVE]
    /// </summary>
    Exclusive = 2,

    /// <summary>
    ///  [LOCK_ONLYONCE]
    /// </summary>
    OnlyOnce = 4
}