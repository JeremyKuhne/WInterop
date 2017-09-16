// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.DeviceManagement.Types;

namespace WInterop.DeviceManagement
{
    public static class DeviceMacros
    {
        // https://msdn.microsoft.com/en-us/library/ms902086.aspx
        public static uint CTL_CODE(ControlCodeDeviceType deviceType, uint function, ControlCodeMethod method, ControlCodeAccess access)
        {
            return ((uint)deviceType << 16) | ((uint)access << 14) | (function << 2) | (uint)method;
        }
    }
}
