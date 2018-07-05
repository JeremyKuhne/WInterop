// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using System.Text;
using WInterop.Console;
using WInterop.Console.Types;
using WInterop.Windows.Types;

namespace CoreConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Performance();
            // ReadInputExample();
        }

        static void Performance()
        {
            var writer = ConsoleWriter.Create(StandardHandleType.Output, autoFlush: false);

            Stopwatch stopwatch = new Stopwatch();
            long prebytes = GC.GetAllocatedBytesForCurrentThread();
            stopwatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                writer.Write(i);
                // Console.Write(i);

                // writer.WriteLine("Line {0}: {1}", i, "Lorem ipsum dolor sit amet, consectetur adipiscing.");
                // Console.WriteLine("Line {0}: {1}", "State", "Lorem ipsum dolor sit amet, consectetur adipiscing.");
            }
            writer.Flush();
            stopwatch.Stop();
            long used = GC.GetAllocatedBytesForCurrentThread() - prebytes;

            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}, Allocated Bytes: {used}");
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
