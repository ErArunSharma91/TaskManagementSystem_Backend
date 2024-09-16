using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.API.Data;
using TaskManagementSystem.API.Models;
using System.Threading.Tasks;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly TaskManagementDbContext _context;

        public TaskController(TaskManagementDbContext context)
        {
            _context = context;
        }

        // Create a new Task (Add Task)
        [HttpPost("add")]
        public async Task<IActionResult> CreateTask([FromBody] UserTask task)
        {
            if (task == null || string.IsNullOrEmpty(task.Title))
            {
                return BadRequest("Task data is invalid.");
            }

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        // Get all tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _context.Tasks.ToListAsync();
            return Ok(tasks);
        }

        // Get a single Task by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }
            return Ok(task);
        }

        // Edit/Update an existing Task
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditTask(int id, [FromBody] UserTask updatedTask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            // Update task properties
            task.Title = updatedTask.Title;
            task.Description = updatedTask.Description;
            task.DueDate = updatedTask.DueDate;
            task.IsCompleted = updatedTask.IsCompleted;

            await _context.SaveChangesAsync();
            return Ok(task);
        }

        // Delete a Task by Id
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return Ok("Task deleted successfully.");
        }

        // Mark a Task as Complete
        [HttpPut("complete/{id}")]
        public async Task<IActionResult> MarkTaskAsComplete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound("Task not found.");
            }

            task.IsCompleted = true;
            await _context.SaveChangesAsync();
            return Ok("Task marked as completed.");
        }
    }
}
