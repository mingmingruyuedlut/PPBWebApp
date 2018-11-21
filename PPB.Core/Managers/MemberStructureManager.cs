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
    public class MemberStructureManager
    {
        public List<MemberStructureModel> GetMemberStructureList()
        {
            var mseList = new MemberStructureRepository().GetMemberStructures();
            var msmList = ConvertEntityToModel(mseList);
            return msmList;
        }

        public List<MemberStructureModel> GetMemberStructureList(string member)
        {
            var mseList = new MemberStructureRepository().GetMemberStructures(member);
            var msmList = ConvertEntityToModel(mseList);
            return msmList;
        }


        #region Convertor between Entity(DB) and Model(UI)
        private List<MemberStructureModel> ConvertEntityToModel(List<MemberStructureEntity> eList)
        {
            var mList = new List<MemberStructureModel>();
            foreach (var e in eList)
            {
                var m = ConvertEntityToModel(e);
                mList.Add(m);
            }
            return mList;
        }

        private MemberStructureModel ConvertEntityToModel(MemberStructureEntity e)
        {
            var m = new MemberStructureModel
            {
                Id = e.Id,
                Member = e.Member,
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

        private List<MemberStructureEntity> ConvertModelToEntity(List<MemberStructureModel> mList)
        {
            var eList = new List<MemberStructureEntity>();
            foreach (var m in mList)
            {
                var e = ConvertModelToEntity(m);
                eList.Add(e);
            }
            return eList;
        }

        private MemberStructureEntity ConvertModelToEntity(MemberStructureModel m)
        {
            var e = new MemberStructureEntity
            {
                Id = m.Id,
                Member = m.Member,
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
