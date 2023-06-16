namespace Warehouse.Processors.Implements
{
    /// <summary>
    /// Исполняет дочерние процессоры по очереди, пока один не вернет Finish. Всегда возвращает Next.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProcessorsPipeline<T> : IProcessor<T>
    {
        public event EventHandler AllSkipped;

        private List<IProcessor<T>> _processors = new List<IProcessor<T>>();

        public void AddProcessor(IProcessor<T> processor)
        {
            _processors.Add(processor);
        }

        public ProcessorResult Process(T info)
        {
            foreach (var proc in _processors)
                if (proc.Process(info) == ProcessorResult.Finish)
                    return ProcessorResult.Next;

            AllSkipped?.Invoke(this, null);
            return ProcessorResult.Next;
        }
    }
}