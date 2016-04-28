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
        public const uint ERROR_TOO_MANY_OPEN_FILES = 4;
        public const uint ERROR_ACCESS_DENIED = 5;
        public const uint ERROR_INVALID_HANDLE = 6;
        public const uint ERROR_ARENA_TRASHED = 7;
        public const uint ERROR_NOT_ENOUGH_MEMORY = 8;
        public const uint ERROR_INVALID_BLOCK = 9;
        public const uint ERROR_BAD_ENVIRONMENT = 10;
        public const uint ERROR_BAD_FORMAT = 11;
        public const uint ERROR_INVALID_ACCESS = 12;
        public const uint ERROR_INVALID_DATA = 13;
        public const uint ERROR_OUTOFMEMORY = 14;
        public const uint ERROR_INVALID_DRIVE = 15;
        public const uint ERROR_CURRENT_DIRECTORY = 16;
        public const uint ERROR_NOT_SAME_DEVICE = 17;
        public const uint ERROR_NO_MORE_FILES = 18;
        public const uint ERROR_WRITE_PROTECT = 19;
        public const uint ERROR_BAD_UNIT = 20;
        public const uint ERROR_NOT_READY = 21;
        public const uint ERROR_BAD_COMMAND = 22;
        public const uint ERROR_CRC = 23;
        public const uint ERROR_BAD_LENGTH = 24;
        public const uint ERROR_SEEK = 25;
        public const uint ERROR_NOT_DOS_DISK = 26;
        public const uint ERROR_SECTOR_NOT_FOUND = 27;
        public const uint ERROR_OUT_OF_PAPER = 28;
        public const uint ERROR_WRITE_FAULT = 29;
        public const uint ERROR_READ_FAULT = 30;
        public const uint ERROR_GEN_FAILURE = 31;
        public const uint ERROR_SHARING_VIOLATION = 32;
        public const uint ERROR_LOCK_VIOLATION = 33;
        public const uint ERROR_WRONG_DISK = 34;
        public const uint ERROR_SHARING_BUFFER_EXCEEDED = 36;
        public const uint ERROR_HANDLE_EOF = 38;
        public const uint ERROR_HANDLE_DISK_FULL = 39;
        public const uint ERROR_NOT_SUPPORTED = 50;
        public const uint ERROR_REM_NOT_LIST = 51;
        public const uint ERROR_DUP_NAME = 52;
        public const uint ERROR_BAD_NETPATH = 53;
        public const uint ERROR_NETWORK_BUSY = 54;
        public const uint ERROR_DEV_NOT_EXIST = 55;
        public const uint ERROR_TOO_MANY_CMDS = 56;
        public const uint ERROR_ADAP_HDW_ERR = 57;
        public const uint ERROR_BAD_NET_RESP = 58;
        public const uint ERROR_UNEXP_NET_ERR = 59;
        public const uint ERROR_BAD_REM_ADAP = 60;
        public const uint ERROR_PRINTQ_FULL = 61;
        public const uint ERROR_NO_SPOOL_SPACE = 62;
        public const uint ERROR_PRINT_CANCELLED = 63;
        public const uint ERROR_NETNAME_DELETED = 64;
        public const uint ERROR_NETWORK_ACCESS_DENIED = 65;
        public const uint ERROR_BAD_DEV_TYPE = 66;
        public const uint ERROR_BAD_NET_NAME = 67;
        public const uint ERROR_TOO_MANY_NAMES = 68;
        public const uint ERROR_TOO_MANY_SESS = 69;
        public const uint ERROR_SHARING_PAUSED = 70;
        public const uint ERROR_REQ_NOT_ACCEP = 71;
        public const uint ERROR_REDIR_PAUSED = 72;
        public const uint ERROR_FILE_EXISTS = 80;
        public const uint ERROR_CANNOT_MAKE = 82;
        public const uint ERROR_FAIL_I24 = 83;
        public const uint ERROR_OUT_OF_STRUCTURES = 84;
        public const uint ERROR_ALREADY_ASSIGNED = 85;
        public const uint ERROR_INVALID_PASSWORD = 86;
        public const uint ERROR_INVALID_PARAMETER = 87;
        public const uint ERROR_NET_WRITE_FAULT = 88;
        public const uint ERROR_NO_PROC_SLOTS = 89;
        public const uint ERROR_TOO_MANY_SEMAPHORES = 100;
        public const uint ERROR_EXCL_SEM_ALREADY_OWNED = 101;
        public const uint ERROR_SEM_IS_SET = 102;
        public const uint ERROR_TOO_MANY_SEM_REQUESTS = 103;
        public const uint ERROR_INVALID_AT_INTERRUPT_TIME = 104;
        public const uint ERROR_SEM_OWNER_DIED = 105;
        public const uint ERROR_SEM_USER_LIMIT = 106;
        public const uint ERROR_DISK_CHANGE = 107;
        public const uint ERROR_DRIVE_LOCKED = 108;
        public const uint ERROR_BROKEN_PIPE = 109;
        public const uint ERROR_OPEN_FAILED = 110;
        public const uint ERROR_BUFFER_OVERFLOW = 111;
        public const uint ERROR_DISK_FULL = 112;
        public const uint ERROR_NO_MORE_SEARCH_HANDLES = 113;
        public const uint ERROR_INVALID_TARGET_HANDLE = 114;
        public const uint ERROR_INVALID_CATEGORY = 117;
        public const uint ERROR_INVALID_VERIFY_SWITCH = 118;
        public const uint ERROR_BAD_DRIVER_LEVEL = 119;
        public const uint ERROR_CALL_NOT_IMPLEMENTED = 120;
        public const uint ERROR_SEM_TIMEOUT = 121;
        public const uint ERROR_INSUFFICIENT_BUFFER = 122;
        public const uint ERROR_INVALID_NAME = 123;
        public const uint ERROR_INVALID_LEVEL = 124;
        public const uint ERROR_NO_VOLUME_LABEL = 125;
        public const uint ERROR_MOD_NOT_FOUND = 126;
        public const uint ERROR_PROC_NOT_FOUND = 127;
        public const uint ERROR_WAIT_NO_CHILDREN = 128;
        public const uint ERROR_CHILD_NOT_COMPLETE = 129;
        public const uint ERROR_DIRECT_ACCESS_HANDLE = 130;
        public const uint ERROR_NEGATIVE_SEEK = 131;
        public const uint ERROR_SEEK_ON_DEVICE = 132;
        public const uint ERROR_IS_JOIN_TARGET = 133;
        public const uint ERROR_IS_JOINED = 134;
        public const uint ERROR_IS_SUBSTED = 135;
        public const uint ERROR_NOT_JOINED = 136;
        public const uint ERROR_NOT_SUBSTED = 137;
        public const uint ERROR_JOIN_TO_JOIN = 138;
        public const uint ERROR_SUBST_TO_SUBST = 139;
        public const uint ERROR_JOIN_TO_SUBST = 140;
        public const uint ERROR_SUBST_TO_JOIN = 141;
        public const uint ERROR_BUSY_DRIVE = 142;
        public const uint ERROR_SAME_DRIVE = 143;
        public const uint ERROR_DIR_NOT_ROOT = 144;
        public const uint ERROR_DIR_NOT_EMPTY = 145;
        public const uint ERROR_IS_SUBST_PATH = 146;
        public const uint ERROR_IS_JOIN_PATH = 147;
        public const uint ERROR_PATH_BUSY = 148;
        public const uint ERROR_IS_SUBST_TARGET = 149;
        public const uint ERROR_SYSTEM_TRACE = 150;
        public const uint ERROR_INVALID_EVENT_COUNT = 151;
        public const uint ERROR_TOO_MANY_MUXWAITERS = 152;
        public const uint ERROR_INVALID_LIST_FORMAT = 153;
        public const uint ERROR_LABEL_TOO_LONG = 154;
        public const uint ERROR_TOO_MANY_TCBS = 155;
        public const uint ERROR_SIGNAL_REFUSED = 156;
        public const uint ERROR_DISCARDED = 157;
        public const uint ERROR_NOT_LOCKED = 158;
        public const uint ERROR_BAD_THREADID_ADDR = 159;
        public const uint ERROR_BAD_ARGUMENTS = 160;
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

        public const uint APPMODEL_ERROR_NO_APPLICATION = 15703;

        public const uint FVE_E_LOCKED_VOLUME = 0x80310000;
    }
}
