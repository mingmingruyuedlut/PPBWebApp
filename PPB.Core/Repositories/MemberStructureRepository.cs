using PPB.Common;
using PPB.Constant.Constants;
using PPB.DBManager.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Repositories
{
    public class MemberStructureRepository
    {
        public List<MemberStructureEntity> GetMemberStructures()
        {
            var query = "SELECT * FROM Member_Structure WHERE Visible = 1 ORDER BY MemberOrder";
            var dt = SqlHelper.ExcuteDataTable(query);
            return ConvertDbTableToEntityList(dt);
        }

        public List<MemberStructureEntity> GetMemberStructures(string member)
        {
            var query = "SELECT * FROM Member_Structure WHERE Member = @Member Visible = 1 ORDER BY MemberOrder";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Member", member));
            var dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt);
        }

        private List<MemberStructureEntity> ConvertDbTableToEntityList(DataTable dt)
        {
            var eList = new List<MemberStructureEntity>();
            foreach (DataRow row in dt.Rows)
            {
                var e = ConvertDbTableRowToEntity(row);
                eList.Add(e);
            }
            return eList;
        }

        private MemberStructureEntity ConvertDbTableRowToEntity(DataRow row)
        {
            var id = Int32.Parse(row[MemberStructureColumnConstant.Id].ToString());
            var member = row[MemberStructureColumnConstant.Member].ToString();
            var memName = row[MemberStructureColumnConstant.MemberName].ToString();
            var memType = row[MemberStructureColumnConstant.MemberType].ToString();
            var memOrder = string.IsNullOrWhiteSpace(row[MemberStructureColumnConstant.MemberOrder].ToString()) ? 0 : Int32.Parse(row[MemberStructureColumnConstant.MemberOrder].ToString());
            var memDes1 = row[MemberStructureColumnConstant.MemberDescription_1].ToString();
            var memDes2 = row[MemberStructureColumnConstant.MemberDescription_2].ToString();
            var memDes3 = row[MemberStructureColumnConstant.MemberDescription_3].ToString();
            var memValues = row[MemberStructureColumnConstant.MemberValues].ToString();
            var taskXrefName = row[MemberStructureColumnConstant.TaskXrefName].ToString();
            var visible = string.IsNullOrWhiteSpace(row[MemberStructureColumnConstant.Visible].ToString()) ? 0 : Int32.Parse(row[MemberStructureColumnConstant.Visible].ToString());
            var global = string.IsNullOrWhiteSpace(row[MemberStructureColumnConstant.Global].ToString()) ? 0 : Int32.Parse(row[MemberStructureColumnConstant.Global].ToString());
            var base1 = string.IsNullOrWhiteSpace(row[MemberStructureColumnConstant.BASE].ToString()) ? 0 : Int32.Parse(row[MemberStructureColumnConstant.BASE].ToString());
            var maxLength = string.IsNullOrWhiteSpace(row[MemberStructureColumnConstant.MaxLength].ToString()) ? 0 : Int32.Parse(row[MemberStructureColumnConstant.MaxLength].ToString());
            var minValue = string.IsNullOrWhiteSpace(row[MemberStructureColumnConstant.MinValue].ToString()) ? 0 : Int32.Parse(row[MemberStructureColumnConstant.MinValue].ToString());
            var maxValue = string.IsNullOrWhiteSpace(row[MemberStructureColumnConstant.MaxValue].ToString()) ? 0 : Int32.Parse(row[MemberStructureColumnConstant.MaxValue].ToString());

            var exclusion = row[MemberStructureColumnConstant.ExclusionString].ToString();
            var version = row[MemberStructureColumnConstant.Version].ToString();

            return new MemberStructureEntity() {
                Id = id,
                Member = member,
                MemberName = memName,
                MemberType = memType,
                MemberOrder = memOrder,
                MemberDescription1 = memDes1,
                MemberDescription2 = memDes2,
                MemberDescription3 = memDes3,
                MemberValues = memValues,
                TaskXrefName = taskXrefName,
                Visible = visible,
                Global1 = global,
                Base = base1,
                MaxLength = maxLength,
                MinValue = minValue,
                MaxValue = maxValue,
                ExclusionString = exclusion,
                Version = version
            };
        }
    }
}
