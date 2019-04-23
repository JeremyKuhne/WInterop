// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Storage;
using WInterop.Support;

namespace WInterop.Storage
{
    public static partial class FindTransforms
    {
        /// <summary>
        /// Returns just the file name for find results.
        /// </summary>
        public class ToFileName : IFindTransform<string>
        {
            private ToFileName() { }
            public static ToFileName Instance = new ToFileName();

            public unsafe string TransformResult(ref RawFindData findData)
            {
                return findData.FileName.CreateString();
            }
        }
	}
}
