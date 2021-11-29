// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com.Native;

public enum ADVF : uint
{
    ADVF_NODATA = 1,
    ADVF_PRIMEFIRST = 2,
    ADVF_ONLYONCE = 4,
    ADVF_DATAONSTOP = 64,
    ADVFCACHE_NOHANDLER = 8,
    ADVFCACHE_FORCEBUILTIN = 16,
    ADVFCACHE_ONSAVE = 32
}