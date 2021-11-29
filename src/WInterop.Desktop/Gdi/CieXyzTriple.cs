// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

/// <summary>
///  [CIEXYZTRIPLE]
/// </summary>
/// <see cref="https://msdn.microsoft.com/en-us/library/dd371833.aspx"/>
public struct CieXyzTriple
{
    /// <summary>
    ///  [ciexyzRed]
    /// </summary>
    public CieXyz Red;

    /// <summary>
    ///  [ciexyzGreen]
    /// </summary>
    public CieXyz Green;

    /// <summary>
    ///  [ciexyzBlue]
    /// </summary>
    public CieXyz Blue;
}