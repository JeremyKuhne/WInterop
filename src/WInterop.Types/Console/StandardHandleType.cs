// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Console
{
    /// <summary>
    /// Standard device handle types.
    /// </summary>
    /// <remarks>
    /// <see cref="https://docs.microsoft.com/en-us/windows/console/getstdhandle"/>
    /// </remarks>
    public enum StandardHandleType : uint
    {
        /// <summary>
        /// The standard input device (CONIN$). [STD_INPUT_HANDLE]
        /// </summary>
        Input = unchecked((uint)-10),

        /// <summary>
        /// The standard output device (CONOUT$). [STD_OUTPUT_HANDLE]
        /// </summary>
        Output = unchecked((uint)-11),

        /// <summary>
        /// The standard error device. [STD_ERROR_HANDLE]
        /// </summary>
        Error = unchecked((uint)-12)
    }
}
