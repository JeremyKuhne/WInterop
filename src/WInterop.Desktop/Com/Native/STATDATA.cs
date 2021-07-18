// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com.Native
{
    public unsafe struct STATDATA
    {
        public FORMATETC formatetc;
        public ADVF advf;
        public IAdviseSink* pAdvSink;
        public uint dwConnection;
    }
}