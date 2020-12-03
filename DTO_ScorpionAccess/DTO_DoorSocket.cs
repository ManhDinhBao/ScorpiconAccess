using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DTO_ScorpionAccess.EType;

namespace DTO_ScorpionAccess
{
    public class DTO_DoorSocket
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Door { get; set; }
        public DoorSocketType Type { get; set; }
        public object Property { get; set; }

        public DTO_DoorSocket()
        {
        }

        public DTO_DoorSocket(string id, string name, string door, DoorSocketType type, object property)
        {
            Id = id;
            Name = name;
            Door = door;
            Type = type;
            Property = property;
        }
    }
}
