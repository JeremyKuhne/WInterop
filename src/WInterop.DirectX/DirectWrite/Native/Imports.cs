// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.DirectWrite.Native;

/// <summary>
///  Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
/// </summary>
public static partial class Imports
{
    // https://docs.microsoft.com/en-us/windows/desktop/api/dwrite/nf-dwrite-dwritecreatefactory
    [DllImport(Libraries.DWrite, ExactSpelling = true)]
    public static extern HResult DWriteCreateFactory(
        FactoryType factoryType,
        in Guid riid,
        out WriteFactory ppIFactory);
}
