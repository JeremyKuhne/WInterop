// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Com
{
    /// <summary>
    ///  [INVOKEKIND]
    /// </summary>
    public enum InvokeKind
    {
        /// <summary>
        ///  [INVOKE_FUNC]
        /// </summary>
        Function = 1,

        /// <summary>
        ///  [INVOKE_PROPERTYGET]
        /// </summary>
        PropertyGet = 2,

        /// <summary>
        ///  [INVOKE_PROPERTYPUT]
        /// </summary>
        PropertyPut = 4,

        /// <summary>
        ///  [INVOKE_PROPERTYPUTREF]
        /// </summary>
        PropertyPutRef = 8
    }
}