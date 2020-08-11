// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Gdi;
using WInterop.Web.Native;
using WInterop.Windows.Native;

namespace WInterop.Web
{
    [ComImport,
        Guid(InterfaceIds.IID_ICoreWebView2Host),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface ICoreWebView2Host
    {
        Boolean32 IsVisible { get; set; }

        Rect Bounds { get; set; }

        double ZoomFactor { get; set; }

        void add_ZoomFactorChanged_Stub();
        //virtual HRESULT STDMETHODCALLTYPE add_ZoomFactorChanged(
        //    /* [in] */ ICoreWebView2ZoomFactorChangedEventHandler* eventHandler,
        //    /* [out] */ EventRegistrationToken* token) = 0;

        void remove_ZoomFactorChanged();
        //virtual HRESULT STDMETHODCALLTYPE remove_ZoomFactorChanged(
        //    /* [in] */ EventRegistrationToken token) = 0;

        void SetBoundsAndZoomFactor(
            Rect bounds,
            double zoomFactor);

        void MoveFocus_Stub();
        //virtual HRESULT STDMETHODCALLTYPE MoveFocus(
        //    /* [in] */ CORE_WEBVIEW2_MOVE_FOCUS_REASON reason) = 0;

        void add_MoveFocusRequested_Stub();
        //virtual HRESULT STDMETHODCALLTYPE add_MoveFocusRequested(
        //    /* [in] */ ICoreWebView2MoveFocusRequestedEventHandler* eventHandler,
        //    /* [out] */ EventRegistrationToken* token) = 0;

        void remove_MoveFocusRequested_Stub();
        //virtual HRESULT STDMETHODCALLTYPE remove_MoveFocusRequested(
        //    /* [in] */ EventRegistrationToken token) = 0;

        void add_GotFocus_Stub();
        //virtual HRESULT STDMETHODCALLTYPE add_GotFocus(
        //    /* [in] */ ICoreWebView2FocusChangedEventHandler* eventHandler,
        //    /* [out] */ EventRegistrationToken* token) = 0;

        void remove_GotFocus_Stub();
        //virtual HRESULT STDMETHODCALLTYPE remove_GotFocus(
        //    /* [in] */ EventRegistrationToken token) = 0;

        void add_LostFocus_Stub();
        //virtual HRESULT STDMETHODCALLTYPE add_LostFocus(
        //    /* [in] */ ICoreWebView2FocusChangedEventHandler* eventHandler,
        //    /* [out] */ EventRegistrationToken* token) = 0;

        void remove_LostFocus_Stub();
        //virtual HRESULT STDMETHODCALLTYPE remove_LostFocus(
        //    /* [in] */ EventRegistrationToken token) = 0;

        void add_AcceleratorKeyPressed_Stub();
        //virtual HRESULT STDMETHODCALLTYPE add_AcceleratorKeyPressed(
        //    /* [in] */ ICoreWebView2AcceleratorKeyPressedEventHandler* eventHandler,
        //    /* [out] */ EventRegistrationToken* token) = 0;

        void remove_AcceleratorKeyPressed_Stub();
        //virtual HRESULT STDMETHODCALLTYPE remove_AcceleratorKeyPressed(
        //    /* [in] */ EventRegistrationToken token) = 0;

        HWND ParentWindow { get; set; }

        void NotifyParentWindowPositionChanged();

        void Close();

        ICoreWebView2 CoreWebView2 { get; }
    }
}
