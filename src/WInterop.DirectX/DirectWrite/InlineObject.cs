// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using TerraFX.Interop.Windows;
using WInterop.Direct2d;

namespace WInterop.DirectWrite
{
    [Guid(InterfaceIds.IID_IDWriteInlineObject)]
    public readonly unsafe struct InlineObject : InlineObject.Interface, IDisposable
    {
        internal IDWriteInlineObject* Handle { get; }

        internal InlineObject(IDWriteInlineObject* handle) => Handle = handle;

        public void Dispose() => Handle->Release();

        public void Draw(
            IntPtr clientDrawingContext,
            TextRenderer renderer,
            PointF origin,
            bool isSideways,
            bool isRightToLeft,
            IntPtr clientDrawingEffect)
        {
            Handle->Draw(
                (void*)clientDrawingContext,
                renderer.Handle,
                origin.X,
                origin.Y,
                isSideways,
                isRightToLeft,
                (IUnknown*)(void*)clientDrawingEffect).ThrowIfFailed();
        }

        public void GetBreakConditions(out BreakCondition breakConditionBefore, out BreakCondition breakConditionAfter)
        {
            BreakCondition before;
            BreakCondition after;
            Handle->GetBreakConditions((DWRITE_BREAK_CONDITION*)&before, (DWRITE_BREAK_CONDITION*)&after);
            breakConditionBefore = before;
            breakConditionAfter = after;
        }

        public InlineObjectMetrics GetMetrics()
        {
            InlineObjectMetrics metrics;
            Handle->GetMetrics((DWRITE_INLINE_OBJECT_METRICS*)&metrics);
            return metrics;
        }

        public OverhangMetrics GetOverhangMetrics()
        {
            OverhangMetrics metrics;
            Handle->GetOverhangMetrics((DWRITE_OVERHANG_METRICS*)&metrics);
            return metrics;
        }

        internal interface Interface
        {
            /// <summary>
            ///  The application implemented rendering callback (IDWriteTextRenderer::DrawInlineObject)
            ///  can use this to draw the inline object without needing to cast or query the object
            ///  type. The text layout does not call this method directly.
            /// </summary>
            /// <param name="clientDrawingContext">The context passed to IDWriteTextLayout::Draw.</param>
            /// <param name="renderer">The renderer passed to IDWriteTextLayout::Draw as the object's containing parent.</param>
            /// <param name="origin">Top-left corner of the inline object.</param>
            /// <param name="isSideways">The object should be drawn on its side.</param>
            /// <param name="isRightToLeft">The object is in an right-to-left context and should be drawn flipped.</param>
            /// <param name="clientDrawingEffect">The drawing effect set in IDWriteTextLayout::SetDrawingEffect.</param>
            void Draw(
                IntPtr clientDrawingContext,
                TextRenderer renderer,
                PointF origin,
                bool isSideways,
                bool isRightToLeft,
                IntPtr clientDrawingEffect);

            /// <summary>
            ///  TextLayout calls this callback function to get the measurement of the inline object.
            /// </summary>
            InlineObjectMetrics GetMetrics();

            /// <summary>
            ///  TextLayout calls this callback function to get the visible extents (in DIPs) of the inline object.
            ///  In the case of a simple bitmap, with no padding and no overhang, all the overhangs will simply be zeroes.
            /// </summary>
            /// <returns>Overshoot of visible extents (in DIPs) outside the object.</returns>
            /// <remarks>
            ///  The overhangs should be returned relative to the reported size of the object (DWRITE_INLINE_OBJECT_METRICS
            ///  ::width/height), and should not be baseline adjusted. If you have an image that is actually 100x100 DIPs,
            ///  but you want it slightly inset (perhaps it has a glow) by 20 DIPs on each side, you would return a
            ///  width/height of 60x60 and four overhangs of 20 DIPs.
            /// </remarks>
            OverhangMetrics GetOverhangMetrics();

            /// <summary>
            ///  Layout uses this to determine the line breaking behavior of the inline object amidst the text.
            /// </summary>
            /// <param name="breakConditionBefore">
            ///  Line-breaking condition between the object and the content immediately preceding it.
            /// </param>
            /// <param name="breakConditionAfter">
            ///  Line-breaking condition between the object and the content immediately following it.
            /// </param>
            void GetBreakConditions(
                out BreakCondition breakConditionBefore,
                out BreakCondition breakConditionAfter);
        }
    }
}
