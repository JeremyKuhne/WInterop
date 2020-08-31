// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi.Native
{
    public readonly struct HMETAFILE
    {
        public nint Value { get; }

        public HMETAFILE(nint handle)
        {
            Value = handle;
        }

        public bool IsInvalid => IsNull;
        public bool IsNull => Value == 0;

        public static implicit operator HGDIOBJ(HMETAFILE handle) => new HGDIOBJ(handle.Value);
        public static explicit operator HMETAFILE(HGDIOBJ handle) => new HMETAFILE(handle.Handle);
        public static explicit operator HMETAFILE(nint handle) => new HMETAFILE(handle);
    }
}