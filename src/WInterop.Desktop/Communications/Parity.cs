// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Communications;

// https://msdn.microsoft.com/en-us/library/windows/desktop/aa363214.aspx
public enum Parity : byte
{
    None = 0,
    Odd = 1,
    Even = 2,
    Mark = 3,
    Space = 4
}