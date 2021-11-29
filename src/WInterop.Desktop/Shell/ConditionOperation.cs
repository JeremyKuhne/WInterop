// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Shell;

/// <summary>
///  [CONDITION_OPERATION]
/// </summary>
public enum ConditionOperation
{
    /// <summary>
    ///  [COP_IMPLICIT]
    /// </summary>
    Implicit = 0,

    /// <summary>
    ///  [COP_EQUAL]
    /// </summary>
    Equal = Implicit + 1,

    /// <summary>
    ///  [COP_NOTEQUAL]
    /// </summary>
    NotEqual = Equal + 1,

    /// <summary>
    ///  [COP_LESSTHAN]
    /// </summary>
    LessThan = NotEqual + 1,

    /// <summary>
    ///  [COP_GREATERTHAN]
    /// </summary>
    GreaterThan = LessThan + 1,

    /// <summary>
    ///  [COP_LESSTHANOREQUAL]
    /// </summary>
    LessThanOrEqual = GreaterThan + 1,

    /// <summary>
    ///  [COP_GREATERTHANOREQUAL]
    /// </summary>
    GreaterThanOrEqual = LessThanOrEqual + 1,

    /// <summary>
    ///  [COP_VALUE_STARTSWITH]
    /// </summary>
    ValueStartsWith = GreaterThanOrEqual + 1,

    /// <summary>
    ///  [COP_VALUE_ENDSWITH]
    /// </summary>
    ValueEndsWith = ValueStartsWith + 1,

    /// <summary>
    ///  [COP_VALUE_CONTAINS]
    /// </summary>
    ValueContains = ValueEndsWith + 1,

    /// <summary>
    ///  [COP_VALUE_NOTCONTAINS]
    /// </summary>
    ValueNotContains = ValueContains + 1,

    /// <summary>
    ///  [COP_DOSWILDCARDS]
    /// </summary>
    DosWildcards = ValueNotContains + 1,

    /// <summary>
    ///  [COP_WORD_EQUAL]
    /// </summary>
    WordEqual = DosWildcards + 1,

    /// <summary>
    ///  [COP_WORD_STARTSWITH]
    /// </summary>
    WordStartsWith = WordEqual + 1,

    /// <summary>
    ///  [COP_APPLICATION_SPECIFIC]
    /// </summary>
    ApplicationSpecific = WordStartsWith + 1
}