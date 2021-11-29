// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows;

public struct DeferWindowPositionHandle
{
    public IntPtr HDWP;
    public bool IsValid => HDWP != IntPtr.Zero && HDWP != (IntPtr)(-1);
}