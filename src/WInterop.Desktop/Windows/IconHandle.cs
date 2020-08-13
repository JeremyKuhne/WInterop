// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Windows;
using WInterop.Windows.Native;

namespace WInterop.Windows
{
    public readonly struct IconHandle : IDisposable
    {
        public HICON HICON { get; }
        private readonly bool _ownsHandle;

        /// <summary>
        ///  Used to specifiy that you don't want a default icon picked in WInterop method calls.
        /// </summary>
        public static IconHandle NoIcon = new IconHandle(new HICON((IntPtr)(-1)));

        public IconHandle(HICON handle, bool ownsHandle = true)
        {
            HICON = handle;
            _ownsHandle = ownsHandle;
        }

        public bool IsInvalid => HICON.IsInvalid;

        public void Dispose()
        {
            if (_ownsHandle)
                Imports.DestroyIcon(HICON);
        }

        public static implicit operator HICON(IconHandle handle) => handle.HICON;
        public static implicit operator LResult(IconHandle handle) => handle.HICON.Value;
        public static implicit operator IconHandle(IconId id) => Windows.LoadIcon(id);

        public override bool Equals(object? obj) => obj is IconHandle other ? other.HICON == HICON : false;
        public bool Equals(IconHandle other) => other.HICON == HICON;
        public static bool operator ==(IconHandle a, IconHandle b) => a.HICON == b.HICON;
        public static bool operator !=(IconHandle a, IconHandle b) => a.HICON != b.HICON;
        public override int GetHashCode() => HICON.GetHashCode();
    }
}
