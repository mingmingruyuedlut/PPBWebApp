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
    public class StationConfigurationManager
    {
        public List<StationConfigurationModel> GetStationConfigurations(StationModel sm)
        {
            var eList = new StationConfigurationRepository().GetStationConfigurations(sm.AreaName, sm.SectionName, sm.StationName, sm.StationInstance);
            var mList = ConvertEntityToModel(eList);
            ReOrderStationConfiguration(mList);
            return mList;
        }

        public List<StationConfigurationModel> GetIntArrayStationConfigurations(StationConfigurationModel scm, string iaMemberType)
        {
            var sceList = new StationConfigurationRepository().GetStationConfigurationsLikeMemberName(scm.AreaName, scm.SectionName, scm.StationName, scm.StationInstance, scm.MemberName);
            //first time to operate the int array type configuration, insert the items and retrieve again
            if (sceList.Count == 0)
            {
                AddInitialStationConfigurations(scm, iaMemberType);
                sceList = new StationConfigurationRepository().GetStationConfigurationsLikeMemberName(scm.AreaName, scm.SectionName, scm.StationName, scm.StationInstance, scm.MemberName);
            }
            var scmList = ConvertEntityToModel(sceList);
            //generate the display name
            for (var i = 0; i < scmList.Count; i++)
            {
                scmList[i].DisplayName = scm.DisplayName + "[" + i.ToString() + "]";
            }
            return scmList;
        }

        public void AddInitialStationConfigurations(StationConfigurationModel scm, string iaMemberType)
        {
            var iaCount = CommonManager.GetMemberTypeLength(iaMemberType);
            for (var i = 0; i < iaCount; i++)
            {
                var config = new StationConfigurationModel
                {
                    AreaName = scm.AreaName,
                    SectionName = scm.SectionName,
                    StationName = scm.StationName,
                    MemberName = scm.MemberName + "[" + i + "]",
                    MemberValue = "0",
                    MemberType = "INT",
                    BaseTag = "Config",
                    StationInstance = scm.StationInstance
                };
                new StationConfigurationRepository().AddStationConfiguration(config);
            }
        }

        /// <summary>
        /// Int Array Type just contains one item
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        public List<StationConfigurationModel> GetStationConfigurationsBaseOnStructure(StationModel sm)
        {
            var initialScmList = GetInitialStationConfigurations(sm);
            var sceList = new StationConfigurationRepository().GetStationConfigurations(sm.AreaName, sm.SectionName, sm.StationName, sm.StationInstance);
            foreach (var scm in initialScmList)
            {
                if (scm.IsIntArray)
                    continue;

                var matchedObj = sceList.FirstOrDefault(x => x.MemberName.Equals(scm.MemberName));
                if (matchedObj == null)
                    continue;


                scm.Id = matchedObj.Id;
                scm.MemberValue = matchedObj.MemberValue;
            }

            return initialScmList;
        }

        private void ReOrderStationConfiguration(List<StationConfigurationModel> scmList)
        {
            var ssmList = new StationStructureManager().GetStationStructures();
            foreach (var ssm in ssmList)
            {
                var tempMemName = ssm.Parent + "." + ssm.MemberName;
                scmList.Where(x => x.MemberName.Contains(tempMemName)).ToList().ForEach(x => { x.MemberOrder = ssm.MemberOrder; });
            }
            scmList.OrderBy(x => x.MemberOrder);
        }

        public List<StationConfigurationModel> GetInitialStationConfigurations(StationModel sm)
        {
            var scmList = new List<StationConfigurationModel>();
            var ssmList = new StationStructureManager().GetStationStructures();
            foreach (StationStructureModel ssm in ssmList)
            {
                var scm = new StationConfigurationModel
                {
                    Id = -1,
                    AreaName = sm.AreaName,
                    SectionName = sm.SectionName,
                    StationName = sm.StationName,
                    MemberName = ssm.Parent + "." + ssm.MemberName,
                    MemberValue = ssm.MemberType.Contains("BOOL") ? "False" : "0",
                    MemberType = ssm.MemberType,
                    BaseTag = "Config",
                    StationInstance = sm.StationInstance,
                    DisplayName = string.IsNullOrWhiteSpace(ssm.MemberDescription1) ? ssm.MemberName : ssm.MemberDescription1,
                    IsIntArray = ssm.MemberType.Contains("INT[")
                };
                scmList.Add(scm);
            }
            return scmList;
        }

        public void SaveStationConfig(List<StationConfigurationModel> stationConfigs)
        {
            foreach (var scm in stationConfigs)
            {
                if (scm.Id > 0)
                {
                    EditStationConfig(scm);
                }
                else
                {
                    AddStationConfig(scm);
                }
            }
        }

        public void AddStationConfig(StationConfigurationModel stationConfig)
        {
            new StationConfigurationRepository().AddStationConfiguration(stationConfig);
        }

        public void EditStationConfig(StationConfigurationModel stationConfig)
        {
            new StationConfigurationRepository().UpdateStationConfiguration(stationConfig);
        }

        public void DownloadStationConfig(List<StationModel> stations)
        {
            foreach (var station in stations)
            {
                var fileObj = new PlcConfigurationManager().GetPythonFileObj(station);
                var stationConfigList = new StationConfigurationRepository().GetStationConfigurations(station.AreaName, station.SectionName, station.StationName, station.StationInstance);
                foreach (var stationConfig in stationConfigList)
                {
                    if (!stationConfig.MemberName.Contains("."))
                        continue;
                    if (stationConfig.MemberType.Contains(DbMemberTypeConstant.MtString))
                    {
                        DownloadStringTypeMember(ref fileObj, stationConfig);
                    }
                    else
                    {
                        DownloadNotStringTypeMember(ref fileObj, stationConfig);
                    }
                }
            }
        }

        private void DownloadNotStringTypeMember(ref dynamic fileObj, StationConfigurationEntity stationConfig)
        {
            var memberName = CommonManager.GetDownloadMemberName(stationConfig.MemberName, stationConfig.BaseTag);
            var memberValue = CommonManager.GetDownloadMemberValue(stationConfig.MemberValue);
            LogixService.DownloadTag(ref fileObj, memberName, memberValue);
        }

        private void DownloadStringTypeMember(ref dynamic fileObj, StationConfigurationEntity stationConfig)
        {
            var memberName = CommonManager.GetDownloadMemberName(stationConfig.MemberName, stationConfig.BaseTag);
            var memberValue = CommonManager.GetDownloadMemberValue(stationConfig.MemberValue);
            var actualMemberValueLength = memberValue.Length;
            var memberLength = CommonManager.GetMemberTypeLength(stationConfig.MemberType);
            memberValue = CommonManager.SupplementMemberValue(memberValue, memberLength);
            CommonManager.DownloadStringTypeMember(ref fileObj, memberName, memberValue);
            CommonManager.DownloadStringTypeLength(ref fileObj, memberName, actualMemberValueLength);
        }

        public void UploadStationConfig(List<StationModel> stations)
        {
            foreach (var station in stations)
            {
                var fileObj = new PlcConfigurationManager().GetPythonFileObj(station);
                var stationConfigList = new StationConfigurationRepository().GetStationConfigurations(station.AreaName, station.SectionName, station.StationName, station.StationInstance);
                foreach (var stationConfig in stationConfigList)
                {
                    if (!stationConfig.MemberName.Contains("."))
                        continue;
                    var memberName = CommonManager.GetDownloadMemberName(stationConfig.MemberName, stationConfig.BaseTag);
                    var memberValue = LogixService.UploadTag(ref fileObj, memberName);
                    if (!memberValue.Equals(stationConfig.MemberValue))
                    {
                        new StationConfigurationRepository().UpdateStationConfiguration(stationConfig.Id, memberValue);
                    }
                }
            }
        }

        #region Convertor between Entity(DB) and Model(UI)
        private List<StationConfigurationModel> ConvertEntityToModel(List<StationConfigurationEntity> eList)
        {
            List<StationConfigurationModel> mList = new List<StationConfigurationModel>();
            foreach (StationConfigurationEntity e in eList)
            {
                StationConfigurationModel m = ConvertEntityToModel(e);
                m.StationInstance = -1; //to-do: suit for multiple station
                mList.Add(m);
            }
            return mList;
        }

        private StationConfigurationModel ConvertEntityToModel(StationConfigurationEntity e)
        {
            StationConfigurationModel m = new StationConfigurationModel
            {
                Id = e.Id,
                AreaName = e.AreaName,
                SectionName = e.SectionName,
                StationName = e.StationName,
                MemberName = e.MemberName,
                MemberValue = e.MemberValue,
                MemberType = e.MemberType,
                BaseTag = e.BaseTag,
                StationInstance = e.StationInstance
            };
            return m;
        }

        private List<StationConfigurationEntity> ConvertModelToEntity(List<StationConfigurationModel> mList)
        {
            List<StationConfigurationEntity> eList = new List<StationConfigurationEntity>();
            foreach (StationConfigurationModel m in mList)
            {
                StationConfigurationEntity e = ConvertModelToEntity(m);
                eList.Add(e);
            }
            return eList;
        }

        private StationConfigurationEntity ConvertModelToEntity(StationConfigurationModel m)
        {
            StationConfigurationEntity e = new StationConfigurationEntity
            {
                Id = m.Id,
                AreaName = m.AreaName,
                SectionName = m.SectionName,
                StationName = m.StationName,
                MemberName = m.MemberName,
                MemberValue = m.MemberValue,
                MemberType = m.MemberType,
                BaseTag = m.BaseTag,
                StationInstance = m.StationInstance
            };
            return e;
        }

        #endregion
    }
}
