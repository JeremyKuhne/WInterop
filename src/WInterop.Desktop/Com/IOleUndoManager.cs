// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Errors;

namespace WInterop.Com;

/// <summary>
///  OLE IOleUndoManager interface.
/// </summary>
/// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nn-ocidl-ioleundomanager"/>
[ComImport,
    Guid("D001F200-EF97-11CE-9BC9-00AA00608E01"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IOleUndoManager
{
    void Open(IOleParentUndoUnit pPUU);

    [PreserveSig]
    HResult Close(
        IOleParentUndoUnit pPUU,
        IntBoolean fCommit);

    void Add(IOleUndoUnit pUU);

    UndoStateFlags GetOpenParentState();

    void DiscardFrom(IOleUndoUnit pUU);

    [PreserveSig]
    HResult UndoTo(IOleUndoUnit pUU);

    [PreserveSig]
    HResult RedoTo(IOleUndoUnit pUU);

    IEnumOleUndoUnits EnumUndoable();

    IEnumOleUndoUnits EnumRedoable();

    string GetLastUndoDescription();

    string GetLastRedoDescription();

    [PreserveSig]
    HResult Enable(IntBoolean fEnable);
}