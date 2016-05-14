// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.FileManagement.CopyFile2
{
    [StructLayout(LayoutKind.Sequential)]
    public struct COPYFILE2_MESSAGE
    {
        public COPYFILE2_MESSAGE_TYPE Type;
        public uint dwPadding;

        // The rest of the struct is a union of the structs that apply to the given type
    }
}
