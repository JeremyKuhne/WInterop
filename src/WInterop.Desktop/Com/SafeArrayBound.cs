// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  [SAFEARRAYBOUND]
///  <see cref="https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-oaut/1941311d-9b7d-4a5b-b246-2d2eaad00f8c"/>
/// </summary>
public struct SafeArrayBound
{
    /// <summary>
    ///  Count of elements in the current dimension. Must not be zero. [cElements]
    /// </summary>
    public uint Count;

    /// <summary>
    ///  Lower bound of the current dimension. [lLbound]
    /// </summary>
    public int LowerBound;
}