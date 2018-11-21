using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Models
{
    public class PlcConfigurationModel
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public string SectionName { get; set; }
        public string StationName { get; set; }
        public string PlcName { get; set; }
        public string MemberName { get; set; }
        public string MemberValue { get; set; }

        public PlcConfigurationModel() { }

        public PlcConfigurationModel(string area, string section, string station, string plc, string memberName, string memberValue)
        {
            AreaName = area;
            SectionName = section;
            StationName = station;
            PlcName = plc;
            MemberName = memberName;
            MemberValue = memberValue;
        }
    }

    public class PlcAddressConfiguration
    {
        public string IpAddressFirstPart { get; set; }
        public string IpAddressSecondPart { get; set; }
        public string IpAddressThirdPart { get; set; }
        public string IpAddressFourthPart { get; set; }
        public string IpAddress { get; set; }
        public int Slot { get; set; }
        public string IpAddressAndSlot { get; set; }

        public PlcAddressConfiguration() { }

        public PlcAddressConfiguration(string ipFirstPart, string ipSecondPart, string ipThirdPart, string ipFourthPart, int slot)
        {
            IpAddressFirstPart = ipFirstPart;
            IpAddressSecondPart = ipSecondPart;
            IpAddressThirdPart = ipThirdPart;
            IpAddressFourthPart = ipFourthPart;
            IpAddress = IpAddressFirstPart + "." + IpAddressSecondPart + "." + IpAddressThirdPart + "." + IpAddressFourthPart;
            Slot = slot;
            IpAddressAndSlot = IpAddress + "," + Slot.ToString();
        }
    }

    public class PlcHierarchy
    {
        public string AreaName { get; set; }
        public string SectionName { get; set; }
        public string StationName { get; set; }
        public string PlcName { get; set; }

        public PlcAddressConfiguration AddressConfig { get; set; }
    }

    public class PlcConfigurationSummary
    {
        public List<PlcHierarchy> plcHierList { get; set; }
    }
}
