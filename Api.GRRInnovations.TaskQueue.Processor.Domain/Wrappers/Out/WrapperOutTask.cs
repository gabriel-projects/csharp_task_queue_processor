using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Enums;
using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.GRRInnovations.TaskQueue.Processor.Domain.Wrappers.Out
{
    public class WrapperOutTask : WrapperBase<ITaskModel>
    {
        public WrapperOutTask() { }
        public WrapperOutTask(ITaskModel data) : base(data) { }


        [JsonPropertyName("uid")]
        public Guid Uid
        {
            get => Data.Uid;
            set => Data.Uid = value;
        }

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

        [JsonPropertyName("status")]
        public string Status
        {
            get => Data.Status.ToString();
            set
            {
                if (Enum.TryParse(value, true, out ETaskStatus result))
                {
                    Data.Status = result;
                }
            }
        }

        [JsonPropertyName("started_at")]
        public DateTime? StartedAt
        {
            get => Data.StartedAt;
            set => Data.StartedAt = value;
        }

        [JsonPropertyName("completed_at")]
        public DateTime? CompletedAt
        {
            get => Data?.CompletedAt;
            set => Data.CompletedAt = value;
        }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt
        {
            get => Data.CreatedAt;
            set => Data.CreatedAt = value;
        }

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt
        {
            get => Data.UpdatedAt;
            set => Data.UpdatedAt = value;
        }

        public new static async Task<WrapperOutTask> From(ITaskModel model)
        {
            var wrapper = new WrapperOutTask();
            await wrapper.Populate(model).ConfigureAwait(false);

            return wrapper;
        }

        public new static async Task<List<WrapperOutTask>> From(List<ITaskModel> models)
        {
            var result = new List<WrapperOutTask>();

            foreach (var model in models)
            {
                var wrapper = await From(model).ConfigureAwait(false);
                result.Add(wrapper);
            }

            return result;
        }
    }
}
