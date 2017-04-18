// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Diagnostics;

namespace WInterop.Handles.DataTypes
{
    [DebuggerDisplay("{Name} {TypeName}")]
    public struct ObjectInformation
    {
        public string Name;
        public string TypeName;
    }
}
