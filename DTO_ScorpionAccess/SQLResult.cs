﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_ScorpionAccess
{
    public class SQLResult
    {
        public bool Result { get; set; }
        public string Detail { get; set; }
        public string ExtraData { get; set; }

        public SQLResult(bool result, string detail)
        {
            Result = result;
            Detail = detail;
        }

        public SQLResult(bool result, string detail, string extraData) : this(result, detail)
        {
            ExtraData = extraData;
        }

        public SQLResult()
        {
        }

    }
}
