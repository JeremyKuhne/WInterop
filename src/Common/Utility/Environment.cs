// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Utility
{
    using Buffers;
    using ErrorHandling;
    using System;
    using System.Runtime.InteropServices;

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
                        uint result = NativeMethods.WindowsStore.Query.Direct.GetCurrentApplicationUserModelId(ref bufferSize, buffer);
                        switch (result)
                        {
                            case WinErrors.APPMODEL_ERROR_NO_APPLICATION:
                                s_isWinRT = false;
                                break;
                            case WinErrors.ERROR_SUCCESS:
                            case WinErrors.ERROR_INSUFFICIENT_BUFFER:
                                s_isWinRT = true;
                                break;
                            default:
                                throw ErrorHelper.GetIoExceptionForError(result);
                        }
                    }
                    catch (Exception e)
                    {
                        if (e.GetType().FullName.Equals("System.EntryPointNotFoundException", StringComparison.Ordinal))
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
