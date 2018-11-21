using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Models
{
    public class AreaModel
    {
        public int Id { get; set; }
        public string AreaName { get; set; }

        //for ui display
        public int ModelCount { get; set; }

        public AreaModel() { }
        public AreaModel(string areaName)
        {
            AreaName = areaName;
        }
    }

    public class AreaSummary
    {
        public List<AreaModel> AreaModelList { get; set; }
    }
}
