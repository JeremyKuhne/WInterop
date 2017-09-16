// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Support;
using WInterop.Windows.Types;

namespace WInterop.Windows
{
    public class SystemParameters
    {
        private SystemParameters() { }

        public static SystemParameters Instance { get; } = new SystemParameters();

        public unsafe bool GetBeep()
        {
            BOOL beeperOn = new BOOL();
            if (!WindowMethods.Imports.SystemParametersInfoW(SystemParameterType.SPI_GETBEEP, 0, &beeperOn, 0))
                throw Errors.GetIoExceptionForLastError();
            return beeperOn;
        }

        public unsafe void SetBeep(bool beeperOn, SystemParameterOptions options = 0)
        {
            if (!WindowMethods.Imports.SystemParametersInfoW(SystemParameterType.SPI_SETBEEP, (BOOL)beeperOn, null, 0))
                throw Errors.GetIoExceptionForLastError();
        }

        public unsafe bool GetBlockSendInputResets()
        {
            BOOL simulatedInputBlocked = new BOOL();
            if (!WindowMethods.Imports.SystemParametersInfoW(SystemParameterType.SPI_GETBLOCKSENDINPUTRESETS, 0, &simulatedInputBlocked, 0))
                throw Errors.GetIoExceptionForLastError();
            return simulatedInputBlocked;
        }

        public unsafe void SetBlockSendInputResets(bool simulatedInputBlocked, SystemParameterOptions options = 0)
        {
            if (!WindowMethods.Imports.SystemParametersInfoW(SystemParameterType.SPI_SETBLOCKSENDINPUTRESETS, (BOOL)simulatedInputBlocked, null, 0))
                throw Errors.GetIoExceptionForLastError();
        }

        public unsafe uint GetWheelScrollLines()
        {
            uint linesToScroll;
            if (!WindowMethods.Imports.SystemParametersInfoW(SystemParameterType.SPI_GETWHEELSCROLLLINES, 0, &linesToScroll, 0))
                throw Errors.GetIoExceptionForLastError();
            return linesToScroll;
        }
    }
}
