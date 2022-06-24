// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles;

namespace WInterop.Storage;

/// <summary>
///  Handle for enumerating volume mount points.
/// </summary>
public class FindVolumeMountPointHandle : HandleZeroOrMinusOneIsInvalid
{
    public FindVolumeMountPointHandle() : base(ownsHandle: true)
    {
    }

    public FindVolumeMountPointHandle(HANDLE handle) : base((IntPtr)handle, ownsHandle: true)
    {
    }

    protected override bool ReleaseHandle() => TerraFXWindows.FindVolumeMountPointClose((HANDLE)handle);

    public static implicit operator HANDLE(FindVolumeMountPointHandle handle) => (HANDLE)handle.handle;
}