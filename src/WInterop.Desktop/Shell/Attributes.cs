// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Shell
{
    /// <summary>
    /// [SFGAOF]
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb762589.aspx
    [Flags]
    public enum Attributes : uint
    {
        CanCopy           = 0x1, // DROPEFFECT_COPY Objects can be copied    (0x1)
        CanMove           = 0x2, // DROPEFFECT_MOVE Objects can be moved     (0x2)
        CanLink           = 0x4, // DROPEFFECT_LINK Objects can be linked    (0x4)
        Storage           = 0x00000008,     // supports BindToObject(IID_IStorage)
        CanRename         = 0x00000010,     // Objects can be renamed
        CanDelete         = 0x00000020,     // Objects can be deleted
        HasPropSheet      = 0x00000040,     // Objects have property sheets
        DropTarget        = 0x00000100,     // Objects are drop target
        CapabilityMask    = 0x00000177,
        System            = 0x00001000,     // System object
        Encrypted         = 0x00002000,     // Object is encrypted (use alt color)
        IsSlow            = 0x00004000,     // 'Slow' object
        Ghosted           = 0x00008000,     // Ghosted icon
        Link              = 0x00010000,     // Shortcut (link)
        Share             = 0x00020000,     // Shared
        ReadOnly          = 0x00040000,     // Read-only
        Hidden            = 0x00080000,     // Hidden object
        DisplayAttrMask   = 0x000FC000,
        FilesysAncestor   = 0x10000000,     // May contain children with SFGAO_FILESYSTEM
        Folder            = 0x20000000,     // Support BindToObject(IID_IShellFolder)
        FileSystem        = 0x40000000,     // Is a win32 file system object (file/folder/root)
        HasSubfolder      = 0x80000000,     // May contain children with SFGAO_FOLDER (may be slow)
        ContentsMask      = 0x80000000,
        Validate          = 0x01000000,     // Invalidate cached information (may be slow)
        Removable         = 0x02000000,     // Is this removeable media?
        Compressed        = 0x04000000,     // Object is compressed (use alt color)
        Browsable         = 0x08000000,     // Supports IShellFolder, but only implements CreateViewObject() (non-folder view)
        NonEnumerated     = 0x00100000,     // Is a non-enumerated object (should be hidden)
        NewContent        = 0x00200000,     // Should show bold in explorer tree
        CanMoniker        = 0x00400000,     // Obsolete
        HasStorage        = 0x00400000,     // Obsolete
        Stream            = 0x00400000,     // Supports BindToObject(IID_IStream)
        StorageAncestor   = 0x00800000,     // May contain children with SFGAO_STORAGE or SFGAO_STREAM
        StorageCapMask    = 0x70C50008,     // For determining storage capabilities, ie for open/save semantics
        PkeySfgaoMask     = 0x81044000      // Attributes that are masked out for PKEY_SFGAOFlags because they are considered to cause slow calculations or lack context (SFGAO_VALIDATE | SFGAO_ISSLOW | SFGAO_HASSUBFOLDER and others)
    }
}
