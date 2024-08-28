using Microsoft.AspNetCore.Mvc;

public class ChatController : Controller
{
    public IActionResult Private(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email is required.");
        }

        ViewBag.RecipientEmail = email;
        return View("Index"); // Trả về view `index.cshtml` trong thư mục `Views/Private`
    }
}
