// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles;

namespace WInterop.Compression;

public class FdiHandle : HandleZeroOrMinusOneIsInvalid
{
    public FdiHandle() : base(ownsHandle: true) { }

    protected override unsafe bool ReleaseHandle() => TerraFXWindows.FDIDestroy((void*)handle);
}