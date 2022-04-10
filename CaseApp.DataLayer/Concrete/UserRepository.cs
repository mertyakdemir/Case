using CaseApp.DataLayer.Abstract;
using CaseApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseApp.DataLayer.Concrete
{
    public class UserRepository : RepositoryBase<User, DatabaseContext>, IUserRepository
    {
        
        public User FindUser(string userName)
        {
            using var dbContext = new DatabaseContext();
            return dbContext.Users.Where(x => x.Username == userName).FirstOrDefault();
        }
    }
}
