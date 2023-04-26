using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraAlertStreamTo1C
{
    public class Settings
    {
        public string CallbackUri { get; set; }
        public List<string> Cameras { get; set; }
    }
}
