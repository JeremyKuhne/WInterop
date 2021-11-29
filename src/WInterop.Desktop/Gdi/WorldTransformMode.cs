// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Gdi;

// https://msdn.microsoft.com/en-us/library/dd145060.aspx
public enum WorldTransformMode : uint
{
    Identity = 1,
    LeftMultiply = 2,
    RightMultiply = 3
}