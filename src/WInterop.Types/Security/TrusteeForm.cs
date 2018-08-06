// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    /// [TRUSTEE_FORM]
    /// </summary>
    public enum TrusteeForm
    {
        // SID*
        /// <summary>
        /// [TRUSTEE_IS_SID]
        /// </summary>
        Sid,

        // LPWSTR
        /// <summary>
        /// [TRUSTEE_IS_NAME]
        /// </summary>
        Name,

        /// <summary>
        /// []
        /// </summary>
        BadForm,

        // OBJECTS_AND_SID*
        /// <summary>
        /// [TRUSTEE_IS_OBJECTS_AND_SID]
        /// </summary>
        ObjectsAndSid,

        // OBJECTS_AND_NAME_W*
        /// <summary>
        /// [TRUSTEE_IS_OBJECTS_AND_NAME]
        /// </summary>
        ObjectsAndName
    }
}
