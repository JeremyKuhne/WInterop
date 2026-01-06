// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions.Equivalency;
using System.Runtime.CompilerServices;

namespace WInterop.Tests;

internal static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        AssertionOptions.AssertEquivalencyUsing(e => e.ExcludingRefStructs());
    }

    public static TSelf ExcludingRefStructs<TSelf>(this SelfReferenceEquivalencyAssertionOptions<TSelf> options)
        where TSelf : SelfReferenceEquivalencyAssertionOptions<TSelf>
    {
        return options.Excluding(e => e.Type.IsByRefLike);
    }
}
