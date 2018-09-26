using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace TaskQueue
{
    class Program
    {

        public class TaskQueue
        {
            private ConcurrentQueue<TaskDelegate> taskQueue = new ConcurrentQueue<TaskDelegate>();
            public List<Thread> threadPool = new List<Thread>();

            public delegate void TaskDelegate();

            public TaskQueue(int threadCount)
            {
                for (int i = 0; i < threadCount; i++)
                {
                    var thread = new Thread(ThreadTask);
                    threadPool.Add(thread);
                    thread.Start();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Поток " + String.Format("{0,2:0}", threadPool[i].ManagedThreadId) + " создан.");
                }
                Console.ResetColor();
            }

            public void EnqueueTask(TaskDelegate task)
            {
                taskQueue.Enqueue(task);
            }

            private void ThreadTask()
            {
                while (true)
                {
                    if (taskQueue.TryDequeue(out TaskDelegate task))
                    {
                        task();
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int threadCount;
            int taskCount;

            Console.WriteLine("Введите количество потоков");
            threadCount = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество задач");
            taskCount = Convert.ToInt32(Console.ReadLine());

            var taskQueue = new TaskQueue(threadCount);
            for (int i = 0; i < taskCount; i++)
            {
                taskQueue.EnqueueTask(ThreadPrint);
            }

            string quit = null;
            while (quit != "q")
            {
                quit = Console.ReadLine();
            }
            foreach (Thread thread in taskQueue.threadPool)
            {
                thread.Abort();
            }
        }

        private static void ThreadPrint()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine($"Поток " + String.Format("{0,2:0}", threadId) + " активен.");
            Console.WriteLine($"Поток " + String.Format("{0,2:0}", threadId) + " ожидает.");
            Thread.Sleep(1);
        }
    }
}
