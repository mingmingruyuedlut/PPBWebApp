using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Models
{
    public class TaskStructureModel
    {
        public int Id { get; set; }
        public string TaskName { get; set; }
        public string MemberName { get; set; }
        public string MemberType { get; set; }
        public int MemberOrder { get; set; }
        public string MemberDescription1 { get; set; }
        public string MemberDescription2 { get; set; }
        public string MemberDescription3 { get; set; }
        public string MemberValues { get; set; }
        public string TaskXrefName { get; set; }
        public int Visible { get; set; }
        public int Global1 { get; set; }
        public int Base { get; set; }
        public int MaxLength { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public string ExclusionString { get; set; }
        public string Version { get; set; }
    }
}
