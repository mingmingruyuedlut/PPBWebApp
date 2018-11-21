using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Entities
{
    public class StationConfigurationEntity
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public string SectionName { get; set; }
        public string StationName { get; set; }
        public string MemberName { get; set; }
        public string MemberValue { get; set; }
        public string MemberType { get; set; }
        public string BaseTag { get; set; }
        public int StationInstance { get; set; }


        public StationConfigurationEntity() { }

        public StationConfigurationEntity(string area, string section, string station, string memberName, string memberValue, string memberType, string baseTag, int stationInstance)
        {
            AreaName = area;
            SectionName = section;
            StationName = station;
            MemberName = memberName;
            MemberValue = memberValue;
            MemberType = memberType;
            BaseTag = baseTag;
            StationInstance = stationInstance;
        }

        public StationConfigurationEntity(int id, string area, string section, string station, string memberName, string memberValue, string memberType, string baseTag, int stationInstance)
        {
            Id = id;
            AreaName = area;
            SectionName = section;
            StationName = station;
            MemberName = memberName;
            MemberValue = memberValue;
            MemberType = memberType;
            BaseTag = baseTag;
            StationInstance = stationInstance;
        }
    }
}
