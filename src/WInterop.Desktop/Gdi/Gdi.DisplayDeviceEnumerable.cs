// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support.Collections;

namespace WInterop.Gdi;

public static partial class Gdi
{
    private sealed class DisplayDeviceEnumerable : Enumerable<DisplayDevice>
    {
        private readonly string _deviceName;
        private uint _index = 0;

        public DisplayDeviceEnumerable(string deviceName) => _deviceName = deviceName;

        public unsafe override bool MoveNext()
        {
            fixed (char* n = _deviceName)
            fixed (DisplayDevice* d = &_current)
            {
                d->SetSize();
                return TerraFXWindows.EnumDisplayDevicesW((ushort*)n, _index++, (DISPLAY_DEVICEW*)d, 0);
            }
        }
    }
}