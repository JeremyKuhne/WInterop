// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization
{
    /// <summary>
    /// <a href="https://msdn.microsoft.com/en-us/library/windows/desktop/aa446632.aspx">Generic access rights</a>.
    /// </summary>
    public static class AccessTypes
    {
        public const uint DELETE = 0x00010000;
        public const uint READ_CONTROL = 0x00020000;
        public const uint WRITE_DAC = 0x00040000;
        public const uint WRITE_OWNER = 0x00080000;
        public const uint SYNCHRONIZE = 0x00100000;
        public const uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        public const uint STANDARD_RIGHTS_READ = READ_CONTROL;
        public const uint STANDARD_RIGHTS_WRITE = READ_CONTROL;
        public const uint STANDARD_RIGHTS_EXECUTE = READ_CONTROL;
        public const uint STANDARD_RIGHTS_ALL = 0x001F0000;
        public const uint SPECIFIC_RIGHTS_ALL = 0x0000FFFF;
        public const uint ACCESS_SYSTEM_SECURITY = 0x01000000;
        public const uint MAXIMUM_ALLOWED = 0x02000000;
        public const uint GENERIC_READ = 0x80000000;
        public const uint GENERIC_WRITE = 0x40000000;
        public const uint GENERIC_EXECUTE = 0x20000000;
        public const uint GENERIC_ALL = 0x10000000;
    }
}
