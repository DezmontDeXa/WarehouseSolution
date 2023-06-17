using NLog;

namespace Warehouse.Processors.Implements
{
    /// <summary>
    /// Исполняет дочерние процессоры по очереди, пока один не вернет Finish. Всегда возвращает Next.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProcessorsPipeline<T> : IProcessor<T>
    {
        public event EventHandler<T> AllSkipped;
        public event EventHandler<ProcessorExceptionArgs<T>> ProcessException;

        private List<IProcessor<T>> _processors = new List<IProcessor<T>>();

        public void AddProcessor(IProcessor<T> processor)
        {
            _processors.Add(processor);
        }

        public ProcessorResult Process(T info)
        {
            foreach (var proc in _processors)
            {
                try
                {
                    if (proc.Process(info) == ProcessorResult.Finish)
                        return ProcessorResult.Next;
                }catch(Exception ex)
                {
                    var exArgs = new ProcessorExceptionArgs<T>(info, ex);
                    OnException(proc, exArgs);
                    ProcessException?.Invoke(proc, exArgs);
                    return ProcessorResult.Finish;
                }
            }

            AllSkipped?.Invoke(this, info);
            return ProcessorResult.Next;
        }

        protected virtual void OnException(IProcessor<T> proc, ProcessorExceptionArgs<T> ex){ }
    }

    public class ProcessorExceptionArgs<T> 
    {
        public ProcessorExceptionArgs(T info, Exception ex )
        {
            Info = info;
            Ex = ex;
        }

        public T Info { get; }
        public Exception Ex { get; }
    }
}