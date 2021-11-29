// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell;

// https://msdn.microsoft.com/en-us/library/windows/desktop/bb762543.aspx
[Flags]
public enum ShellItemCompareFlags : uint
{
    Display = 0,
    AllFields = 0x80000000,
    Canonical = 0x10000000,
    TestFileSystemPathIfNotEqual = 0x20000000
}