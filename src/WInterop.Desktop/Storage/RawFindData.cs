// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Storage.Native;

namespace WInterop.Storage;

/// <summary>
///  Used for processing and filtering find results.
/// </summary>
public unsafe ref struct RawFindData
{
    private readonly FILE_FULL_DIR_INFORMATION* _info;

    public RawFindData(FILE_FULL_DIR_INFORMATION* info, string directory)
    {
        _info = info;
        Directory = directory;
    }

    public ReadOnlySpan<char> FileName => _info->FileName;
    public string Directory { get; private set; }
    public AllFileAttributes FileAttributes => _info->FileAttributes;
    public ulong FileSize => (ulong)_info->AllocationSize;

    public DateTimeOffset CreationTimeUtc => _info->CreationTime.ToDateTimeUtc();
    public DateTimeOffset LastAccessTimeUtc => _info->LastAccessTime.ToDateTimeUtc();
    public DateTimeOffset LastWriteTimeUtc => _info->LastWriteTime.ToDateTimeUtc();
}