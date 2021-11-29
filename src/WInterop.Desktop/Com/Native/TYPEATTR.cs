// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;
using WInterop.Globalization;

namespace WInterop.Com.Native;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public unsafe struct TYPEATTR
{
    public Guid guid;
    public LocaleId lcid;
    public uint dwReserved;
    public MemberId memidConstructor;
    public MemberId memidDestructor;
    public char* lpstrSchema;
    public uint cbSizeInstance;
    public TypeKind typekind;
    public ushort cFuncs;
    public ushort cVars;
    public ushort cImplTypes;
    public ushort cbSizeVft;
    public ushort cbAlignment;
    public TypeFlags wTypeFlags;
    public ushort wMajorVerNum;
    public ushort wMinorVerNum;
    public TYPEDESC tdescAlias;
    public IdlDescription idldescType;
}