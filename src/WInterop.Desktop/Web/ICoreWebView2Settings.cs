// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Web.Native;

namespace WInterop.Web;

[ComImport,
    Guid(InterfaceIds.IID_ICoreWebView2Settings),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ICoreWebView2Settings
{
    IntBoolean IsScriptEnabled { get; set; }

    IntBoolean IsWebMessageEnabled { get; set; }

    IntBoolean AreDefaultScriptDialogsEnabled { get; set; }

    IntBoolean IsStatusBarEnabled { get; set; }

    IntBoolean AreDevToolsEnabled { get; set; }

    IntBoolean AreDefaultContextMenusEnabled { get; set; }

    IntBoolean AreRemoteObjectsAllowed { get; set; }

    IntBoolean IsZoomControlEnabled { get; set; }
}