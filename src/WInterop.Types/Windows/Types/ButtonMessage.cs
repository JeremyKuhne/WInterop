// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows.Types
{
    public enum ButtonMessage : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ff485886.aspx

        /// <summary>
        /// (BM_GETCHECK)
        /// </summary>
        GetCheck = 0x00F0,

        /// <summary>
        /// (BM_SETCHECK)
        /// </summary>
        SetCheck = 0x00F1,

        /// <summary>
        /// (BM_GETSTATE)
        /// </summary>
        GetState = 0x00F2,

        /// <summary>
        /// (BM_SETSTATE)
        /// </summary>
        SetState = 0x00F3,

        /// <summary>
        /// (BM_SETSTYLE)
        /// </summary>
        SetStyle = 0x00F4,

        /// <summary>
        /// (BM_CLICK)
        /// </summary>
        Click = 0x00F5,

        /// <summary>
        /// (BM_GETIMAGE)
        /// </summary>
        GetImage = 0x00F6,

        /// <summary>
        /// (BM_SETIMAGE)
        /// </summary>
        SetImage = 0x00F7,

        /// <summary>
        /// (BM_SETDONTCLICK)
        /// </summary>
        SetDoNotClick = 0x00F8,

        /// <summary>
        /// (BCM_GETIDEALSIZE)
        /// </summary>
        GetIdealSize = WindowDefines.BCM_FIRST + 0x0001,

        /// <summary>
        /// (BCM_SETIMAGELIST)
        /// </summary>
        SetImageList = WindowDefines.BCM_FIRST + 0x0002,

        /// <summary>
        /// (BCM_GETIMAGELIST)
        /// </summary>
        GetImageList = WindowDefines.BCM_FIRST + 0x0003,

        /// <summary>
        /// (BCM_SETTEXTMARGIN)
        /// </summary>
        SetTextMargin = WindowDefines.BCM_FIRST + 0x0004,

        /// <summary>
        /// (BCM_GETTEXTMARGIN)
        /// </summary>
        GetTextMargin = WindowDefines.BCM_FIRST + 0x0005,

        /// <summary>
        /// (BCM_SETDROPDOWNSTATE)
        /// </summary>
        SetDropDownState = WindowDefines.BCM_FIRST + 0x0006,

        /// <summary>
        /// (BCM_SETSPLITINFO)
        /// </summary>
        SetSplitInfo = WindowDefines.BCM_FIRST + 0x0007,

        /// <summary>
        /// (BCM_GETSPLITINFO)
        /// </summary>
        GetSplitInfo = WindowDefines.BCM_FIRST + 0x0008,

        /// <summary>
        /// (BCM_SETNOTE)
        /// </summary>
        SetNote = WindowDefines.BCM_FIRST + 0x0009,

        /// <summary>
        /// (BCM_GETNOTE)
        /// </summary>
        GetNote = WindowDefines.BCM_FIRST + 0x000A,

        /// <summary>
        /// (BCM_GETNOTELENGTH)
        /// </summary>
        GetNoteLength = WindowDefines.BCM_FIRST + 0x000B,

        /// <summary>
        /// (BCM_SETSHIELD)
        /// </summary>
        SetShield = WindowDefines.BCM_FIRST + 0x000C
    }
}
