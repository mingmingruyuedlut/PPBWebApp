using PPB.Constant.Constants;
using PPB.DBManager.Entities;
using PPB.DBManager.Models;
using PPB.DBManager.Repositories;
using PPB.PythonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Managers
{
    public class PlcConfigurationManager
    {
        public PlcConfigurationSummary GetPlcConfigurationSummary()
        {
            var pcSummary = new PlcConfigurationSummary { plcHierList = GetPlcHierList() };
            return pcSummary;
        }

        public List<PlcHierarchy> GetPlcHierList()
        {
            var seList = new StationRepository().GetStations();

            var phList = new List<PlcHierarchy>();

            foreach (var se in seList)
            {
                var ipAddressConfig = GetPlcAddressConfiguration(se.AreaName, se.SectionName, se.StationName);
                var plcHier = new PlcHierarchy
                {
                    AreaName = se.AreaName,
                    SectionName = se.SectionName,
                    StationName = se.StationName,
                    PlcName = se.PlcType,
                    AddressConfig = ipAddressConfig
                };
                phList.Add(plcHier);
            }

            return phList;
        }

        public List<PlcConfigurationModel> GetPlcConfigurations(PlcHierarchy plcHier)
        {
            var peList = new PlcConfigurationRepository().GetPlcConfigurations(plcHier.AreaName, plcHier.SectionName, plcHier.StationName);
            var pmList = ConvertEntityToModel(peList);
            return pmList;
        }

        public void SavePlcConfig(List<PlcConfigurationModel> plcConfigs)
        {
            foreach (var pcm in plcConfigs)
            {
                if (pcm.Id > 0)
                {
                    EditPlcConfig(pcm);
                }
                else
                {
                    AddPlcConfig(pcm);
                }
            }
        }

        public void AddPlcConfig(PlcConfigurationModel plcConfig)
        {
            new PlcConfigurationRepository().AddPlcConfiguration(plcConfig);
        }

        public void EditPlcConfig(PlcConfigurationModel plcConfig)
        {
            new PlcConfigurationRepository().EditPlcConfiguration(plcConfig);
        }

        public List<PlcConfigurationModel> GetInitialPlcConfigurations(PlcHierarchy plc)
        {
            var mList = new List<PlcConfigurationModel>();
            //to-do: get from structure
            var plcNameItem = new PlcConfigurationModel
            {
                Id = -1,
                AreaName = plc.AreaName,
                SectionName = plc.SectionName,
                StationName = plc.StationName,
                PlcName = plc.PlcName,
                MemberName = "TaskName_Actual",
                MemberValue = plc.PlcName
            };

            var address1Item = new PlcConfigurationModel
            {
                Id = -1,
                AreaName = plc.AreaName,
                SectionName = plc.SectionName,
                StationName = plc.StationName,
                PlcName = plc.PlcName,
                MemberName = "PLC.IP Address[1]",
                MemberValue = string.Empty
            };

            var address2Item = new PlcConfigurationModel
            {
                Id = -1,
                AreaName = plc.AreaName,
                SectionName = plc.SectionName,
                StationName = plc.StationName,
                PlcName = plc.PlcName,
                MemberName = "PLC.IP Address[2]",
                MemberValue = string.Empty
            };

            var address3Item = new PlcConfigurationModel
            {
                Id = -1,
                AreaName = plc.AreaName,
                SectionName = plc.SectionName,
                StationName = plc.StationName,
                PlcName = plc.PlcName,
                MemberName = "PLC.IP Address[3]",
                MemberValue = string.Empty
            };

            var address4Item = new PlcConfigurationModel
            {
                Id = -1,
                AreaName = plc.AreaName,
                SectionName = plc.SectionName,
                StationName = plc.StationName,
                PlcName = plc.PlcName,
                MemberName = "PLC.IP Address[4]",
                MemberValue = string.Empty
            };

            var slotItem = new PlcConfigurationModel
            {
                Id = -1,
                AreaName = plc.AreaName,
                SectionName = plc.SectionName,
                StationName = plc.StationName,
                PlcName = plc.PlcName,
                MemberName = "PLC.PLC Slot",
                MemberValue = string.Empty
            };

            var plcDownloadItem = new PlcConfigurationModel
            {
                Id = -1,
                AreaName = plc.AreaName,
                SectionName = plc.SectionName,
                StationName = plc.StationName,
                PlcName = plc.PlcName,
                MemberName = "PLC.Disable Download",
                MemberValue = string.Empty
            };

            mList.Add(plcNameItem);
            mList.Add(address1Item);
            mList.Add(address2Item);
            mList.Add(address3Item);
            mList.Add(address4Item);
            mList.Add(slotItem);
            mList.Add(plcDownloadItem);

            //Insert the initial plc configuration
            foreach (var m in mList)
            {
                AddPlcConfig(m);
            }

            //Get the initial plc configuration
            return GetPlcConfigurations(plc);
        }

        public PlcAddressConfiguration GetPlcAddressConfiguration(string area, string section, string station)
        {
            var pceList = new PlcConfigurationRepository().GetPlcConfigurations(area, section, station);
            var plcAddressConfig = GetPlcAddressConfiguration(pceList);
            return plcAddressConfig;
        }

        public PlcAddressConfiguration GetPlcAddressConfiguration(List<PlcConfigurationEntity> pceList)
        {
            if (pceList.Count > 0)
            {
                var ipAddress1 = pceList.FirstOrDefault(x => x.MemberName.Equals(PlcsConfigurationColumnConstant.Address1)).MemberValue;
                var ipAddress2 = pceList.FirstOrDefault(x => x.MemberName.Equals(PlcsConfigurationColumnConstant.Address2)).MemberValue;
                var ipAddress3 = pceList.FirstOrDefault(x => x.MemberName.Equals(PlcsConfigurationColumnConstant.Address3)).MemberValue;
                var ipAddress4 = pceList.FirstOrDefault(x => x.MemberName.Equals(PlcsConfigurationColumnConstant.Address4)).MemberValue;
                var slot = pceList.FirstOrDefault(x => x.MemberName.Equals(PlcsConfigurationColumnConstant.Slot)).MemberValue;

                return new PlcAddressConfiguration(ipAddress1, ipAddress2, ipAddress3, ipAddress4, Int32.Parse(slot));
            }
            
            return new PlcAddressConfiguration("0", "0", "0", "0", 0); ;
        }

        public dynamic GetPythonFileObj(StationModel sm)
        {
            var plcAddressConfig = GetPlcAddressConfiguration(sm.AreaName, sm.SectionName, sm.StationName);
            var fileObj = LogixService.GetPythonFileObj(plcAddressConfig.IpAddress, plcAddressConfig.Slot);
            return fileObj;
        }

        public dynamic GetPythonFileObj(TaskModel tm)
        {
            var plcAddressConfig = GetPlcAddressConfiguration(tm.AreaName, tm.SectionName, tm.StationName);
            var fileObj = LogixService.GetPythonFileObj(plcAddressConfig.IpAddress, plcAddressConfig.Slot);
            return fileObj;
        }


        #region Convertor between Entity(DB) and Model(UI)
        private List<PlcConfigurationModel> ConvertEntityToModel(List<PlcConfigurationEntity> eList)
        {
            var mList = new List<PlcConfigurationModel>();
            foreach (var e in eList)
            {
                var m = ConvertEntityToModel(e);
                mList.Add(m);
            }
            return mList;
        }

        private PlcConfigurationModel ConvertEntityToModel(PlcConfigurationEntity e)
        {
            var m = new PlcConfigurationModel
            {
                Id = e.Id,
                AreaName = e.AreaName,
                SectionName = e.SectionName,
                StationName = e.StationName,
                PlcName = e.PlcName,
                MemberName = e.MemberName,
                MemberValue = e.MemberValue
            };
            return m;
        }

        private List<PlcConfigurationEntity> ConvertModelToEntity(List<PlcConfigurationModel> mList)
        {
            var eList = new List<PlcConfigurationEntity>();
            foreach (var m in mList)
            {
                var e = ConvertModelToEntity(m);
                eList.Add(e);
            }
            return eList;
        }

        private PlcConfigurationEntity ConvertModelToEntity(PlcConfigurationModel m)
        {
            var e = new PlcConfigurationEntity
            {
                Id = m.Id,
                AreaName = m.AreaName,
                SectionName = m.SectionName,
                StationName = m.StationName,
                PlcName = m.PlcName,
                MemberName = m.MemberName,
                MemberValue = m.MemberValue
            };
            return e;
        }

        #endregion
    }
}
