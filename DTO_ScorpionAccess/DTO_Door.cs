using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_Door
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DTO_DoorMode DoorMode { get; set; }
        public string Description { get; set; }
        public int LOTimeOut { get; set; }
        public int DOTimeOut { get; set; }

        public DTO_Door()
        {
        }

        public DTO_Door(string id, string name, DTO_DoorMode doorMode, string description, int lOTimeOut, int dOTimeOut)
        {
            Id = id;
            Name = name;
            DoorMode = doorMode;
            Description = description;
            LOTimeOut = lOTimeOut;
            DOTimeOut = dOTimeOut;
        }
    }
}
