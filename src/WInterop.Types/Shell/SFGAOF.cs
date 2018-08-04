// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell.Types
{
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762589.aspx
    [Flags]
    public enum SFGAOF : uint
    {
        CANCOPY           = 0x1, // DROPEFFECT_COPY Objects can be copied    (0x1)
        CANMOVE           = 0x2, // DROPEFFECT_MOVE Objects can be moved     (0x2)
        CANLINK           = 0x4, // DROPEFFECT_LINK Objects can be linked    (0x4)
        STORAGE           = 0x00000008,     // supports BindToObject(IID_IStorage)
        CANRENAME         = 0x00000010,     // Objects can be renamed
        CANDELETE         = 0x00000020,     // Objects can be deleted
        HASPROPSHEET      = 0x00000040,     // Objects have property sheets
        DROPTARGET        = 0x00000100,     // Objects are drop target
        CAPABILITYMASK    = 0x00000177,
        SYSTEM            = 0x00001000,     // System object
        ENCRYPTED         = 0x00002000,     // Object is encrypted (use alt color)
        ISSLOW            = 0x00004000,     // 'Slow' object
        GHOSTED           = 0x00008000,     // Ghosted icon
        LINK              = 0x00010000,     // Shortcut (link)
        SHARE             = 0x00020000,     // Shared
        READONLY          = 0x00040000,     // Read-only
        HIDDEN            = 0x00080000,     // Hidden object
        DISPLAYATTRMASK   = 0x000FC000,
        FILESYSANCESTOR   = 0x10000000,     // May contain children with SFGAO_FILESYSTEM
        FOLDER            = 0x20000000,     // Support BindToObject(IID_IShellFolder)
        FILESYSTEM        = 0x40000000,     // Is a win32 file system object (file/folder/root)
        HASSUBFOLDER      = 0x80000000,     // May contain children with SFGAO_FOLDER (may be slow)
        CONTENTSMASK      = 0x80000000,
        VALIDATE          = 0x01000000,     // Invalidate cached information (may be slow)
        REMOVABLE         = 0x02000000,     // Is this removeable media?
        COMPRESSED        = 0x04000000,     // Object is compressed (use alt color)
        BROWSABLE         = 0x08000000,     // Supports IShellFolder, but only implements CreateViewObject() (non-folder view)
        NONENUMERATED     = 0x00100000,     // Is a non-enumerated object (should be hidden)
        NEWCONTENT        = 0x00200000,     // Should show bold in explorer tree
        CANMONIKER        = 0x00400000,     // Obsolete
        HASSTORAGE        = 0x00400000,     // Obsolete
        STREAM            = 0x00400000,     // Supports BindToObject(IID_IStream)
        STORAGEANCESTOR   = 0x00800000,     // May contain children with SFGAO_STORAGE or SFGAO_STREAM
        STORAGECAPMASK    = 0x70C50008,     // For determining storage capabilities, ie for open/save semantics
        PKEYSFGAOMASK     = 0x81044000      // Attributes that are masked out for PKEY_SFGAOFlags because they are considered to cause slow calculations or lack context (SFGAO_VALIDATE | SFGAO_ISSLOW | SFGAO_HASSUBFOLDER and others)
    }
}
