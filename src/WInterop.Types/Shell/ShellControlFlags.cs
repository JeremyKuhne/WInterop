// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    /// [SHCONTF]
    /// </summary>
    [Flags]
    public enum ShellControlFlags : uint
    {
        /// <summary>
        /// [SHCONTF_CHECKING_FOR_CHILDREN]
        /// </summary>
        CheckingForChildren = 0x10,

        /// <summary>
        /// [SHCONTF_FOLDERS]
        /// </summary>
        Folders = 0x20,

        /// <summary>
        /// [SHCONTF_NONFOLDERS]
        /// </summary>
        NonFolders = 0x40,

        /// <summary>
        /// [SHCONTF_INCLUDEHIDDEN]
        /// </summary>
        IncludeHidden = 0x80,

        /// <summary>
        /// [SHCONTF_INIT_ON_FIRST_NEXT]
        /// </summary>
        InitOnFirstNext = 0x100,

        /// <summary>
        /// [SHCONTF_NETPRINTERSRCH]
        /// </summary>
        NetPrinterSearch = 0x200,

        /// <summary>
        /// [SHCONTF_SHAREABLE]
        /// </summary>
        Shareable = 0x400,

        /// <summary>
        /// [SHCONTF_STORAGE]
        /// </summary>
        Storage = 0x800,

        /// <summary>
        /// [SHCONTF_NAVIGATION_ENUM]
        /// </summary>
        Navigation = 0x1000,

        /// <summary>
        /// [SHCONTF_FASTITEMS]
        /// </summary>
        FastItems = 0x2000,

        /// <summary>
        /// [SHCONTF_FLATLIST]
        /// </summary>
        FlatList = 0x4000,

        /// <summary>
        /// [SHCONTF_ENABLE_ASYNC]
        /// </summary>
        EnableAsync = 0x8000,

        /// <summary>
        /// [SHCONTF_INCLUDESUPERHIDDEN]
        /// </summary>
        IncludeSuperHidden = 0x10000
    }
}
