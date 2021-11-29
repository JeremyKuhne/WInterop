// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Web.Native;

namespace WInterop.Web;

[ComImport,
    Guid(InterfaceIds.IID_ICoreWebView2),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface ICoreWebView2
{
    ICoreWebView2Settings Settings { get; }

    string Source { [return: MarshalAs(UnmanagedType.LPWStr)] get; }

    void Navigate([MarshalAs(UnmanagedType.LPWStr)] string uri);

    void NavigateToString([MarshalAs(UnmanagedType.LPWStr)] string htmlContent);

    // virtual HRESULT STDMETHODCALLTYPE add_NavigationStarting(
    //    /* [in] */ ICoreWebView2NavigationStartingEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_NavigationStarting(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_ContentLoading(
    //    /* [in] */ ICoreWebView2ContentLoadingEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_ContentLoading(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_SourceChanged(
    //    /* [in] */ ICoreWebView2SourceChangedEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_SourceChanged(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_HistoryChanged(
    //    /* [in] */ ICoreWebView2HistoryChangedEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_HistoryChanged(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_NavigationCompleted(
    //    /* [in] */ ICoreWebView2NavigationCompletedEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_NavigationCompleted(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_FrameNavigationStarting(
    //    /* [in] */ ICoreWebView2NavigationStartingEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_FrameNavigationStarting(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_ScriptDialogOpening(
    //    /* [in] */ ICoreWebView2ScriptDialogOpeningEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_ScriptDialogOpening(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_PermissionRequested(
    //    /* [in] */ ICoreWebView2PermissionRequestedEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_PermissionRequested(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_ProcessFailed(
    //    /* [in] */ ICoreWebView2ProcessFailedEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_ProcessFailed(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE AddScriptToExecuteOnDocumentCreated(
    //    /* [in] */ LPCWSTR javaScript,
    //    /* [in] */ ICoreWebView2AddScriptToExecuteOnDocumentCreatedCompletedHandler* handler) = 0;

    // virtual HRESULT STDMETHODCALLTYPE RemoveScriptToExecuteOnDocumentCreated(
    //    /* [in] */ LPCWSTR id) = 0;

    // virtual HRESULT STDMETHODCALLTYPE ExecuteScript(
    //    /* [in] */ LPCWSTR javaScript,
    //    /* [in] */ ICoreWebView2ExecuteScriptCompletedHandler* handler) = 0;

    // virtual HRESULT STDMETHODCALLTYPE CapturePreview(
    //    /* [in] */ CORE_WEBVIEW2_CAPTURE_PREVIEW_IMAGE_FORMAT imageFormat,
    //    /* [in] */ IStream* imageStream,
    //    /* [in] */ ICoreWebView2CapturePreviewCompletedHandler* handler) = 0;

    // virtual HRESULT STDMETHODCALLTYPE Reload(void) = 0;

    // virtual HRESULT STDMETHODCALLTYPE PostWebMessageAsJson(
    //    /* [in] */ LPCWSTR webMessageAsJson) = 0;

    // virtual HRESULT STDMETHODCALLTYPE PostWebMessageAsString(
    //    /* [in] */ LPCWSTR webMessageAsString) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_WebMessageReceived(
    //    /* [in] */ ICoreWebView2WebMessageReceivedEventHandler* handler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_WebMessageReceived(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE CallDevToolsProtocolMethod(
    //    /* [in] */ LPCWSTR methodName,
    //    /* [in] */ LPCWSTR parametersAsJson,
    //    /* [in] */ ICoreWebView2CallDevToolsProtocolMethodCompletedHandler* handler) = 0;

    // virtual HRESULT STDMETHODCALLTYPE get_BrowserProcessId(
    //    /* [retval][out] */ UINT32* value) = 0;

    // virtual HRESULT STDMETHODCALLTYPE get_CanGoBack(
    //    /* [retval][out] */ BOOL* canGoBack) = 0;

    // virtual HRESULT STDMETHODCALLTYPE get_CanGoForward(
    //    /* [retval][out] */ BOOL* canGoForward) = 0;

    // virtual HRESULT STDMETHODCALLTYPE GoBack(void) = 0;

    // virtual HRESULT STDMETHODCALLTYPE GoForward(void) = 0;

    // virtual HRESULT STDMETHODCALLTYPE GetDevToolsProtocolEventReceiver(
    //    /* [in] */ LPCWSTR eventName,
    //    /* [retval][out] */ ICoreWebView2DevToolsProtocolEventReceiver** receiver) = 0;

    // virtual HRESULT STDMETHODCALLTYPE Stop(void) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_NewWindowRequested(
    //    /* [in] */ ICoreWebView2NewWindowRequestedEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_NewWindowRequested(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_DocumentTitleChanged(
    //    /* [in] */ ICoreWebView2DocumentTitleChangedEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_DocumentTitleChanged(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE get_DocumentTitle(
    //    /* [retval][out] */ LPWSTR* title) = 0;

    // virtual HRESULT STDMETHODCALLTYPE AddRemoteObject(
    //    /* [in] */ LPCWSTR name,
    //    /* [in] */ VARIANT*object) = 0;

    // virtual HRESULT STDMETHODCALLTYPE RemoveRemoteObject(
    //    /* [in] */ LPCWSTR name) = 0;

    // virtual HRESULT STDMETHODCALLTYPE OpenDevToolsWindow(void) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_ContainsFullScreenElementChanged(
    //    /* [in] */ ICoreWebView2ContainsFullScreenElementChangedEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_ContainsFullScreenElementChanged(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE get_ContainsFullScreenElement(
    //    /* [retval][out] */ BOOL* containsFullScreenElement) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_WebResourceRequested(
    //    /* [in] */ ICoreWebView2WebResourceRequestedEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_WebResourceRequested(
    //    /* [in] */ EventRegistrationToken token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE AddWebResourceRequestedFilter(
    //    /* [in] */ const LPCWSTR uri,
    //    /* [in] */ const CORE_WEBVIEW2_WEB_RESOURCE_CONTEXT resourceContext) = 0;

    // virtual HRESULT STDMETHODCALLTYPE RemoveWebResourceRequestedFilter(
    //    /* [in] */ const LPCWSTR uri,
    //    /* [in] */ const CORE_WEBVIEW2_WEB_RESOURCE_CONTEXT resourceContext) = 0;

    // virtual HRESULT STDMETHODCALLTYPE add_WindowCloseRequested(
    //    /* [in] */ ICoreWebView2WindowCloseRequestedEventHandler* eventHandler,
    //    /* [out] */ EventRegistrationToken* token) = 0;

    // virtual HRESULT STDMETHODCALLTYPE remove_WindowCloseRequested(
    //    /* [in] */ EventRegistrationToken token) = 0;
}