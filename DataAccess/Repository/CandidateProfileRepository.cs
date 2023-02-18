using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CandidateProfileRepository : ICandidateProfileRepository
    {
        public CandidateProfile GetCandidateById(String id) => CandidateDAO.Instance.GetCandidateById(id);
        public IEnumerable<CandidateProfile> GetAll() => CandidateDAO.Instance.GetAll();
        public IEnumerable<CandidateProfile> GetAll(string name, DateTime birthDay) => CandidateDAO.Instance.GetAll(name, birthDay);
        public IEnumerable<CandidateProfile> FindByCodition(int page, int size, string fullName, DateTime? birthDay) => CandidateDAO.Instance.FindByCodition(page, size, fullName, birthDay);
        public void CreateCandidate(CandidateProfile newCandidate) => CandidateDAO.Instance.CreateCandidate(newCandidate);
        public void UpdateCandidate(CandidateProfile newCandidate) => CandidateDAO.Instance.UpdateCandidate(newCandidate);
        public void DeleteCandidate(string id) => CandidateDAO.Instance.DeleteCandidate(id);
        public string GetNextId( ) => CandidateDAO.Instance.GetNextId( );
        public int PageCount(int countList, int size) => CandidateDAO.Instance.PageCount(countList, size);
    }
}
