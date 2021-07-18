// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Errors;

namespace WInterop.Com.Native
{
    public unsafe partial struct IDropSource
    {
        public unsafe struct VTable
        {
            public IUnknown.VTable UnknownVTable;

            public delegate* unmanaged<void*, BOOL, uint, HResult> QueryContinueDrag;
            public delegate* unmanaged<void*, DropEffect, HResult> GiveFeedback;
        }
    }
}