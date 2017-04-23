// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows.DataTypes
{
    /// <summary>
    /// Simple wrapper for an ATOM
    /// </summary>
    public struct Atom
    {
        // #define MAXINTATOM 0xC000
        // #define MAKEINTATOM(i)  (LPTSTR)((ULONG_PTR)((WORD)(i)))
        // #define INVALID_ATOM ((ATOM)0)

        // Strange uses for window class atoms
        // https://blogs.msdn.microsoft.com/oldnewthing/20080501-00/?p=22503/

        public uint ATOM;

        public Atom(uint atom)
        {
            ATOM = atom;
        }

        public bool IsValid
        {
            get { return ATOM != 0; }
        }

        public static bool IsAtom(IntPtr pointer)
        {
            // While MAXINTATOM is defined at 0xC000, this is not actually the maximum.
            // Any INTRESOURCE value is possible.

            // IS_INTRESOURCE(_r) ((((ULONG_PTR)(_r)) >> 16) == 0)
            ulong value = (ulong)pointer.ToInt64();
            return value != 0 && value >> 16 == 0;
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
