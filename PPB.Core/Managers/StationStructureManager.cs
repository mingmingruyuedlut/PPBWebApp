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
    public class StationStructureManager
    {
        public List<StationStructureModel> GetStationStructures()
        {
            var sseList = new StationStructureRepository().GetStationStructures();
            var ssmList = ConvertEntityToModel(sseList);
            return ssmList;
        }

        #region Convertor between Entity(DB) and Model(UI)
        private List<StationStructureModel> ConvertEntityToModel(List<StationStructureEntity> eList)
        {
            var mList = new List<StationStructureModel>();
            foreach (var e in eList)
            {
                var m = ConvertEntityToModel(e);
                mList.Add(m);
            }
            return mList;
        }

        private StationStructureModel ConvertEntityToModel(StationStructureEntity e)
        {
            var m = new StationStructureModel
            {
                Id = e.Id,
                Parent = e.Parent,
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
                Version = e.Version,
                MemberGroup = e.MemberGroup
            };
            return m;
        }

        private List<StationStructureEntity> ConvertModelToEntity(List<StationStructureModel> mList)
        {
            var eList = new List<StationStructureEntity>();
            foreach (var m in mList)
            {
                var e = ConvertModelToEntity(m);
                eList.Add(e);
            }
            return eList;
        }

        private StationStructureEntity ConvertModelToEntity(StationStructureModel m)
        {
            var e = new StationStructureEntity
            {
                Id = m.Id,
                Parent = m.Parent,
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
                Version = m.Version,
                MemberGroup = m.MemberGroup
            };
            return e;
        }

        #endregion
    }
}
