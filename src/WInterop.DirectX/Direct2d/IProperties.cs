// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Direct2d
{
    /// <summary>
    /// Represents a set of run-time bindable and discoverable properties that allow a
    /// data-driven application to modify the state of a Direct2D effect. [ID2D1Properties]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Properties),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IProperties
    {
        /// <summary>
        /// Returns the total number of custom properties in this interface.
        /// </summary>
        [PreserveSig]
        uint GetPropertyCount();

        /// <summary>
        /// Retrieves the property name from the given property index.
        /// </summary>
        [PreserveSig]
        unsafe HRESULT GetPropertyName(
            uint index,
            char* name,
            uint nameCount);

        /// <summary>
        /// Returns the length of the property name from the given index.
        /// </summary>
        [PreserveSig]
        uint GetPropertyNameLength(uint index);

        /// <summary>
        /// Retrieves the type of the given property.
        /// </summary>
        [PreserveSig]
        PropertyType GetType(uint index);

        /// <summary>
        /// Retrieves the property index for the given property name.
        /// </summary>
        [PreserveSig]
        uint GetPropertyIndex(
            [MarshalAs(UnmanagedType.LPWStr)]
            string name);

        /// <summary>
        /// Sets the value of the given property using its name.
        /// </summary>
        unsafe void SetValueByName(
            [MarshalAs(UnmanagedType.LPWStr)]
            string name,
            PropertyType type,
            byte* data,
            uint dataSize);

        /// <summary>
        /// Sets the given value using the property index.
        /// </summary>
        unsafe void SetValue(
            uint index,
            PropertyType type,
            byte* data,
            uint dataSize);

        /// <summary>
        /// Retrieves the given property or sub-property by name. '.' is the delimiter for
        /// sub-properties.
        /// </summary>
        unsafe void GetValueByName(
            [MarshalAs(UnmanagedType.LPWStr)]
            string name,
            PropertyType type,
            byte* data,
            uint dataSize);

        /// <summary>
        /// Retrieves the given value by index.
        /// </summary>
        unsafe void GetValue(
            uint index,
            PropertyType type,
            byte* data,
            uint dataSize);

        /// <summary>
        /// Returns the value size for the given property index.
        /// </summary>
        [PreserveSig]
        uint GetValueSize(uint index);

        /// <summary>
        /// Retrieves the sub-properties of the given property by index.
        /// </summary>
        IProperties GetSubProperties(uint index);
    }
}
