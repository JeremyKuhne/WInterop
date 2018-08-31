// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;
using WInterop.Console;
using WInterop.Windows;

namespace CoreConsoleApp
{
    class Program
    {
        // ␛
        const char Esc = (char)27;


        static void Main(string[] args)
        {
            // Performance();
            // ReadInputExample();
            VirtualTerminalColor();
        }

        static void VirtualTerminalColor()
        {
            var writer = ConsoleWriter.Create(autoFlush: false);

            using (new TemporaryOutputMode(ConsoleOuputMode.EnableVirtualTerminalProcessing, addFlag: true))
            {
                for (int r = 30; r < 256; r += 30)
                for (int g = 30; g < 256; g += 30)
                for (int b = 30; b < 256; b += 30)
                {
                    //Console.Write($"{Esc}[38;2;{r};{g};{b}m");
                    //Console.Write("Color!");

                    writer.Write($"{Esc}[38;2;{r};{g};{b}m");
                    writer.Write("Color!");
                    writer.Write($"{Esc}[7m");
                    writer.Write("Color!");
                    writer.Write($"{Esc}[27m");
                }

                writer.Flush();

                // Tangerine
                System.Console.WriteLine();
                System.Console.Write($"{Esc}[38;2;242;133;0m");
                System.Console.WriteLine("In living color!");
                System.Console.Write($"{Esc}[4m");
                System.Console.WriteLine("In living color!");
            }

            System.Console.WriteLine("After exiting terminal mode.");
        }

        static void Performance()
        {
            var writer = ConsoleWriter.Create(autoFlush: false);

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

            System.Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds}, Allocated Bytes: {used}");
        }

        static void ReadInputExample()
        {
            // https://docs.microsoft.com/en-us/windows/console/reading-input-buffer-events

            var inputHandle = WInterop.Console.Console.GetStandardHandle(StandardHandleType.Input);
            var oldMode = WInterop.Console.Console.GetConsoleInputMode(inputHandle);

            // Setting EnableExtendedFlags without EnableQuickEditMode turns off QuickEdit mode, which is necessary to
            // get mouse events.

            WInterop.Console.Console.SetConsoleInputMode(inputHandle,
                ConsoleInputMode.EnableWindowInput | ConsoleInputMode.EnableMouseInput | ConsoleInputMode.EnableExtendedFlags);

            bool exit = false;
            foreach (var i in WInterop.Console.Console.ReadConsoleInput(inputHandle))
            {
                switch (i.EventType)
                {
                    case EventType.Focus:
                        System.Console.WriteLine($"Focus: {i.Data.FocusEvent.SetFocus}");
                        break;
                    case EventType.Key:
                        var keyEvent = i.Data.KeyEvent;
                        System.Console.WriteLine($"Key: Down = {keyEvent.KeyDown} Char = '{keyEvent.Char.UnicodeChar}' Virtual Key = {keyEvent.VirtualKeyCode} Modifiers = {keyEvent.ControlKeyState}");
                        exit = keyEvent.VirtualKeyCode == VirtualKey.C
                            && (keyEvent.ControlKeyState & (ControlKeyState.LeftCtrlPressed | ControlKeyState.RightCtrlPressed)) != 0;
                        break;
                    case EventType.Mouse:
                        var mouseEvent = i.Data.MouseEvent;
                        System.Console.WriteLine($"Mouse: {mouseEvent.EventFlags} {mouseEvent.MousePosition.X}, {mouseEvent.MousePosition.Y}");
                        break;
                }

                if (exit)
                    break;
            }

            WInterop.Console.Console.SetConsoleInputMode(inputHandle, oldMode);
        }
    }
}
