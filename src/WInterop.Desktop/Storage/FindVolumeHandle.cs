// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles;

namespace WInterop.Storage;

/// <summary>
///  Handle for enumerating volumes.
/// </summary>
public class FindVolumeHandle : HandleZeroOrMinusOneIsInvalid
{
    public FindVolumeHandle() : base(ownsHandle: true)
    {
    }

    public FindVolumeHandle(HANDLE handle) : base(handle, ownsHandle: true)
    {
    }

    protected override bool ReleaseHandle() => TerraFXWindows.FindVolumeClose((HANDLE)handle);

    public static implicit operator HANDLE(FindVolumeHandle handle) => (HANDLE)handle.handle;
}