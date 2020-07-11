// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Web.Native
{
    /// <summary>
    /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
    /// </summary>
    public static partial class Imports
    {
        [DllImport(Libraries.WebView2Loader, ExactSpelling = true)]
        public static extern HResult CreateCoreWebView2EnvironmentWithDetails(
            string? browserExecutableFolder,
            string? userDataFolder,
            string? additionalBrowserArguments,
            ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler environment_created_handler);

        [DllImport(Libraries.WebView2Loader, ExactSpelling = true)]
        public static extern HResult CreateCoreWebView2Environment(
            ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler environment_created_handler);

        [DllImport(Libraries.WebView2Loader, ExactSpelling = true)]
        public static extern HResult GetCoreWebView2BrowserVersionInfo(
            string browserExecutableFolder,
            // This is a CoTaskMem allocated string
            out string versionInfo);

        [DllImport(Libraries.WebView2Loader, ExactSpelling = true)]
        public unsafe static extern HResult CompareBrowserVersions(
            string version1,
            string version2,
            int* result);
    }
}
