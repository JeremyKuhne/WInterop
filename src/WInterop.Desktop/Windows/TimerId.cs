// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop.Windows;

public readonly struct TimerId
{
    public static TimerId Null => default;

    public UIntPtr Value { get; }

    public TimerId(uint id) => Value = (UIntPtr)id;

    public override bool Equals(object? obj) => obj is TimerId other && other.Value == Value;

    public bool Equals(TimerId other) => other.Value == Value;

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(TimerId a, TimerId b) => a.Value == b.Value;

    public static bool operator !=(TimerId a, TimerId b) => a.Value != b.Value;

    public static implicit operator TimerId(uint value) => new TimerId(value);
}