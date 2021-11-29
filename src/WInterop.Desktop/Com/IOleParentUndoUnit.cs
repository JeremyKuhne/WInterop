// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Com;

/// <summary>
///  OLE IOleParentUndoUnit interface.
/// </summary>
/// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nn-ocidl-ioleparentundounit"/>
[ComImport,
    Guid("A1FAF330-EF97-11CE-9BC9-00AA00608E01"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IOleParentUndoUnit
{
    /// <summary>
    ///  Opens a new parent undo unit, which becomes part of the containing unit's undo stack.
    /// </summary>
    /// <param name="pPUU">The parent undo unit to be opened.</param>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nf-ocidl-ioleparentundounit-open"/>
    void Open(
        IOleParentUndoUnit pPUU);

    /// <summary>
    ///  Closes the specified parent undo unit.
    /// </summary>
    /// <param name="pPUU">The parent undo unit to be closed.</param>
    /// <param name="fCommit">Indicates whether to keep or discard the unit.</param>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nf-ocidl-ioleparentundounit-close"/>
    [PreserveSig]
    HResult Close(
        IOleParentUndoUnit pPUU,
        IntBoolean fCommit);

    /// <summary>
    ///  Adds a simple undo unit to the collection.
    /// </summary>
    /// <param name="pUU">The undo unit to be added.</param>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nf-ocidl-ioleparentundounit-add"/>
    void Add(
        IOleUndoUnit pUU);

    /// <summary>
    ///  Indicates whether the specified unit is a child of this undo unit or one of its children.
    /// </summary>
    /// <param name="pUU">The undo unit to be found.</param>
    /// <returns><see cref="HResult.S_OK"/> if found, <see cref="HResult.S_FALSE"/> otherwise.</returns>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nf-ocidl-ioleparentundounit-findunit"/>
    [PreserveSig]
    HResult FindUnit(
        IOleUndoUnit pUU);

    /// <summary>
    ///  Retrieves state information about the innermost open parent undo unit.
    /// </summary>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nf-ocidl-ioleparentundounit-getparentstate"/>
    UndoStateFlags GetParentState();
}