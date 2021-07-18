// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Com.Native
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public unsafe struct VARDESC
    {
        public MemberId memid;
        public char* lpstrSchema;
        public UnionType Union;

        [StructLayout(LayoutKind.Explicit)]
        public struct UnionType
        {
            [FieldOffset(0)]
            public uint oInst;

            [FieldOffset(0)]
            public VARIANT* lpvarValue;
        }

        public ELEMDESC elemdescVar;
        public VariableFlags wVarFlags;
        public VariableKind varkind;
    }
}