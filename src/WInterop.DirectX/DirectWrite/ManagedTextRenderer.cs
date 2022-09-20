// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;
using WInterop.Com;

namespace WInterop.DirectWrite;

public abstract unsafe class ManagedTextRenderer : IDisposable
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

    private static unsafe class CCW
    {
        private static readonly IDWriteTextRenderer.Vtbl<IDWriteTextRenderer>* s_vtable = AllocateVTable();

        private static IDWriteTextRenderer.Vtbl<IDWriteTextRenderer>* AllocateVTable()
        {
            // Allocate and create a singular VTable for this type projection.
            var vtable = (IDWriteTextRenderer.Vtbl<IDWriteTextRenderer>*)RuntimeHelpers.AllocateTypeAssociatedMemory(
                typeof(CCW),
                sizeof(IDWriteTextRenderer.Vtbl<IDWriteTextRenderer>));

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
            => (IDWriteTextRenderer*)Lifetime<IDWriteTextRenderer.Vtbl<IDWriteTextRenderer>, ManagedTextRenderer>
                .Allocate(renderer, s_vtable);

        private static ManagedTextRenderer? Renderer(IDWriteTextRenderer* @this)
            => Lifetime<IDWriteTextRenderer.Vtbl<IDWriteTextRenderer>, ManagedTextRenderer>.GetObject((IUnknown*)@this);

        [UnmanagedCallersOnly]
        private static int QueryInterface(IDWriteTextRenderer* @this, Guid* iid, void* ppObject)
        {
            if (*iid == typeof(IUnknown).GUID
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

            Lifetime<IDWriteTextRenderer.Vtbl<IDWriteTextRenderer>, ManagedTextRenderer>.AddRef((IUnknown*)@this);
            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static uint AddRef(IDWriteTextRenderer* @this)
            => Lifetime<IDWriteTextRenderer.Vtbl<IDWriteTextRenderer>, ManagedTextRenderer>.AddRef((IUnknown*)@this);

        [UnmanagedCallersOnly]
        private static uint Release(IDWriteTextRenderer* @this)
            => Lifetime<IDWriteTextRenderer.Vtbl<IDWriteTextRenderer>, ManagedTextRenderer>.Release((IUnknown*)@this);

        [UnmanagedCallersOnly]
        private static int IsPixelSnappingDisabled(IDWriteTextRenderer* @this, void* clientDrawingContext, BOOL* isDisabled)
        {
            *isDisabled = Renderer(@this)?.IsPixelSnappingDisabled((IntPtr)clientDrawingContext) ?? false;
            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static int GetCurrentTransform(IDWriteTextRenderer* @this, void* clientDrawingContext, DWRITE_MATRIX* transform)
        {
            Matrix3x2 matrix = Renderer(@this)?.GetCurrentTransform((IntPtr)clientDrawingContext) ?? Matrix3x2.Identity;
            *transform = *(DWRITE_MATRIX*)&matrix;
            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static unsafe int GetPixelsPerDip(IDWriteTextRenderer* @this, void* clientDrawingContext, float* pixelsPerDip)
        {
            *pixelsPerDip = Renderer(@this)?.GetPixelsPerDip((IntPtr)clientDrawingContext) ?? 1;
            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static unsafe int DrawGlyphRun(
            IDWriteTextRenderer* @this,
            void* clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            DWRITE_MEASURING_MODE measuringMode,
            DWRITE_GLYPH_RUN* glyphRun,
            DWRITE_GLYPH_RUN_DESCRIPTION* glyphRunDescription,
            IUnknown* clientDrawingEffect)
        {
            Renderer(@this)?.DrawGlyphRun(
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
            IDWriteTextRenderer* @this,
            void* clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            DWRITE_UNDERLINE* underline,
            IUnknown* clientDrawingEffect)
        {
            Renderer(@this)?.DrawUnderline(
                (IntPtr)clientDrawingContext,
                new(baselineOriginX, baselineOriginY),
                *(Underline*)underline,
                (IntPtr)clientDrawingEffect);

            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static unsafe int DrawStrikethrough(
            IDWriteTextRenderer* @this,
            void* clientDrawingContext,
            float baselineOriginX,
            float baselineOriginY,
            DWRITE_STRIKETHROUGH* strikethrough,
            IUnknown* clientDrawingEffect)
        {
            Renderer(@this)?.DrawStrikethrough(
                (IntPtr)clientDrawingContext,
                new(baselineOriginX, baselineOriginY),
                *(Strikethrough*)strikethrough,
                (IntPtr)clientDrawingEffect);

            return (int)Errors.HResult.S_OK;
        }

        [UnmanagedCallersOnly]
        private static unsafe int DrawInlineObject(
            IDWriteTextRenderer* @this,
            void* clientDrawingContext,
            float originX,
            float originY,
            IDWriteInlineObject* inlineObject,
            BOOL isSideways,
            BOOL isRightToLeft,
            IUnknown* clientDrawingEffect)
        {
            Renderer(@this)?.DrawInlineObject(
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
