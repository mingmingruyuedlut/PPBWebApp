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
    public class TaskStructureRepository
    {
        public List<TaskStructureEntity> GetTaskStructures(string taskXrefName)
        {
            var query = "SELECT * FROM Task_Structures WHERE TaskXrefName = @TaskXrefName AND Visible = 1 ORDER BY MemberOrder";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@TaskXrefName", taskXrefName));
            var dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt);
        }

        private List<TaskStructureEntity> ConvertDbTableToEntityList(DataTable dt)
        {
            var eList = new List<TaskStructureEntity>();
            foreach (DataRow row in dt.Rows)
            {
                var e = ConvertDbTableRowToEntity(row);
                eList.Add(e);
            }
            return eList;
        }

        private TaskStructureEntity ConvertDbTableRowToEntity(DataRow row)
        {
            var id = Int32.Parse(row[TaskStructureColumnConstant.Id].ToString());
            var taskName = row[TaskStructureColumnConstant.TaskName].ToString();
            var memName = row[TaskStructureColumnConstant.MemberName].ToString();
            var memType = row[TaskStructureColumnConstant.MemberType].ToString();
            var memOrder = string.IsNullOrWhiteSpace(row[TaskStructureColumnConstant.MemberOrder].ToString()) ? 0 : Int32.Parse(row[TaskStructureColumnConstant.MemberOrder].ToString());
            var memDes1 = row[TaskStructureColumnConstant.MemberDescription_1].ToString();
            var memDes2 = row[TaskStructureColumnConstant.MemberDescription_2].ToString();
            var memDes3 = row[TaskStructureColumnConstant.MemberDescription_3].ToString();
            var memValues = row[TaskStructureColumnConstant.MemberValues].ToString();
            var taskXrefName = row[TaskStructureColumnConstant.TaskXrefName].ToString();
            var visible = string.IsNullOrWhiteSpace(row[TaskStructureColumnConstant.Visible].ToString()) ? 0 : Int32.Parse(row[TaskStructureColumnConstant.Visible].ToString());
            var global = string.IsNullOrWhiteSpace(row[TaskStructureColumnConstant.Global].ToString()) ? 0 : Int32.Parse(row[TaskStructureColumnConstant.Global].ToString());
            var base1 = string.IsNullOrWhiteSpace(row[TaskStructureColumnConstant.BASE].ToString()) ? 0 : Int32.Parse(row[TaskStructureColumnConstant.BASE].ToString());
            var maxLength = string.IsNullOrWhiteSpace(row[TaskStructureColumnConstant.MaxLength].ToString()) ? 0 : Int32.Parse(row[TaskStructureColumnConstant.MaxLength].ToString());
            var minValue = string.IsNullOrWhiteSpace(row[TaskStructureColumnConstant.MinValue].ToString()) ? 0 : Int32.Parse(row[TaskStructureColumnConstant.MinValue].ToString());
            var maxValue = string.IsNullOrWhiteSpace(row[TaskStructureColumnConstant.MaxValue].ToString()) ? 0 : Int32.Parse(row[TaskStructureColumnConstant.MaxValue].ToString());

            var exclusion = row[TaskStructureColumnConstant.ExclusionString].ToString();
            var version = row[TaskStructureColumnConstant.Version].ToString();

            return new TaskStructureEntity()
            {
                Id = id,
                TaskName = taskName,
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
                Version = version,
            };
        }
    }
}
