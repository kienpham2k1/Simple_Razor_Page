using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class HraccountRepository : IHraccountRepository
    {
        public Hraccount Login(string email, string password)
        {
            Hraccount loginAcc = null;
            try
            {
                using var context = new CandidateManagementContext();
                loginAcc = context.Hraccounts.SingleOrDefault(c => c.Email.Equals(email) && c.Password.Equals(password));
                if (loginAcc == null) throw new Exception("Email or password wrong");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return loginAcc;
        }
    }
}
