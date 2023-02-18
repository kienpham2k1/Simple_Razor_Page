using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace PRN_Trial.Pages.Candidate
{
    public class DetailsModel : PageModel
    {
        private readonly ICandidateProfileRepository candidateRepo = null;
        public CandidateProfile Candidate { get; set; }
        public DetailsModel()
        {
            candidateRepo = new CandidateProfileRepository();
        }
        public async Task<IActionResult> OnGet(string id)
        {
            var session = HttpContext.Session;
            if (session.GetInt32("Role") != 2) return RedirectToPage("/Auth/AccessDenied");

            Candidate = candidateRepo.GetCandidateById(id);
            if (Candidate == null)
                return RedirectToPage("NotFoundPage");
            return Page();
        }
    }
}
