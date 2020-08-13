// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  [ID2D1TessellationSink]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1TessellationSink),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ITesselationSink
    {
        [PreserveSig]
        void AddTriangles(
            ref Triangle triangles,
            uint trianglesCount);

        [PreserveSig]
        void Close();
    }
}
