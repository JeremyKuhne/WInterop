// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

/// <summary>
///  Simple wrapper for an ATOM
/// </summary>
public struct Atom
{
    // #define MAXINTATOM 0xC000
    // #define MAKEINTATOM(i)  (LPTSTR)((ULONG_PTR)((WORD)(i)))
    // #define INVALID_ATOM ((ATOM)0)

    // Strange uses for window class atoms
    // https://blogs.msdn.microsoft.com/oldnewthing/20080501-00/?p=22503/

    public ushort ATOM;

    public Atom(ushort atom) => ATOM = atom;

    public static Atom Null = new(0);

    public bool IsValid => ATOM != 0;

    public static bool IsAtom(nint pointer)
    {
        // While MAXINTATOM is defined at 0xC000, this is not actually the maximum.
        // Any INTRESOURCE value is possible.

        // IS_INTRESOURCE(_r) ((((ULONG_PTR)(_r)) >> 16) == 0)
        ulong value = (ulong)pointer;
        return value != 0 && value >> 16 == 0;
    }

    public static implicit operator uint(Atom atom) => atom.ATOM;
    public static implicit operator nint(Atom atom) => atom.ATOM;
    public static implicit operator Atom(ushort atom) => new(atom);
    public static implicit operator ushort(Atom atom) => atom.ATOM;
    public static implicit operator Atom(nint atom) => new((ushort)atom);
}