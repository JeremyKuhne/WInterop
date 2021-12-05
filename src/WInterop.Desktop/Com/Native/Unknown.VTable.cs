// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.Com.Native;

public unsafe partial struct Unknown
{
    public unsafe struct VTable
    {
        public delegate* unmanaged<void*, Guid*, void**, HResult> QueryInterface;
        public delegate* unmanaged<void*, uint> AddRef;
        public delegate* unmanaged<void*, uint> Release;
    }
}