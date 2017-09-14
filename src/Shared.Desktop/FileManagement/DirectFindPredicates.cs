// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

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
        unsafe bool Match(FILE_FULL_DIR_INFORMATION* record);
    }

    public class DosFilterPredicate : IDirectFindFilter
    {
        private StringBuffer _buffer;
        private bool _ignoreCase;

        public DosFilterPredicate(string filter, bool ignoreCase)
        {
            ProcessFilter(filter, ignoreCase);
            _ignoreCase = ignoreCase;
        }

        public unsafe bool Match(FILE_FULL_DIR_INFORMATION* record)
        {
            if (_buffer == null)
                return true;

            UNICODE_STRING name = new UNICODE_STRING((char*)&record->FileName, record->FileNameLength);
            UNICODE_STRING filter = _buffer.ToUnicodeString();
            return FileMethods.Imports.RtlIsNameInExpression(&filter, &name, _ignoreCase, IntPtr.Zero);
        }

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
