// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using WInterop.Errors;

namespace WInterop.Pipes
{
    public static partial class PipeMethods
    {
        public unsafe static void CreatePipe(out SafeFileHandle readPipe, out SafeFileHandle writePipe, uint bufferSize = 0)
        {
            if (!Imports.CreatePipe(out readPipe, out writePipe, null, bufferSize))
                throw Error.GetIoExceptionForLastError();
        }
    }
}
