// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.CompilerServices;

namespace WInterop.Support;

public static class Enums
{
    /// <summary>
    ///  Returns true if the given flag or flags are set.
    /// </summary>
    /// <remarks>
    ///  Simple wrapper for <see cref="Enum.HasFlag(Enum)"/> that gives you better intellisense.
    /// </remarks>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static unsafe bool AreFlagsSet<T>(this ref T value, T flags) where T : unmanaged, Enum => value.HasFlag(flags);

    /// <summary>
    ///  Sets the given flag or flags.
    /// </summary>
    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static unsafe void SetFlags<T>(this ref T value, T flags) where T : unmanaged, Enum
    {
        fixed (T* v = &value)
        {
            // Note that the non-relevant if clauses will be omitted by the JIT so these become one statement.
            if (sizeof(T) == sizeof(byte))
            {
                *(byte*)v |= *(byte*)&flags;
            }
            else if (sizeof(T) == sizeof(short))
            {
                *(short*)v |= *(short*)&flags;
            }
            else if (sizeof(T) == sizeof(int))
            {
                *(int*)v |= *(int*)&flags;
            }
            else if (sizeof(T) == sizeof(long))
            {
                *(long*)v |= *(long*)&flags;
            }
        }
    }

    [SkipLocalsInit]
    [MethodImpl(MethodImplOptions.AggressiveOptimization | MethodImplOptions.AggressiveInlining)]
    public static unsafe void ClearFlags<T>(this ref T value, T flags) where T : unmanaged, Enum
    {
        fixed (T* v = &value)
        {
            // Note that the non-relevant if clauses will be omitted by the JIT so these become one statement.
            if (sizeof(T) == sizeof(byte))
            {
                *(byte*)v &= (byte)~*(byte*)&flags;
            }
            else if (sizeof(T) == sizeof(short))
            {
                *(short*)v &= (short)~*(short*)&flags;
            }
            else if (sizeof(T) == sizeof(int))
            {
                *(int*)v &= ~*(int*)&flags;
            }
            else if (sizeof(T) == sizeof(long))
            {
                *(long*)v &= ~*(long*)&flags;
            }
        }
    }
}