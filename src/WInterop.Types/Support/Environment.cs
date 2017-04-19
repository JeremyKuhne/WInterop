// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;
using WInterop.Support.Buffers;
using WInterop.Support.Internal;

namespace WInterop.Support
{
    public static class Environment
    {
        private static int s_isWinRT = -1;

        /// <summary>
        /// True if the current process is running in 64 bit.
        /// </summary>
        /// <remarks>
        /// This isn't defined in Portable so we need our own.
        /// </remarks>
        public static bool Is64BitProcess = IntPtr.Size == sizeof(ulong);

        /// <summary>
        /// Returns true if the current process is a Windows Store application (WinRT).
        /// </summary>
        public static bool IsWindowsStoreApplication()
        {
            if (s_isWinRT == -1)
            {
                BufferHelper.CachedInvoke((StringBuffer buffer) =>
                {
                    uint bufferSize = buffer.CharCapacity;
                    try
                    {
                        WindowsError result = Internal.Imports.GetCurrentApplicationUserModelId(ref bufferSize, buffer);
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
                                throw Errors.GetIoExceptionForError(result);
                        }
                    }
                    catch (Exception e)
                    {
                        if (Errors.IsEntryPointNotFoundException(e))
                            // API doesn't exist, pre Win8
                            s_isWinRT = 0;
                        else
                            throw;
                    }
                });
            }

            return s_isWinRT == 1;
        }
    }
}
