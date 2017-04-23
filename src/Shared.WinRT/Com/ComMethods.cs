// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.ErrorHandling.Types;

namespace WInterop.Com
{
    public static class ComMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            public static HRESULT VariantClear(IntPtr pvarg) => Support.Internal.Imports.VariantClear(pvarg);
        }
    }
}
