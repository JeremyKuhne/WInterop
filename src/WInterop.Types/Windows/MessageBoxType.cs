// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows
{
    [Flags]
    public enum MessageBoxType : uint
    {
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms645505.aspx

        /// <summary>
        /// One OK button, default. (MB_OK)
        /// </summary>
        Ok = 0x00000000,

        /// <summary>
        /// OK and Cancel buttons. (MB_OKCANCEL)
        /// </summary>
        OkCancel = 0x00000001,

        /// <summary>
        /// Abort, Retry, and Ignore buttons. (MB_ABORTRETRYIGNORE)
        /// </summary>
        AbortRetryIgnore = 0x00000002,

        /// <summary>
        /// Yes, No, and Cancel buttons. (MB_YESNOCANCEL)
        /// </summary>
        YesNoCancel = 0x00000003,

        /// <summary>
        /// Yes and No buttons. (MB_YESNO)
        /// </summary>
        YesNo = 0x00000004,

        /// <summary>
        /// Retry and Cancel buttons. (MB_RETRYCANCEL)
        /// </summary>
        RetryCancel = 0x00000005,

        /// <summary>
        /// Cancel, Try, and Continue buttons. (MB_CANCELTRYCONTINUE)
        /// </summary>
        CancelTryContinue = 0x00000006,

        /// <summary>
        /// Stop sign icon. (MB_ICONHAND)
        /// </summary>
        IconHand = 0x00000010,

        /// <summary>
        /// Question mark icon. Not recommended. (MB_ICONQUESTION)
        /// </summary>
        IconQuestion = 0x00000020,

        /// <summary>
        /// Exclamation point icon. (MB_ICONEXCLAMATION)
        /// </summary>
        IconExclamation = 0x00000030,

        /// <summary>
        /// Info icon. (MB_ICONASTERISK)
        /// </summary>
        IconAsterisk = 0x00000040,

        /// <summary>
        /// Use the specified user icon. (MB_USERICON)
        /// </summary>
        UserIcon = 0x00000080,

        /// <summary>
        /// Exclamation point icon. (MB_ICONWARNING)
        /// </summary>
        IconWarning = IconExclamation,

        /// <summary>
        /// (MB_ICONERROR)
        /// </summary>
        IconError = IconHand,

        /// <summary>
        /// Info icon. (MB_ICONINFORMATION)
        /// </summary>
        IconInformation = IconAsterisk,

        /// <summary>
        /// Stop sign icon. (MB_ICONSTOP)
        /// </summary>
        IconStop = IconHand,

        // MB_DEFBUTTON1              = 0x00000000,

        /// <summary>
        /// 2nd button is the default. (MB_DEFBUTTON2)
        /// </summary>
        DefaultButton2 = 0x00000100,

        /// <summary>
        /// 3rd button is the default. (MB_DEFBUTTON3)
        /// </summary>
        DefaultButton3 = 0x00000200,

        /// <summary>
        /// 4th button is the default. (MB_DEFBUTTON4)
        /// </summary>
        DefaultButton4 = 0x00000300,

        // MB_APPLMODAL               = 0x00000000,

        /// <summary>
        /// Puts the dialog topmost (WS_EX_TOPMOST). (MB_SYSTEMMODAL)
        /// </summary>
        SystemModal = 0x00001000,

        /// <summary>
        /// Disable all threads top-level windows if no window handle is
        /// specified. (MB_TASKMODAL)
        /// </summary>
        TaskModal = 0x00002000,

        /// <summary>
        /// Adds a Help button. When clicked or F1 is pressed, help message is
        /// sent to the owner (WM_HELP). (MB_HELP)
        /// </summary>
        Help = 0x00004000,

        /// <summary>
        /// Ensures no button initially has focus. (MB_NOFOCUS)
        /// </summary>
        NoFocus = 0x00008000,

        /// <summary>
        /// Message box is set to the foreground. (MB_SETFOREGROUND)
        /// </summary>
        SetForeground = 0x00010000,

        /// <summary>
        /// If the current input desktop is not the default desktop,
        /// MessageBox does not return until the user switches to the
        /// default desktop. (MB_DEFAULT_DESKTOP_ONLY)
        /// </summary>
        DefaultDesktopOnly = 0x00020000,

        /// <summary>
        /// Messagebox is created with the topmost style (WS_EX_TOPMOST). (MB_TOPMOST)
        /// </summary>
        TopMost = 0x00040000,

        /// <summary>
        /// Text is right justified. (MB_RIGHT)
        /// </summary>
        Right = 0x00080000,

        /// <summary>
        /// Display right-to-left. (MB_RTLREADING)
        /// </summary>
        RightToLeftReading = 0x00100000,

        /// <summary>
        /// Used for services to send notifications. (MB_SERVICE_NOTIFICATION)
        /// </summary>
        ServiceNotification = 0x00200000,
    }
}
