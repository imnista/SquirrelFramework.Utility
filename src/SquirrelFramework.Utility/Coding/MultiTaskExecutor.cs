namespace SquirrelFramework.Utility.Coding
{
    public class MultiTaskExecutor<T>
    {
        public delegate void TaskExecutor(IEnumerable<T> assignTaskDataSet);

        private readonly object locker = new object();
        private readonly int multiTaskNumber;
        private List<Task> tasks = new List<Task>();

        public MultiTaskExecutor(int multiTaskNumber = 10)
        {
            this.multiTaskNumber = multiTaskNumber;
        }

        public void AssignTask(IEnumerable<T> taskDataSet, TaskExecutor taskExecutor)
        {
            lock (locker)
            {
                if (taskDataSet == null)
                {
                    throw new ArgumentNullException(nameof(taskDataSet));
                }
                if (multiTaskNumber < 1)
                {
                    throw new ArgumentException("The value of MultiTaskNumber must be greater than zero.");
                }
                var taskDataList = taskDataSet as IList<T> ?? taskDataSet.ToList();
                if (!taskDataList.Any())
                {
                    return;
                }
                var taskCount = taskDataList.Count();
                var perTaskDataNumber = taskCount / multiTaskNumber;
                var lastTaskRemainDataNumber = taskCount % multiTaskNumber;
                tasks = new List<Task>();
                var currentTaskDataIndex = 0;

                for (var taskId = 0; taskId < multiTaskNumber; taskId++)
                {
                    // DEBUG
                    // var debugStartIndex = currentTaskDataIndex;

                    var assignTaskDataSet = new List<T>();
                    if (taskId == multiTaskNumber - 1)
                    {
                        for (var i = 0; i < perTaskDataNumber + lastTaskRemainDataNumber; i++)
                        {
                            assignTaskDataSet.Add(taskDataList[currentTaskDataIndex]);
                            currentTaskDataIndex++;
                        }
                    }
                    else
                    {
                        for (var i = 0; i < perTaskDataNumber; i++)
                        {
                            assignTaskDataSet.Add(taskDataList[currentTaskDataIndex]);
                            currentTaskDataIndex++;
                        }
                    }

                    // DEBUG
                    // Console.WriteLine("Current Task ID: {0}, [{1}, {2})", taskId, debugStartIndex, currentTaskDataIndex);

                    var task = new Task(() => taskExecutor(assignTaskDataSet));
                    tasks.Add(task);
                }
            }
        }

        public async Task Execute()
        {
            foreach (var task in tasks)
            {
                task.Start();
            }
            await Task.WhenAll(tasks.ToArray());
        }
    }
}