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
    public class TaskRepository
    {
        public List<TaskEntity> GetTasks()
        {
            var query = "SELECT * FROM Tasks";
            var dt = SqlHelper.ExcuteDataTable(query);
            return ConvertDbTableToEntityList(dt);
        }

        public TaskEntity GetTask(int id)
        {
            var query = "SELECT * FROM Tasks WHERE ID = @ID";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@ID", id));
            var dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt).FirstOrDefault();
        }

        private List<TaskEntity> ConvertDbTableToEntityList(DataTable dt)
        {
            var eList = new List<TaskEntity>();
            foreach (DataRow row in dt.Rows)
            {
                var e = ConvertDbTableRowToEntity(row);
                eList.Add(e);
            }
            return eList;
        }

        private TaskEntity ConvertDbTableRowToEntity(DataRow row)
        {
            var id = Int32.Parse(row[TasksColumnConstant.Id].ToString());
            var area = row[TasksColumnConstant.AreaName].ToString();
            var section = row[TasksColumnConstant.SectionName].ToString();
            var station = row[TasksColumnConstant.StationName].ToString();
            var mfName = row[TasksColumnConstant.MasterFileName].ToString();
            var taskName = row[TasksColumnConstant.TaskName].ToString();
            var taskMemory = Int32.Parse(row[TasksColumnConstant.TaskMemory].ToString());
            var taskMemoryPlus = Int32.Parse(row[TasksColumnConstant.TaskMemoryPlus].ToString());
            var taskNodes = Int32.Parse(row[TasksColumnConstant.TaskNodes].ToString());
            var taskConnection = Int32.Parse(row[TasksColumnConstant.TaskConnection].ToString());
            var instances = Int32.Parse(row[TasksColumnConstant.MaxNoOfInstances].ToString());
            var modelAffiliation = row[TasksColumnConstant.ModelAffiliation].ToString();

            return new TaskEntity
            {
                Id = id,
                AreaName = area,
                SectionName = section,
                StationName = station,
                MasterFileName = mfName,
                TaskName = taskName,
                TaskMemory = taskMemory,
                TaskMemoryPlus = taskMemoryPlus,
                TaskNodes = taskNodes,
                TaskConnection = taskConnection,
                MaxNoOfInstances = instances,
                ModelAffiliation = modelAffiliation
            };
        }
    }
}
