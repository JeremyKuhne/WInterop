// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi
{
    // https://msdn.microsoft.com/en-us/library/dd183499.aspx
    public enum FontWeight : int
    {
        /// <summary>
        ///  Don't care. (FW_DONTCARE)
        /// </summary>
        DoNotCare = 0,

        /// <summary>
        ///  Thin. (FW_THIN)
        /// </summary>
        Thin = 100,

        /// <summary>
        ///  Extra Light. (FW_EXTRALIGHT)
        /// </summary>
        ExtraLight = 200,

        /// <summary>
        ///  Light. (FW_LIGHT)
        /// </summary>
        Light = 300,

        /// <summary>
        ///  Normal. (FW_NORMAL)
        /// </summary>
        Normal = 400,

        /// <summary>
        ///  Medium. (FW_MEDIUM)
        /// </summary>
        Medium = 500,

        /// <summary>
        ///  Semibold. (FW_SEMIBOLD)
        /// </summary>
        Semibold = 600,

        /// <summary>
        ///  Bold. (FW_BOLD)
        /// </summary>
        Bold = 700,

        /// <summary>
        ///  Extra Bold. (FW_EXTRABOLD)
        /// </summary>
        ExtraBold = 800,

        /// <summary>
        ///  Heavy. (FW_HEAVY)
        /// </summary>
        Heavy = 900,

        /// <summary>
        ///  Ultra Light. (FW_ULTRALIGHT)
        /// </summary>
        UltraLight = ExtraLight,

        /// <summary>
        ///  Regular. (FW_REGULAR)
        /// </summary>
        Regular = Normal,

        /// <summary>
        ///  Demibold. (FW_DEMIBOLD)
        /// </summary>
        Demibold = Semibold,

        /// <summary>
        ///  Ultra Bold. (FW_ULTRABOLD)
        /// </summary>
        UltraBold = ExtraBold,

        /// <summary>
        ///  Black. (FW_BLACK)
        /// </summary>
        Black = Heavy
    }
}