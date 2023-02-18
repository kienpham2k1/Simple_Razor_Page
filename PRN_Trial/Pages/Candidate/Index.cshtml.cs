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
        public async Task<IActionResult> OnGet(string fullName, string birthDay, int pageSelect)
        {
            var session = HttpContext.Session;
            if (session.GetInt32("Role") != 2) return RedirectToPage("/Auth/AccessDenied");

            DateTime _birthDay = (DateTime)SqlDateTime.MinValue;

            if (string.IsNullOrEmpty(fullName)) fullName = "";
            if (!string.IsNullOrEmpty(birthDay))
            {
                _birthDay = DateTime.Parse(birthDay);
                //birthDay = _birthDay.ToString("yyyy-MM-dd");
            }
            if (pageSelect == 0) pageSelect = 1;

            int pageCount = candidateRepo.PageCount(candidateRepo.GetAll(fullName, _birthDay).Count(), 3);
            Candidates = candidateRepo.FindByCodition(pageSelect, 3, fullName, _birthDay);

            ViewData["pageCount"] = pageCount;
            ViewData["activePage"] = pageSelect;
            ViewData["fullName"] = fullName;
            ViewData["birthDay"] = birthDay;
            return Page();
        }
        //public async Task<IActionResult> OnPost()
        //{
        //    var session = HttpContext.Session;
        //    if (session.GetInt32("Role") != 2) return RedirectToPage("/Auth/AccessDenied");
        //    string fullname = Request.Form["fullname"];
        //    string birthDay = Request.Form["birthDay"];
        //    DateTime _birthDay = (DateTime)SqlDateTime.MinValue;
        //    if (!string.IsNullOrEmpty(birthDay))
        //    {
        //        _birthDay = DateTime.Parse(birthDay);
        //        birthDay = _birthDay.ToString("yyyy-MM-dd");
        //    }
        //    Candidates = candidateRepo.FindByCodition(1, 3, fullname, _birthDay);
        //    int pageCount = candidateRepo.PageCount(candidateRepo.GetAll(fullname, _birthDay).Count(), 3);
        //    ViewData["pageCount"] = pageCount;
        //    //ViewData["activePage"] = 1;
        //    ViewData["fullName"] = fullname;
        //    ViewData["birthDay"] = birthDay;
        //    return Page();
        //}
    }
}
