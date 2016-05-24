// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices.ComTypes;
using WInterop.FileManagement;
using WInterop.FileManagement.DataTypes;

namespace WInterop.Support
{
    public static class Conversion
    {
        public static ulong HighLowToLong(uint high, uint low)
        {
            return ((ulong)high) << 32 | ((ulong)low & 0xFFFFFFFFL);
        }

        public static DateTime FileTimeToDateTime(FILETIME fileTime)
        {
            return DateTime.FromFileTime((((long)fileTime.dwHighDateTime) << 32) + (uint)fileTime.dwLowDateTime);
        }

        public static DesiredAccess FileAccessToDesiredAccess(System.IO.FileAccess fileAccess)
        {
            // See FileStream.Init to see how the mapping is done in .NET
            switch (fileAccess)
            {
                case System.IO.FileAccess.Read:
                    return DesiredAccess.GENERIC_READ;
                case System.IO.FileAccess.Write:
                    return DesiredAccess.GENERIC_WRITE;
                case System.IO.FileAccess.ReadWrite:
                    return DesiredAccess.GENERIC_READ | DesiredAccess.GENERIC_WRITE;
                default:
                    return (DesiredAccess)0;
            }
        }

        public static System.IO.FileAccess DesiredAccessToFileAccess(DesiredAccess desiredAccess)
        {
            System.IO.FileAccess fileAccess = 0;
            if ((desiredAccess & (DesiredAccess.GENERIC_READ | DesiredAccess.FILE_READ_DATA)) > 0)
                fileAccess = System.IO.FileAccess.Read;

            if ((desiredAccess & (DesiredAccess.GENERIC_WRITE | DesiredAccess.FILE_WRITE_DATA)) > 0)
                fileAccess = fileAccess == System.IO.FileAccess.Read ? System.IO.FileAccess.ReadWrite : System.IO.FileAccess.Write;

            return fileAccess;
        }

        public static ShareMode FileShareToShareMode(System.IO.FileShare fileShare)
        {
            // See additional comments on ShareMode
            fileShare &= ~System.IO.FileShare.Inheritable;
            return unchecked((ShareMode)fileShare);
        }

        public static FileFlags FileOptionsToFileFlags(System.IO.FileOptions fileOptions)
        {
            return unchecked((FileFlags)fileOptions);
        }

        public static CreationDisposition FileModeToCreationDisposition(System.IO.FileMode fileMode)
        {
            if (fileMode == System.IO.FileMode.Append)
                return CreationDisposition.OPEN_ALWAYS;
            else
                return unchecked((CreationDisposition)fileMode);
        }
    }
}
