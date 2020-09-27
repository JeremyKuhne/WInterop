// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GdiPlus.Native
{
    public readonly struct GpMetafile
    {
        public nuint Handle { get; }

        private GpMetafile(nuint handle) => Handle = handle;

        public static explicit operator GpMetafile(GpImage metafile) => new GpMetafile(metafile.Handle);
    }
}