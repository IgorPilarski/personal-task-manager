using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalTaskManager.Data;
using PersonalTaskManager.Models;
using System.Security.Claims;

namespace PersonalTaskManager.Controllers
{
    [ApiController]
    [Authorize]
    [Route("tasks")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        // GET /tasks
        [HttpGet]
        public async Task<IActionResult> GetUserTasks()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();

            return Ok(tasks);
        }

        // POST /tasks
        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskItem task)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            task.UserId = userId;
            task.CreatedAt = DateTime.UtcNow;
            task.IsCompleted = false;

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return Ok(task);
        }

        // PUT /tasks/{id}/complete
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteTask(int id)
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var task = await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if (task == null)
                return NotFound();

            task.IsCompleted = true;
            task.CompletedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(task);
        }
    }
}
