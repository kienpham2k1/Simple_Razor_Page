using BusinessObject.Models;
using DataAccess.Repository;
using DataAccess.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PRN_Trial.Pages.Candidate
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ICandidateProfileRepository candidateRepo = null;
        private readonly IJobPostingRepository jobPostingRepo = null;
        public CandidateProfile Candidate { get; set; }
        public EditModel()
        {
            candidateRepo = new CandidateProfileRepository();
            jobPostingRepo = new JobPostingRepository();
        }
        public async Task<IActionResult> OnGet(string id)
        {
            var session = HttpContext.Session;
            if (session.GetInt32("Role") != 2) return RedirectToPage("/Auth/AccessDenied");

            Candidate = candidateRepo.GetCandidateById(id);
            if (Candidate == null) return RedirectToPage("NotFoundPage");
            IEnumerable<JobPosting> jops = jobPostingRepo.GetAll();
            ViewData["jobPostings"] = jops;
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            var session = HttpContext.Session;
            if (session.GetInt32("Role") != 2) return RedirectToPage("/Auth/AccessDenied");

            if (!String.IsNullOrEmpty(Candidate.Fullname))
                if (!Validation.ValidateCapitalName(Candidate.Fullname))
                    ModelState.AddModelError(string.Empty, "Name must capital each letter");
            if (ModelState.IsValid)
            {
                candidateRepo.UpdateCandidate(Candidate);
                return RedirectToPage("Index");
            }
            else
            {
                IEnumerable<JobPosting> jops = jobPostingRepo.GetAll();
                ViewData["jobPostings"] = jops;
                return Page();
            }
        }
    }
}