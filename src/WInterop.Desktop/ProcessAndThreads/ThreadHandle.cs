// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.Handles;

namespace WInterop.ProcessAndThreads;

/// <summary>
///  Safe handle for a thread.
/// </summary>
public class ThreadHandle : CloseHandle
{
    public ThreadHandle() : base() { }
}