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
    public class AreasRepository
    {
        public List<AreaEntity> GetAreas()
        {
            var query = "SELECT * FROM Areas";
            var dt = SqlHelper.ExcuteDataTable(query);
            return ConvertDbTableToEntityList(dt);
        }


        private List<AreaEntity> ConvertDbTableToEntityList(DataTable dt)
        {
            var eList = new List<AreaEntity>();
            foreach (DataRow row in dt.Rows)
            {
                AreaEntity e = ConvertDbTableRowToEntity(row);
                eList.Add(e);
            }
            return eList;
        }

        private AreaEntity ConvertDbTableRowToEntity(DataRow row)
        {
            var id = Int32.Parse(row[AreasColumnConstant.Id].ToString());
            var area = row[AreasColumnConstant.AreaName].ToString();

            return new AreaEntity
            {
                Id = id,
                AreaName = area
            };
        }
    }
}
