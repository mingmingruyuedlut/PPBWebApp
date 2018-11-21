using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Models
{
    public class AreaConfigurationModel
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public int ModelNumber { get; set; }
        public string ModelName { get; set; }

        //for ui
        public string DisplayName { get; set; }
    }
}
