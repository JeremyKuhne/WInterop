// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WInterop.Direct2d
{
    /// <summary>
    /// [ID2D1Geometry]
    /// </summary>
    [ComImport,
        Guid(InterfaceIds.IID_ID2D1Geometry),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public unsafe interface IGeometry : IResource
    {
        // ID2D1Resource
        [PreserveSig]
        new void GetFactory(
            out IFactory factory);

        /// <summary>
        /// Retrieve the bounds of the geometry, with an optional applied transform.
        /// </summary>
        LtrbRectangleF GetBounds(
            Matrix3x2* worldTransform);

        /// <summary>
        /// Get the bounds of the corresponding geometry after it has been widened or have
        /// an optional pen style applied.
        /// </summary>
        LtrbRectangleF GetWidenedBounds(
            float strokeWidth,
            IStrokeStyle strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        /// Checks to see whether the corresponding penned and widened geometry contains the
        /// given point.
        /// </summary>
        BOOL StrokeContainsPoint(
            PointF point,
            float strokeWidth,
            IStrokeStyle strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        /// Test whether the given fill of this geometry would contain this point.
        /// </summary>
        BOOL FillContainsPoint(
            PointF point,
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        /// Compare how one geometry intersects or contains another geometry.
        /// </summary>
        GeometryRelation CompareWithGeometry(
            IGeometry inputGeometry,
            Matrix3x2* inputGeometryTransform,
            float flatteningTolerance);

        /// <summary>
        /// Converts a geometry to a simplified geometry that has arcs and quadratic beziers
        /// removed.
        /// </summary>
        void Simplify(
            GeometrySimplificationOption simplificationOption,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);

        /// <summary>
        /// Tessellates a geometry into triangles.
        /// </summary>
        void Tessellate(
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ITesselationSink tessellationSink);

        /// <summary>
        /// Performs a combine operation between the two geometries to produce a resulting
        /// geometry.
        /// </summary>
        void CombineWithGeometry(
            IGeometry inputGeometry,
            CombineMode combineMode,
            Matrix3x2* inputGeometryTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);

        /// <summary>
        /// Computes the outline of the geometry. The result is written back into a
        /// simplified geometry sink.
        /// </summary>
        void Outline(
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);

        /// <summary>
        /// Computes the area of the geometry.
        /// </summary>
        float ComputeArea(
            Matrix3x2* worldTransform,
            float flatteningTolerance);

        /// <summary>
        /// Computes the length of the geometry.
        /// </summary>
        float ComputeLength(
            Matrix3x2 worldTransform,
            float flatteningTolerance);

        /// <summary>
        /// Computes the point and tangent a given distance along the path.
        /// </summary>
        void ComputePointAtLength(
            float length,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            PointF* point,
            PointF* unitTangentVector);

        /// <summary>
        /// Get the geometry and widen it as well as apply an optional pen style.
        /// </summary>
        void Widen(
            float strokeWidth,
            IStrokeStyle strokeStyle,
            Matrix3x2* worldTransform,
            float flatteningTolerance,
            ISimplifiedGeometrySink geometrySink);
    }


    public static class GeometryExtensions
    {
        /// <summary>
        /// Retrieve the bounds of the geometry.
        /// </summary>
        public static unsafe RectangleF GetBounds(this IGeometry geometry) => geometry.GetBounds(null);

        /// <summary>
        /// Retrieve the bounds of the geometry with a transform.
        /// </summary>
        public static unsafe RectangleF GetBounds(this IGeometry geometry, Matrix3x2 worldTransform)
            => geometry.GetBounds(&worldTransform);
/*
        /// <summary>
        /// Get the bounds of the corresponding geometry after it has been widened or have
        /// an optional pen style applied.
        /// </summary>
        COM_DECLSPEC_NOTHROW
        HRESULT
    GetWidenedBounds(
        FLOAT strokeWidth,
        _In_opt_ ID2D1StrokeStyle *strokeStyle,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        FLOAT flatteningTolerance,
        _Out_ D2D1_RECT_F *bounds
        ) CONST  
    {
        return GetWidenedBounds(strokeWidth, strokeStyle, &worldTransform, flatteningTolerance, bounds);
}

/// <summary>
/// Get the bounds of the corresponding geometry after it has been widened or have
/// an optional pen style applied.
/// </summary>
COM_DECLSPEC_NOTHROW
HRESULT
    GetWidenedBounds(
        FLOAT strokeWidth,
        _In_opt_ ID2D1StrokeStyle *strokeStyle,
        _In_opt_ CONST D2D1_MATRIX_3X2_F* worldTransform,
        _Out_ D2D1_RECT_F *bounds
        ) CONST  
    {
        return GetWidenedBounds(strokeWidth, strokeStyle, worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, bounds);
    }
    
    /// <summary>
    /// Get the bounds of the corresponding geometry after it has been widened or have
    /// an optional pen style applied.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    GetWidenedBounds(
        FLOAT strokeWidth,
        _In_opt_ ID2D1StrokeStyle *strokeStyle,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        _Out_ D2D1_RECT_F *bounds
        ) CONST  
    {
        return GetWidenedBounds(strokeWidth, strokeStyle, &worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, bounds);
    }
    
    COM_DECLSPEC_NOTHROW
    HRESULT
    StrokeContainsPoint(
        D2D1_POINT_2F point,
        FLOAT strokeWidth,
        _In_opt_ ID2D1StrokeStyle *strokeStyle,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        FLOAT flatteningTolerance,
        _Out_ BOOL *contains
        ) CONST  
    {
        return StrokeContainsPoint(point, strokeWidth, strokeStyle, &worldTransform, flatteningTolerance, contains);
    }
    
    /// <summary>
    /// Checks to see whether the corresponding penned and widened geometry contains the
    /// given point.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    StrokeContainsPoint(
        D2D1_POINT_2F point,
        FLOAT strokeWidth,
        _In_opt_ ID2D1StrokeStyle *strokeStyle,
        _In_opt_ CONST D2D1_MATRIX_3X2_F* worldTransform,
        _Out_ BOOL *contains
        ) CONST  
    {
        return StrokeContainsPoint(point, strokeWidth, strokeStyle, worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, contains);
    }
    
    COM_DECLSPEC_NOTHROW
    HRESULT
    StrokeContainsPoint(
        D2D1_POINT_2F point,
        FLOAT strokeWidth,
        _In_opt_ ID2D1StrokeStyle *strokeStyle,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        _Out_ BOOL *contains
        ) CONST  
    {
        return StrokeContainsPoint(point, strokeWidth, strokeStyle, &worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, contains);
    }
    
    COM_DECLSPEC_NOTHROW
    HRESULT
    FillContainsPoint(
        D2D1_POINT_2F point,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        FLOAT flatteningTolerance,
        _Out_ BOOL *contains
        ) CONST  
    {
        return FillContainsPoint(point, &worldTransform, flatteningTolerance, contains);
    }
    
    /// <summary>
    /// Test whether the given fill of this geometry would contain this point.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    FillContainsPoint(
        D2D1_POINT_2F point,
        _In_opt_ CONST D2D1_MATRIX_3X2_F* worldTransform,
        _Out_ BOOL *contains
        ) CONST  
    {
        return FillContainsPoint(point, worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, contains);
    }
    
    COM_DECLSPEC_NOTHROW
    HRESULT
    FillContainsPoint(
        D2D1_POINT_2F point,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        _Out_ BOOL *contains
        ) CONST  
    {
        return FillContainsPoint(point, &worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, contains);
    }
    
    /// <summary>
    /// Compare how one geometry intersects or contains another geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    CompareWithGeometry(
        _In_ ID2D1Geometry *inputGeometry,
        CONST D2D1_MATRIX_3X2_F &inputGeometryTransform,
        FLOAT flatteningTolerance,
        _Out_ D2D1_GEOMETRY_RELATION *relation
        ) CONST  
    {
        return CompareWithGeometry(inputGeometry, &inputGeometryTransform, flatteningTolerance, relation);
    }
    
    /// <summary>
    /// Compare how one geometry intersects or contains another geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    CompareWithGeometry(
        _In_ ID2D1Geometry *inputGeometry,
        _In_opt_ CONST D2D1_MATRIX_3X2_F* inputGeometryTransform,
        _Out_ D2D1_GEOMETRY_RELATION *relation
        ) CONST  
    {
        return CompareWithGeometry(inputGeometry, inputGeometryTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, relation);
    }
    
    /// <summary>
    /// Compare how one geometry intersects or contains another geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    CompareWithGeometry(
        _In_ ID2D1Geometry *inputGeometry,
        CONST D2D1_MATRIX_3X2_F &inputGeometryTransform,
        _Out_ D2D1_GEOMETRY_RELATION *relation
        ) CONST  
    {
        return CompareWithGeometry(inputGeometry, &inputGeometryTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, relation);
    }
    
    /// <summary>
    /// Converts a geometry to a simplified geometry that has arcs and quadratic beziers
    /// removed.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Simplify(
        D2D1_GEOMETRY_SIMPLIFICATION_OPTION simplificationOption,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        FLOAT flatteningTolerance,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return Simplify(simplificationOption, &worldTransform, flatteningTolerance, geometrySink);
    }
    
    /// <summary>
    /// Converts a geometry to a simplified geometry that has arcs and quadratic beziers
    /// removed.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Simplify(
        D2D1_GEOMETRY_SIMPLIFICATION_OPTION simplificationOption,
        _In_opt_ CONST D2D1_MATRIX_3X2_F* worldTransform,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return Simplify(simplificationOption, worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, geometrySink);
    }
    
    /// <summary>
    /// Converts a geometry to a simplified geometry that has arcs and quadratic beziers
    /// removed.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Simplify(
        D2D1_GEOMETRY_SIMPLIFICATION_OPTION simplificationOption,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return Simplify(simplificationOption, &worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, geometrySink);
    }
    
    /// <summary>
    /// Tessellates a geometry into triangles.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Tessellate(
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        FLOAT flatteningTolerance,
        _In_ ID2D1TessellationSink *tessellationSink
        ) CONST  
    {
        return Tessellate(&worldTransform, flatteningTolerance, tessellationSink);
    }
    
    /// <summary>
    /// Tessellates a geometry into triangles.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Tessellate(
        _In_opt_ CONST D2D1_MATRIX_3X2_F* worldTransform,
        _In_ ID2D1TessellationSink *tessellationSink
        ) CONST  
    {
        return Tessellate(worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, tessellationSink);
    }
    
    /// <summary>
    /// Tessellates a geometry into triangles.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Tessellate(
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        _In_ ID2D1TessellationSink *tessellationSink
        ) CONST  
    {
        return Tessellate(&worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, tessellationSink);
    }
    
    /// <summary>
    /// Performs a combine operation between the two geometries to produce a resulting
    /// geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    CombineWithGeometry(
        _In_ ID2D1Geometry *inputGeometry,
        D2D1_COMBINE_MODE combineMode,
        CONST D2D1_MATRIX_3X2_F &inputGeometryTransform,
        FLOAT flatteningTolerance,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return CombineWithGeometry(inputGeometry, combineMode, &inputGeometryTransform, flatteningTolerance, geometrySink);
    }
    
    /// <summary>
    /// Performs a combine operation between the two geometries to produce a resulting
    /// geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    CombineWithGeometry(
        _In_ ID2D1Geometry *inputGeometry,
        D2D1_COMBINE_MODE combineMode,
        _In_opt_ CONST D2D1_MATRIX_3X2_F* inputGeometryTransform,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return CombineWithGeometry(inputGeometry, combineMode, inputGeometryTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, geometrySink);
    }
    
    /// <summary>
    /// Performs a combine operation between the two geometries to produce a resulting
    /// geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    CombineWithGeometry(
        _In_ ID2D1Geometry *inputGeometry,
        D2D1_COMBINE_MODE combineMode,
        CONST D2D1_MATRIX_3X2_F &inputGeometryTransform,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return CombineWithGeometry(inputGeometry, combineMode, &inputGeometryTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, geometrySink);
    }
    
    /// <summary>
    /// Computes the outline of the geometry. The result is written back into a
    /// simplified geometry sink.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Outline(
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        FLOAT flatteningTolerance,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return Outline(&worldTransform, flatteningTolerance, geometrySink);
    }
    
    /// <summary>
    /// Computes the outline of the geometry. The result is written back into a
    /// simplified geometry sink.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Outline(
        _In_opt_ CONST D2D1_MATRIX_3X2_F* worldTransform,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return Outline(worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, geometrySink);
    }
    
    /// <summary>
    /// Computes the outline of the geometry. The result is written back into a
    /// simplified geometry sink.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Outline(
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return Outline(&worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, geometrySink);
    }
    
    /// <summary>
    /// Computes the area of the geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    ComputeArea(
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        FLOAT flatteningTolerance,
        _Out_ FLOAT *area
        ) CONST  
    {
        return ComputeArea(&worldTransform, flatteningTolerance, area);
    }
    
    /// <summary>
    /// Computes the area of the geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    ComputeArea(
        _In_opt_ CONST D2D1_MATRIX_3X2_F* worldTransform,
        _Out_ FLOAT *area
        ) CONST  
    {
        return ComputeArea(worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, area);
    }
    
    /// <summary>
    /// Computes the area of the geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    ComputeArea(
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        _Out_ FLOAT *area
        ) CONST  
    {
        return ComputeArea(&worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, area);
    }
    
    /// <summary>
    /// Computes the length of the geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    ComputeLength(
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        FLOAT flatteningTolerance,
        _Out_ FLOAT *length
        ) CONST  
    {
        return ComputeLength(&worldTransform, flatteningTolerance, length);
    }
    
    /// <summary>
    /// Computes the length of the geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    ComputeLength(
        _In_opt_ CONST D2D1_MATRIX_3X2_F* worldTransform,
        _Out_ FLOAT *length
        ) CONST  
    {
        return ComputeLength(worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, length);
    }
    
    /// <summary>
    /// Computes the length of the geometry.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    ComputeLength(
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        _Out_ FLOAT *length
        ) CONST  
    {
        return ComputeLength(&worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, length);
    }
    
    /// <summary>
    /// Computes the point and tangent a given distance along the path.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    ComputePointAtLength(
        FLOAT length,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        FLOAT flatteningTolerance,
        _Out_opt_ D2D1_POINT_2F *point,
        _Out_opt_ D2D1_POINT_2F *unitTangentVector
        ) CONST  
    {
        return ComputePointAtLength(length, &worldTransform, flatteningTolerance, point, unitTangentVector);
    }
    
    /// <summary>
    /// Computes the point and tangent a given distance along the path.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    ComputePointAtLength(
        FLOAT length,
        _In_opt_ CONST D2D1_MATRIX_3X2_F* worldTransform,
        _Out_opt_ D2D1_POINT_2F *point,
        _Out_opt_ D2D1_POINT_2F *unitTangentVector
        ) CONST  
    {
        return ComputePointAtLength(length, worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, point, unitTangentVector);
    }
    
    /// <summary>
    /// Computes the point and tangent a given distance along the path.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    ComputePointAtLength(
        FLOAT length,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        _Out_opt_ D2D1_POINT_2F *point,
        _Out_opt_ D2D1_POINT_2F *unitTangentVector
        ) CONST  
    {
        return ComputePointAtLength(length, &worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, point, unitTangentVector);
    }
    
    /// <summary>
    /// Get the geometry and widen it as well as apply an optional pen style.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Widen(
        FLOAT strokeWidth,
        _In_opt_ ID2D1StrokeStyle *strokeStyle,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        FLOAT flatteningTolerance,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return Widen(strokeWidth, strokeStyle, &worldTransform, flatteningTolerance, geometrySink);
    }
    
    /// <summary>
    /// Get the geometry and widen it as well as apply an optional pen style.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Widen(
        FLOAT strokeWidth,
        _In_opt_ ID2D1StrokeStyle *strokeStyle,
        _In_opt_ CONST D2D1_MATRIX_3X2_F* worldTransform,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return Widen(strokeWidth, strokeStyle, worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, geometrySink);
    }
    
    /// <summary>
    /// Get the geometry and widen it as well as apply an optional pen style.
    /// </summary>
    COM_DECLSPEC_NOTHROW
    HRESULT
    Widen(
        FLOAT strokeWidth,
        _In_opt_ ID2D1StrokeStyle *strokeStyle,
        CONST D2D1_MATRIX_3X2_F &worldTransform,
        _In_ ID2D1SimplifiedGeometrySink *geometrySink
        ) CONST  
    {
        return Widen(strokeWidth, strokeStyle, &worldTransform, D2D1_DEFAULT_FLATTENING_TOLERANCE, geometrySink);
    }
*/
    }
}
