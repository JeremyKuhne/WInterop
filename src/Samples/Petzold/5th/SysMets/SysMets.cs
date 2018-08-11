// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Windows;

namespace SysMets
{
    class SysMets : SysMets4
    {
        int iDeltaPerLine, iAccumDelta; // for mouse wheel logic

        protected override LResult WindowProcedure(WindowHandle window, MessageType message, WParam wParam, LParam lParam)
        {
            switch (message)
            {
                case MessageType.Create:
                    base.WindowProcedure(window, message, wParam, lParam);

                    // Fall through for mouse wheel information
                    goto case MessageType.SettingChange;
                case MessageType.SettingChange:
                    uint ulScrollLines = Windows.SystemParameters.GetWheelScrollLines();

                    // ulScrollLines usually equals 3 or 0 (for no scrolling)
                    // WHEEL_DELTA equals 120, so iDeltaPerLine will be 40
                    if (ulScrollLines > 0)
                        iDeltaPerLine = (int)(120 / ulScrollLines);
                    else
                        iDeltaPerLine = 0;
                    return 0;
                case MessageType.MouseWheel:
                    if (iDeltaPerLine == 0)
                        break;
                    iAccumDelta += (short)wParam.HighWord; // 120 or -120
                    while (iAccumDelta >= iDeltaPerLine)
                    {
                        window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.LineUp, 0);
                        iAccumDelta -= iDeltaPerLine;
                    }
                    while (iAccumDelta <= -iDeltaPerLine)
                    {
                        window.SendMessage(MessageType.VerticalScroll, (uint)ScrollCommand.LineDown, 0);
                        iAccumDelta += iDeltaPerLine;
                    }
                    return 0;
            }

            return base.WindowProcedure(window, message, wParam, lParam);
        }
    }
}
