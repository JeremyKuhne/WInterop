// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using WInterop.Com.Native;

namespace WInterop.DirectWrite;

public unsafe abstract class ManagedTextRenderer : IDisposable
{
    private readonly IDWriteTextRenderer* _ccw;
    private bool _disposedValue;

    public ManagedTextRenderer()
    {
        _ccw = CCW.CreateInstance(this);
    }

    public virtual bool IsPixelSnappingDisabled(IntPtr clientDrawingContext) => false;

    public virtual Matrix3x2 GetCurrentTransform(IntPtr clientDrawingContext) => Matrix3x2.Identity;

    public virtual float GetPixelsPerDip(IntPtr clientDrawingContext) => 1.0f;


    public virtual void DrawGlyphRun(
        IntPtr clientDrawingContext,
        PointF baselineOrigin,
        MeasuringMode measuringMode,
        GlyphRun glyphRun,
        GlyphRunDescription glyphRunDescription,
        IntPtr clientDrawingEffect)
    {
    }

    public virtual void DrawUnderline(
        IntPtr clientDrawingContext,
        PointF baselineOrigin,
        Underline underline,
        IntPtr clientDrawingEffect)
    {
    }

    public virtual void DrawStrikethrough(
        IntPtr clientDrawingContext,
        PointF baselineOrigin,
        Strikethrough strikethrough,
        IntPtr clientDrawingEffect)
    {
    }

    public virtual void DrawInlineObject(
        IntPtr clientDrawingContext,
        PointF origin,
        InlineObject inlineObject,
        bool isSideways,
        bool isRightToLeft,
        IntPtr clientDrawingEffect)
    {
    }

    public static implicit operator TextRenderer(ManagedTextRenderer renderer) => new(renderer._ccw);

    private unsafe static class CCW
    {
        private static readonly IDWriteTextRenderer.Vtbl* s_vtable = AllocateVTable();

        private static IDWriteTextRenderer.Vtbl* AllocateVTable()
        {
            // Allocate and create a singular VTable for this type projection.
            var vtable = (IDWriteTextRenderer.Vtbl*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(CCW), sizeof(IDWriteTextRenderer.Vtbl));

            // IUnknown
            vtable->QueryInterface = &QueryInterface;
            vtable->AddRef = &AddRef;
            vtable->Release = &Release;
            vtable->GetPixelsPerDip = &GetPixelsPerDip;
            vtable->GetCurrentTransform = &GetCurrentTransform;
            vtable->IsPixelSnappingDisabled = &IsPixelSnappingDisabled;
            vtable->DrawGlyphRun = &DrawGlyphRun;
            vtable->DrawInlineObject = &DrawInlineObject;
            vtable->DrawStrikethrough = &DrawStrikethrough;
            vtable->DrawUnderline = &DrawUnderline;
            return vtable;
        }

        public static IDWriteTextRenderer* CreateInstance(ManagedTextRenderer renderer)
            => (IDWriteTextRenderer*)Lifetime<IDWriteTextRenderer.Vtbl, ManagedTextRenderer>.Allocate(renderer, s_vtable);

        [UnmanagedCallersOnly]
        private static int QueryInterface(void* @this, Guid* iid, void* ppObject)
        {
            if (*iid == Com.Native.IUnknown.IID_IUnknown
                || *iid == typeof(PixelSnapping).GUID
                || *iid == typeof(TextRenderer).GUID)
            {
                ppObject = @this;
            }
            else
            {
                ppObject = null;
                return (int)Errors.HResult.E_NOINTERFACE;
            }

            Lifetime<IDWriteTextRenderer.Vtbl, ManagedTextRenderer>.AddRef(@this);
            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static uint AddRef(void* @this) => Lifetime<IDWriteTextRenderer.Vtbl, ManagedTextRenderer>.AddRef(@this);

        [UnmanagedCallersOnly]
        private static uint Release(void* @this) => Lifetime<IDWriteTextRenderer.Vtbl, ManagedTextRenderer>.Release(@this);

        [UnmanagedCallersOnly]
        private static int IsPixelSnappingDisabled(void* @this, void* clientDrawingContext, TerraFX.Interop.Windows.BOOL* isDisabled)
        {
            *isDisabled = Lifetime<IDWriteTextRenderer.Vtbl, ManagedTextRenderer>.GetObject(@this)
                ?.IsPixelSnappingDisabled((IntPtr)clientDrawingContext) ?? false;
            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static int GetCurrentTransform(void* @this, void* clientDrawingContext, DWRITE_MATRIX* transform)
        {
            Matrix3x2 matrix = Lifetime<IDWriteTextRenderer.Vtbl, ManagedTextRenderer>.GetObject(@this)
                ?.GetCurrentTransform((IntPtr)clientDrawingContext) ?? Matrix3x2.Identity;
            *transform = *(DWRITE_MATRIX*)&matrix;
            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static unsafe int GetPixelsPerDip(void* @this, void* clientDrawingContext, float* pixelsPerDip)
        {
            *pixelsPerDip = Lifetime<IDWriteTextRenderer.Vtbl, ManagedTextRenderer>.GetObject(@this)
                ?.GetPixelsPerDip((IntPtr)clientDrawingContext) ?? 1;
            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static unsafe int DrawGlyphRun(
            void* @this,
            void* clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            DWRITE_MEASURING_MODE measuringMode,
            DWRITE_GLYPH_RUN* glyphRun,
            DWRITE_GLYPH_RUN_DESCRIPTION* glyphRunDescription,
            TerraFX.Interop.Windows.IUnknown* clientDrawingEffect)
        {
            Lifetime<IDWriteTextRenderer.Vtbl, ManagedTextRenderer>.GetObject(@this)?.DrawGlyphRun(
                (IntPtr)clientDrawingContext,
                new(baselineOriginX, baselineOriginY),
                (MeasuringMode)measuringMode,
                *(GlyphRun*)glyphRun,
                *(GlyphRunDescription*)glyphRunDescription,
                (IntPtr)clientDrawingEffect);

            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static unsafe int DrawUnderline(
            void* @this,
            void* clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            DWRITE_UNDERLINE* underline,
            TerraFX.Interop.Windows.IUnknown* clientDrawingEffect)
        {
            Lifetime<IDWriteTextRenderer.Vtbl, ManagedTextRenderer>.GetObject(@this)?.DrawUnderline(
                (IntPtr)clientDrawingContext,
                new(baselineOriginX, baselineOriginY),
                *(Underline*)underline,
                (IntPtr)clientDrawingEffect);

            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static unsafe int DrawStrikethrough(
            void* @this,
            void* clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            DWRITE_STRIKETHROUGH* strikethrough,
            TerraFX.Interop.Windows.IUnknown* clientDrawingEffect)
        {
            Lifetime<IDWriteTextRenderer.Vtbl, ManagedTextRenderer>.GetObject(@this)?.DrawStrikethrough(
                (IntPtr)clientDrawingContext,
                new(baselineOriginX, baselineOriginY),
                *(Strikethrough*)strikethrough,
                (IntPtr)clientDrawingEffect);

            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static unsafe int DrawInlineObject(
            void* @this,
            void* clientDrawingContext,
            float originX,
            float originY,
            IDWriteInlineObject* inlineObject,
            TerraFX.Interop.Windows.BOOL isSideways,
            TerraFX.Interop.Windows.BOOL isRightToLeft,
            TerraFX.Interop.Windows.IUnknown* clientDrawingEffect)
        {
            Lifetime<IDWriteTextRenderer.Vtbl, ManagedTextRenderer>.GetObject(@this)?.DrawInlineObject(
                (IntPtr)clientDrawingContext,
                new(originX, originY),
                new(inlineObject),
                isSideways,
                isRightToLeft,
                (IntPtr)clientDrawingEffect);

            return (int)Errors.HResult.S_OK;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            _ccw->Release();
            _disposedValue = true;
        }
    }

    ~ManagedTextRenderer()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
