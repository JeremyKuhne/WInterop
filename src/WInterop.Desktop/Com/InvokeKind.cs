// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com;

/// <summary>
///  [INVOKEKIND]
/// </summary>
public enum InvokeKind
{
    /// <summary>
    ///  [INVOKE_FUNC]
    /// </summary>
    Function = INVOKEKIND.INVOKE_FUNC,

    /// <summary>
    ///  [INVOKE_PROPERTYGET]
    /// </summary>
    PropertyGet = INVOKEKIND.INVOKE_PROPERTYGET,

    /// <summary>
    ///  [INVOKE_PROPERTYPUT]
    /// </summary>
    PropertyPut = INVOKEKIND.INVOKE_PROPERTYPUT,

    /// <summary>
    ///  [INVOKE_PROPERTYPUTREF]
    /// </summary>
    PropertyPutRef = INVOKEKIND.INVOKE_PROPERTYPUTREF
}