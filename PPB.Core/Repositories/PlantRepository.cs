using PPB.Common;
using PPB.Constant.Constants;
using PPB.DBManager.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Repositories
{
    public class PlantRepository
    {
        public List<PlantEntity> GetPlantList()
        {
            var query = "SELECT * FROM Plant";
            DataTable dt = SqlHelper.ExcuteDataTable(query);
            return ConvertDbTableToPlantList(dt);
        }

        private List<PlantEntity> ConvertDbTableToPlantList(DataTable dt)
        {
            var plantList = new List<PlantEntity>();
            foreach (DataRow row in dt.Rows)
            {
                PlantEntity pl = ConvertDbTableRowToPlant(row);
                plantList.Add(pl);
            }
            return plantList;
        }

        private PlantEntity ConvertDbTableRowToPlant(DataRow row)
        {
            var id = Int32.Parse(row[PlantColumnConstant.Id].ToString());
            var name = row[PlantColumnConstant.PlantName].ToString();
            return new PlantEntity(id, name);
        }
    }
}
