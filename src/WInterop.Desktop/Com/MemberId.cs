// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  [MEMBERID]
/// </summary>
public struct MemberId
{
    /// <summary>
    ///  Used for getting info about the type itself. [MEMBERID_NIL]
    /// </summary>
    public static MemberId Nil => new MemberId(-1);

    public int Value;

    public MemberId(int id) => Value = id;

    public static implicit operator int(MemberId id) => id.Value;
    public static implicit operator MemberId(int id) => new MemberId(id);
}