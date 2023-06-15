namespace Warehouse.Processors.Implements
{
    /// <summary>
    /// Исполняет дочерние процессоры по очереди, пока один не вернет Finish. Всегда возвращает Next.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProcessorsQueue<T> : IProcessor<T>
    {
        private ICollection<IProcessor<T>> _processors;

        public ProcessorsQueue(ICollection<IProcessor<T>> processors)
        {
            _processors = processors.ToList();
        }

        public ProcessorResult Process(T info)
        {
            foreach (var proc in _processors)
                if (proc.Process(info) == ProcessorResult.Finish)
                    return ProcessorResult.Next;

            return ProcessorResult.Next;
        }
    }
}