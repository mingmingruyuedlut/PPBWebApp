using PPB.DBManager.Entities;
using PPB.DBManager.Models;
using PPB.DBManager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Managers
{
    public class AreaConfigurationManager
    {
        public List<AreaConfigurationModel> GetAreaConfigurations()
        {
            var eList = new AreaConfigurationRepository().GetAreaConfigurations();
            var mList = ConvertEntityToModel(eList);
            return mList;
        }

        public List<AreaConfigurationModel> GetAreaConfigurations(AreaModel am)
        {
            var eList = new AreaConfigurationRepository().GetAreaConfigurations(am.AreaName);
            var mList = ConvertEntityToModel(eList);
            return mList;
        }

        public void SaveAreaConfig(List<AreaConfigurationModel> areaConfigs)
        {
            foreach (var acm in areaConfigs)
            {
                if (acm.Id > 0)
                {
                    EditAreaConfig(acm);
                }
                else
                {
                    AddAreaConfig(acm);
                }
            }
        }

        public void SaveAreaConfig(AreaConfigurationModel areaConfig)
        {
            if (areaConfig.Id > 0)
            {
                EditAreaConfig(areaConfig);
            }
            else
            {
                AddAreaConfig(areaConfig);
            }
        }

        public void AddAreaConfig(AreaConfigurationModel areaConfig)
        {
            //get max model number of current area configs
            areaConfig.ModelNumber = GetAreaConfigNextModelNumber();
            new AreaConfigurationRepository().AddAreaConfiguration(areaConfig);
        }

        public int GetAreaConfigNextModelNumber()
        {
            var maxNumber = new AreaConfigurationRepository().GetMaxAreaConfigModelNumber();
            return maxNumber + 1;
        }

        public void EditAreaConfig(AreaConfigurationModel stationConfig)
        {
            new AreaConfigurationRepository().EditAreaConfiguration(stationConfig);
        }

        public void DeleteAreaConfig(AreaConfigurationModel areaConfig)
        {
            new AreaConfigurationRepository().DeleteAreaConfiguration(areaConfig);
            
            //update the remaining config -- match for VB.Net version. What a magic logic!!!
            var eList = new AreaConfigurationRepository().GetAreaConfigurations(areaConfig.ModelNumber);
            foreach (var e in eList)
            {
                e.ModelNumber -= 1;
                EditAreaConfig(ConvertEntityToModel(e));
            }
        }

        #region Convertor between Entity(DB) and Model(UI)
        private List<AreaConfigurationModel> ConvertEntityToModel(List<AreaConfigurationEntity> eList)
        {
            var mList = new List<AreaConfigurationModel>();
            foreach (var e in eList)
            {
                var m = ConvertEntityToModel(e);
                mList.Add(m);
            }
            return mList;
        }

        private AreaConfigurationModel ConvertEntityToModel(AreaConfigurationEntity e)
        {
            var m = new AreaConfigurationModel
            {
                Id = e.Id,
                AreaName = e.AreaName,
                ModelNumber = e.ModelNumber,
                ModelName = e.ModelName,
                DisplayName = e.ModelNumber.ToString() + "-" + "Model ID:" //it's hard code - to do...
            };
            return m;
        }

        private List<AreaConfigurationEntity> ConvertModelToEntity(List<AreaConfigurationModel> mList)
        {
            var eList = new List<AreaConfigurationEntity>();
            foreach (var m in mList)
            {
                var e = ConvertModelToEntity(m);
                eList.Add(e);
            }
            return eList;
        }

        private AreaConfigurationEntity ConvertModelToEntity(AreaConfigurationModel m)
        {
            var e = new AreaConfigurationEntity
            {
                Id = m.Id,
                AreaName = m.AreaName,
                ModelNumber = m.ModelNumber,
                ModelName = m.ModelName
            };
            return e;
        }

        #endregion
    }
}
