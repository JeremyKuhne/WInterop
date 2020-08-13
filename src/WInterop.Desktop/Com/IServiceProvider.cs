// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Com
{
    /// <summary>
    ///  OLE IServiceProvider interface.
    /// </summary>
    [ComImport,
        Guid("6d5140c1-7436-11ce-8034-00aa006009fa"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IServiceProvider
    {
        object QueryService(
            ref Guid guidService,
            ref Guid riid);
    }
}
