// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Authorization
{
    /// <summary>
    /// Object types that support security. [SE_OBJECT_TYPE]
    /// </summary>
    /// <remarks>
    /// <see cref="https://msdn.microsoft.com/en-us/library/windows/desktop/aa379593.aspx"/>
    /// </remarks>
    public enum SecurityObjectType : uint
    {
        /// <summary>
        /// [SE_UNKNOWN_OBJECT_TYPE]
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// [SE_FILE_OBJECT]
        /// </summary>
        File,

        /// <summary>
        /// [SE_SERVICE]
        /// </summary>
        Service,

        /// <summary>
        /// [SE_PRINTER]
        /// </summary>
        Printer,

        /// <summary>
        /// [SE_REGISTRY_KEY]
        /// </summary>
        RegistryKey,

        /// <summary>
        /// [SE_LMSHARE]
        /// </summary>
        NetworkShare,

        /// <summary>
        /// Kernel object such as semaphores, mutexes, etc. S[SE_KERNEL_OBJECT]
        /// </summary>
        Kernel,

        /// <summary>
        /// [SE_WINDOW_OBJECT]
        /// </summary>
        Window,

        /// <summary>
        /// [SE_DS_OBJECT]
        /// </summary>
        DirectoryService,

        /// <summary>
        /// [SE_DS_OBJECT_ALL]
        /// </summary>
        DirectoryServiceAll,

        /// <summary>
        /// [SE_PROVIDER_DEFINED_OBJECT]
        /// </summary>
        ProviderDefined,

        /// <summary>
        /// [SE_WMIGUID_OBJECT]
        /// </summary>
        Wmi,

        /// <summary>
        /// [SE_REGISTRY_WOW64_32KEY]
        /// </summary>
        RegistryKeyWow64_32,

        /// <summary>
        /// [SE_REGISTRY_WOW64_64KEY]
        /// </summary>
        RegistryKeyWow64_64,
    }
}
