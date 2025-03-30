using Api.GRRInnovations.TaskQueue.Processor.Interfaces.Models;
using System.Text.Json.Serialization;

namespace Api.GRRInnovations.TaskQueue.Processor.Domain.Wrappers.Out
{
    public class WrapperOutTaskDiagnostics : WrapperBase<ITaskStatusSummary>
    {
        public WrapperOutTaskDiagnostics() : base(null) { }
        public WrapperOutTaskDiagnostics(ITaskStatusSummary data) : base(data) { }

        [JsonPropertyName("pending")]
        public int Pending
        {
            get => Data.Pending;
            set => Data.Pending = value;
        }

        [JsonPropertyName("processing")]
        public int Processing
        {
            get => Data.Processing;
            set => Data.Processing = value;
        }

        [JsonPropertyName("completed")]
        public int Completed
        {
            get => Data.Completed;
            set => Data.Completed = value;
        }

        [JsonPropertyName("cancelled")]
        public int Cancelled
        {
            get => Data.Cancelled;
            set => Data.Cancelled = value;
        }

        [JsonPropertyName("error")]
        public int Error
        {
            get => Data.Error;
            set => Data.Error = value;
        }

        public new static async Task<WrapperOutTaskDiagnostics> From(ITaskStatusSummary model)
        {
            var wrapper = new WrapperOutTaskDiagnostics();
            await wrapper.Populate(model).ConfigureAwait(false);

            return wrapper;
        }

        public new static async Task<List<WrapperOutTaskDiagnostics>> From(List<ITaskStatusSummary> models)
        {
            var result = new List<WrapperOutTaskDiagnostics>();

            foreach (var model in models)
            {
                var wrapper = await From(model).ConfigureAwait(false);
                result.Add(wrapper);
            }

            return result;
        }
    }
}
