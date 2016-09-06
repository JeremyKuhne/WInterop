// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WInterop.Shell.DataTypes
{
    /// <summary>
    /// Simple holder for SHITEMID.
    /// </summary>
    // https://msdn.microsoft.com/en-us/library/windows/desktop/bb759800.aspx
    public struct ShellItemId
    {
        public byte[] Id;
    }
}
