// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379633.aspx">TOKEN_TYPE</a> structure.
    ///  [TOKEN_TYPE]
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        ///  An access token that can be used with CreateProcessAsUser. [TokenPrimary]
        /// </summary>
        Primary = 1,

        /// <summary>
        ///  An impersonation token. [TokenImpersonation]
        /// </summary>
        Impersonation
    }
}