// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Console
{
    /// <summary>
    ///  Console coordinate. [COORD]
    /// </summary>
    /// <msdn><see cref="https://docs.microsoft.com/en-us/windows/console/coord-str"/></msdn>
    public readonly struct Coordinate
    {
        public readonly short X;
        public readonly short Y;
    }
}