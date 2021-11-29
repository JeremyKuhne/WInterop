// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Globalization;

/// <summary>
///  [NLSVERSIONINFOEX]
/// </summary>
/// <docs>https://docs.microsoft.com/windows/win32/api/winnls/ns-winnls-nlsversioninfoex</docs>
public struct NlsVersionInfoExtended
{
    public uint NLSVersionInfoSize;
    public uint NLSVersion;
    public uint DefinedVersion;
    public uint EffectiveId;
    public Guid CustomVersion;
}