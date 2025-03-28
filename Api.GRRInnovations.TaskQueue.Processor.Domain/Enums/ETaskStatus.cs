﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Domain.Enums
{
    public enum ETaskStatus
    {
        Pending,
        Processing,
        Completed,
        Error,
        Cancelled
    }
}
