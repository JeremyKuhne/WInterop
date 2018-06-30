// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.File.Types;

namespace WInterop.File
{
    public static partial class FindFilters
    {
        /// <summary>
        /// Filters out current "." and previous ".." directory entries.
        /// </summary>
        public class NormalDirectory : IFindFilter
        {
            public static NormalDirectory Instance = new NormalDirectory();

            private NormalDirectory() { }

            public unsafe bool Match(ref RawFindData findData) => NotSpecialDirectory(ref findData);

            public static unsafe bool NotSpecialDirectory(ref RawFindData findData)
            {
                return findData.FileName.Length > 2
                    || findData.FileName[0] != '.'
                    || (findData.FileName.Length == 2 && findData.FileName[1] != '.');
            }
        }
    }
}
