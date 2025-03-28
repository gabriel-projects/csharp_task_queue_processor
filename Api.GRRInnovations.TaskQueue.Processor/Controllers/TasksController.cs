using Api.GRRInnovations.TaskQueue.Processor.Application.Interfaces;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Interfaces;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Models;
using Api.GRRInnovations.TaskQueue.Processor.Domain.Wrappers.In;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.TaskQueue.Processor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WrapperInTask<TaskModel> wrapperInTask)
        {
            var wrapperModel = await wrapperInTask.Result();

            var id = await _taskService.CreateAsync(wrapperModel);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskService.GetByIdAsync(id);
            return task is null ? NotFound() : Ok(task);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] TaskStatus? status)
        {
            //todo: passar o options
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpPost("{id}/retry")]
        public async Task<IActionResult> Retry(Guid id)
        {
            var success = await _taskService.RetryAsync(id);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var success = await _taskService.CancelAsync(id);
            return success ? NoContent() : NotFound();
        }

    }
}
