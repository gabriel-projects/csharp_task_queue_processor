using Api.GRRInnovations.TaskQueue.Processor.Domain.Enums;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Domain.Wrappers.In
{
    public class WrapperInTask<TTask> : WrapperBase<TTask, WrapperInTask<TTask>> 
        where TTask : ITaskModel
    {
        public WrapperInTask() { }
        public WrapperInTask(TTask data) : base(data) { }

        [JsonPropertyName("description")]
        public string Description
        {
            get => Data.Description;
            set => Data.Description = value;
        }

        [JsonPropertyName("type")]
        public string Type
        {
            get => Data.Type.ToString();
            set
            {
                if (Enum.TryParse(value, true, out ETaskType result))
                {
                    Data.Type = result;
                }
            }
        }

        [JsonPropertyName("priority")]
        public string Priority
        {
            get => Data.Priority.ToString();
            set
            {
                if (Enum.TryParse(value, true, out ETaskPriority result))
                {
                    Data.Priority = result;
                }
            }
        }
    }
}
