using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Models
{
    public class TaskModel
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

        public int TaskInstance { get; set; }
        public int TaskInstanceDisplay { get; set; } //it's for task instance display
    }

    public class TaskSummary
    {
        public List<TaskModel> TaskList { get; set; }
    }
}
