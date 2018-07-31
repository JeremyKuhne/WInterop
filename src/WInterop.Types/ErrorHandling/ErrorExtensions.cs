// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.ErrorHandling;
using WInterop.Support;

namespace WInterop
{
    public static class ErrorExtensions
    {
        public static bool Succeeded(this HRESULT result) => ErrorMacros.SUCCEEDED(result);
        public static bool Failed(this HRESULT result) => ErrorMacros.FAILED(result);
        public static Exception GetException(this HRESULT result, string path = null) => Errors.GetIoExceptionForHResult(result, path);
        public static void ThrowIfFailed(this HRESULT result, string path = null)
        {
            if (result.Failed()) throw result.GetException(path);
        }
    }
}
