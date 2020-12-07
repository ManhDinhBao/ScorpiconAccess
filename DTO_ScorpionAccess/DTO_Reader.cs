using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_Reader
    {
        public string Id { get; set; }
        public EType.ReaderType Type { get; set; }
        public bool IsUse { get; set; }

        public DTO_Reader()
        {
        }

        public DTO_Reader(string id, EType.ReaderType type, bool isUse)
        {
            Id = id;
            Type = type;
            IsUse = isUse;
        }
    }
}
