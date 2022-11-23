﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code.Repository.EntityFramework.Models
{
    public class ProcedureDTO
    {
        public ProcedureDTO()
        {
            this.parametros = new Dictionary<string, object>();
        }
        public string commandText { get; set; }
        public Dictionary<string, object> parametros { get; set; }
    }
}
