// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using WInterop.Security;
using WInterop.Security.Native;
using WInterop.Pipes.Types;

namespace WInterop.Pipes
{
    public static partial class PipeMethods
    {
        /// <summary>
        /// Direct usage of Imports isn't recommended. Use the wrappers that do the heavy lifting for you.
        /// </summary>
        public static partial class Imports
        {
            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365152.aspx
            [DllImport(Libraries.Kernel32, ExactSpelling = true, SetLastError = true)]
            public unsafe static extern bool CreatePipe(
                out SafeFileHandle hReadPipe,
                out SafeFileHandle hWritePipe,
                SECURITY_ATTRIBUTES* lpPipeAttributes,
                uint nSize);

            // https://msdn.microsoft.com/en-us/library/windows/desktop/aa365150.aspx
            [DllImport(Libraries.Kernel32, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
            public unsafe static extern PipeHandle CreateNamedPipeW(
                string lpName,
                uint dwOpenMode,
                uint dwPipeMode,
                uint nMaxInstances,
                uint nOutBufferSize,
                uint nInBufferSize,
                uint nDefaultTimeOut,
                SECURITY_ATTRIBUTES* lpSecurityAttributes);
        }
    }
}
