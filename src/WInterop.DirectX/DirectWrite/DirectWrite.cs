// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.DirectWrite.Native;

namespace WInterop.DirectWrite;

public static class DirectWrite
{
    public static WriteFactory CreateFactory(FactoryType factoryType = FactoryType.Shared)
    {
        Imports.DWriteCreateFactory(
            factoryType, new Guid(InterfaceIds.IID_IDWriteFactory), out WriteFactory factory)
            .ThrowIfFailed();

        return factory;
    }
}
