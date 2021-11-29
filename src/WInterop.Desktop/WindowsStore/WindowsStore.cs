// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Errors;
using WInterop.Support.Buffers;
using WInterop.WindowsStore.Native;

namespace WInterop.WindowsStore;

public static class WindowsStore
{
    private static int s_isWinRT = -1;

    /// <summary>
    ///  Returns true if the current process is a Windows Store application (WinRT).
    /// </summary>
    public static bool IsWindowsStoreApplication()
    {
        if (s_isWinRT == -1)
        {
            BufferHelper.BufferInvoke((StringBuffer buffer) =>
            {
                uint bufferSize = buffer.CharCapacity;
                try
                {
                    WindowsError result = Imports.GetCurrentApplicationUserModelId(ref bufferSize, buffer);
                    switch (result)
                    {
                        case WindowsError.APPMODEL_ERROR_NO_APPLICATION:
                            s_isWinRT = 0;
                            break;
                        case WindowsError.ERROR_SUCCESS:
                        case WindowsError.ERROR_INSUFFICIENT_BUFFER:
                            s_isWinRT = 1;
                            break;
                        default:
                            throw result.GetException();
                    }
                }
                catch (EntryPointNotFoundException)
                {
                    s_isWinRT = 0;
                }
            });
        }

        return s_isWinRT == 1;
    }
}