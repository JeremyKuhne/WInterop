// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.ProcessAndThreads;

/// <summary>
///  Simple struct to encapsulate a module handle (Handle). Use this for APIs that do NOT
///  increment the reference count when creating a handle. Use SafeModuleHandle for those
///  that DO increment the ref count.
/// </summary>
public struct ProcessHandle
{
    public IntPtr HANDLE;

    public static ProcessHandle Null = new(IntPtr.Zero);

    public ProcessHandle(IntPtr hprocess)
    {
        HANDLE = hprocess;
    }

    public static implicit operator IntPtr(ProcessHandle handle)
    {
        return handle.HANDLE;
    }

    public static implicit operator SafeProcessHandle(ProcessHandle handle)
    {
        return new SafeProcessHandle(handle.HANDLE, ownsHandle: false);
    }
}