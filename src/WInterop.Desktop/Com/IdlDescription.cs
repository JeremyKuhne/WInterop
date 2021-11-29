// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

public struct IdlDescription
{
    /// <summary>
    ///  [dwReserved]
    /// </summary>
    public uint Reserved;

    /// <summary>
    ///  [wIDLFlags]
    /// </summary>
    public IdlFlag Flags;
}