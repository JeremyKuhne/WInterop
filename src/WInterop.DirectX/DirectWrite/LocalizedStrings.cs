// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.DirectWrite;

/// <summary>
///  Represents a collection of strings indexed by locale name. [IDWriteLocalizedStrings]
/// </summary>
[Guid(InterfaceIds.IID_IDWriteLocalizedStrings)]
public readonly unsafe struct LocalizedStrings : LocalizedStrings.Interface, IDisposable
{
    private readonly IDWriteLocalizedStrings* _handle;

    internal LocalizedStrings(IDWriteLocalizedStrings* handle) => _handle = handle;

    public uint Count => throw new NotImplementedException();

    public void Dispose()
    {
        if (_handle is not null)
        {
            _handle->Release();
        }
    }

    public bool FindLocaleName(string localeName, out uint index)
    {
        TerraFX.Interop.Windows.BOOL exists;
        fixed (void* l = localeName)
        fixed (uint* i = &index)
        {
            _handle->FindLocaleName((ushort*)l, i, &exists).ThrowIfFailed();
        }

        return exists;
    }

    public string GetLocaleName(uint index)
    {
        uint length;
        _handle->GetLocaleNameLength(index, &length).ThrowIfFailed();
        Span<char> name = stackalloc char[(int)length + 1];
        fixed (void* n = name)
        {
            _handle->GetLocaleName(index, (ushort*)n, length + 1);
        }

        return name[..(int)length].ToString();
    }

    public string GetString(uint index)
    {
        uint length;
        _handle->GetStringLength(index, &length).ThrowIfFailed();
        Span<char> name = stackalloc char[(int)length + 1];
        fixed (void* n = name)
        {
            _handle->GetString(index, (ushort*)n, length + 1);
        }

        return name[..(int)length].ToString();
    }

    internal interface Interface
    {
        /// <summary>
        ///  Gets the number of language/string pairs.
        /// </summary>
        uint Count { get; }

        /// <summary>
        ///  Gets the index of the item with the specified locale name.
        /// </summary>
        /// <param name="localeName">Locale name to look for.</param>
        /// <param name="index">Receives the zero-based index of the locale name/string pair.</param>
        /// <returns><see langword="true"/> if the locale name exists or <see langword="false"/> if not.</returns>
        bool FindLocaleName(
            string localeName,
            out uint index);

        /// <summary>
        ///  Gets the locale name with the specified index.
        /// </summary>
        /// <param name="index">Zero-based index of the locale name.</param>
        string GetLocaleName(
            uint index);

        /// <summary>
        ///  Gets the string with the specified index.
        /// </summary>
        /// <param name="index">Zero-based index of the string.</param>
        string GetString(
            uint index);
    }
}
