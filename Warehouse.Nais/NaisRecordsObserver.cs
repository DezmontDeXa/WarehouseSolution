using NLog;
using Warehouse.Interfaces.Nais;

namespace Warehouse.Nais
{
    public class NaisRecordsObserver
    {
        private const int UpdateDelay = 5000;
        private readonly NaisDataBaseConnection naisDataBase;
        private readonly ILogger logger;
        private List<IWeightsRecord> weightsRecords;

        public DateTime TargetDate { get; set; }
        public IReadOnlyList<IWeightsRecord> WeightsRecords => weightsRecords;

        public event EventHandler<IWeightsRecord> RecordAdded;
        public event EventHandler<IWeightsRecord> RecordModified;

        public NaisRecordsObserver(NaisDataBaseConnection naisDataBase, ILogger logger)
        {
            TargetDate = DateTime.Now;
            this.naisDataBase = naisDataBase;
            this.logger = logger;
        }

        public void Run()
        {
            logger.Info("Nais records getting...");
            weightsRecords = naisDataBase.GetRecordsAsync(TargetDate).Result;
            logger.Info("Nais records getted");
            Task.Run(Working);
        }

        private void Working()
        {
            while (true)
            {
                try
                {
                    var records = naisDataBase.GetRecordsAsync(TargetDate).Result;
                    foreach (var record in records)
                    {
                        var existRecord = weightsRecords.FirstOrDefault(x => x.Id == record.Id);

                        if (existRecord == null)
                        {
                            existRecord = record;
                            weightsRecords.Add(existRecord);
                            RecordAdded?.Invoke(this, existRecord);
                        }
                        else
                        {
                            if (existRecord.FirstWeighting != null && existRecord.SecondWeighting != null)
                                continue;

                            if (existRecord.FirstWeighting == record.FirstWeighting &&
                                existRecord.SecondWeighting == record.SecondWeighting &&
                                existRecord.Netto == record.Netto &&
                                existRecord.StorageName == record.StorageName)
                                continue;


                            existRecord.FirstWeighting = record.FirstWeighting;
                            existRecord.SecondWeighting = record.SecondWeighting;
                            existRecord.Netto = record.Netto;
                            existRecord.StorageName = record.StorageName;
                            RecordModified?.Invoke(this, existRecord);

                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Error(e, $"Error on NaisRole.Working. {e.Message}. Continue.");
                }
                Task.Delay(UpdateDelay).Wait();
            }
        }

    }
}
