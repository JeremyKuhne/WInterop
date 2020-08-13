// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    ///  [TRUSTEE_FORM]
    /// </summary>
    public enum TrusteeForm
    {
        /// <summary>
        ///  (SID*) [TRUSTEE_IS_SID]
        /// </summary>
        Sid,

        /// <summary>
        ///  (LPWSTR) [TRUSTEE_IS_NAME]
        /// </summary>
        Name,

        BadForm,

        /// <summary>
        ///  (OBJECTS_AND_SID*) [TRUSTEE_IS_OBJECTS_AND_SID]
        /// </summary>
        ObjectsAndSid,

        /// <summary>
        ///  (OBJECTS_AND_NAME_W*) [TRUSTEE_IS_OBJECTS_AND_NAME]
        /// </summary>
        ObjectsAndName
    }
}
