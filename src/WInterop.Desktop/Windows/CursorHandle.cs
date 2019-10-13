// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public readonly struct CursorHandle : IDisposable
    {
        public HCURSOR HCURSOR { get; }
        private readonly bool _ownsHandle;

        /// <summary>
        /// Used to specifiy that you don't want a default cursor picked in WInterop method calls.
        /// </summary>
        public static CursorHandle NoCursor = new CursorHandle(new HCURSOR((IntPtr)(-1)));

        public CursorHandle(HCURSOR handle, bool ownsHandle = true)
        {
            HCURSOR = handle;
            _ownsHandle = ownsHandle;
        }

        public bool IsInvalid => HCURSOR.IsInvalid;

        public void Dispose()
        {
            if (_ownsHandle)
                Imports.DestroyCursor(HCURSOR);
        }

        public static implicit operator HCURSOR(CursorHandle handle) => handle.HCURSOR;
        public static implicit operator LResult(CursorHandle handle) => handle.HCURSOR.Value;
        public static implicit operator CursorHandle(CursorId id) => Windows.LoadCursor(id);

        public override bool Equals(object? obj) => obj is CursorHandle other ? other.HCURSOR == HCURSOR : false;
        public bool Equals(CursorHandle other) => other.HCURSOR == HCURSOR;
        public static bool operator ==(CursorHandle a, CursorHandle b) => a.HCURSOR == b.HCURSOR;
        public static bool operator !=(CursorHandle a, CursorHandle b) => a.HCURSOR != b.HCURSOR;
        public override int GetHashCode() => HCURSOR.GetHashCode();
    }
}
