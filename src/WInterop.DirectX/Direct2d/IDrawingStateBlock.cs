// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.DirectWrite;

namespace WInterop.Direct2d;

/// <summary>
///  Represents the drawing state of a render target: the antialiasing mode,
///  transform, tags, and text-rendering options. [ID2D1DrawingStateBlock]
/// </summary>
[Guid(InterfaceIds.IID_ID2D1DrawingStateBlock)]
public readonly unsafe struct DrawingStateBlock : DrawingStateBlock.Interface, IDisposable
{
    private readonly ID2D1DrawingStateBlock* _handle;

    internal DrawingStateBlock(ID2D1DrawingStateBlock* handle) => _handle = handle;

    public DrawingStateDescription Description
    {
        get
        {
            DrawingStateDescription description;
            _handle->GetDescription((D2D1_DRAWING_STATE_DESCRIPTION*)&description);
            return description;
        }
        set => _handle->SetDescription((D2D1_DRAWING_STATE_DESCRIPTION*)&value);
    }

    public RenderingParams TextRenderingParams
    {
        get
        {
            IDWriteRenderingParams* rendering;
            _handle->GetTextRenderingParams(&rendering);
            return new(rendering);
        }
        set => _handle->SetTextRenderingParams(value.Handle);
    }

    public void Dispose() => _handle->Release();

    public Factory GetFactory() => Resource.From(this).GetFactory();

    internal interface Interface : Resource.Interface
    {
        /// <summary>
        ///  Gets/sets the state description of this state block resource.
        /// </summary>
        DrawingStateDescription Description { get; set; }

        /// <summary>
        ///  Gets/Sets the text rendering parameters of this state block resource.
        /// </summary>
        RenderingParams TextRenderingParams { get; set; }
    }
}
