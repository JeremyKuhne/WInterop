// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.FileManagement.Types;

namespace WInterop.FileManagement
{
    public static partial class FindFilters
    {
        /// <summary>
        /// Returns all results. (No filtering)
        /// </summary>
        public class All : IFindFilter
        {
            public static All Instance = new All();

            private All() { }

            public bool Match(ref RawFindData findData) => true;
        }
    }
}
