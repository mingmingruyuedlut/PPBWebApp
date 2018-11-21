using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Models
{
    public class StationModel
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public string SectionName { get; set; }
        public string StationName { get; set; }
        public string StationType { get; set; }
        public string PlcType { get; set; }
        public string MasterFileName { get; set; }
        public string MasterFileRevision { get; set; }
        public string ModelAffiliation { get; set; }
        public int Accept { get; set; }
        public int MaxNoOfInstances { get; set; }

        //used for ui
        public int StationInstance { get; set; }
    }

    public class StationSummary
    {
        public List<StationModel> StationList { get; set; }
    }
}
