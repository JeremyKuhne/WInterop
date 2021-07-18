// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    /// <summary>
    ///  [NEWTEXTMETRICEX]
    /// </summary>
    /// <msdn>https://msdn.microsoft.com/en-us/library/dd162742.aspx</msdn>
    public struct NewTextMetricsExtended
    {
        public NewTextMetrics TextMetrics;
        public FontSignature FontSignature;
    }
}