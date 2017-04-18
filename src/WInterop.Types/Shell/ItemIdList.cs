// ------------------------
//    WInterop Framework
// ------------------------

// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WInterop.Com.DataTypes;
using WInterop.Support.Buffers;

namespace WInterop.Shell.DataTypes
{
    public class ItemIdList : SafeComHandle
    {
        public ItemIdList() : this (ownsHandle: true)
        {
        }

        public ItemIdList(bool ownsHandle) : base(ownsHandle)
        {
        }
    }
}
