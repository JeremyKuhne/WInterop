// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com
{
    /// <summary>
    ///  [HREFTYPE]
    /// </summary>
    /// <docs>https://docs.microsoft.com/en-us/openspecs/windows_protocols/ms-oaut/ed6620b1-6b23-4fa1-99e6-781832999f93</docs>
    public struct RefTypeHandle
    {
        public uint Value;

        public RefTypeHandle(uint id) => Value = id;

        public static implicit operator uint(RefTypeHandle id) => id.Value;
        public static implicit operator RefTypeHandle(uint id) => new RefTypeHandle(id);
    }
}
