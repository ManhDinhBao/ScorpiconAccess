using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_DoorMode
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReaderUse { get; set; }
        public int InputUse { get; set; }
        public int OutputUse { get; set; }

        public DTO_DoorMode()
        {
        }

        public DTO_DoorMode(string id, string name, string description, int readerUse, int inputUse, int outputUse)
        {
            Id = id;
            Name = name;
            Description = description;
            ReaderUse = readerUse;
            InputUse = inputUse;
            OutputUse = outputUse;
        }
    }
}
