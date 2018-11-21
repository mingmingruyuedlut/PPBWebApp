using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Entities
{
    public class AreaEntity
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public AreaEntity() { }
        public AreaEntity(string areaName)
        {
            AreaName = areaName;
        }
    }
}
