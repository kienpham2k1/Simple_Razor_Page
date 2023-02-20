//using BusinessObject.Models;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace DataAccess
//{
//    public class JobPostingDAO
//    {
//        private static JobPostingDAO instance = null;
//        private static readonly object instanceLook = new object();
//        public static JobPostingDAO Instance
//        {
//            get
//            {
//                lock (instanceLook)
//                {
//                    if (instance == null)
//                    {
//                        instance = new JobPostingDAO();
//                    }
//                    return instance;
//                }
//            }
//        }
//        //===========================================================
//        public IEnumerable<JobPosting> GetAll()
//        {
//            IEnumerable<JobPosting> jobs = null;
//            try
//            {
//                using var context = new CandidateManagementContext();
//                jobs = context.JobPostings.ToList();
//            }
//            catch (Exception ex)
//            {
//                throw new Exception(ex.Message);
//            }
//            return jobs;
//        }
//    }
//}
