using PPB.Common;
using PPB.Constant.Constants;
using PPB.DBManager.Entities;
using PPB.DBManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Repositories
{
    public class TaskConfigurationRepository
    {
        public List<TaskConfigurationEntity> GetTaskConfigurations(string area, string section, string station, string task)
        {
            var query = "SELECT * FROM Tasks_Configuration WHERE Area_Name = @Area_Name AND Section_Name = @Section_Name AND Station_Name = @Station_Name AND Task_Name = @Task_Name";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", area));
            param.Add(new OleDbParameter("@Section_Name", section));
            param.Add(new OleDbParameter("@Station_Name", station));
            param.Add(new OleDbParameter("@Task_Name", task));
            var dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt);
        }

        public List<TaskConfigurationEntity> GetTaskConfigurations(string area, string section, string station, string task, int instance)
        {
            var query = "SELECT * FROM Tasks_Configuration WHERE Area_Name = @Area_Name AND Section_Name = @Section_Name AND Station_Name = @Station_Name AND Task_Name = @Task_Name AND Task_Instance = @Task_Instance";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", area));
            param.Add(new OleDbParameter("@Section_Name", section));
            param.Add(new OleDbParameter("@Station_Name", station));
            param.Add(new OleDbParameter("@Task_Name", task));
            param.Add(new OleDbParameter("@Task_Instance", instance));
            var dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt);
        }

        /// <summary>
        /// This is for bool array type
        /// </summary>
        /// <param name="area"></param>
        /// <param name="section"></param>
        /// <param name="station"></param>
        /// <param name="task"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public List<TaskConfigurationEntity> GetTaskConfigurations(string area, string section, string station, string task, int instance, string memberNameForBoolArray)
        {
            var query = "SELECT * FROM Tasks_Configuration WHERE Area_Name = @Area_Name AND Section_Name = @Section_Name AND Station_Name = @Station_Name AND Task_Name = @Task_Name AND Task_Instance = @Task_Instance AND Member_Name = @Member_Name";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", area));
            param.Add(new OleDbParameter("@Section_Name", section));
            param.Add(new OleDbParameter("@Station_Name", station));
            param.Add(new OleDbParameter("@Task_Name", task));
            param.Add(new OleDbParameter("@Task_Instance", instance));
            param.Add(new OleDbParameter("@Member_Name", memberNameForBoolArray));
            var dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt);
        }

        public List<TaskConfigurationEntity> GetTaskConfigurationsLikeMemberName(string area, string section, string station, string task, int instance, string memberNameForIntArray)
        {
            var query = "SELECT * FROM Tasks_Configuration WHERE Area_Name = @Area_Name AND Section_Name = @Section_Name AND Station_Name = @Station_Name AND Task_Name = @Task_Name AND Task_Instance = @Task_Instance AND Member_Name LIKE @Member_Name";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", area));
            param.Add(new OleDbParameter("@Section_Name", section));
            param.Add(new OleDbParameter("@Station_Name", station));
            param.Add(new OleDbParameter("@Task_Name", task));
            param.Add(new OleDbParameter("@Task_Instance", instance));
            param.Add(new OleDbParameter("@Member_Name", memberNameForIntArray.Replace("[", "leftbracket").Replace("]", "rightbracket").Replace("leftbracket", "[[]").Replace("rightbracket", "[]]") + "%")); //deal with the special char in sql
            var dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt);
        }

        public void AddTaskConfiguration(TaskConfigurationModel taskConfig)
        {
            var query = "INSERT INTO Tasks_Configuration (Area_Name, Section_Name, Station_Name, Task_Name, Member_Name, Member_Value, Member_Type, Base_Tag, Task_Instance) VALUES (@Area_Name, @Section_Name, @Station_Name, @Task_Name, @Member_Name, @Member_Value, @Member_Type, @Base_Tag, @Task_Instance)";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", taskConfig.AreaName));
            param.Add(new OleDbParameter("@Section_Name", taskConfig.SectionName));
            param.Add(new OleDbParameter("@Station_Name", taskConfig.StationName));
            param.Add(new OleDbParameter("@Task_Name", taskConfig.TaskName));
            param.Add(new OleDbParameter("@Member_Name", taskConfig.MemberName));
            param.Add(new OleDbParameter("@Member_Value", taskConfig.MemberValue));
            param.Add(new OleDbParameter("@Member_Type", taskConfig.MemberType));
            param.Add(new OleDbParameter("@Base_Tag", taskConfig.BaseTag));
            param.Add(new OleDbParameter("@Task_Instance", taskConfig.TaskInstance));
            SqlHelper.ExcuteNonQuery(CommandType.Text, query, param);
        }

        public void UpdateTaskConfiguration(TaskConfigurationModel taskConfig)
        {
            var query = "UPDATE Tasks_Configuration SET Member_Value = @Member_Value WHERE ID = @ID";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Member_Value", taskConfig.MemberValue));
            param.Add(new OleDbParameter("@ID", taskConfig.Id));
            SqlHelper.ExcuteNonQuery(CommandType.Text, query, param);
        }

        public void UpdateTaskConfiguration(int id, string memberValue)
        {
            var query = "UPDATE Tasks_Configuration SET Member_Value = @Member_Value WHERE ID = @ID";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Member_Value", memberValue));
            param.Add(new OleDbParameter("@ID", id));
            SqlHelper.ExcuteNonQuery(CommandType.Text, query, param);
        }

        private List<TaskConfigurationEntity> ConvertDbTableToEntityList(DataTable dt)
        {
            var eList = new List<TaskConfigurationEntity>();
            foreach (DataRow row in dt.Rows)
            {
                var e = ConvertDbTableRowToEntity(row);
                eList.Add(e);
            }
            return eList;
        }

        private TaskConfigurationEntity ConvertDbTableRowToEntity(DataRow row)
        {
            var id = Int32.Parse(row[TasksConfigurationColumnConstant.Id].ToString());
            var area = row[TasksConfigurationColumnConstant.AreaName].ToString();
            var section = row[TasksConfigurationColumnConstant.SectionName].ToString();
            var station = row[TasksConfigurationColumnConstant.StationName].ToString();
            var taskName = row[TasksConfigurationColumnConstant.TaskName].ToString();
            var instance = string.IsNullOrWhiteSpace(row[TasksConfigurationColumnConstant.TaskInstance].ToString()) ? -1 : Int32.Parse(row[TasksConfigurationColumnConstant.TaskInstance].ToString());
            var memberName = row[TasksConfigurationColumnConstant.MemberName].ToString();
            var memberValue = row[TasksConfigurationColumnConstant.MemberValue].ToString();
            var memberType = row[TasksConfigurationColumnConstant.MemberType].ToString();
            var baseTag = row[TasksConfigurationColumnConstant.BaseTag].ToString();

            return new TaskConfigurationEntity()
            {
                Id = id,
                AreaName = area,
                SectionName = section,
                StationName = station,
                TaskName = taskName,
                TaskInstance = instance,
                MemberName = memberName,
                MemberValue = memberValue,
                MemberType = memberType,
                BaseTag = baseTag
            };
        }
    }
}
