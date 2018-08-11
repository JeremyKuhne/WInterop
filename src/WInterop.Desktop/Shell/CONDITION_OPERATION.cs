// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Shell
{
    public enum CONDITION_OPERATION
    {
        COP_IMPLICIT = 0,
        COP_EQUAL = (COP_IMPLICIT + 1),
        COP_NOTEQUAL = (COP_EQUAL + 1),
        COP_LESSTHAN = (COP_NOTEQUAL + 1),
        COP_GREATERTHAN = (COP_LESSTHAN + 1),
        COP_LESSTHANOREQUAL = (COP_GREATERTHAN + 1),
        COP_GREATERTHANOREQUAL = (COP_LESSTHANOREQUAL + 1),
        COP_VALUE_STARTSWITH = (COP_GREATERTHANOREQUAL + 1),
        COP_VALUE_ENDSWITH = (COP_VALUE_STARTSWITH + 1),
        COP_VALUE_CONTAINS = (COP_VALUE_ENDSWITH + 1),
        COP_VALUE_NOTCONTAINS = (COP_VALUE_CONTAINS + 1),
        COP_DOSWILDCARDS = (COP_VALUE_NOTCONTAINS + 1),
        COP_WORD_EQUAL = (COP_DOSWILDCARDS + 1),
        COP_WORD_STARTSWITH = (COP_WORD_EQUAL + 1),
        COP_APPLICATION_SPECIFIC = (COP_WORD_STARTSWITH + 1)
    }
}
