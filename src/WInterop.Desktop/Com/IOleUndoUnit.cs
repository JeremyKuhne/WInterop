// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Com;

/// <summary>
///  OLE IOleUndoUnit interface.
/// </summary>
/// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nn-ocidl-ioleundounit"/>
[ComImport,
    Guid("894AD3B0-EF97-11CE-9BC9-00AA00608E01"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public interface IOleUndoUnit
{
    /// <summary>
    ///  Instructs the undo unit to carry out its action.
    /// </summary>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nf-ocidl-ioleundounit-do"/>
    void Do(
        IOleUndoManager pUndoManager);

    /// <summary>
    ///  Retrieves a description of the undo unit that can be used in the undo or redo user interface.
    /// </summary>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nf-ocidl-ioleundounit-getdescription"/>
    string GetDescription();

    /// <summary>
    ///  Retrieves the CLSID and a type identifier for the undo unit.
    /// </summary>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nf-ocidl-ioleundounit-getunittype"/>
    void GetUnitType(
        out Guid pClsid,
        out int plID);

    /// <summary>
    ///  Notifies the last undo unit in the collection that a new unit has been added.
    /// </summary>
    /// <see cref="https://docs.microsoft.com/windows/win32/api/ocidl/nf-ocidl-ioleundounit-onnextadd"/>
    void OnNextAdd();
}