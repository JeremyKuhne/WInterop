// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows
{
    public static partial class Message
    {
        public readonly ref struct Size
        {
            public System.Drawing.Size NewSize { get; }
            public SizeType SizeType { get; }

            public Size(WParam wParam, LParam lParam)
            {
                NewSize = new System.Drawing.Size(lParam.LowWord, lParam.HighWord);
                SizeType = (SizeType)(int)wParam;
            }

            private Size(System.Drawing.Size size)
            {
                NewSize = size;
                SizeType = SizeType.MaxShow;
            }

            public static Size FromDrawingSize(System.Drawing.Size size)
            {
                return new Size(size);
            }
        }
    }
}
