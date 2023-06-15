namespace Warehouse.Processors
{
    public interface IProcessor<T>
    {
        ProcessorResult Process(T info);
    }
}