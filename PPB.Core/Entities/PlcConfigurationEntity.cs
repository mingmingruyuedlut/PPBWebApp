using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Entities
{
    public class PlcConfigurationEntity
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public string SectionName { get; set; }
        public string StationName { get; set; }
        public string PlcName { get; set; }
        public string MemberName { get; set; }
        public string MemberValue { get; set; }

        public PlcConfigurationEntity() { }

        public PlcConfigurationEntity(string area, string section, string station, string plc, string memberName, string memberValue)
        {
            AreaName = area;
            SectionName = section;
            StationName = station;
            PlcName = plc;
            MemberName = memberName;
            MemberValue = memberValue;
        }
    }
}
