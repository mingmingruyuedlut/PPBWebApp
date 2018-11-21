using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Entities
{
    public class PlantEntity
    {
        public int Id { get; set; }
        public string PlantName { get; set; }

        public PlantEntity() { }
        public PlantEntity(string name)
        {
            PlantName = name;
        }
        public PlantEntity(int id, string name)
        {
            Id = id;
            PlantName = name;
        }
    }
}
