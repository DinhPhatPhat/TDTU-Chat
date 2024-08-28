using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TDTU_Chat.Data;
using TDTU_Chat.Models;
using System.Linq;
using System.Threading.Tasks;

namespace TDTU_Chat.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // API để tìm kiếm người dùng bằng email
        [HttpGet]
        [Route("User/SearchByEmail")]
        public async Task<IActionResult> SearchByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            var users = await _context.Users
                            .Where(u => u.Email.Contains(email))
                            .Select(u => new { u.Id, u.Email })
                            .ToListAsync();

            if (users == null || users.Count == 0)
            {
                return NotFound("No users found with that email.");
            }

            return Json(users);
        }

        // Hành động để điều hướng đến trang chat riêng
        [HttpGet]
        [Route("Chat/Private")]
        public async Task<IActionResult> Private(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            var user = await _context.Users
                            .FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            ViewBag.RecipientEmail = email;
            return View();
        }
    }
}
