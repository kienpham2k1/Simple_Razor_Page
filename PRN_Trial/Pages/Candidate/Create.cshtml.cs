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
using System.Data.SqlTypes;

namespace PRN_Trial.Pages.Candidate
{
    public class CreateModel : PageModel
    {
        private readonly ICandidateProfileRepository candidateRepo = null;
        private readonly IJobPostingRepository jobPostingRepo = null;
        [BindProperty]
        public CandidateProfile Candidate { get; set; }
        public CreateModel()
        {
            candidateRepo = new CandidateProfileRepository();
            jobPostingRepo = new JobPostingRepository();
        }
        public IActionResult OnGet()
        {
            var session = HttpContext.Session;
            if (session.GetInt32("Role") != 2) return RedirectToPage("/Auth/AccessDenied");
            ViewData["jobPostings"] = jobPostingRepo.GetAll();
            return Page();
        }
        public IActionResult OnPost()
        {
            var session = HttpContext.Session;
            if (session.GetInt32("Role") != 2) return RedirectToPage("/Auth/AccessDenied");
            //Validate name
            if (!string.IsNullOrEmpty(Candidate.Fullname))
                if (!Validation.ValidateCapitalName(Candidate.Fullname))
                    ModelState.AddModelError(string.Empty, "Name must capital each letter.");
            //Validate birth day
            if (Candidate.Birthday < (DateTime)SqlDateTime.MinValue)
                ModelState.AddModelError(string.Empty, $"Birth day must greater {SqlDateTime.MinValue}.");

            if (ModelState.IsValid)
            {
                    Candidate.CandidateId = candidateRepo.GetNextId();
                    candidateRepo.CreateCandidate(Candidate);
                    return RedirectToPage("Index");
            }
            ViewData["jobPostings"] = jobPostingRepo.GetAll();
            return Page();

        }
    }
}
