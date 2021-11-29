// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace WInterop;

internal interface IHandle<T> where T : unmanaged
{
    unsafe T* Handle { get; }
}
