// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

// Not using "CallingConvention" to avoid colliding with .NET

/// <summary>
///  [CALLCONV]
/// </summary>
public enum CallConvention
{
    /// <summary>
    ///  [CC_FASTCALL]
    /// </summary>
    FastCall = 0,

    /// <summary>
    ///  [CC_CDECL]
    /// </summary>
    CDecl = 1,

    /// <summary>
    ///  [CC_PASCAL]
    /// </summary>
    Pascal = CDecl + 1,

    /// <summary>
    ///  [CC_MACPASCAL]
    /// </summary>
    MacPascal = Pascal + 1,

    /// <summary>
    ///  [CC_STDCALL]
    /// </summary>
    StdCall = MacPascal + 1,

    // No idea on CC_FPFASTCALL

    /// <summary>
    ///  [CC_FPFASTCALL]
    /// </summary>
    FpFastCall = StdCall + 1,

    /// <summary>
    ///  [CC_SYSCALL]
    /// </summary>
    SysCall = FpFastCall + 1,

    // Fairly confident in the next two being Macintosh Programmer's Workshop, but not positive.

    /// <summary>
    ///  Macintosh Programmer's Workshop CDecl. [CC_MPWCDECL]
    /// </summary>
    MpwCDecl = SysCall + 1,

    /// <summary>
    ///  Macintosh Programmer's Workshop Pascal. [CC_MPWPASCAL]
    /// </summary>
    MpwPascal = MpwCDecl + 1,
}