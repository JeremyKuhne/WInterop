// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.CRT.Types;

public enum pmode : int
{
    _S_IFMT = 0xF000,   // File type mask
    _S_IFDIR = 0x4000,  // Directory
    _S_IFCHR = 0x2000,  // Character special
    _S_IFIFO = 0x1000,  // Pipe
    _S_IFREG = 0x8000,  // Regular
    _S_IREAD = 0x0100,  // Read permission, owner
    _S_IWRITE = 0x0080, // Write permission, owner
    _S_IEXEC = 0x0040   // Execute/search permission, owner
}