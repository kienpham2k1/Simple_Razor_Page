using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;

namespace DataAccess.Repository
{
    public class JobPostingRepository : IJobPostingRepository
    {
        public IEnumerable<JobPosting> GetAll()
        {
            IEnumerable<JobPosting> jobs = null;
            try
            {
                using var context = new CandidateManagementContext();
                jobs = context.JobPostings.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return jobs;
        }
    }
}
