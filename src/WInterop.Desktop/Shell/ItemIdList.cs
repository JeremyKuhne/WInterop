// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Com;

namespace WInterop.Shell;

/// <summary>
///  List of item ids in the shell.
/// </summary>
/// <remarks>
///  Includes [LPITEMIDLIST], [LPCITEMIDLIST], [PIDLIST_ABSOLUTE], [PCIDLIST_ABSOLUTE], [PCUIDLIST_ABSOLUTE],
///  [PIDLIST_RELATIVE], [PCIDLIST_RELATIVE], [PUIDLIST_RELATIVE], [PCUIDLIST_RELATIVE], [PITEMID_CHILD],
///  [PCITEMID_CHILD], [PUITEMID_CHILD], [PCUITEMID_CHILD]
///
///  https://docs.microsoft.com/previous-versions/windows/desktop/legacy/cc144089(v=vs.85)#pidls
/// </remarks>
public class ItemIdList : SafeComHandle
{
    public ItemIdList() : this(ownsHandle: true)
    {
    }

    public ItemIdList(bool ownsHandle) : base(ownsHandle)
    {
    }
}