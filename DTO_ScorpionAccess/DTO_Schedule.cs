using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_Schedule
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<DTO_Period> Periods { get; set; }

        public DTO_Schedule()
        {
        }

        public DTO_Schedule(string id, string name, string description, List<DTO_Period> periods)
        {
            Id = id;
            Name = name;
            Description = description;
            Periods = periods;
        }

    }
}
