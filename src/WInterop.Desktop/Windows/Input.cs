// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.InteropServices;

namespace WInterop.Windows;

/// <summary>
///  [INPUT]
/// </summary>
// https://msdn.microsoft.com/en-us/library/windows/desktop/ms646270.aspx
public struct Input
{
    public InputType Type;
    public InputUnion Data;

    [StructLayout(LayoutKind.Explicit)]
    public struct InputUnion
    {
        [FieldOffset(0)]
        public MouseInput MouseInput;
        [FieldOffset(0)]
        public KeyboardInput KeyboardInput;
        [FieldOffset(0)]
        public HardwareInput HardwareInput;
    }
}