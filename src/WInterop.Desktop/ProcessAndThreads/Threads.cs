// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using WInterop.ProcessAndThreads.Native;

namespace WInterop.ProcessAndThreads
{
    public static class Threads
    {
        public static ThreadHandle GetCurrentThread() => ProcessAndThreadImports.GetCurrentThread();

        public static uint GetCurrentThreadId() => ProcessAndThreadImports.GetCurrentThreadId();
    }
}