// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    /// The device defines a resource domain whose objects and device contexts can be
    /// used together. [ID2D1Device]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Device),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IDevice : IResource
    {
        #region ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);
        #endregion

        /// <summary>
        /// Creates a new device context with no initially assigned target.
        /// </summary>
        IDeviceContext CreateDeviceContext(DeviceContextOptions options = DeviceContextOptions.None);

        /// <summary>
        /// Creates a D2D print control.
        /// </summary>
        void CreatePrintControlSTUB();
        //STDMETHOD(CreatePrintControl)(
        //    _In_ IWICImagingFactory * wicFactory,
        //    _In_ IPrintDocumentPackageTarget* documentTarget,
        //    _In_opt_ CONST D2D1_PRINT_CONTROL_PROPERTIES *printControlProperties,
        //    _COM_Outptr_ ID2D1PrintControl **printControl 
        //    ) PURE;

        /// <summary>
        /// Sets the maximum amount of texture memory to maintain before evicting caches.
        /// </summary>
        [PreserveSig]
        void SetMaximumTextureMemory(ulong maximumInBytes);

        /// <summary>
        /// Gets the maximum amount of texture memory to maintain before evicting caches.
        /// </summary>
        [PreserveSig]
        ulong GetMaximumTextureMemory();

        /// <summary>
        /// Clears all resources that are cached but not held in use by the application
        /// through an interface reference.
        /// </summary>
        [PreserveSig]
        void ClearResources(uint millisecondsSinceUse = 0); 
    }
}
