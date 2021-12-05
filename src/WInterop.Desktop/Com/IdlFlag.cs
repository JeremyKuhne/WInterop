// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

[Flags]
public enum IdlFlag : ushort
{
    /// <summary>
    ///  [IDLFLAG_NONE]
    /// </summary>
    None = TerraFXWindows.IDLFLAG_NONE,

    /// <summary>
    ///  [IDLFLAG_FIN]
    /// </summary>
    In = TerraFXWindows.IDLFLAG_FIN,

    /// <summary>
    ///  [IDLFLAG_FOUT]
    /// </summary>
    Out = TerraFXWindows.IDLFLAG_FOUT,

    /// <summary>
    ///  [IDLFLAG_FLCID]
    /// </summary>
    LocalIdentifer = TerraFXWindows.IDLFLAG_FLCID,

    /// <summary>
    ///  [IDLFLAG_FRETVAL]
    /// </summary>
    ReturnValue = TerraFXWindows.IDLFLAG_FRETVAL,
}