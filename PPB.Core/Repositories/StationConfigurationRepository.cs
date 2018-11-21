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
    public class StationConfigurationRepository
    {
        public List<StationConfigurationEntity> GetStationConfigurations(string area, string section, string station, int instance)
        {
            var query = "SELECT * FROM Stations_Configuration WHERE Area_Name = @Area_Name AND Section_Name = @Section_Name AND Station_Name = @Station_Name AND Station_Instance = @Station_Instance";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", area));
            param.Add(new OleDbParameter("@Section_Name", section));
            param.Add(new OleDbParameter("@Station_Name", station));
            param.Add(new OleDbParameter("@Station_Instance", instance));
            var dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt);
        }

        public List<StationConfigurationEntity> GetStationConfigurationsLikeMemberName(string area, string section, string station, int instance, string memberNameForIntArray)
        {
            var query = "SELECT * FROM Stations_Configuration WHERE Area_Name = @Area_Name AND Section_Name = @Section_Name AND Station_Name = @Station_Name AND Station_Instance = @Station_Instance AND Member_Name LIKE @Member_Name";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", area));
            param.Add(new OleDbParameter("@Section_Name", section));
            param.Add(new OleDbParameter("@Station_Name", station));
            param.Add(new OleDbParameter("@Station_Instance", instance));
            param.Add(new OleDbParameter("@Member_Name", memberNameForIntArray.Replace("[", "leftbracket").Replace("]", "rightbracket").Replace("leftbracket", "[[]").Replace("rightbracket", "[]]") + "%")); //deal with the special char in sql
            var dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt);
        }

        public void AddStationConfiguration(StationConfigurationModel stationConfig)
        {
            var query = "INSERT INTO Stations_Configuration (Area_Name, Section_Name, Station_Name, Member_Name, Member_Value, Member_Type, Base_Tag, Station_Instance) VALUES (@Area_Name, @Section_Name, @Station_Name, @Member_Name, @Member_Value, @Member_Type, @Base_Tag, @Station_Instance)";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", stationConfig.AreaName));
            param.Add(new OleDbParameter("@Section_Name", stationConfig.SectionName));
            param.Add(new OleDbParameter("@Station_Name", stationConfig.StationName));
            param.Add(new OleDbParameter("@Member_Name", stationConfig.MemberName));
            param.Add(new OleDbParameter("@Member_Value", stationConfig.MemberValue));
            param.Add(new OleDbParameter("@Member_Type", stationConfig.MemberType));
            param.Add(new OleDbParameter("@Base_Tag", stationConfig.BaseTag));
            param.Add(new OleDbParameter("@Station_Instance", stationConfig.StationInstance));
            SqlHelper.ExcuteNonQuery(CommandType.Text, query, param);
        }

        public void UpdateStationConfiguration(StationConfigurationModel stationConfig)
        {
            var query = "UPDATE Stations_Configuration SET Member_Value = @Member_Value WHERE ID = @ID";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Member_Value", stationConfig.MemberValue));
            param.Add(new OleDbParameter("@ID", stationConfig.Id));
            SqlHelper.ExcuteNonQuery(CommandType.Text, query, param);
        }

        public void UpdateStationConfiguration(int id, string memberValue)
        {
            var query = "UPDATE Stations_Configuration SET Member_Value = @Member_Value WHERE ID = @ID";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Member_Value", memberValue));
            param.Add(new OleDbParameter("@ID", id));
            SqlHelper.ExcuteNonQuery(CommandType.Text, query, param);
        }

        private List<StationConfigurationEntity> ConvertDbTableToEntityList(DataTable dt)
        {
            var eList = new List<StationConfigurationEntity>();
            foreach (DataRow row in dt.Rows)
            {
                var e = ConvertDbTableRowToEntity(row);
                eList.Add(e);
            }
            return eList;
        }

        private StationConfigurationEntity ConvertDbTableRowToEntity(DataRow row)
        {
            var id = Int32.Parse(row[StationsConfigurationColumnConstant.Id].ToString());
            var area = row[StationsConfigurationColumnConstant.AreaName].ToString();
            var section = row[StationsConfigurationColumnConstant.SectionName].ToString();
            var station = row[StationsConfigurationColumnConstant.StationName].ToString();
            var memberName = row[StationsConfigurationColumnConstant.MemberName].ToString();
            var memberValue = row[StationsConfigurationColumnConstant.MemberValue].ToString();
            var memberType = row[StationsConfigurationColumnConstant.MemberType].ToString();
            var baseTag = row[StationsConfigurationColumnConstant.BaseTag].ToString();
            var instance = string.IsNullOrWhiteSpace(row[StationsConfigurationColumnConstant.StationInstance].ToString()) ? -1 : Int32.Parse(row[StationsConfigurationColumnConstant.StationInstance].ToString());

            return new StationConfigurationEntity(id, area, section, station, memberName, memberValue, memberType , baseTag, instance);
        }
    }
}
