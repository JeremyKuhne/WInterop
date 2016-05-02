// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Authorization
{
    // https://msdn.microsoft.com/en-us/library/aa374892.aspx
    [Flags]
    public enum ACCESS_MASK : uint
    {
        FILE_READ_DATA = 0x0001,
        FILE_LIST_DIRECTORY = 0x0001,
        DIRECTORY_QUERY = 0x0001,
        FILE_WRITE_DATA = 0x0002,
        FILE_ADD_FILE = 0x0002,
        DIRECTORY_TRAVERSE = 0x0002,
        FILE_APPEND_DATA = 0x0004,
        FILE_ADD_SUBDIRECTORY = 0x0004,
        FILE_CREATE_PIPE_INSTANCE = 0x0004,
        DIRECTORY_CREATE_OBJECT = 0x0004,
        FILE_READ_EA = 0x0008,
        DIRECTORY_CREATE_SUBDIRECTORY = 0x0008,
        DELETE = 0x00010000,
        READ_CONTROL = 0x00020000,
        WRITE_DAC = 0x00040000,
        WRITE_OWNER = 0x00080000,
        SYNCHRONIZE = 0x00100000,
        STANDARD_RIGHTS_REQUIRED = 0x000F0000,
        STANDARD_RIGHTS_READ = READ_CONTROL,
        STANDARD_RIGHTS_WRITE = READ_CONTROL,
        STANDARD_RIGHTS_EXECUTE = READ_CONTROL,
        STANDARD_RIGHTS_ALL = 0x001F0000,
        SPECIFIC_RIGHTS_ALL = 0x0000FFFF,
        ACCESS_SYSTEM_SECURITY = 0x01000000,
        MAXIMUM_ALLOWED = 0x02000000,
        GENERIC_READ = 0x80000000,
        GENERIC_WRITE = 0x40000000,
        GENERIC_EXECUTE = 0x20000000,
        GENERIC_ALL = 0x10000000,
        DIRECTORY_ALL_ACCESS = DIRECTORY_QUERY | DIRECTORY_TRAVERSE | DIRECTORY_CREATE_OBJECT | DIRECTORY_CREATE_SUBDIRECTORY | STANDARD_RIGHTS_REQUIRED
    }
}
