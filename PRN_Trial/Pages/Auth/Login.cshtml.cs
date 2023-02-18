using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Http;

namespace PRN_Trial.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IHraccountRepository HrAccountRepo = null;
        public Hraccount hraccount { get; set; }
        public LoginModel()
        {
            HrAccountRepo = new HraccountRepository();
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(string email, string password)
        {

            if (ModelState.IsValid)
            {
            var session = HttpContext.Session;
                try
                {
                    var loginAccount = HrAccountRepo.Login(email, password);
                    if (loginAccount.MemberRole == 2)
                    {
                        if (session.GetInt32("Role") == null)
                        {
                            session.SetInt32("Role", 2);
                            session.SetString("FullName", loginAccount.FullName);
                            return RedirectToPage("/Candidate/Index");
                        }
                    }
                    else throw new Exception("You are not allowed to do this function!");
                }
                catch (Exception e) { ViewData["Message"] = e.Message; return Page(); }
            }
            return Page();
        }

    }
}
