using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;
using DataAccess.Repository;

namespace DataAccess.Repository
{
    public interface ICandidateProfileRepository
    {
        CandidateProfile GetCandidateById(String id);
        IEnumerable<CandidateProfile> GetAll();
        IEnumerable<CandidateProfile> GetAll(string name, DateTime birthDay);
        IEnumerable<CandidateProfile>  FindByCodition(int page, int size, string fullName, DateTime? birthDay);
        void CreateCandidate(CandidateProfile newCandidate);
        void UpdateCandidate(CandidateProfile newCandidate);
        void DeleteCandidate(string id);
        String GetNextId();
        int PageCount(int countList,int size);
    }
}
