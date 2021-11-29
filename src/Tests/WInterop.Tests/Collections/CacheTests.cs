﻿// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using FluentAssertions;
using WInterop.Support.Collections;
using Xunit;

namespace CollectionTests;

public class CacheTests
{
    public class TestItem : IDisposable
    {
        public void Dispose() { }
    }

    public class TestCache : Cache<TestItem>
    {
        public TestCache(int cacheSpace) : base(cacheSpace)
        {
        }

        public TestItem[] Cache
        {
            get { return this._itemsCache; }
        }

        public int CachedCount
        {
            get
            {
                int count = 0;
                foreach (var item in Cache)
                    if (item != null) count++;
                return count;
            }
        }
    }

    [Fact]
    public void CachedItemCountTest()
    {
        using var cache = new TestCache(5);
        TestItem item = new();
        for (int i = 0; i < 7; i++)
        {
            cache.Release(item);
        }

        cache.CachedCount.Should().Be(5);
    }

    [Fact]
    public void GetCachedItem()
    {
        using var cache = new TestCache(5);
        TestItem item = new();
        cache.Release(item);
        cache.Acquire().Should().BeSameAs(item);
        cache.Acquire().Should().NotBeSameAs(item);
    }

    [Fact]
    public void CachedItemParallelCountTest()
    {
        using var cache = new TestCache(5);
        TestItem item = new();
        Parallel.For(0, 5, (i) => cache.Release(item));
        cache.CachedCount.Should().Be(5);
    }

    [Fact]
    public void NonDisposableContent()
    {
        using var cache = new Cache<object>(1);
        cache.Release(new object());
        cache.Release(new object());
    }
}
