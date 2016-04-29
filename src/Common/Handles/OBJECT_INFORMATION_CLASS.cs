// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Handles
{
    // https://msdn.microsoft.com/en-us/library/windows/hardware/ff550964.aspx
    public enum OBJECT_INFORMATION_CLASS
    {
        ObjectBasicInformation,

        // Undocumented directly, returns a UNICODE_STRING
        // https://msdn.microsoft.com/en-us/library/windows/hardware/ff548474(v=vs.85).aspx
        ObjectNameInformation,

        ObjectTypeInformation,

        // Undocumented
        // https://ntquery.wordpress.com/2014/03/30/anti-debug-ntqueryobject/#more-21
        ObjectTypesInformation
    }
}
