// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Gdi
{
    [Flags]
    public enum TextFormat : uint
    {
        // https://msdn.microsoft.com/en-us/library/dd162498.aspx

        /// <summary>
        ///  (DT_TOP)
        /// </summary>
        Top = 0x00000000,

        /// <summary>
        ///  (DT_LEFT)
        /// </summary>
        Left = 0x00000000,

        /// <summary>
        ///  (DT_CENTER)
        /// </summary>
        Center = 0x00000001,

        /// <summary>
        ///  (DT_RIGHT)
        /// </summary>
        Right = 0x00000002,

        /// <summary>
        ///  (DT_VCENTER)
        /// </summary>
        VerticallyCenter = 0x00000004,

        /// <summary>
        ///  (DT_BOTTOM)
        /// </summary>
        Bottom = 0x00000008,

        /// <summary>
        ///  (DT_WORDBREAK)
        /// </summary>
        WordBreak = 0x00000010,

        /// <summary>
        ///  (DT_SINGLELINE)
        /// </summary>
        SingleLine = 0x00000020,

        /// <summary>
        ///  (DT_EXPANDTABS)
        /// </summary>
        ExpandTabs = 0x00000040,

        /// <summary>
        ///  (DT_TABSTOP)
        /// </summary>
        TabStop = 0x00000080,

        /// <summary>
        ///  (DT_NOCLIP)
        /// </summary>
        NoClip = 0x00000100,

        /// <summary>
        ///  (DT_EXTERNALLEADING)
        /// </summary>
        ExternalLeading = 0x00000200,

        /// <summary>
        ///  (DT_CALCRECT)
        /// </summary>
        CalculateRectangle = 0x00000400,

        /// <summary>
        ///  (DT_NOPREFIX)
        /// </summary>
        NoPrefix = 0x00000800,

        /// <summary>
        ///  (DT_INTERNAL)
        /// </summary>
        Internal = 0x00001000,

        /// <summary>
        ///  (DT_EDITCONTROL)
        /// </summary>
        EditControl = 0x00002000,

        /// <summary>
        ///  (DT_PATH_ELLIPSIS)
        /// </summary>
        PathEllipsis = 0x00004000,

        /// <summary>
        ///  (DT_END_ELLIPSIS)
        /// </summary>
        EndEllipsis = 0x00008000,

        /// <summary>
        ///  (DT_MODIFYSTRING)
        /// </summary>
        ModifyString = 0x00010000,

        /// <summary>
        ///  (DT_RTLREADING)
        /// </summary>
        RightToLeftReading = 0x00020000,

        /// <summary>
        ///  (DT_WORD_ELLIPSIS)
        /// </summary>
        WordEllipsis = 0x00040000,

        /// <summary>
        ///  (DT_NOFULLWIDTHCHARBREAK)
        /// </summary>
        NoFullWidthCharacterBreak = 0x00080000,

        /// <summary>
        ///  (DT_HIDEPREFIX)
        /// </summary>
        HidePrefix = 0x00100000,

        /// <summary>
        ///  (DT_PREFIXONLY)
        /// </summary>
        PrefixOnly = 0x00200000
    }
}
