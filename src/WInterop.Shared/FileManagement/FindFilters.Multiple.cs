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
        /// Allows combining multiple filters. Runs through filters in order until
        /// a filter rejects the result (returns false).
        /// </summary>
        public class Multiple : IFindFilter
        {
            private IFindFilter[] _filters;

            public Multiple(params IFindFilter[] filters)
            {
                _filters = filters;
            }

            public unsafe bool Match(ref RawFindData record)
            {
                foreach (var filter in _filters)
                {
                    if (!filter.Match(ref record))
                        return false;
                }
                return true;
            }
        }
    }
}
