// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.ErrorHandling
{
    /// <summary>
    /// Windows error defines.
    /// </summary>
    public static class WinErrors
    {
        // From winerror.h
        // https://msdn.microsoft.com/en-us/library/windows/desktop/ms681382.aspx
        public const uint NO_ERROR = 0;
        public const uint ERROR_SUCCESS = 0;
        public const uint NERR_Success = 0;
        public const uint ERROR_INVALID_FUNCTION = 1;
        public const uint ERROR_FILE_NOT_FOUND = 2;
        public const uint ERROR_PATH_NOT_FOUND = 3;
        public const uint ERROR_ACCESS_DENIED = 5;
        public const uint ERROR_INVALID_DRIVE = 15;
        public const uint ERROR_NO_MORE_FILES = 18;
        public const uint ERROR_NOT_READY = 21;
        public const uint ERROR_SEEK = 25;
        public const uint ERROR_SHARING_VIOLATION = 32;
        public const uint ERROR_BAD_NETPATH = 53;
        public const uint ERROR_NETNAME_DELETED = 64;
        public const uint ERROR_NETWORK_ACCESS_DENIED = 65;
        public const uint ERROR_BAD_NET_NAME = 67;
        public const uint ERROR_FILE_EXISTS = 80;
        public const uint ERROR_INVALID_PARAMETER = 87;
        public const uint ERROR_INSUFFICIENT_BUFFER = 122;
        public const uint ERROR_INVALID_NAME = 123;
        public const uint ERROR_BAD_PATHNAME = 161;
        public const uint ERROR_ALREADY_EXISTS = 183;
        public const uint ERROR_ENVVAR_NOT_FOUND = 203;
        public const uint ERROR_FILENAME_EXCED_RANGE = 206;
        public const uint ERROR_MORE_DATA = 234;
        public const uint ERROR_OPERATION_ABORTED = 995;
        public const uint ERROR_NO_TOKEN = 1008;
        public const uint ERROR_NOT_FOUND = 1168;
        public const uint ERROR_PRIVILEGE_NOT_HELD = 1314;
        public const uint ERROR_DISK_CORRUPT = 1393;

        // From lmerr.h
        public const uint NERR_BASE = 2100;
        public const uint NERR_BufTooSmall = NERR_BASE + 23;
        public const uint NERR_InvalidComputer = NERR_BASE + 251;

        public const uint FVE_E_LOCKED_VOLUME = 0x80310000;
    }
}
