// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Handles;

namespace WInterop.Modules;

/// <summary>
///  Module handle. This is synonymous with HINSTANCE and HMODULE.
/// </summary>
public class ModuleInstance : HandleZeroIsInvalid
{
    public static readonly ModuleInstance Null = new(IntPtr.Zero);

    public ModuleInstance() : this(ownsHandle: false) { }

    protected ModuleInstance(bool ownsHandle) : base(ownsHandle) { }

    public ModuleInstance(IntPtr handle, bool ownsHandle = false) : base(handle, ownsHandle) { }

    public static implicit operator ModuleInstance(IntPtr handle) => new(handle);

    public static ModuleInstance GetModuleForType(Type type)
        => Marshal.GetHINSTANCE(type.Module);

    internal unsafe HMODULE Handle => new((void*)handle);

    public static implicit operator HINSTANCE(ModuleInstance value) => value?.Handle ?? HINSTANCE.NULL;

    protected override bool ReleaseHandle()
    {
        // Module handles are ref counted with LoadLibrary/FreeLibrary.
        // We use a derived class to return a module handle from LoadLibary
        // that will decrement appropriately.
        return true;
    }
}