// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace System.Drawing
{
    public static class DrawingExtensions
    {
        public static bool Is(this Color color, Color value)
        {
            return color.ToArgb() == value.ToArgb();
        }
    }
}