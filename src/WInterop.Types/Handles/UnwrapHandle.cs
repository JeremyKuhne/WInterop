﻿// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.InteropServices;

namespace WInterop.Handles.Types
{
    /// <summary>
    /// Use to scope unwrapping safe handles.
    /// </summary>
    public struct UnwrapHandle : IDisposable
    {
        private SafeHandle _handle;
        private bool _refCounted;

        public UnwrapHandle(SafeHandle handle)
        {
            _handle = handle;
            _refCounted = false;
            _handle?.DangerousAddRef(ref _refCounted);
        }

        public static implicit operator IntPtr(UnwrapHandle handle) => handle._handle?.DangerousGetHandle() ?? IntPtr.Zero;

        public void Dispose()
        {
            if (_refCounted)
                _handle.DangerousRelease();
        }
    }
}
