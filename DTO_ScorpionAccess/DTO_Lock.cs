using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_Lock
    {
        public string Id { get; set; }
        public EType.LockType Type { get; set; }

        public DTO_Lock()
        {
        }

        public DTO_Lock(string id, EType.LockType type)
        {
            Id = id;
            Type = type;
        }
    }
}
