// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Com
{
    /// <summary>
    ///  OLE IEnumOleUndoUnits interface.
    /// </summary>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nn-ocidl-ienumoleundounits"/>
    [ComImport,
        Guid("B3E7C340-EF97-11CE-9BC9-00AA00608E01"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumOleUndoUnits
    {
        [PreserveSig]
        HResult Next(
            uint cElt,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)]
            out IOleUndoUnit[] rgElt,
            out uint pcEltFetched);

        [PreserveSig]
        HResult Skip(uint cElt);

        void Reset();

        IEnumOleUndoUnits Clone();
    }
}
