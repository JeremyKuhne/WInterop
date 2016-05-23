// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.Desktop
{
    /// <summary>
    /// Simple wrapper for an ATOM
    /// </summary>
    public struct Atom
    {
        public uint ATOM;

        public Atom(uint atom)
        {
            ATOM = atom;
        }

        public bool IsValid
        {
            get { return ATOM != 0; }
        }

        static public implicit operator uint(Atom atom)
        {
            return atom.ATOM;
        }

        static public implicit operator IntPtr(Atom atom)
        {
            return (IntPtr)atom.ATOM;
        }
    }
}
