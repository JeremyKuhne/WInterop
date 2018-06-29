// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.FileManagement.Types
{
    public enum FileDispositionFlags : uint
    {
        DoNotDelete = 0x00000000,
        Delete = 0x00000001,
        PosixSemantics = 0x00000002,
        ForceImageSectionCheck = 0x00000004,
        FlagOnClose = 0x00000008
    }
}
