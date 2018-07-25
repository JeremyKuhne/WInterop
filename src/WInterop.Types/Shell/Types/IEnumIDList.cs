// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.ErrorHandling.Types;

namespace WInterop.Shell.Types
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/shobjidl_core/nn-shobjidl_core-ienumidlist
    [ComImport,
        Guid(InterfaceIds.IID_IEnumIDList),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEnumIDList
    {
        /// <summary>
        /// Retrieves the specified number of item identifiers in the enumeration sequence and advances the current position by the number of items retrieved.
        /// </summary>
        /// <param name="celt">Number of elements to retrieve.</param>
        /// <param name="rgelt">Ids relative to the IShellFolder being enumerated.</param>
        /// <returns>
        /// <see cref="HRESULT.S_OK"/> if the requested <paramref name="celt"/> elements were returned.
        /// <see cref="HRESULT.S_FALSE"/> if less items than requested were returned.
        /// </returns>
        [PreserveSig]
        HRESULT Next(
            uint celt,
            out ItemIdList rgelt,
            out uint pceltFetched);

        void Skip(
            uint celt);

        void Reset();

        IEnumIDList Clone();
    }
}
