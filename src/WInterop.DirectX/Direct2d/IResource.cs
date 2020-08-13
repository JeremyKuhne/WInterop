// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    ///  [ID2D1Resource]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Resource),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IResource
    {
        [PreserveSig]
        void GetFactory(
            out IFactory factory);
    }
}
