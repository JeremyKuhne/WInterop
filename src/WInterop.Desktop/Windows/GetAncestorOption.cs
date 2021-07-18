// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public enum GetAncestorOption : uint
    {
        /// <summary>
        ///  Retrieves the parent window. This does not include the owner, as it does with the GetParent function.
        ///  [GA_PARENT]
        /// </summary>
        Parent = 1,

        /// <summary>
        ///  Retrieves the root window by walking the chain of parent windows.
        ///  []
        /// </summary>
        Root = 2,

        /// <summary>
        ///  Retrieves the owned root window by walking the chain of parent and owner windows returned by GetParent.
        ///  []
        /// </summary>
        RootOwner = 3
    }
}