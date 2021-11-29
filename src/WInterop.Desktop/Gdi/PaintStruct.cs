// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using WInterop.Gdi.Native;

namespace WInterop.Gdi;

public readonly ref struct PaintStruct
{
    /// <summary>
    ///  The display DeviceContext for painting
    /// </summary>
    public DeviceContext DeviceContext { get; }

    /// <summary>
    ///  True if the background needs erased (the Window class doesn't have a background brush).
    /// </summary>
    public bool Erase { get; }

    /// <summary>
    ///  The bounds for what needs to be painted, relative to the upper left corner of the client.
    /// </summary>
    public Rectangle Paint { get; }

    private PaintStruct(in PAINTSTRUCT ps)
    {
        DeviceContext = new DeviceContext(ps.hdc);
        Erase = ps.fErase;
        Paint = ps.rcPaint;
    }

    public static implicit operator PaintStruct(in PAINTSTRUCT ps) => new(in ps);
}