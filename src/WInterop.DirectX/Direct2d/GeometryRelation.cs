// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Direct2d
{
    /// <summary>
    ///  Describes how one geometry object is spatially related to another geometry
    ///  object.
    /// </summary>
    public enum GeometryRelation : uint
    {
        /// <summary>
        ///  The relation between the geometries couldn't be determined. This value is never
        ///  returned by any D2D method. [D2D1_GEOMETRY_RELATION_UNKNOWN]
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///  The two geometries do not intersect at all. [D2D1_GEOMETRY_RELATION_DISJOINT]
        /// </summary>
        Disjoint = 1,

        /// <summary>
        ///  The passed in geometry is entirely contained by the object.
        ///  [D2D1_GEOMETRY_RELATION_IS_CONTAINED]
        /// </summary>
        IsContained = 2,

        /// <summary>
        ///  The object entirely contains the passed in geometry.
        ///  [D2D1_GEOMETRY_RELATION_CONTAINS]
        /// </summary>
        Contains = 3,

        /// <summary>
        ///  The two geometries overlap but neither completely contains the other.
        ///  [D2D1_GEOMETRY_RELATION_OVERLAP]
        /// </summary>
        Overlap = 4
    }
}
