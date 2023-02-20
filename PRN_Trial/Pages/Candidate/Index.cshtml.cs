using BusinessObject.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace PRN_Trial.Pages.Candidate
{
    public class IndexModel : PageModel
    {
        private readonly ICandidateProfileRepository candidateRepo = null;
        private readonly IJobPostingRepository jobPostingRepo = null;
        public IEnumerable<CandidateProfile> Candidates { get; set; }
        public IndexModel()
        {
            candidateRepo = new CandidateProfileRepository();
            jobPostingRepo = new JobPostingRepository();
        }
        public IActionResult OnGet(string fullName, string birthDay = "", int pageSelect =1, int size  = 3 )
        {
            var session = HttpContext.Session;
            if (session.GetInt32("Role") != 2) return RedirectToPage("/Auth/AccessDenied");
            ViewData["FullName"] = session.GetString("fullName");
            DateTime _birthDay = (DateTime)SqlDateTime.MinValue;
            if (string.IsNullOrEmpty(fullName)) fullName = "";
            if (!string.IsNullOrEmpty(birthDay)) _birthDay = DateTime.Parse(birthDay);
                if (_birthDay < (DateTime)SqlDateTime.MinValue) _birthDay = (DateTime)SqlDateTime.MinValue;
            int pageCount = candidateRepo.PageCount(candidateRepo.GetAll(fullName, _birthDay).Count(), size);
            Candidates = candidateRepo.FindByCodition(pageSelect, size, fullName, _birthDay);

            ViewData["pageCount"] = pageCount;
            ViewData["activePage"] = pageSelect;
            ViewData["fullName"] = fullName;
            ViewData["birthDay"] = birthDay;
            return Page();
        }
    }
}
