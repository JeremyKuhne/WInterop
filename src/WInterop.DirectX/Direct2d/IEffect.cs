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
    /// The effect interface. Properties control how the effect is rendered. The effect
    /// is Drawn with the DrawImage call. [ID2D1Effect]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Effect),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IEffect : IProperties
    {
        #region ID2D1Properties
        /// <summary>
        /// Returns the total number of custom properties in this interface.
        /// </summary>
        [PreserveSig]
        new uint GetPropertyCount();

        /// <summary>
        /// Retrieves the property name from the given property index.
        /// </summary>
        [PreserveSig]
        new unsafe HRESULT GetPropertyName(
            uint index,
            char* name,
            uint nameCount);

        /// <summary>
        /// Returns the length of the property name from the given index.
        /// </summary>
        [PreserveSig]
        new uint GetPropertyNameLength(uint index);

        /// <summary>
        /// Retrieves the type of the given property.
        /// </summary>
        [PreserveSig]
        new PropertyType GetType(uint index);

        /// <summary>
        /// Retrieves the property index for the given property name.
        /// </summary>
        [PreserveSig]
        new uint GetPropertyIndex(
            [MarshalAs(UnmanagedType.LPWStr)]
            string name);

        /// <summary>
        /// Sets the value of the given property using its name.
        /// </summary>
        new unsafe void SetValueByName(
            [MarshalAs(UnmanagedType.LPWStr)]
            string name,
            PropertyType type,
            byte* data,
            uint dataSize);

        /// <summary>
        /// Sets the given value using the property index.
        /// </summary>
        new unsafe void SetValue(
            uint index,
            PropertyType type,
            byte* data,
            uint dataSize);

        /// <summary>
        /// Retrieves the given property or sub-property by name. '.' is the delimiter for
        /// sub-properties.
        /// </summary>
        new unsafe void GetValueByName(
            [MarshalAs(UnmanagedType.LPWStr)]
            string name,
            PropertyType type,
            byte* data,
            uint dataSize);

        /// <summary>
        /// Retrieves the given value by index.
        /// </summary>
        new unsafe void GetValue(
            uint index,
            PropertyType type,
            byte* data,
            uint dataSize);

        /// <summary>
        /// Returns the value size for the given property index.
        /// </summary>
        [PreserveSig]
        new uint GetValueSize(uint index);

        /// <summary>
        /// Retrieves the sub-properties of the given property by index.
        /// </summary>
        new IProperties GetSubProperties(uint index);
        #endregion

        /// <summary>
        /// Sets the input to the given effect. The input can be a concrete bitmap or the
        /// output of another effect.
        /// </summary>
        [PreserveSig]
        void SetInput(
            uint index,
            IImage input,
            BOOL invalidate);

        /// <summary>
        /// If the effect supports a variable number of inputs, this sets the number of
        /// input that are currently active on the effect.
        /// </summary>
        void SetInputCount(uint inputCount);

        /// <summary>
        /// Returns the input image to the effect. The input could be another effect or a
        /// bitmap.
        /// </summary>
        [PreserveSig]
        void GetInput(
            uint index,
            out IImage input);

        /// <summary>
        /// This returns the number of input that are bound into this effect.
        /// </summary>
        [PreserveSig]
        uint GetInputCount();

        /// <summary>
        /// Returns the output image of the given effect. This can be set as the input to
        /// another effect or can be drawn with DrawImage.
        /// </summary>
        [PreserveSig]
        void GetOutput(out IImage outputImage);
    }
}
