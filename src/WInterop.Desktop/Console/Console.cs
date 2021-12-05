// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;

using WInterop.Errors;
using WInterop.Support.Buffers;
using WInterop.Windows;

namespace WInterop.Console;

public static unsafe partial class Console
{
    public static WindowHandle GetConsoleWindow() => TerraFXWindows.GetConsoleWindow();

    public static void FreeConsole() => Error.ThrowLastErrorIfFalse(TerraFXWindows.FreeConsole());

    public static bool TryFreeConsole() => TerraFXWindows.FreeConsole();

    public static uint GetConsoleInputCodePage() => TerraFXWindows.GetConsoleCP();

    public static uint GetConsoleOuputCodePage() => TerraFXWindows.GetConsoleOutputCP();

    public static ConsoleInputMode GetConsoleInputMode(SafeFileHandle inputHandle)
    {
        uint mode;
        Error.ThrowLastErrorIfFalse(TerraFXWindows.GetConsoleMode(inputHandle.ToHANDLE(), &mode));
        return (ConsoleInputMode)mode;
    }

    public static void SetConsoleInputMode(SafeFileHandle inputHandle, ConsoleInputMode mode)
        => Error.ThrowLastErrorIfFalse(TerraFXWindows.SetConsoleMode(inputHandle.ToHANDLE(), (uint)mode));

    public static ConsoleOuputMode GetConsoleOutputMode(SafeFileHandle outputHandle)
    {
        uint mode;
        Error.ThrowLastErrorIfFalse(TerraFXWindows.GetConsoleMode(outputHandle.ToHANDLE(), &mode));
        return (ConsoleOuputMode)mode;
    }

    public static void SetConsoleOutputMode(SafeFileHandle outputHandle, ConsoleOuputMode mode)
        => Error.ThrowLastErrorIfFalse(TerraFXWindows.SetConsoleMode(outputHandle.ToHANDLE(), (uint)mode));

    /// <summary>
    ///  Get the specified standard handle.
    /// </summary>
    /// <returns>Handle or null if there is no associated handle for the given type.</returns>
    public static SafeFileHandle? GetStandardHandle(StandardHandleType type)
    {
        HANDLE handle = TerraFXWindows.GetStdHandle((uint)type);
        if (handle == HANDLE.INVALID_VALUE)
            Error.ThrowLastError();

        // If there is no associated standard handle, return null
        if (handle == HANDLE.NULL)
            return null;

        // The standard handles do not need to be released.
        return new SafeFileHandle(handle, ownsHandle: false);
    }

    /// <summary>
    ///  Reads input from the console. Will wait for next input, exit the iterator to stop listening.
    /// </summary>
    public static IEnumerable<InputRecord> ReadConsoleInput(SafeFileHandle inputHandle)
    {
        while (ReadNextLine(out InputRecord record))
        {
            yield return record;
        }

        bool ReadNextLine(out InputRecord record)
        {
            uint read;
            fixed (void* r = &record)
            {
                return TerraFXWindows.ReadConsoleInputW(inputHandle.ToHANDLE(), (INPUT_RECORD*)r, 1, &read);
            }
        }

        Error.ThrowLastError();
    }

    /// <summary>
    ///  Peek at the console input records.
    /// </summary>
    /// <param name="count">The maximum number of records to investigate.</param>
    public static IEnumerable<InputRecord> PeekConsoleInput(SafeFileHandle inputHandle, int count)
    {
        var owner = OwnedMemoryPool.Rent<InputRecord>(count);
        uint read;

        fixed (void* r = owner.Memory.Span)
        {
            if (!TerraFXWindows.PeekConsoleInputW(inputHandle.ToHANDLE(), (INPUT_RECORD*)r, (uint)count, &read))
            {
                owner.Dispose();
                Error.ThrowLastError();
            }
        }

        return new OwnedMemoryEnumerable<InputRecord>(owner, 0, (int)read);
    }

    /// <summary>
    ///  Writes the specified <paramref name="text"/> to the given console output handle.
    /// </summary>
    public static unsafe uint WriteConsole(SafeFileHandle outputHandle, ReadOnlySpan<char> text)
    {
        fixed (char* c = text)
        {
            uint charsWritten;
            Error.ThrowLastErrorIfFalse(
                TerraFXWindows.WriteConsoleW(outputHandle.ToHANDLE(), (void*)c, (uint)text.Length, &charsWritten, null));

            return charsWritten;
        }
    }
}