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
    public class StationStructureRepository
    {
        public List<StationStructureEntity> GetStationStructures()
        {
            var query = "SELECT * FROM Station_Structures WHERE Visible = 1 ORDER BY MemberOrder";
            var dt = SqlHelper.ExcuteDataTable(query);
            return ConvertDbTableToEntityList(dt);
        }

        public List<StationStructureEntity> GetStationStructuresOfIntArrayType()
        {
            var query = "SELECT * FROM Station_Structures WHERE Visible = 1 AND MemberType LIKE 'INT[%' ORDER BY MemberOrder";
            var dt = SqlHelper.ExcuteDataTable(query);
            return ConvertDbTableToEntityList(dt);
        }

        private List<StationStructureEntity> ConvertDbTableToEntityList(DataTable dt)
        {
            var eList = new List<StationStructureEntity>();
            foreach (DataRow row in dt.Rows)
            {
                var e = ConvertDbTableRowToEntity(row);
                eList.Add(e);
            }
            return eList;
        }

        private StationStructureEntity ConvertDbTableRowToEntity(DataRow row)
        {
            var id = Int32.Parse(row[StationStructureColumnConstant.Id].ToString());
            var parent = row[StationStructureColumnConstant.Parent].ToString();
            var memName = row[StationStructureColumnConstant.MemberName].ToString();
            var memType = row[StationStructureColumnConstant.MemberType].ToString();
            var memOrder = string.IsNullOrWhiteSpace(row[StationStructureColumnConstant.MemberOrder].ToString()) ? 0 : Int32.Parse(row[StationStructureColumnConstant.MemberOrder].ToString());
            var memDes1 = row[StationStructureColumnConstant.MemberDescription_1].ToString();
            var memDes2 = row[StationStructureColumnConstant.MemberDescription_2].ToString();
            var memDes3 = row[StationStructureColumnConstant.MemberDescription_3].ToString();
            var memValues = row[StationStructureColumnConstant.MemberValues].ToString();
            var taskXrefName = row[StationStructureColumnConstant.TaskXrefName].ToString();
            var visible = string.IsNullOrWhiteSpace(row[StationStructureColumnConstant.Visible].ToString()) ? 0 : Int32.Parse(row[StationStructureColumnConstant.Visible].ToString());
            var global = string.IsNullOrWhiteSpace(row[StationStructureColumnConstant.Global].ToString()) ? 0 : Int32.Parse(row[StationStructureColumnConstant.Global].ToString());
            var base1 = string.IsNullOrWhiteSpace(row[StationStructureColumnConstant.BASE].ToString()) ? 0 : Int32.Parse(row[StationStructureColumnConstant.BASE].ToString());
            var maxLength = string.IsNullOrWhiteSpace(row[StationStructureColumnConstant.MaxLength].ToString()) ? 0 : Int32.Parse(row[StationStructureColumnConstant.MaxLength].ToString());
            var minValue = string.IsNullOrWhiteSpace(row[StationStructureColumnConstant.MinValue].ToString()) ? 0 : Int32.Parse(row[StationStructureColumnConstant.MinValue].ToString());
            var maxValue = string.IsNullOrWhiteSpace(row[StationStructureColumnConstant.MaxValue].ToString()) ? 0 : Int32.Parse(row[StationStructureColumnConstant.MaxValue].ToString());

            var exclusion = row[StationStructureColumnConstant.ExclusionString].ToString();
            var version = row[StationStructureColumnConstant.Version].ToString();
            var memGroup = row[StationStructureColumnConstant.MemberGroup].ToString();

            return new StationStructureEntity(id, parent, memName, memType, memOrder, memDes1, memDes2, memDes3, memValues, taskXrefName, visible, global, base1, maxLength, minValue, maxValue, exclusion, version, memGroup);
        }
    }
}
