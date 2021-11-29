// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.GdiPlus;

// https://msdn.microsoft.com/en-us/library/ms534067.aspx
public struct StartupInput
{
    public uint GdiplusVersion;
    public IntPtr DebugEventCallback;
    public IntBoolean SuppressBackgroundThread;
    public IntBoolean SuppressExternalCodecs;
}
