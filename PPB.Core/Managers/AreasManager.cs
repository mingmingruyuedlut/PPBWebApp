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
    public class AreasManager
    {
        public AreaSummary GetAreaSummary()
        {
            var mList = GetAreas();
            return new AreaSummary
            {
                AreaModelList = mList
            };
        }

        public List<AreaModel> GetAreas()
        {
            var eList = new AreasRepository().GetAreas();
            var mList = ConvertEntityToModel(eList);

            var areaConfigList = new AreaConfigurationRepository().GetAreaConfigurations();
            foreach (var m in mList)
            {
                m.ModelCount = areaConfigList.Count(x => x.AreaName.Equals(m.AreaName));
            }
            return mList;
        }



        #region Convertor between Entity(DB) and Model(UI)
        private List<AreaModel> ConvertEntityToModel(List<AreaEntity> eList)
        {
            var mList = new List<AreaModel>();
            foreach (var e in eList)
            {
                var m = ConvertEntityToModel(e);
                mList.Add(m);
            }
            return mList;
        }

        private AreaModel ConvertEntityToModel(AreaEntity e)
        {
            var m = new AreaModel
            {
                Id = e.Id,
                AreaName = e.AreaName
            };
            return m;
        }

        private List<AreaEntity> ConvertModelToEntity(List<AreaModel> mList)
        {
            var eList = new List<AreaEntity>();
            foreach (var m in mList)
            {
                var e = ConvertModelToEntity(m);
                eList.Add(e);
            }
            return eList;
        }

        private AreaEntity ConvertModelToEntity(AreaModel m)
        {
            var e = new AreaEntity
            {
                Id = m.Id,
                AreaName = m.AreaName
            };
            return e;
        }

        #endregion

    }
}
