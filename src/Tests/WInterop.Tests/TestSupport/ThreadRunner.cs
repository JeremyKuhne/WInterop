// Copyright (c) Jeremy W. Kuhne. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Runtime.ExceptionServices;

namespace Tests.Shared.Support;

public class ThreadRunner
{
    private void InstanceRun(Action action)
    {
        Exception exception = null;
        void CatchException()
        {
            try
            {
                action();
            }
            catch (Exception inner)
            {
                exception = inner;
            }
        }

        Thread thread = new Thread(CatchException);
        thread.Start();
        thread.Join();
        if (exception != null)
            ExceptionDispatchInfo.Capture(exception).Throw();
    }

    /// <summary>
    ///  Helper to run code on a separate thread and propogate exceptions to the current thread.
    ///  Unlike Task.Run this won't use the ThreadPool to avoid messing up ThreadPool threads when
    ///  attempting to change native thread state.
    /// </summary>
    public static void Run(Action action)
    {
        new ThreadRunner().InstanceRun(action);
    }
}
