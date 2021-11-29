// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Storage.Native;

/// <summary>
///  Used to enumerate streams for a file.
/// </summary>
/// <remarks>
///  Equivalent to <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa364406.aspx">FILE_STREAM_INFO</a> structure.
/// <see cref="https://msdn.microsoft.com/en-us/library/windows/hardware/ff540364.aspx"/>
/// </remarks>
public struct FILE_STREAM_INFORMATION
{
    public uint NextEntryOffset;
    public uint StreamNameLength;
    public ulong StreamSize;
    public ulong StreamAllocationSize;
    private char _StreamName;
    public ReadOnlySpan<char> StreamName => TrailingArray<char>.GetBufferInBytes(in _StreamName, StreamNameLength);
}