// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;
using WInterop.Errors;
using WInterop.Support;

namespace WInterop.Com.Native;

/// <summary>
///  Direct COM IDropSource method access.
/// </summary>
/// <remarks>
///  This must only be used via pointers. Cast a void* or IntPtr to IDropSource* and access via dereference.
/// </remarks>
/// <example>
///  IDropSource* dropSource = (IDropSource*)GetData(/* ... */);
///  uint refCount = unknown->AddRef();
/// </example>
public unsafe partial struct IDropSource
{
    public static readonly Guid IID_IDropSource = new Guid("00000121-0000-0000-C000-000000000046");

    private readonly VTable* _vtable;

    public HResult QueryInterface(Guid* riid, void** ppvObject)
        => _vtable->UnknownVTable.QueryInterface(Unsafe.AsPointer(ref this), riid, ppvObject);

    public uint AddRef()
        => _vtable->UnknownVTable.AddRef(Unsafe.AsPointer(ref this));

    public uint Release()
        => _vtable->UnknownVTable.Release(Unsafe.AsPointer(ref this));

    public HResult QueryContinueDrag(bool fEscapePressed, KeyState grfKeyState)
        => _vtable->QueryContinueDrag(Unsafe.AsPointer(ref this), fEscapePressed.ToBOOL(), (uint)grfKeyState);

    public HResult GiveFeedback(DropEffect dwEffect)
        => _vtable->GiveFeedback(Unsafe.AsPointer(ref this), dwEffect);
}