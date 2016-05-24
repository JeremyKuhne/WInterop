// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.ErrorHandling.DataTypes
{
    [Flags]
    public enum ErrorMode : uint
    {
        /// <summary>
        /// The system does not display the critical-error-handler message box. Instead, the system sends the error to the calling process.
        /// </summary>
        SEM_FAILCRITICALERRORS = 0x0001,

        /// <summary>
        /// The system does not display the Windows Error Reporting dialog.
        /// </summary>
        SEM_NOGPFAULTERRORBOX = 0x0002,

        /// <summary>
        /// The system automatically fixes memory alignment faults and makes them invisible to the application. It does this for the calling process and any descendant processes.
        /// </summary>
        SEM_NOALIGNMENTFAULTEXCEPT = 0x0004,

        /// <summary>
        /// The system does not display a message box when it fails to find a file. Instead, the error is returned to the calling process.
        /// </summary>
        SEM_NOOPENFILEERRORBOX = 0x8000
    }
}
