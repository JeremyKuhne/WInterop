// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.File.Types;
using WInterop.Support.Buffers;

namespace WInterop.File
{
    public static partial class FindFilters
    {
        /// <summary>
        /// Match file names via the traditional DOS matching.
        /// ("*.*" matches everything, "foo*." matches all files without an extension that begin with foo, etc.)
        /// </summary>
        public class DosMatch : IFindFilter
        {
            private string _filter;
            private bool _ignoreCase;

            public DosMatch(string filter, bool ignoreCase)
            {
                _filter = ProcessFilter(filter);
                _ignoreCase = ignoreCase;
            }

            public unsafe bool Match(ref RawFindData findData)
            {
                if (_filter == null)
                    return true;

                // RtlIsNameInExpression is the native API DosMatcher is replicating.
                return DosMatcher.MatchPattern(_filter, findData.FileName, _ignoreCase);
            }

            private unsafe string ProcessFilter(string filter)
            {
                // These always match
                if (string.IsNullOrEmpty(filter) || filter == "*" || filter == "*.*")
                    return null;

                return BufferHelper.BufferInvoke((StringBuffer buffer) =>
                {
                    int length = filter.Length;
                    fixed (char* c = filter)
                    {
                        for (int i = 0; i < length; i++)
                        {
                            switch (c[i])
                            {
                                case '.':
                                    if (i > 0 && i == length - 1 && c[i - 1] == '*')
                                    {
                                        buffer.Length--;
                                        buffer.Append('<'); // DOS_STAR (ends in *.)
                                    }
                                    else if (i < length - 1 && (c[i + 1] == '?' || c[i + 1] == '*'))
                                    {
                                        buffer.Append('\"'); // DOS_DOT
                                        i++;
                                    }
                                    else
                                    {
                                        buffer.Append('.');
                                    }
                                    break;
                                case '?':
                                    buffer.Append('>'); // DOS_QM
                                    break;
                                default:
                                    buffer.Append(c[i]);
                                    break;
                            }
                        }
                    }

                    return buffer.ToString();
                });
            }
        }
    }
}
