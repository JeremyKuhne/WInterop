// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Com.Native
{
    [StructLayout(LayoutKind.Explicit)]
    public struct VARIANT
    {
        [FieldOffset(0)]
        public VariantData Data;

        // "decimal" is the same size (16 bytes) and layout as DECIMAL. The first ushort of space is "reserved" and
        // that is actually used to allow overlapping with the VARTYPE in the main part of the union. .NET has the
        // exact same layout for System.Decimal.

        [FieldOffset(0)]
        public decimal decVal;

        public struct VariantData
        {
            // The first four values are 8 bytes total (4 ushorts).
            public VariantType vt;
            public ushort wReserved1;
            public ushort wReserved2;
            public ushort wReserved3;

            // The union that actually holds the data (other than decimal) has as its largest value the BRECORD, which
            // is two pointers. This makes the union 8 bytes on 32 bit and 16 bytes on 64 bit. Together with the 8
            // bytes above this gives the total struct size of 16/24 bytes on 32/64 bit. (Decimal is unioned with this,
            // but isn't any larger than this fork of the union.)
            public UnionType Value;

            [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
            public unsafe struct UnionType
            {
                [FieldOffset(0)]
                public long llVal;

                [FieldOffset(0)]
                public int lVal;

                [FieldOffset(0)]
                public byte bVal;

                [FieldOffset(0)]
                public short iVal;

                [FieldOffset(0)]
                public float fltVal;

                [FieldOffset(0)]
                public double dblVal;

                // VARIANT_BOOL is a short
                [FieldOffset(0)]
                public short boolVal;

                // SCODE is an int
                [FieldOffset(0)]
                public int scode;

                // CY is a long
                [FieldOffset(0)]
                public long cyVal;

                // DATE is a double
                [FieldOffset(0)]
                public double date;

                // BSTR is a char* (null terminated)
                [FieldOffset(0)]
                public char* bstrVal;

                // punkVal is IUnknown*
                [FieldOffset(0)]
                public void* punkVal;

                // pdispVal is IDispatch
                [FieldOffset(0)]
                public void* pdispVal;

                [FieldOffset(0)]
                public SAFEARRAY* parray;

                [FieldOffset(0)]
                public byte* pbVal;

                [FieldOffset(0)]
                public short* piVal;

                [FieldOffset(0)]
                public int* plVal;

                [FieldOffset(0)]
                public long* pllVal;

                [FieldOffset(0)]
                public float* pfltVal;

                [FieldOffset(0)]
                public double* pdblVal;

                [FieldOffset(0)]
                public short* pboolVal;

                // SCODE is an int
                [FieldOffset(0)]
                public int* pscode;

                // CY is a long
                [FieldOffset(0)]
                public long* pcyVal;

                // DATE is a double
                [FieldOffset(0)]
                public double* pdate;

                // BSTR is a char* (null terminated)
                [FieldOffset(0)]
                public char** pbstrVal;

                // ppunkVal is IUnknown**
                [FieldOffset(0)]
                public void** ppunkVal;

                // ppdispVal is IDispatch**
                [FieldOffset(0)]
                public void** ppdispVal;

                [FieldOffset(0)]
                public SAFEARRAY** pparray;

                [FieldOffset(0)]
                public VARIANT* pvarVal;

                [FieldOffset(0)]
                public void* byref;

                [FieldOffset(0)]
                public sbyte cVal;

                [FieldOffset(0)]
                public ushort uiVal;

                [FieldOffset(0)]
                public uint ulVal;

                [FieldOffset(0)]
                public ulong ullVal;

                [FieldOffset(0)]
                public int intVal;

                [FieldOffset(0)]
                public uint uintVal;

                [FieldOffset(0)]
                public decimal* pdecVal;

                [FieldOffset(0)]
                public sbyte* pcVal;

                [FieldOffset(0)]
                public ushort* puiVal;

                [FieldOffset(0)]
                public uint* pulVal;

                [FieldOffset(0)]
                public ulong* pullVal;

                [FieldOffset(0)]
                public int* pintVal;

                [FieldOffset(0)]
                public uint* puintVal;

                [FieldOffset(0)]
                public BRECORD brecord;

                public unsafe struct BRECORD
                {
                    public void* pvRecord;

                    // pRecInfo is IRecordInfo*
                    public void* pRecInfo;
                }
            }
        }
    }
}