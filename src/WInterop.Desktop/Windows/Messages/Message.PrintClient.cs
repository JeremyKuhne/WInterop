﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Gdi;

namespace WInterop.Windows;

public static partial class Message
{
    public readonly ref struct PrintClient
    {
        public HDC HDC { get; }

        public DeviceContext DeviceContext => new(HDC);

        public unsafe PrintClient(WParam wParam)
        {
            HDC = new(wParam);
        }
    }
}