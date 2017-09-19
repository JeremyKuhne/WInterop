// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.FileManagement.Types;
using WInterop.SafeString;
using WInterop.SafeString.Types;
using WInterop.Support.Buffers;

namespace WInterop.FileManagement
{
    public interface IFindFilter
    {
        bool Match(ref RawFindData record);
    }

    public class MultiFilter : IFindFilter
    {
        private IFindFilter[] _filters;

        public MultiFilter(params IFindFilter[] filters)
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

    public class NormalDirectoryFilter : IFindFilter
    {
        public static NormalDirectoryFilter Instance = new NormalDirectoryFilter();

        private NormalDirectoryFilter() { }

        public unsafe bool Match(ref RawFindData record) => NotSpecialDirectory(ref record);

        public static unsafe bool NotSpecialDirectory(ref RawFindData record)
        {
            return record.FileName.Length > 2
                || *record.FileName.Value != '.'
                || (record.FileName.Length == 2 && *(record.FileName.Value + 1) != '.');
        }
    }

    public class DosFilter : IFindFilter
    {
        private StringBuffer _buffer;
        private bool _ignoreCase;

        public DosFilter(string filter, bool ignoreCase)
        {
            ProcessFilter(filter, ignoreCase);
            _ignoreCase = ignoreCase;
        }

        public unsafe bool Match(ref RawFindData record)
        {
            if (_buffer == null)
                return true;

            UNICODE_STRING name = new UNICODE_STRING(record.FileName.Value, (uint)(record.FileName.Length * sizeof(char)));
            UNICODE_STRING filter = _buffer.ToUnicodeString();
            return FileMethods.Imports.RtlIsNameInExpression(&filter, &name, _ignoreCase, IntPtr.Zero);
        }

        private unsafe void ProcessFilter(string filter, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(filter) || filter == "*" || filter == "*.*")
                return;

            int length = filter.Length;
            _buffer = new StringBuffer((uint)length);
            fixed(char* c = filter)
            {
                for (int i = 0; i < length; i++)
                {
                    switch (c[i])
                    {
                        case '.':
                            if (i > 1 && i == length - 1 && c[i - 1] == '*')
                            {
                                _buffer.Length--;
                                _buffer.Append('<'); // DOS_STAR (ends in *.)
                            }
                            else if (i < length - 1 && (c[i + 1] == '?' || c[i + 1] == '*'))
                            {
                                _buffer.Append('\"'); // DOS_DOT
                                i++;
                            }
                            else
                            {
                                _buffer.Append('.');
                            }
                            break;
                        case '?':
                            _buffer.Append('>'); // DOS_QM
                            break;
                        default:
                            _buffer.Append(c[i]);
                            break;
                    }
                }
            }

            UNICODE_STRING s = _buffer.ToUnicodeString();
            if (ignoreCase)
                StringMethods.ToUpperInvariant(ref s);
        }
    }
}
