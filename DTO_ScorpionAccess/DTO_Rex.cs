using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_Rex
    {
        public string Id { get; set; }
        public EType.RexType Type { get; set; }

        public DTO_Rex()
        {
        }

        public DTO_Rex(string id, EType.RexType type)
        {
            Id = id;
            Type = type;
        }
    }
}
