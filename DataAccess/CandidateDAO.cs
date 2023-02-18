using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;
using BusinessObject.Models;
using DataAccess.Repository;
using DataAccess.Utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DataAccess
{
    public class CandidateDAO
    {
        private static CandidateDAO instance = null;
        private static readonly object instanceLook = new object();
        public static CandidateDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new CandidateDAO();
                    }
                    return instance;
                }
            }
        }

        //===========================================================
        public CandidateProfile GetCandidateById(String id)
        {
            CandidateProfile candidate = null;
            try
            {
                using var context = new CandidateManagementContext();
                candidate = context.CandidateProfiles.SingleOrDefault(c => c.CandidateId.Equals(id));
            }
            catch (Exception e) { throw new Exception(e.Message); }
            return candidate;
        }
        public IEnumerable<CandidateProfile> GetAll()
        {
            var candidates = new List<CandidateProfile>();
            try
            {
                using var context = new CandidateManagementContext();
                candidates = context.CandidateProfiles.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return candidates;
        }
        public IEnumerable<CandidateProfile> GetAll(string name, DateTime birthDay)
        {
            var candidates = new List<CandidateProfile>();
            try
            {
                using var context = new CandidateManagementContext();
                if (!String.IsNullOrEmpty(name) || birthDay > DateTime.MinValue)
                    candidates = context.CandidateProfiles.Where(c => c.Fullname.Contains(name) && c.Birthday > birthDay).ToList();
                else candidates = GetAll().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            return candidates;
        }
        public IEnumerable<CandidateProfile> FindByCodition(int page, int pageSize, string fullName, DateTime? birthDay)
        {
            var candidates = new List<CandidateProfile>();
            try
            {
                using var context = new CandidateManagementContext();

                var skip = (page - 1) * pageSize;
                candidates = context.CandidateProfiles
                    .OrderBy(c => c.CandidateId)
                    .Where(c => c.Fullname.Contains(fullName) && c.Birthday > birthDay)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return candidates;
        }

        public void CreateCandidate(CandidateProfile newCandidate)
        {
            try
            {
                CandidateProfile _c = GetCandidateById(newCandidate.CandidateId);
                if (_c == null)
                {
                    using var context = new CandidateManagementContext();
                    context.CandidateProfiles.Add(newCandidate);
                    context.SaveChanges();
                }
                else { throw new Exception("This candidate is already exist."); }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void UpdateCandidate(CandidateProfile newCandidate)
        {
            try
            {
                CandidateProfile _c = GetCandidateById(newCandidate.CandidateId);
                if (_c != null)
                {
                    using var context = new CandidateManagementContext();
                    context.CandidateProfiles.Update(newCandidate);
                    context.SaveChanges();
                }
                else throw new Exception("This candidate is not exist.");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public void DeleteCandidate(string id)
        {
            try
            {
                CandidateProfile _c = GetCandidateById(id);
                if (_c != null)
                {
                    using var context = new CandidateManagementContext();
                    context.CandidateProfiles.Remove(_c);
                    context.SaveChanges();
                }
                else throw new Exception("This candidate is not exist.");
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public string GetNextId()
        {
            string result = "";
            try
            {
                using var context = new CandidateManagementContext();
                var maxValue = context.CandidateProfiles.Max(x => x.CandidateId);
                CandidateProfile _result = context.CandidateProfiles.First(x => x.CandidateId == maxValue);
                result = RegexFormat.FormatId(_result.CandidateId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public int PageCount(int countList, int pageSize)
        {
            int pageCount = 0;
            try
            {
                var _pageCount = (double)countList / pageSize;
                pageCount = (int)Math.Ceiling(_pageCount);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return pageCount;
        }
    }
}
