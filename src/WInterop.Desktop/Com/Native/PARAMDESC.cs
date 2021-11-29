// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com.Native;

/// <summary>
///  <see cref="https://docs.microsoft.com/en-us/windows/win32/api/oaidl/ns-oaidl-paramdesc"/>
/// </summary>
public unsafe struct PARAMDESC
{
    public PARAMDESCEX* pparamdescex;
    public ParameterFlags wParamFlags;
}