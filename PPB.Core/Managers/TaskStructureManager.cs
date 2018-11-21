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
    public class TaskStructureManager
    {
        public List<TaskStructureModel> GetTaskStructures(string taskXrefName)
        {
            var tseList = new TaskStructureRepository().GetTaskStructures(taskXrefName);
            var tsmList = ConvertEntityToModel(tseList);
            return tsmList;
        }

        #region Convertor between Entity(DB) and Model(UI)
        private List<TaskStructureModel> ConvertEntityToModel(List<TaskStructureEntity> eList)
        {
            var mList = new List<TaskStructureModel>();
            foreach (var e in eList)
            {
                var m = ConvertEntityToModel(e);
                mList.Add(m);
            }
            return mList;
        }

        private TaskStructureModel ConvertEntityToModel(TaskStructureEntity e)
        {
            var m = new TaskStructureModel
            {
                Id = e.Id,
                TaskName = e.TaskName,
                MemberName = e.MemberName,
                MemberType = e.MemberType,
                MemberOrder = e.MemberOrder,
                MemberDescription1 = e.MemberDescription1,
                MemberDescription2 = e.MemberDescription2,
                MemberDescription3 = e.MemberDescription3,
                MemberValues = e.MemberValues,
                TaskXrefName = e.TaskXrefName,
                Visible = e.Visible,
                Global1 = e.Global1,
                Base = e.Base,
                MaxLength = e.MaxLength,
                MinValue = e.MinValue,
                MaxValue = e.MaxValue,
                ExclusionString = e.ExclusionString,
                Version = e.Version
            };
            return m;
        }

        private List<TaskStructureEntity> ConvertModelToEntity(List<TaskStructureModel> mList)
        {
            var eList = new List<TaskStructureEntity>();
            foreach (var m in mList)
            {
                var e = ConvertModelToEntity(m);
                eList.Add(e);
            }
            return eList;
        }

        private TaskStructureEntity ConvertModelToEntity(TaskStructureModel m)
        {
            var e = new TaskStructureEntity
            {
                Id = m.Id,
                TaskName = m.TaskName,
                MemberName = m.MemberName,
                MemberType = m.MemberType,
                MemberOrder = m.MemberOrder,
                MemberDescription1 = m.MemberDescription1,
                MemberDescription2 = m.MemberDescription2,
                MemberDescription3 = m.MemberDescription3,
                MemberValues = m.MemberValues,
                TaskXrefName = m.TaskXrefName,
                Visible = m.Visible,
                Global1 = m.Global1,
                Base = m.Base,
                MaxLength = m.MaxLength,
                MinValue = m.MinValue,
                MaxValue = m.MaxValue,
                ExclusionString = m.ExclusionString,
                Version = m.Version
            };
            return e;
        }

        #endregion
    }
}
