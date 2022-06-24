// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Errors;

[Flags]
public enum ErrorMode : uint
{
    /// <summary>
    ///  The system does not display the critical-error-handler message box. Instead, the system sends
    ///  the error to the calling process.
    /// </summary>
    FailCriticalErrors = TerraFXWindows.SEM_FAILCRITICALERRORS,

    /// <summary>
    ///  The system does not display the Windows Error Reporting dialog.
    /// </summary>
    NoGPFaultErrorBox = TerraFXWindows.SEM_NOGPFAULTERRORBOX,

    /// <summary>
    ///  The system automatically fixes memory alignment faults and makes them invisible to the application.
    ///  It does this for the calling process and any descendant processes.
    /// </summary>
    NoAligmentFaultExceptions = TerraFXWindows.SEM_NOALIGNMENTFAULTEXCEPT,

    /// <summary>
    ///  The system does not display a message box when it fails to find a file. Instead, the error is
    ///  returned to the calling process.
    /// </summary>
    NoOpenFileErrorBox = TerraFXWindows.SEM_NOOPENFILEERRORBOX
}