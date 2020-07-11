// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Web.Native;

namespace WInterop.Web
{
    [ComImport,
        Guid(InterfaceIds.IID_ICoreWebView2Settings),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICoreWebView2Settings
    {
        Boolean32 IsScriptEnabled { get; set; }

        Boolean32 IsWebMessageEnabled { get; set; }

        Boolean32 AreDefaultScriptDialogsEnabled { get; set; }

        Boolean32 IsStatusBarEnabled { get; set; }

        Boolean32 AreDevToolsEnabled { get; set; }

        Boolean32 AreDefaultContextMenusEnabled { get; set; }

        Boolean32 AreRemoteObjectsAllowed { get; set; }

        Boolean32 IsZoomControlEnabled { get; set; }
    }
}
