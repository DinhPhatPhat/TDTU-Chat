using Microsoft.AspNetCore.Mvc;

namespace TDTU_Chat.Controllers
{
    public class PrivateController : Controller
    {
        public IActionResult Index(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            ViewBag.RecipientEmail = email;
            return View();
        }
    }

}
