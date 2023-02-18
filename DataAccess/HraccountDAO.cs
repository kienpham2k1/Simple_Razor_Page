using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class HraccountDAO
    {
        private static HraccountDAO instance = null;
        private static readonly object instanceLook = new object();
        public static HraccountDAO Instance
        {
            get
            {
                lock (instanceLook)
                {
                    if (instance == null)
                    {
                        instance = new HraccountDAO();
                    }
                    return instance;
                }
            }
        }
        //===========================================================
      public  Hraccount Login(string email, string password)
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
