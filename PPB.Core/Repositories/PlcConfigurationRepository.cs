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
    public class PlcConfigurationRepository
    {
        public List<PlcConfigurationEntity> GetPlcConfigurations()
        {
            var query = "SELECT * FROM PLCs_Configuration";
            var dt = SqlHelper.ExcuteDataTable(query);
            return ConvertDbTableToEntityList(dt);
        }

        public List<PlcConfigurationEntity> GetPlcConfigurations(string area, string section, string station)
        {
            var query = "SELECT * FROM PLCs_Configuration WHERE Area_Name = @Area_Name AND Section_Name = @Section_Name AND Station_Name = @Station_Name";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", area));
            param.Add(new OleDbParameter("@Section_Name", section));
            param.Add(new OleDbParameter("@Station_Name", station));
            var dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt);
        }

        public void AddPlcConfiguration(PlcConfigurationModel plcConfig)
        {
            var query = "INSERT INTO PLCs_Configuration (Area_Name, Section_Name, Station_Name, PLC_Name, Member_Name, Member_Value) VALUES (@Area_Name, @Section_Name, @Station_Name, @PLC_Name, @Member_Name, @Member_Value)";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", plcConfig.AreaName));
            param.Add(new OleDbParameter("@Section_Name", plcConfig.SectionName));
            param.Add(new OleDbParameter("@Station_Name", plcConfig.StationName));
            param.Add(new OleDbParameter("@PLC_Name", plcConfig.PlcName));
            param.Add(new OleDbParameter("@Member_Name", plcConfig.MemberName));
            param.Add(new OleDbParameter("@Member_Value", plcConfig.MemberValue));
            SqlHelper.ExcuteNonQuery(CommandType.Text, query, param);
        }

        public void EditPlcConfiguration(PlcConfigurationModel plcConfig)
        {
            var query = "UPDATE PLCs_Configuration SET Member_Value = @Member_Value WHERE ID = @ID";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Member_Value", plcConfig.MemberValue));
            param.Add(new OleDbParameter("@ID", plcConfig.Id));
            SqlHelper.ExcuteNonQuery(CommandType.Text, query, param);
        }

        private List<PlcConfigurationEntity> ConvertDbTableToEntityList(DataTable dt)
        {
            var eList = new List<PlcConfigurationEntity>();
            foreach (DataRow row in dt.Rows)
            {
                var e = ConvertDbTableRowToEntity(row);
                eList.Add(e);
            }
            return eList;
        }

        private PlcConfigurationEntity ConvertDbTableRowToEntity(DataRow row)
        {
            var id = Int32.Parse(row[PlcsConfigurationColumnConstant.Id].ToString());
            var area = row[PlcsConfigurationColumnConstant.AreaName].ToString();
            var section = row[PlcsConfigurationColumnConstant.SectionName].ToString();
            var station = row[PlcsConfigurationColumnConstant.StationName].ToString();
            var plc = row[PlcsConfigurationColumnConstant.PlcName].ToString();
            var memberName = row[PlcsConfigurationColumnConstant.MemberName].ToString();
            var memberValue = row[PlcsConfigurationColumnConstant.MemberValue].ToString();

            return new PlcConfigurationEntity
            {
                Id = id,
                AreaName = area,
                SectionName = section,
                StationName = station,
                PlcName = plc,
                MemberName = memberName,
                MemberValue = memberValue
            };
        }
    }
}
