// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace WInterop.Windows;

/// <summary>
///  Digitizer input flags returned by <see cref="SystemMetric.Digitizer"/>
/// </summary>
[Flags]
public enum NaturalInputDevice
{
    /// <summary>
    ///  [NID_INTEGRATED_TOUCH]
    /// </summary>
    IntegratedTouch = 0x00000001,

    /// <summary>
    ///  [NID_EXTERNAL_TOUCH]
    /// </summary>
    ExternalTouch = 0x00000002,

    /// <summary>
    ///  [NID_INTEGRATED_PEN]
    /// </summary>
    IntegratedPen = 0x00000004,

    /// <summary>
    ///  [NID_EXTERNAL_PEN]
    /// </summary>
    ExternalPen = 0x00000008,

    /// <summary>
    ///  [NID_MULTI_INPUT]
    /// </summary>
    MultiInput = 0x00000040,

    /// <summary>
    ///  [NID_READY]
    /// </summary>
    Ready = 0x00000080
}