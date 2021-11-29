// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.GdiPlus;

public class Session : IDisposable
{
    private UIntPtr _token;

    public Session(uint version = 2)
    {
        _token = GdiPlus.Startup(version);
    }

    ~Session() => Dispose();

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        GdiPlus.Shutdown(_token);
        _token = UIntPtr.Zero;
    }
}
