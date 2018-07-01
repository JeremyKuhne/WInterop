using System;
using WInterop.Console.Types;
using WInterop.Desktop.Console;
using WInterop.Windows.Types;

namespace CoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            uint inputCodePage = ConsoleMethods.GetConsoleInputCodePage();
            uint ouputCodePage = ConsoleMethods.GetConsoleOuputCodePage();

            ReadInputExample();
        }

        static void ReadInputExample()
        {
            // https://docs.microsoft.com/en-us/windows/console/reading-input-buffer-events

            var inputHandle = ConsoleMethods.GetStandardHandle(StandardHandleType.Input);
            var oldMode = ConsoleMethods.GetConsoleInputMode(inputHandle);

            // Setting EnableExtendedFlags without EnableQuickEditMode turns off QuickEdit mode, which is necessary to
            // get mouse events.

            ConsoleMethods.SetConsoleInputMode(inputHandle,
                ConsoleInputMode.EnableWindowInput | ConsoleInputMode.EnableMouseInput | ConsoleInputMode.EnableExtendedFlags);

            bool exit = false;
            foreach (var i in ConsoleMethods.ReadConsoleInput(inputHandle))
            {
                switch (i.EventType)
                {
                    case EventType.Focus:
                        Console.WriteLine($"Focus: {i.Data.FocusEvent.bSetFocus}");
                        break;
                    case EventType.Key:
                        var keyEvent = i.Data.KeyEvent;
                        Console.WriteLine($"Key: Down = {keyEvent.bKeyDown} Char = '{keyEvent.uChar.UnicodeChar}' Virtual Key = {keyEvent.wVirtualKeyCode} Modifiers = {keyEvent.dwControlKeyState}");
                        exit = keyEvent.wVirtualKeyCode == VirtualKey.C
                            && (keyEvent.dwControlKeyState & (ControlKeyState.LeftCtrlPressed | ControlKeyState.RightCtrlPressed)) != 0;
                        break;
                    case EventType.Mouse:
                        var mouseEvent = i.Data.MouseEvent;
                        Console.WriteLine($"Mouse: {mouseEvent.dwEventFlags} {mouseEvent.dwMousePosition.X}, {mouseEvent.dwMousePosition.Y}");
                        break;
                }

                if (exit)
                    break;
            }

            ConsoleMethods.SetConsoleInputMode(inputHandle, oldMode);
        }
    }
}
