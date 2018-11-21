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
    public class AreaConfigurationRepository
    {
        public List<AreaConfigurationEntity> GetAreaConfigurations()
        {
            var query = "SELECT * FROM Areas_Configuration";
            var dt = SqlHelper.ExcuteDataTable(query);
            return ConvertDbTableToEntityList(dt);
        }

        public List<AreaConfigurationEntity> GetAreaConfigurations(string area)
        {
            var query = "SELECT * FROM Areas_Configuration WHERE Area_Name = @Area_Name";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", area));
            DataTable dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt);
        }

        public List<AreaConfigurationEntity> GetAreaConfigurations(int modelNumber)
        {
            var query = "SELECT * FROM Areas_Configuration WHERE Model_Number > @Model_Number";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Model_Number", modelNumber));
            DataTable dt = SqlHelper.ExcuteDataTable(CommandType.Text, query, param);
            return ConvertDbTableToEntityList(dt);
        }

        public int GetMaxAreaConfigModelNumber()
        {
            var query = "SELECT MAX(Model_Number) AS MaxModelNumber FROM Areas_Configuration";
            var dt = SqlHelper.ExcuteDataTable(query);
            var maxModelNumberStr = dt.Rows[0]["MaxModelNumber"].ToString();
            if (string.IsNullOrWhiteSpace(maxModelNumberStr))
            {
                return 0;
            }

            var maxModelNumber = Int32.Parse(maxModelNumberStr);
            return maxModelNumber;
        }

        public void AddAreaConfiguration(AreaConfigurationModel areaConfig)
        {
            var query = "INSERT INTO Areas_Configuration (Area_Name, Model_Number, Model_Name) VALUES (@Area_Name, @Model_Number, @Model_Name)";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Area_Name", areaConfig.AreaName));
            param.Add(new OleDbParameter("@Model_Number", areaConfig.ModelNumber));
            param.Add(new OleDbParameter("@Model_Name", areaConfig.ModelName));
            SqlHelper.ExcuteNonQuery(CommandType.Text, query, param);
        }

        public void EditAreaConfiguration(AreaConfigurationModel areaConfig)
        {
            var query = "UPDATE Areas_Configuration SET Model_Number = @Model_Number, Model_Name = @Model_Name WHERE ID = @ID";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@Model_Number", areaConfig.ModelNumber));
            param.Add(new OleDbParameter("@Model_Name", areaConfig.ModelName));
            param.Add(new OleDbParameter("@ID", areaConfig.Id));
            SqlHelper.ExcuteNonQuery(CommandType.Text, query, param);
        }

        public void DeleteAreaConfiguration(AreaConfigurationModel areaConfig)
        {
            var query = "Delete * FROM Areas_Configuration WHERE ID = @ID";
            var param = new List<OleDbParameter>();
            param.Add(new OleDbParameter("@ID", areaConfig.Id));
            SqlHelper.ExcuteNonQuery(CommandType.Text, query, param);
        }


        private List<AreaConfigurationEntity> ConvertDbTableToEntityList(DataTable dt)
        {
            var eList = new List<AreaConfigurationEntity>();
            foreach (DataRow row in dt.Rows)
            {
                AreaConfigurationEntity e = ConvertDbTableRowToEntity(row);
                eList.Add(e);
            }
            return eList;
        }

        private AreaConfigurationEntity ConvertDbTableRowToEntity(DataRow row)
        {
            var id = Int32.Parse(row[AreasConfigurationColumnConstant.Id].ToString());
            var area = row[AreasConfigurationColumnConstant.AreaName].ToString();
            var modelNumber = Int32.Parse(row[AreasConfigurationColumnConstant.ModelNumber].ToString());
            var modelName = row[AreasConfigurationColumnConstant.ModelName].ToString();

            return new AreaConfigurationEntity
            {
                Id = id,
                AreaName = area,
                ModelNumber = modelNumber,
                ModelName = modelName
            };
        }
    }
}
