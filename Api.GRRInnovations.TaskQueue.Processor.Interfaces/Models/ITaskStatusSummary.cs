using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models
{
    public interface ITaskStatusSummary
    {
        public int Pending { get; set; }
        public int Processing { get; set; }
        public int Completed { get; set; }
        public int Cancelled { get; set; }
        public int Error { get; set; }
    }
}
