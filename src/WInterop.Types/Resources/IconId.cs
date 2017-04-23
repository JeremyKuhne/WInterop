// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Resources.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms648072.aspx
    public enum IconId : uint
    {
        IDI_APPLICATION    = 32512,
        IDI_HAND           = 32513,
        IDI_QUESTION       = 32514,
        IDI_EXCLAMATION    = 32515,
        IDI_ASTERISK       = 32516,
        IDI_WINLOGO        = 32517,
        IDI_SHIELD         = 32518,
        IDI_WARNING        = IDI_EXCLAMATION,
        IDI_ERROR          = IDI_HAND,
        IDI_INFORMATION    = IDI_ASTERISK
    }
}