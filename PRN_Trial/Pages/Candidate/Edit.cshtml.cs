using BusinessObject.Models;
using DataAccess.Repository;
using DataAccess.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Threading.Tasks;

namespace PRN_Trial.Pages.Candidate
{
    public class EditModel : PageModel
    {
        private readonly ICandidateProfileRepository candidateRepo = null;
        private readonly IJobPostingRepository jobPostingRepo = null;
        [BindProperty]
        public CandidateProfile Candidate { get; set; }
        public EditModel()
        {
            candidateRepo = new CandidateProfileRepository();
            jobPostingRepo = new JobPostingRepository();
        }
        public IActionResult OnGet(string id)
        {
            var session = HttpContext.Session;
            if (session.GetInt32("Role") != 2) return RedirectToPage("/Auth/AccessDenied");

            Candidate = candidateRepo.GetCandidateById(id);
            if (Candidate == null) return RedirectToPage("NotFoundPage");
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
                if (candidateRepo.GetCandidateById(Candidate.CandidateId) == null)
                    //throw new Exception("Not found Candidate");
                    return RedirectToPage("NotFoundPage");

                candidateRepo.UpdateCandidate(Candidate);
                return RedirectToPage("Index");
            }
            ViewData["jobPostings"] = jobPostingRepo.GetAll();
            return Page();
        }
    }
}
