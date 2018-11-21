using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Entities
{
    public class TaskConfigurationEntity
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public string SectionName { get; set; }
        public string StationName { get; set; }
        public string TaskName { get; set; }
        public int TaskInstance { get; set; }
        public string MemberName { get; set; }
        public string MemberValue { get; set; }
        public string MemberType { get; set; }
        public string BaseTag { get; set; }
    }
}
