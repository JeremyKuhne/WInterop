// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Storage;

/// <summary>
///  Interface for transforming raw find data into a result.
/// </summary>
public interface IFindTransform<T>
{
    T TransformResult(ref RawFindData findData);
}