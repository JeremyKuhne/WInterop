// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.GdiPlus
{
    public enum SmoothingMode
    {
        Invalid = QualityMode.Invalid,
        Default = QualityMode.Default,
        HighSpeed = QualityMode.Low,
        HighQuality = QualityMode.High,
        None,
        AntiAlias,
        AntiAlias8x4 = AntiAlias,
        AntiAlias8x8
    }
}
