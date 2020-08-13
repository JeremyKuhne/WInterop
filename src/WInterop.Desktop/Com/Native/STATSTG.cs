// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Storage;

namespace WInterop.Com.Native
{
    /// <summary>
    ///  Statistical data about storage objects.
    ///  <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa380319.aspx"/>
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe ref struct STATSTG
    {
        /// <summary>
        ///  Pointer to the name. This needs to be freed via <see cref="Marshal.FreeCoTaskMem(IntPtr)"/>.
        /// </summary>
        public char* pwcsName;
        public StorageType type;
        public ulong cbSize;
        public FileTime mtime;
        public FileTime ctime;
        public FileTime atime;
        public StorageMode grfMode;
        public LockType grfLocksSupported;
        public Guid clsid;
        public uint grfStateBits;
        public uint reserved;

        public string GetAndFreeString()
        {
            string value = new string(pwcsName);
            Marshal.FreeCoTaskMem((IntPtr)pwcsName);
            return value;
        }
    }
}
