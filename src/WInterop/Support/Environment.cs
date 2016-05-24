// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Buffers;
using WInterop.ErrorHandling;
using WInterop.ErrorHandling.DataTypes;

namespace WInterop.Utility
{
    public static class Environment
    {
        private static bool? s_isWinRT;

        /// <summary>
        /// True if the current process is running in 64 bit.
        /// </summary>
        /// <remarks>
        /// This isn't defined in Portable so we need our own.
        /// </remarks>
        public static bool Is64BitProcess = Marshal.SizeOf<IntPtr>() == sizeof(ulong);

        /// <summary>
        /// Returns true if the current process is a Windows Store application (WinRT).
        /// </summary>
        public static bool IsWindowsStoreApplication()
        {
            if (!s_isWinRT.HasValue)
            {
                StringBufferCache.CachedBufferInvoke((buffer) =>
                {
                    uint bufferSize = buffer.CharCapacity;
                    try
                    {
                        WindowsError result = WindowsStore.Query.NativeMethods.Direct.GetCurrentApplicationUserModelId(ref bufferSize, buffer);
                        switch (result)
                        {
                            case WindowsError.APPMODEL_ERROR_NO_APPLICATION:
                                s_isWinRT = false;
                                break;
                            case WindowsError.ERROR_SUCCESS:
                            case WindowsError.ERROR_INSUFFICIENT_BUFFER:
                                s_isWinRT = true;
                                break;
                            default:
                                throw ErrorHelper.GetIoExceptionForError(result);
                        }
                    }
                    catch (Exception e)
                    {
                        if (ErrorHelper.IsEntryPointNotFoundException(e))
                            // API doesn't exist, pre Win8
                            s_isWinRT = false;
                        else
                            throw;
                    }
                });
            }

            return s_isWinRT.Value;
        }
    }
}
