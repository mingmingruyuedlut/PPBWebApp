using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Models
{
    public class PlantModel
    {
        public int Id { get; set; }
        public string PlantName { get; set; }

        public PlantModel() { }
        public PlantModel(string name)
        {
            PlantName = name;
        }
        public PlantModel(int id, string name)
        {
            Id = id;
            PlantName = name;
        }
    }
}
