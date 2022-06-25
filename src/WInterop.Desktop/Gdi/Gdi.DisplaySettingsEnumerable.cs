// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support.Collections;

namespace WInterop.Gdi;

public static partial class Gdi
{
    private sealed class DisplaySettingsEnumerable : Enumerable<DeviceMode>
    {
        private readonly string _deviceName;
        private uint _modeIndex;
        private bool _finished;

        public DisplaySettingsEnumerable(string deviceName, uint modeIndex)
        {
            _deviceName = deviceName;
            _modeIndex = modeIndex;
        }

        public unsafe override bool MoveNext()
        {
            if (_finished)
            {
                return false;
            }

            fixed (char* n = _deviceName)
            fixed (DeviceMode* d = &_current)
            {
                d->Size = (ushort)sizeof(DeviceMode);

                bool result = TerraFXWindows.EnumDisplaySettingsW((ushort*)n, _modeIndex, (DEVMODEW*)d);

                if (!result
                    || _modeIndex == GdiDefines.ENUM_CURRENT_SETTINGS
                    || _modeIndex == GdiDefines.ENUM_REGISTRY_SETTINGS)
                {
                    _finished = true;
                }
                else
                {
                    _modeIndex++;
                }

                return result;
            }
        }
    }
}