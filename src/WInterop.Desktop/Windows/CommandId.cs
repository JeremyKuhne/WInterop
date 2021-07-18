// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645507.aspx
    public enum CommandId : int
    {
        Error = 0,

        /// <summary>
        ///  [IDOK]
        /// </summary>
        Ok = 1,

        /// <summary>
        ///  [IDCANCEL]
        /// </summary>
        Cancel = 2,

        /// <summary>
        ///  [IDABORT]
        /// </summary>
        Abort = 3,

        /// <summary>
        ///  [IDRETRY]
        /// </summary>
        Retry = 4,

        /// <summary>
        ///  [IDIGNORE]
        /// </summary>
        Ignore = 5,

        /// <summary>
        ///  [IDYES]
        /// </summary>
        Yes = 6,

        /// <summary>
        ///  [IDNO]
        /// </summary>
        No = 7,

        /// <summary>
        ///  [IDCLOSE]
        /// </summary>
        Close = 8,

        /// <summary>
        ///  [IDHELP]
        /// </summary>
        Help = 9,

        /// <summary>
        ///  [IDTRYAGAIN]
        /// </summary>
        TryAgain = 10,

        /// <summary>
        ///  [IDCONTINUE]
        /// </summary>
        Continue = 11,

        /// <summary>
        ///  [IDTIMEOUT]
        /// </summary>
        Timeout = 32000
    }
}