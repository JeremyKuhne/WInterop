// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage
{
    /// <summary>
    ///  Interface for filtering out find results.
    /// </summary>
    public interface IFindFilter
    {
        bool Match(ref RawFindData findData);
    }
}