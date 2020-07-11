// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading;
using WInterop.Errors;
using WInterop.Web.Native;

namespace WInterop.Web
{
    public static partial class Web
    {
        public static ICoreWebView2Environment CreateWebView2Environment()
        {
            var handler = new CreateEnvironmentHandler();
            Imports.CreateCoreWebView2Environment(handler).ThrowIfFailed();

            //Imports.CreateCoreWebView2EnvironmentWithDetails(
            //    @"C:\Program Files (x86)\Microsoft\Edge\Application",
            //    null,
            //    null,
            //    handler).ThrowIfFailed();

            while (!handler.Completed)
            {
                Thread.Yield();
            }

            handler.Result.ThrowIfFailed();
            return handler.Environment!;
        }

        private class CreateEnvironmentHandler : ICoreWebView2CreateCoreWebView2EnvironmentCompletedHandler
        {
            public ICoreWebView2Environment? Environment;
            public bool Completed;
            public HResult Result;

            public HResult Invoke(HResult result, ICoreWebView2Environment created_environment)
            {
                Environment = created_environment;
                Result = result;
                Completed = true;

                return HResult.S_OK;
            }
        }
    }
}
