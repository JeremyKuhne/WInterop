// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Support.Buffers
{
    public unsafe struct NativeString
    {
        public char* Value;
        public int Length;

        public override string ToString() => new string(Value, 0, Length);
    }
}
