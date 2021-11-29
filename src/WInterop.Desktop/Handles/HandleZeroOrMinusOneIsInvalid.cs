// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Handles;

public abstract class HandleZeroOrMinusOneIsInvalid : BaseHandle
{
    protected HandleZeroOrMinusOneIsInvalid(bool ownsHandle)
        : base(ownsHandle) { }

    protected HandleZeroOrMinusOneIsInvalid(IntPtr handle, bool ownsHandle = false)
        : base(handle, ownsHandle)
    {
    }

    public override bool IsInvalid => handle == IntPtr.Zero || handle == new IntPtr(-1);
}