// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Clipboard;

namespace WInterop.Com.Native
{
    /// <docs>https://docs.microsoft.com/windows/win32/api/objidl/ns-objidl-formatetc</docs>
    public unsafe struct FORMATETC
    {
        /// <summary>
        ///  <see cref="ClipboardFormat"/>
        /// </summary>
        public ushort cfFormat;
        public DVTARGETDEVICE* ptd;
        public DVASPECT dwAspect;

        /// <summary>
        ///  Part of the <see cref="dwAspect"/>, -1 is all data.
        /// </summary>
        public int lindex;

        public MediumType tymed;

        public FORMATETC(ClipboardFormat format, MediumType medium)
        {
            cfFormat = checked((ushort)format);
            tymed = medium;
            ptd = null;
            dwAspect = DVASPECT.DVASPECT_CONTENT;
            lindex = -1;
        }

        public ClipboardFormat Format => (ClipboardFormat)cfFormat;
        public MediumType Medium => tymed;
    }
}
