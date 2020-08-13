// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.CRT.Types
{
    [Flags]
    public enum FCNTL : int
    {
        _O_RDONLY      = 0x00000,  // open for reading only
        _O_WRONLY      = 0x00001,  // open for writing only
        _O_RDWR        = 0x00002,  // open for reading and writing
        _O_APPEND      = 0x00008,  // writes done at eof
        _O_CREAT       = 0x00100,  // create and open file
        _O_TRUNC       = 0x00200,  // open and truncate
        _O_EXCL        = 0x00400,  // open only if file doesn't already exist
        _O_TEXT        = 0x04000,  // file mode is text (translated)
        _O_BINARY      = 0x08000,  // file mode is binary (untranslated)
        _O_WTEXT       = 0x10000,  // file mode is UTF16 (translated)
        _O_U16TEXT     = 0x20000,  // file mode is UTF16 no BOM (translated)
        _O_U8TEXT      = 0x40000,  // file mode is UTF8  no BOM (translated)
        _O_NOINHERIT   = 0x00080,  // child process doesn't inherit file
        _O_TEMPORARY   = 0x00040,  // temporary file bit (file is deleted when last handle is closed)
        _O_SHORT_LIVED = 0x01000,  // temporary storage file, try not to flush
        _O_OBTAIN_DIR  = 0x02000,  // get information about a directory
        _O_SEQUENTIAL  = 0x00020,  // file access is primarily sequential
        _O_RANDOM      = 0x00010   // file access is primarily random
    }
}
