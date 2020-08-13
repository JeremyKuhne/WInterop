// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public class SystemParameters
    {
        private SystemParameters() { }

        public static SystemParameters Instance { get; } = new SystemParameters();

        public unsafe bool GetBeep()
        {
            IntBoolean beeperOn = default;
            Error.ThrowLastErrorIfFalse(
                Imports.SystemParametersInfoW(SystemParameterType.SPI_GETBEEP, 0, &beeperOn, 0));
            return beeperOn;
        }

        public unsafe void SetBeep(bool beeperOn, SystemParameterOptions options = 0)
            => Error.ThrowLastErrorIfFalse(
                Imports.SystemParametersInfoW(SystemParameterType.SPI_SETBEEP, (IntBoolean)beeperOn, null, options));

        public unsafe bool GetBlockSendInputResets()
        {
            IntBoolean simulatedInputBlocked = default;
            Error.ThrowLastErrorIfFalse(
                Imports.SystemParametersInfoW(
                    SystemParameterType.SPI_GETBLOCKSENDINPUTRESETS,
                    0,
                    &simulatedInputBlocked,
                    0));
            return simulatedInputBlocked;
        }

        public unsafe void SetBlockSendInputResets(bool simulatedInputBlocked, SystemParameterOptions options = 0)
            => Error.ThrowLastErrorIfFalse(
                Imports.SystemParametersInfoW(
                    SystemParameterType.SPI_SETBLOCKSENDINPUTRESETS,
                    (IntBoolean)simulatedInputBlocked,
                    null,
                    options));

        public unsafe uint GetWheelScrollLines()
        {
            uint linesToScroll;
            Error.ThrowLastErrorIfFalse(
                Imports.SystemParametersInfoW(SystemParameterType.SPI_GETWHEELSCROLLLINES, 0, &linesToScroll, 0));
            return linesToScroll;
        }
    }
}
