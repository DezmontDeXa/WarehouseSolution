using SharedLibrary.DataBaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AreaService
    {
        public IReadOnlyList<Area> Areas => areas;
        public Area SelectedArea
        {
            get => selectedArea; set
            {
                selectedArea = value;
                if (value != null)
                    AreaSelected?.Invoke(this, value);
            }
        }

        public event EventHandler<Area> AreaSelected;

        private readonly List<Area> areas;
        private Area selectedArea;

        public AreaService()
        {
            using (var db = new WarehouseContext())
                areas = db.Areas.ToList();
        }
    }
}
