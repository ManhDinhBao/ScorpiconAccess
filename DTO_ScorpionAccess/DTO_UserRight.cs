using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_UserRight
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DTO_CardHolder> CardHolders { get; set; }
        public List<DTO_Door> Doors { get; set; }
        public List<DTO_Schedule> Schedules { get; set; }

        public DTO_UserRight()
        {
        }

        public DTO_UserRight(string id, string name, string description, List<DTO_CardHolder> cardHolders, List<DTO_Door> doors, List<DTO_Schedule> schedules)
        {
            Id = id;
            Name = name;
            Description = description;
            CardHolders = cardHolders;
            Doors = doors;
            Schedules = schedules;
        }
    }
}
