# MPP-ThreadPool
Modern Programming Platforms, Lab #1

Create class on C# which:
- named TaskQueue and realize pool thread logic;
- creates specifed number of pool threads in constuctor;
- contains a task queue in the form of delegates without parameters: delegate void TaskDelegate();
- provides the queue and subsequent execution of delegates using the method void EnqueueTask(TaskDelegate task); 
