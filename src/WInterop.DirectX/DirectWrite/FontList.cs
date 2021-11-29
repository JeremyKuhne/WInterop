// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WInterop.DirectWrite;

/// <summary>
///  The <see cref="FontList"/> interface represents an ordered set of fonts that are part of an <see cref="FontCollection"/>.
///  [IDWriteFontList]
/// </summary>
[Guid(InterfaceIds.IID_IDWriteFontList)]
public readonly unsafe struct FontList : FontList.Interface, IDisposable
{
    private readonly IDWriteFontList* _handle;

    internal FontList(IDWriteFontList* handle) => _handle = handle;

    public uint FontCount => _handle->GetFontCount();

    internal static ref FontList From<TFrom>(in TFrom from)
        where TFrom : unmanaged, Interface
        => ref Unsafe.AsRef<FontList>(Unsafe.AsPointer(ref Unsafe.AsRef(from)));

    public void Dispose() => _handle->Release();

    public Font GetFont(uint index)
    {
        IDWriteFont* font;
        _handle->GetFont(index, &font).ThrowIfFailed();
        return new(font);
    }

    public FontCollection GetFontCollection()
    {
        IDWriteFontCollection* collection;
        _handle->GetFontCollection(&collection).ThrowIfFailed();
        return new(collection);
    }

    internal interface Interface
    {
        /// <summary>
        ///  Gets the font collection that contains the fonts.
        /// </summary>
        FontCollection GetFontCollection();

        /// <summary>
        ///  Gets the number of fonts in the font list.
        /// </summary>
        uint FontCount { get; }

        /// <summary>
        ///  Gets a font given its zero-based index.
        /// </summary>
        /// <param name="index">Zero-based index of the font in the font list.</param>
        Font GetFont(uint index);
    }
}
