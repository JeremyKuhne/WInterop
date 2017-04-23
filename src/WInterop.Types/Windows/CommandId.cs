// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645507.aspx
    public enum CommandId : int
    {
        Error = 0,
        IDOK           = 1,
        IDCANCEL       = 2,
        IDABORT        = 3,
        IDRETRY        = 4,
        IDIGNORE       = 5,
        IDYES          = 6,
        IDNO           = 7,
        IDCLOSE        = 8,
        IDHELP         = 9,
        IDTRYAGAIN     = 10,
        IDCONTINUE     = 11,
        IDTIMEOUT      = 32000
    }
}
