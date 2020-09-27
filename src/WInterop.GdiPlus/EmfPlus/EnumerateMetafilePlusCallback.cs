// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.GdiPlus.EmfPlus
{
    public delegate bool EnumerateMetafilePlusCallback(
        RecordType recordType,
        uint flags,
        uint dataSize,
        IntPtr data,
        IntPtr callbackData);
}
