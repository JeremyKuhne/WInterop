// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Security
{
    /// <summary>
    ///  Object types that support security. [SE_OBJECT_TYPE]
    /// </summary>
    /// <remarks>
    /// <see cref="https://docs.microsoft.com/en-us/windows/desktop/api/accctrl/ne-accctrl-_se_object_type"/>
    /// </remarks>
    public enum ObjectType
    {
        /// <summary>
        ///  [SE_UNKNOWN_OBJECT_TYPE]
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///  File or directory. [SE_FILE_OBJECT]
        /// </summary>
        File,

        /// <summary>
        ///  Windows service. [SE_SERVICE]
        /// </summary>
        Service,

        /// <summary>
        ///  [SE_PRINTER]
        /// </summary>
        Printer,

        /// <summary>
        ///  [SE_REGISTRY_KEY]
        /// </summary>
        RegistryKey,

        /// <summary>
        ///  [SE_LMSHARE]
        /// </summary>
        NetworkShare,

        /// <summary>
        ///  [SE_KERNEL_OBJECT]
        /// </summary>
        Kernel,

        /// <summary>
        ///  Window station or dektop object. [SE_WINDOW_OBJECT]
        /// </summary>
        Window,

        /// <summary>
        ///  Directory service object or property / property set thereof. [SE_DS_OBJECT]
        /// </summary>
        DirectoryService,

        /// <summary>
        ///  Directory service object and all its property / property sets. [SE_DS_OBJECT_ALL]
        /// </summary>
        DirectoryServiceAll,

        /// <summary>
        ///  [SE_PROVIDER_DEFINED_OBJECT]
        /// </summary>
        ProviderDefined,

        /// <summary>
        ///  WMI object. [SE_WMIGUID_OBJECT]
        /// </summary>
        Wmi,

        /// <summary>
        ///  [SE_REGISTRY_WOW64_32KEY]
        /// </summary>
        RegistryWow6432,

        /// <summary>
        ///  [SE_REGISTRY_WOW64_64KEY]
        /// </summary>
        RegistryWow6464,
    }
}
