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
    public class StationRepository
    {
        public List<StationEntity> GetStations()
        {
            var query = "SELECT * FROM Stations";
            var dt = SqlHelper.ExcuteDataTable(query);
            return ConvertDbTableToEntityList(dt);
        }

        public StationEntity GetStation()
        {
            return null;
        }
        
        private List<StationEntity> ConvertDbTableToEntityList(DataTable dt)
        {
            var eList = new List<StationEntity>();
            foreach (DataRow row in dt.Rows)
            {
                var e = ConvertDbTableRowToEntity(row);
                eList.Add(e);
            }
            return eList;
        }

        private StationEntity ConvertDbTableRowToEntity(DataRow row)
        {
            var id = Int32.Parse(row[StationsColumnConstant.Id].ToString());
            var area = row[StationsColumnConstant.AreaName].ToString();
            var section = row[StationsColumnConstant.SectionName].ToString();
            var station = row[StationsColumnConstant.StationName].ToString();
            var stationType = row[StationsColumnConstant.StationType].ToString();
            var plcType = row[StationsColumnConstant.PlcType].ToString();
            var mfName = row[StationsColumnConstant.MasterFileName].ToString();
            var mfRevision = row[StationsColumnConstant.MasterFileRevision].ToString();
            var modelAffiliation = row[StationsColumnConstant.ModelAffiliation].ToString();
            var accept = Int32.Parse(row[StationsColumnConstant.Accept].ToString());
            var instances = Int32.Parse(row[StationsColumnConstant.MaxNoOfInstances].ToString());

            return new StationEntity
            {
                Id = id,
                AreaName = area,
                SectionName = section,
                StationName = station,
                StationType = stationType,
                PlcType = plcType,
                MasterFileName = mfName,
                MasterFileRevision = mfRevision,
                ModelAffiliation = modelAffiliation,
                Accept = accept,
                MaxNoOfInstances = instances
            };
        }
    }
}
