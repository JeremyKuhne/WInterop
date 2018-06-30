// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Storage.Types;

namespace WInterop.Storage
{
    public static partial class FindTransforms
    {
        /// <summary>
        /// Returns find result data in FindResult.
        /// </summary>
        public class ToFindResult : IFindTransform<FindResult>
        {
            private ToFindResult() { }
            public static ToFindResult Instance = new ToFindResult();

            public unsafe FindResult TransformResult(ref RawFindData findData) => new FindResult(ref findData);
        }
    }
}
