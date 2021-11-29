// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Windows;

public static partial class Message
{
    public readonly ref struct Timer
    {
        // https://docs.microsoft.com/en-us/windows/desktop/winmsg/wm-timer

        private readonly WParam _wParam;
        private readonly LParam _lParam;

        public Timer(WParam wParam, LParam lParam)
        {
            _wParam = wParam;
            _lParam = lParam;
        }

        public uint Id => _wParam;
        public TimerProcedure? Procedure
            => _lParam.IsNull ? null : Marshal.GetDelegateForFunctionPointer<TimerProcedure>(_lParam);
    }
}