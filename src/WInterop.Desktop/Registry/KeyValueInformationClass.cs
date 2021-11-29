// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Registry;

/// <summary>
///  [KEY_VALUE_INFORMATION_CLASS]
/// </summary>
// https://msdn.microsoft.com/en-us/library/windows/hardware/ff554218.aspx
public enum KeyValueInformationClass : uint
{
    BasicInformation = 0,
    FullInformation,
    PartialInformation,
    FullInformationAlign64,
    PartialInformationAlign64
}