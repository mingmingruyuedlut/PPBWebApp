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
    public class StationManager
    {
        public StationSummary GetStationSummary()
        {
            StationSummary stationSummary = new StationSummary { StationList = GetStations() };
            return stationSummary;
        }

        public List<StationModel> GetStations()
        {
            List<StationEntity> seList = new StationRepository().GetStations();
            List<StationModel> smList = ConvertEntityToModel(seList);
            return smList;
        }


        #region Convertor between Entity(DB) and Model(UI)
        private List<StationModel> ConvertEntityToModel(List<StationEntity> eList)
        {
            List<StationModel> mList = new List<StationModel>();
            foreach (StationEntity e in eList)
            {
                StationModel m = ConvertEntityToModel(e);
                m.StationInstance = -1; //to-do: suit for multiple station
                mList.Add(m);
            }
            return mList;
        }

        private StationModel ConvertEntityToModel(StationEntity e)
        {
            StationModel m = new StationModel
            {
                Id = e.Id,
                AreaName = e.AreaName,
                SectionName = e.SectionName,
                StationName = e.StationName,
                StationType = e.StationType,
                MasterFileName = e.MasterFileName,
                MasterFileRevision = e.MasterFileRevision,
                ModelAffiliation = e.ModelAffiliation,
                Accept = e.Accept,
                MaxNoOfInstances = e.MaxNoOfInstances
            };
            return m;
        }

        private List<StationEntity> ConvertModelToEntity(List<StationModel> mList)
        {
            List<StationEntity> eList = new List<StationEntity>();
            foreach (StationModel m in mList)
            {
                StationEntity e = ConvertModelToEntity(m);
                eList.Add(e);
            }
            return eList;
        }

        private StationEntity ConvertModelToEntity(StationModel m)
        {
            StationEntity e = new StationEntity
            {
                Id = m.Id,
                AreaName = m.AreaName,
                SectionName = m.SectionName,
                StationName = m.StationName,
                StationType = m.StationType,
                MasterFileName = m.MasterFileName,
                MasterFileRevision = m.MasterFileRevision,
                ModelAffiliation = m.ModelAffiliation,
                Accept = m.Accept,
                MaxNoOfInstances = m.MaxNoOfInstances
            };
            return e;
        }

        #endregion
    }
}
