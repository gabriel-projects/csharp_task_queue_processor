using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Domain.Models
{
    public class RabbitMQSetting
    {
        [Required]
        public string HostName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        public int RetryCount { get; set; }
    }

    public static class RabbitMQQueues
    {
        public const string TaskQueue = "taskQueue";
        public const string TaskQueueDead = "task-queue-dead";
    }
}
