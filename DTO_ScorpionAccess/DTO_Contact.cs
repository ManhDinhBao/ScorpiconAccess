﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class DTO_Contact
    {
        public string Id { get; set; }
        public EType.ContactType Type { get; set; }
        public EType.ContactMode Mode { get; set; }

        public DTO_Contact()
        {
        }

        public DTO_Contact(string id, EType.ContactType type, EType.ContactMode mode)
        {
            Id = id;
            Type = type;
            Mode = mode;
        }
    }
}
