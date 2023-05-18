using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Configuration;
using SharedLibrary.AppSettings;

namespace NaisService
{
    public class NaisDataBase : IDisposable
    {
        private const string GetWeightsRecordsCommand = "select FIRST_WEIGHT, SECOND_WEIGHT, NETTO, STORAGENAME, TRANSPORT_NUMBER, INVOICE_DATE from WEIGHTS_BOOK where INVOICE_DATE like '[DATE]'";
        private FbConnection _conNAIS;

        public bool IsOpen { get; private set; }


        public NaisDataBase()
        {
            var connectionString = GetConnectionString();
            _conNAIS = new FbConnection(connectionString);
            _conNAIS.StateChange += ConNAIS_StateChange;
            _conNAIS.OpenAsync();
        }

        private string GetConnectionString()
        {
            IConfiguration config = new ConfigurationBuilder()
           .AddJsonFile("AppSettings.json")
           .Build();
            var settings = config.GetSection("Settings").Get<Settings>();
            return settings.NaisConnectionString;
        }

        public async Task<List<WeightsRecord>> GetRecordsAsync(DateTime date)
        {
            while (!IsOpen)
                await Task.Delay(100);

            var result = new List<WeightsRecord>();
            var commandString = GetWeightsRecordsCommand.Replace("[DATE]", date.ToString("yyy-MM-dd"));
            using (var command = new FbCommand(commandString, _conNAIS))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var record = new WeightsRecord()
                            {
                                FirstWeighting = GetDbValue<float?>(reader, 0),
                                SecondWeighting = GetDbValue<float?>(reader, 1),
                                Netto = GetDbValue<float?>(reader, 2),
                                StorageName = GetDbValue<string>(reader,3),
                                PlateNumber = GetDbValue<string>(reader, 4)
                            };

                            result.Add(record);
                        }
                    }
                }
            }

            return result;
        }

        private static T GetDbValue<T>(FbDataReader reader, int column)
        {
            var value = reader.GetValue(column);
            if (value is DBNull)
                return default(T);
            else
                return (T)value;
        }

        private void ConNAIS_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            IsOpen = e.CurrentState == System.Data.ConnectionState.Open;
        }

        public void Dispose()
        {
            _conNAIS?.Dispose();
        }
    }
}
