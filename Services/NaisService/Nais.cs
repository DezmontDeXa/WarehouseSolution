using NLog;

namespace NaisServiceLibrary
{
    public class Nais
    {
        private const int UpdateDelay = 5000;
        private readonly NaisDataBase naisDataBase;
        private readonly ILogger logger;
        private List<WeightsRecord> weightsRecords;

        public DateTime TargetDate { get; set; }
        public IReadOnlyList<WeightsRecord> WeightsRecords => weightsRecords;

        public event EventHandler<WeightsRecord> RecordAdded;
        public event EventHandler<WeightsRecord> RecordModified;

        public Nais(NaisDataBase naisDataBase, ILogger logger)
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
                    logger.Error($"Error on NaisRole.Working. {e.Message}. Continue.", e);
                }
                Task.Delay(UpdateDelay).Wait();
            }
        }

    }
}
