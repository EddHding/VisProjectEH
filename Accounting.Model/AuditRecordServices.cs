﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Model
{
    public class AuditRecordServices : AuditRecord
    {        
        public virtual string ServiceName { get; set; }
    }
}
