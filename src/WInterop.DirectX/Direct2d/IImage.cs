// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Represents a producer of pixels that can fill an arbitrary 2D plane. [ID2D1Image]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Image),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IImage : IResource
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion
    }
}
