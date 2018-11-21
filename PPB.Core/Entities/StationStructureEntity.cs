using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Entities
{
    public class StationStructureEntity
    {
        public int Id { get; set; }
        public string Parent { get; set; }
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
        public string MemberGroup { get; set; }

        public StationStructureEntity() { }

        public StationStructureEntity(int id, string parent, string memberName, string memberType, int memberOrder, string memberDescription1, string memberDescription2, string memberDescription3, string memberValues, string taskXrefName, int visible, int global1, int base1, int maxLength, int minValue, int maxValue, string exclusionString, string version, string memberGroup)
        {
            Id = id;
            Parent = parent;
            MemberName = memberName;
            MemberType = memberType;
            MemberOrder = memberOrder;
            MemberDescription1 = memberDescription1;
            MemberDescription2 = memberDescription2;
            MemberDescription3 = memberDescription3;
            MemberValues = memberValues;
            TaskXrefName = taskXrefName;
            Visible = visible;
            Global1 = global1;
            Base = base1;
            MaxLength = maxLength;
            MinValue = minValue;
            MaxValue = maxValue;
            ExclusionString = exclusionString;
            Version = version;
            MemberGroup = memberGroup;
        }
    }
}
