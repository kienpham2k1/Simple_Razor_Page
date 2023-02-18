using BusinessObject.Models;
using DataAccess;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using DataAccess.Utils;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http;

namespace PRN_Trial.Pages.Candidate
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ICandidateProfileRepository candidateRepo = null;
        private readonly IJobPostingRepository jobPostingRepo = null;
        public CandidateProfile Candidate { get; set; }
        public CreateModel()
        {
            candidateRepo = new CandidateProfileRepository();
            jobPostingRepo = new JobPostingRepository();
        }
        public async Task<IActionResult> OnGet()
        {
            var session = HttpContext.Session;
            if (session.GetInt32("Role") != 2) return RedirectToPage("/Auth/AccessDenied");
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
                string nextId = candidateRepo.GetNextId();
                Candidate.CandidateId = nextId;
                candidateRepo.CreateCandidate(Candidate);
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
