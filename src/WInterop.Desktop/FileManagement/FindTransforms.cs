// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// #define WIN32FIND

using System;
using System.Collections.Generic;
using System.Text;
using WInterop.FileManagement.Types;
using WInterop.Support;

namespace WInterop.FileManagement
{
    public interface IFindTransform<T>
    {
        unsafe T TransformResult(ref RawFindData record, string directory);
    }

    public static class FindTransforms
    {
        public class ToFullPath : IFindTransform<string>
        {
            private ToFullPath() { }
            public static ToFullPath Instance = new ToFullPath();

            public unsafe string TransformResult(ref RawFindData record, string directory)
            {
                int totalLength = record.FileName.Length + directory.Length + 1;
                string fullPath = new string('\0', totalLength);
                fixed (char* f = fullPath)
                fixed (char* d = directory)
                {
                    Buffer.MemoryCopy(d, f, fullPath.Length * sizeof(char), directory.Length * sizeof(char));
                    f[directory.Length] = Paths.DirectorySeparator;
                    Buffer.MemoryCopy((void*)record.FileName.Value, f + directory.Length + 1, fullPath.Length * sizeof(char), record.FileName.Length * sizeof(char));
                }
                return fullPath;
            }
        }

        public class ToFindResult : IFindTransform<FindResult>
        {
            private ToFindResult() { }
            public static ToFindResult Instance = new ToFindResult();

            public unsafe FindResult TransformResult(ref RawFindData record, string directory) => new FindResult(ref record, directory);
        }
    }
}
