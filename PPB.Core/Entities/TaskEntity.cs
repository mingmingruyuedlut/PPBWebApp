using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public string SectionName { get; set; }
        public string StationName { get; set; }
        public string MasterFileName { get; set; }
        public string TaskName { get; set; }
        public int TaskMemory { get; set; }
        public int TaskMemoryPlus { get; set; }
        public int TaskNodes { get; set; }
        public int TaskConnection { get; set; }
        public int MaxNoOfInstances { get; set; }
        public string ModelAffiliation { get; set; }
    }
}
