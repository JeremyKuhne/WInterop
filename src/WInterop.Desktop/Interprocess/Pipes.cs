// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Win32.SafeHandles;
using WInterop.Errors;
using WInterop.Interprocess.Native;

namespace WInterop.Interprocess
{
    public static partial class Pipes
    {
        public static unsafe void CreatePipe(out SafeFileHandle readPipe, out SafeFileHandle writePipe, uint bufferSize = 0)
            => Error.ThrowLastErrorIfFalse(Imports.CreatePipe(out readPipe, out writePipe, null, bufferSize));
    }
}
