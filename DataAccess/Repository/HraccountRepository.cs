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
        public Hraccount Login(string email, string password) => HraccountDAO.Instance.Login(email, password);
    }
}
