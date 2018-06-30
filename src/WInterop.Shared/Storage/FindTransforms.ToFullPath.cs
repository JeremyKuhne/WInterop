// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Storage.Types;
using WInterop.Support;

namespace WInterop.Storage
{
    public static partial class FindTransforms
    {
        /// <summary>
        /// Returns just the full path for find results.
        /// </summary>
        public class ToFullPath : IFindTransform<string>
        {
            private ToFullPath() { }
            public static ToFullPath Instance = new ToFullPath();

            public unsafe string TransformResult(ref RawFindData findData)
            {
                int totalLength = findData.FileName.Length + findData.Directory.Length + 1;
                string fullPath = new string('\0', totalLength);
                fixed (char* f = fullPath)
                {
                    Span<char> pathSpan = new Span<char>(f, totalLength);
                    ReadOnlySpan<char> directorySpan = findData.Directory.AsSpan();
                    directorySpan.CopyTo(pathSpan);
                    pathSpan[directorySpan.Length] = Paths.DirectorySeparator;
                    findData.FileName.CopyTo(pathSpan.Slice(directorySpan.Length + 1));
                }
                return fullPath;
            }
        }
	}
}
