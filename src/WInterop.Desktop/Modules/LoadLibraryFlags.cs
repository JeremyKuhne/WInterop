// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Modules;

// https://msdn.microsoft.com/en-us/library/windows/desktop/ms684179.aspx
[Flags]
public enum LoadLibraryFlags : uint
{
    DontResolveDllReferences = 0x00000001,
    LoadLibraryAsDatafile = 0x00000002,
    LoadWithAlteredSearchPath = 0x00000008,
    LoadIgnoreCodeAuthzLevel = 0x00000010,
    LoadLibraryAsImageResource = 0x00000020,
    LoadLibraryAsDatafileExclusive = 0x00000040,
    LoadLibrarySearchDllLoadDir = 0x00000100,
    LoadLibrarySearchApplicationDir = 0x00000200,
    LoadLibrarySearchUserDirs = 0x00000400,
    LoadLibrarySearchSystem32 = 0x00000800,
    LoadLibrarySearchDefaultDirs = 0x00001000
}