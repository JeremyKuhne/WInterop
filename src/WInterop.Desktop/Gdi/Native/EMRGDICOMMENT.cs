// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi.Native;

public readonly ref struct EMRGDICOMMENT
{
    public readonly EMR emr;

    /// <summary>
    ///  Size of the data, in bytes.
    /// </summary>
    public readonly uint cbData;

    /// <summary>
    ///  Start of the application-specific data.
    /// </summary>
    public readonly byte Data;

    public const uint EMFPlusSignature = 0x2B464D45; // '+' 'F' 'M' 'E'
}