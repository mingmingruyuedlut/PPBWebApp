using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Models
{
    public class SectionModel
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public string SectionName { get; set; }

        public SectionModel() { }
        public SectionModel(string areaName, string sectionName)
        {
            AreaName = areaName;
            SectionName = sectionName;
        }
    }
}
