// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles;

namespace WInterop.Security
{
    /// <summary>
    ///  Safe handle for an access token
    /// </summary>
    public class AccessToken : CloseHandle
    {
        public AccessToken() : base() { }
    }
}
