using Prism.Mvvm;
using Services;
using SharedLibrary.DataBaseModels;
using System.Collections.Generic;

namespace CheckPointControl.ViewModels
{
    public class SelectAreaViewModel : BindableBase
    {
        public IEnumerable<Area> Areas => areaService.Areas;
        public Area SelectedArea { get => areaService.SelectedArea; set => areaService.SelectedArea = value; }

        private readonly AreaService areaService;

        public SelectAreaViewModel(AreaService areaService)
        {
            this.areaService = areaService;
        }
    }
}
