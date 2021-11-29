// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Represents a producer of pixels that can fill an arbitrary 2D plane. [ID2D1Image]
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    [Guid(InterfaceIds.IID_ID2D1Image)]
    public unsafe struct Image : Resource.Interface, IDisposable
    {
        internal readonly ID2D1Image* _handle;

        internal Image(ID2D1Image* handle) => _handle = handle;

        public unsafe Factory GetFactory() => Resource.From(this).GetFactory();

        public void Dispose() => _handle->Release();
    }
}
