// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace System;

internal static class ConversionExtensions
{
    public static uint HighWord(this ulong value) => (uint)(value >> 32);
    public static uint LowWord(this ulong value) => (uint)value;
}