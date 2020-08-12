// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using System.Threading;
using WInterop.Errors;
using WInterop.Web.Native;
using WInterop.Windows.Native;

namespace WInterop.Web
{
    [ComImport,
        Guid(InterfaceIds.IID_ICoreWebView2Environment),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICoreWebView2Environment
    {
        void CreateCoreWebView2Host(
            HWND parentWindow,
            ICoreWebView2CreateCoreWebView2HostCompletedHandler handler);

        void CreateWebResourceResponse_STUB();
            ///* [in] */ IStream* content,
            ///* [in] */ int statusCode,
            ///* [in] */ LPCWSTR reasonPhrase,
            ///* [in] */ LPCWSTR headers,
            ///* [retval][out] */ ICoreWebView2WebResourceResponse** response) = 0;

        string BrowserVersionInfo { [return: MarshalAs(UnmanagedType.LPWStr)]  get; }

        void add_NewBrowserVersionAvailable_STUB();
        ///* [in] */ ICoreWebView2NewBrowserVersionAvailableEventHandler* eventHandler,
        ///* [out] */ EventRegistrationToken* token) = 0;

        void remove_NewBrowserVersionAvailable_STUB();
            ///* [in] */ EventRegistrationToken token) = 0;
    }

    public class CreateHostCompletedHandler : ICoreWebView2CreateCoreWebView2HostCompletedHandler
    {
        public event EventHandler<(HResult Result, ICoreWebView2Host Host)>? HostCreated;

        public HResult Invoke(HResult result, ICoreWebView2Host created_host)
        {
            HostCreated?.Invoke(this, (result, created_host));
            return HResult.S_OK;
        }
    }
}
