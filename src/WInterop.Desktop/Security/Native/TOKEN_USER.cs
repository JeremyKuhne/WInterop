// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security.Native
{
    /// <summary>
    ///  Identifies the user associated with an access token.
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379634.aspx"/>
    /// </summary>
    public struct TOKEN_USER
    {
        public SID_AND_ATTRIBUTES User;
    }
}
