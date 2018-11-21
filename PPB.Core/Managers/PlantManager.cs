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
    public class PlantManager
    {
        public List<PlantModel> GetPlantList()
        {
            var peList = new PlantRepository().GetPlantList();
            var pmList = ConvertEntityToModel(peList);
            return pmList;
        }

        public string GetPlantName()
        {
            var pm = GetPlantList().FirstOrDefault();
            return pm.PlantName;
        }

        #region Convertor between Entity(DB) and Model(UI)
        private List<PlantModel> ConvertEntityToModel(List<PlantEntity> eList)
        {
            var mList = new List<PlantModel>();
            foreach (var pe in eList)
            {
                var pm = ConvertEntityToModel(pe);
                mList.Add(pm);
            }
            return mList;
        }

        private PlantModel ConvertEntityToModel(PlantEntity e)
        {
            var m = new PlantModel
            {
                Id = e.Id,
                PlantName = e.PlantName
            };
            return m;
        }

        private List<PlantEntity> ConvertModelToEntity(List<PlantModel> mList)
        {
            var eList = new List<PlantEntity>();
            foreach (var pm in mList)
            {
                var pe = ConvertModelToEntity(pm);
                eList.Add(pe);
            }
            return eList;
        }

        private PlantEntity ConvertModelToEntity(PlantModel m)
        {
            var e = new PlantEntity
            {
                Id = m.Id,
                PlantName = m.PlantName
            };
            return e;
        }
        #endregion
    }
}
