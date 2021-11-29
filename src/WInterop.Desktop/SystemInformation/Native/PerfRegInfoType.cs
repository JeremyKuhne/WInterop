// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.SystemInformation.Native;

public enum PerfRegInfoType
{
    /// <summary>
    ///  [PERF_REG_COUNTERSET_STRUCT]
    /// </summary>
    CountersetStruct,

    /// <summary>
    ///  [PERF_REG_COUNTER_STRUCT]
    /// </summary>
    CounterStruct,

    /// <summary>
    ///  [PERF_REG_COUNTERSET_NAME_STRING]
    /// </summary>
    CountersetNameString,

    /// <summary>
    ///  [PERF_REG_COUNTERSET_HELP_STRING]
    /// </summary>
    CountersetHelpString,

    /// <summary>
    ///  [PERF_REG_COUNTER_NAME_STRINGS]
    /// </summary>
    CounterNameStrings,

    /// <summary>
    ///  [PERF_REG_COUNTER_HELP_STRINGS]
    /// </summary>
    CounterHelpStrings,

    /// <summary>
    ///  [PERF_REG_PROVIDER_NAME]
    /// </summary>
    ProviderName,

    /// <summary>
    ///  [PERF_REG_PROVIDER_GUID]
    /// </summary>
    ProviderGuid,

    /// <summary>
    ///  [PERF_REG_COUNTERSET_ENGLISH_NAME]
    /// </summary>
    CountersetEnglishName,

    /// <summary>
    ///  [PERF_REG_COUNTER_ENGLISH_NAMES]
    /// </summary>
    CounterEnglishNames
}