// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics.CodeAnalysis;

namespace WInterop.Storage.Native;

/// <summary>
///  Used to enumerate streams for a file.
/// </summary>
/// <remarks>
///  Equivalent to <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364406.aspx">FILE_STREAM_INFO</a> structure.
/// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff540364.aspx"/>
/// </remarks>
public readonly struct FILE_STREAM_INFORMATION
{
    public readonly uint NextEntryOffset;
    public readonly uint StreamNameLength;
    public readonly ulong StreamSize;
    public readonly ulong StreamAllocationSize;
    private readonly char _StreamName;

    [UnscopedRef]
    public ReadOnlySpan<char> StreamName => TrailingArray<char>.GetBufferInBytes(in _StreamName, StreamNameLength);
}