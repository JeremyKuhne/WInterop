// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

.class public abstract sealed WInterop.Support.Structs
{
    .method public hidebysig static !!T* AddressOf<valuetype .ctor T> (
            !!T& target
        ) cil managed
    {
        .maxstack 1
        ldarg.0
        ret
    }

    .method public hidebysig static void NoInit<valuetype .ctor (System.ValueType) T> (
            [out] !!T& target
        ) cil managed 
    {
        .maxstack 0
        ret
    }

    .method public hidebysig static uint32 SizeInBytes<valuetype .ctor (System.ValueType) T> () cil managed
    {
        .maxstack 1
        sizeof !!T
        ret
    }

    .method public hidebysig static uint32 SizeInBytes<valuetype .ctor (System.ValueType) T> (
        !!T& target) cil managed
    {
        .maxstack 1
        sizeof !!T
        ret
    }
}
