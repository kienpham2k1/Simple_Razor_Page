using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace PRN_Trial.Pages.Candidate
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ICandidateProfileRepository candidateRepo = null;
        public CandidateProfile Candidate { get; set; }
        public DeleteModel()
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
        public async Task<IActionResult> OnPost()
        {
            var session = HttpContext.Session;
            if (session.GetInt32("Role") != 2) return RedirectToPage("/Auth/AccessDenied");
            try
            {
                if (candidateRepo.GetCandidateById(Candidate.CandidateId) == null) {
                    //throw new Exception("Not found candidate");
                    return RedirectToPage("NotFoundCandidate");
                }
                candidateRepo.DeleteCandidate(Candidate.CandidateId);
                return RedirectToPage("Index");
            }
            catch (Exception e)
            {
                ViewData["Message"] = e.Message;
                return Page();
            }
        }
    }
}
