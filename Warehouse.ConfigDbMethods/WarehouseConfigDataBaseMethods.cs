using Warehouse.ConfigDataBase;
using Warehouse.DataBase.Models.Config;
using Warehouse.Interfaces.AppSettings;
using Warehouse.Interfaces.Barriers;
using Warehouse.Interfaces.DataBase.Configs;

namespace Warehouse.ConfigDbMethods
{
    public class WarehouseConfigDataBaseMethods : IWarehouseConfigDataBaseMethods
    {
        private readonly IAppSettings settings;

        public WarehouseConfigDataBaseMethods(IAppSettings settings)
        {
            this.settings = settings;
        }

        public IBarrierInfo? GetBarrierInfo(ICamera camera)
        {
            using(var db = new WarehouseConfig(settings))
            {
                return db.BarrierInfos.FirstOrDefault(x => x.AreaId == camera.AreaId);
            }
        }

        public int GetListAliveDuration()
        {
            using (var db = new WarehouseConfig(settings))
            {
                var value = db.Configs.FirstOrDefault(x => x.Key == "ListAliveDuration").Value;
                if (value == null)
                    value = "48";

                return int.Parse(value);
            }
        }
    }
}