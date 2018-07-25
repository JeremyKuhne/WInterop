// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Resources
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648072.aspx
    public enum IconId : uint
    {
        /// <summary>
        /// Application icon. (IDI_APPLICATION)
        /// </summary>
        Application = 32512,

        /// <summary>
        /// Error icon (hand). (IDI_HAND)
        /// </summary>
        Hand = 32513,

        /// <summary>
        /// Question mark icon. (IDI_QUESTION)
        /// </summary>
        Question = 32514,

        /// <summary>
        /// Warning icon (exclamation point). (IDI_EXCLAMATION)
        /// </summary>
        Exclamation = 32515,

        /// <summary>
        /// Information icon (asterisk). (IDI_ASTERISK)
        /// </summary>
        Asterisk = 32516,

        /// <summary>
        /// Application icon (Windows logo on Windows 2000). (IDI_WINLOGO)
        /// </summary>
        WindowsLogo = 32517,

        /// <summary>
        /// Security shield icon. (IDI_SHIELD)
        /// </summary>
        Shield = 32518,

        /// <summary>
        /// Warning icon (exclamation point). (IDI_WARNING)
        /// </summary>
        Warning = Exclamation,

        /// <summary>
        /// Error icon (hand). (IDI_ERROR)
        /// </summary>
        Error = Hand,

        /// <summary>
        /// Information icon (asterisk). (IDI_INFORMATION)
        /// </summary>
        Information = Asterisk
    }
}