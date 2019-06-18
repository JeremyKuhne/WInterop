// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    /// This defines the valid property types that can be used in an effect property
    /// interface. [D2D1_PROPERTY_TYPE]
    /// </summary>
    public enum PropertyType : uint
    {
        Unknown = 0,
        String = 1,
        Bool = 2,
        UInt32 = 3,
        Int32 = 4,
        Float = 5,
        Vector2 = 6,
        Vector3 = 7,
        Vector4 = 8,
        Blob = 9,
        IUnknown = 10,
        Enum = 11,
        Array = 12,
        Clsid = 13,
        Matrix3x2 = 14,
        Matrix4x3 = 15,
        Matrix4x4 = 16,
        Matrix5x4 = 17,
        ColorContext = 18,
    }
}
