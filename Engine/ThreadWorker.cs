using System.Threading;
using System.Collections.Concurrent;
using System;

namespace _3D_Engine.Engine;

#nullable enable
internal sealed class ThreadWorker
{
    private readonly Thread thread;
    private readonly ConcurrentQueue<Action> workQueue = new();
    private readonly SemaphoreSlim queueSemaphore = new(0);
    private readonly ManualResetEventSlim completionSignal = new(true);

    public ThreadWorker(string? name)
    {
        thread = new Thread(Work);
        thread.Name = name;
        thread.Start();
    }

    public void QueueWork(Action work)
    {
        completionSignal.Reset();
        workQueue.Enqueue(work);
        queueSemaphore.Release();
    }

    public void WaitForCompletion()
    {
        completionSignal.Wait();
    }

    private void Work()
    {
        while (true)
        {
            if (workQueue.IsEmpty)
                completionSignal.Set();

            queueSemaphore.Wait();
            if (workQueue.TryDequeue(out Action? work))
            {
                work();
            }
        }
    }
}
