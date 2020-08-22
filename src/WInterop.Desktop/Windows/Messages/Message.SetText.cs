// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using WInterop.Support;

namespace WInterop.Windows
{
    public static partial class Message
    {
        public readonly ref struct SetText
        {
            public ReadOnlySpan<char> Text { get; }

            public unsafe SetText(LParam lParam)
            {
                Text = Strings.GetSpanFromNullTerminatedBuffer((char*)lParam);
            }
        }
    }
}
