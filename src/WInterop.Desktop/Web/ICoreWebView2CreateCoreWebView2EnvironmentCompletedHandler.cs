// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;
using WInterop.Web.Native;

namespace WInterop.Web
{
    [ComImport,
        Guid(InterfaceIds.IID_ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler
    {
        [PreserveSig]
        HResult Invoke(
            HResult result,
            ICoreWebView2Environment created_environment);
    }
}