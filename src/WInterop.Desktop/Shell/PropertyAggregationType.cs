// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Shell;

/// <summary>
///  [PROPDESC_AGGREGATION_TYPE]
/// </summary>
/// <msdn><see cref="https://docs.microsoft.com/en-us/windows/desktop/api/propsys/ne-propsys-propdesc_aggregation_type"/></msdn>
public enum PropertyAggregationType
{
    /// <summary>
    ///  Display the string "Multiple Values". [PDAT_DEFAULT]
    /// </summary>
    Default = 0,

    /// <summary>
    ///  Display the first value in the selection. [PDAT_FIRST]
    /// </summary>
    First = 1,

    /// <summary>
    ///  [PDAT_SUM]
    /// </summary>
    Sum = 2,

    /// <summary>
    ///  [PDAT_AVERAGE]
    /// </summary>
    Average = 3,

    /// <summary>
    ///  [PDAT_DATERANGE]
    /// </summary>
    DateRange = 4,

    /// <summary>
    ///  Display a concatenated string of all the values. [PDAT_UNION]
    /// </summary>
    Union = 5,

    /// <summary>
    ///  [PDAT_MAX]
    /// </summary>
    Max = 6,

    /// <summary>
    ///  [PDAT_MIN]
    /// </summary>
    Min = 7
}