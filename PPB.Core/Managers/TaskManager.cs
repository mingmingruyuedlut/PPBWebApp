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
    public class TaskManager
    {
        public TaskSummary GetTaskSummary()
        {
            var taskSummary = new TaskSummary { TaskList = GetTasks() };
            return taskSummary;
        }

        public List<TaskModel> GetTasks()
        {
            var eList = new TaskRepository().GetTasks();
            var mList = ConvertEntityToModel(eList);
            return mList;
        }

        public List<TaskModel> GetTaskInstances(TaskModel tm)
        {
            var tmList = new List<TaskModel>();
            var eTask = new TaskRepository().GetTask(tm.Id);
            if (eTask != null)
            {
                for (var i = 0; i < eTask.MaxNoOfInstances; i++)
                {
                    var mTask = ConvertEntityToModel(eTask);
                    mTask.TaskInstance = i;
                    mTask.TaskInstanceDisplay = i + 1;
                    tmList.Add(mTask);
                }
            }
            return tmList;
        }


        #region Convertor between Entity(DB) and Model(UI)
        private List<TaskModel> ConvertEntityToModel(List<TaskEntity> eList)
        {
            var mList = new List<TaskModel>();
            foreach (TaskEntity e in eList)
            {
                var m = ConvertEntityToModel(e);
                mList.Add(m);
            }
            return mList;
        }

        private TaskModel ConvertEntityToModel(TaskEntity e)
        {
            var m = new TaskModel
            {
                Id = e.Id,
                AreaName = e.AreaName,
                SectionName = e.SectionName,
                StationName = e.StationName,
                MasterFileName = e.MasterFileName,
                TaskName = e.TaskName,
                TaskMemory = e.TaskMemory,
                TaskMemoryPlus = e.TaskMemoryPlus,
                TaskNodes = e.TaskNodes,
                TaskConnection = e.TaskConnection,
                MaxNoOfInstances = e.MaxNoOfInstances,
                ModelAffiliation = e.ModelAffiliation
            };
            return m;
        }

        private List<TaskEntity> ConvertModelToEntity(List<TaskModel> mList)
        {
            var eList = new List<TaskEntity>();
            foreach (TaskModel m in mList)
            {
                var e = ConvertModelToEntity(m);
                eList.Add(e);
            }
            return eList;
        }

        private TaskEntity ConvertModelToEntity(TaskModel m)
        {
            var e = new TaskEntity
            {
                Id = m.Id,
                AreaName = m.AreaName,
                SectionName = m.SectionName,
                StationName = m.StationName,
                MasterFileName = m.MasterFileName,
                TaskName = m.TaskName,
                TaskMemory = m.TaskMemory,
                TaskMemoryPlus = m.TaskMemoryPlus,
                TaskNodes = m.TaskNodes,
                TaskConnection = m.TaskConnection,
                MaxNoOfInstances = m.MaxNoOfInstances,
                ModelAffiliation = m.ModelAffiliation
            };
            return e;
        }

        #endregion
    }
}
