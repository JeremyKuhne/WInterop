// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// #define WIN32FIND

using System;
using WInterop.FileManagement.Types;
using WInterop.SafeString;
using WInterop.SafeString.Types;
using WInterop.Support;
using WInterop.Support.Buffers;

namespace WInterop.FileManagement
{
    public interface IDirectFindFilter
    {
#if WIN32FIND
        bool Match(ref WIN32_FIND_DATA record);
#else
        unsafe bool Match(FILE_FULL_DIR_INFORMATION* record);
#endif
    }

    public class MultiFilter : IDirectFindFilter
    {
        private IDirectFindFilter[] _filters;

        public MultiFilter(params IDirectFindFilter[] filters)
        {
            _filters = filters;
        }


#if WIN32FIND
        public bool Match(ref WIN32_FIND_DATA record)
        {
            foreach (var filter in _filters)
            {
                if (!filter.Match(ref record))
                    return false;
            }
            return true;
        }

#else
        public unsafe bool Match(FILE_FULL_DIR_INFORMATION* record)
        {
            foreach (var filter in _filters)
            {
                if (!filter.Match(record))
                    return false;
            }
            return true;
        }
#endif
    }

    public class NormalDirectoryFilter : IDirectFindFilter
    {
        public static NormalDirectoryFilter Instance = new NormalDirectoryFilter();

        private NormalDirectoryFilter() { }

#if WIN32FIND
        public bool Match(ref WIN32_FIND_DATA record)
        {
            return !record.cFileName.Equals(".") && !record.cFileName.Equals("..");
        }

#else
        public unsafe bool Match(FILE_FULL_DIR_INFORMATION* record) => NotSpecialDirectory(record);

        public static unsafe bool NotSpecialDirectory(FILE_FULL_DIR_INFORMATION* record)
        {
            return record->FileNameLength > 2 * sizeof(char)
                || *(char*)&record->FileName != '.'
                || (record->FileNameLength == 2 * sizeof(char) && *((char*)&record->FileName + 1) != '.');
        }
#endif
    }

    public class DosFilter : IDirectFindFilter
    {
        private StringBuffer _buffer;
        private bool _ignoreCase;

        public DosFilter(string filter, bool ignoreCase)
        {
            ProcessFilter(filter, ignoreCase);
            _ignoreCase = ignoreCase;
        }

#if WIN32FIND
        public unsafe bool Match(ref WIN32_FIND_DATA record)
        {
            if (_buffer == null)
                return true;

            fixed (void* v = &record.cFileName)
            {
                char* c = (char*)v;
                uint count = 0;
                while (*c != '\0') { c++; count++; }
                UNICODE_STRING name = new UNICODE_STRING((char*)v, count * sizeof(char));
                UNICODE_STRING filter = _buffer.ToUnicodeString();
                return FileMethods.Imports.RtlIsNameInExpression(&filter, &name, _ignoreCase, IntPtr.Zero);
            }
        }
#else
        public unsafe bool Match(FILE_FULL_DIR_INFORMATION* record)
        {
            if (_buffer == null)
                return true;

            UNICODE_STRING name = new UNICODE_STRING((char*)&record->FileName, record->FileNameLength);
            UNICODE_STRING filter = _buffer.ToUnicodeString();
            return FileMethods.Imports.RtlIsNameInExpression(&filter, &name, _ignoreCase, IntPtr.Zero);
        }
#endif

        private unsafe void ProcessFilter(string filter, bool ignoreCase)
        {
            if (string.IsNullOrEmpty(filter))
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
