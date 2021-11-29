// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Storage.Native;

namespace WInterop.Storage;

/// <summary>
///  Basic information about an alternate stream.
/// </summary>
public struct StreamInformation
{
    /// <summary>
    ///  Name of the stream
    /// </summary>
    public string Name;

    /// <summary>
    ///  Size of the stream
    /// </summary>
    public ulong Size;

    /// <summary>
    ///  Allocated size of the stream
    /// </summary>
    public ulong AllocationSize;

    public unsafe StreamInformation(FILE_STREAM_INFORMATION* info)
    {
        Name = info->StreamName.CreateString();
        Size = info->StreamSize;
        AllocationSize = info->StreamAllocationSize;
    }

    public static unsafe IEnumerable<StreamInformation> Create(FILE_STREAM_INFORMATION* info)
    {
        var infos = new List<StreamInformation>();
        byte* start = (byte*)info;
        while (true)
        {
            infos.Add(new StreamInformation(info));
            if (info->NextEntryOffset == 0) break;
            info = (FILE_STREAM_INFORMATION*)(start + info->NextEntryOffset);
        }

        return infos;
    }
}