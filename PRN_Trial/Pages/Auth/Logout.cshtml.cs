using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace PRN_Trial.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        public void OnGet()
        {
            //var session = HttpContext.Session;
            //session.Clear();
            //return RedirectToPage("Login");
        }
        public IActionResult OnPost()
        {
            var session = HttpContext.Session;
            session.Clear();
            return RedirectToPage("Login");
        }
    }
}
