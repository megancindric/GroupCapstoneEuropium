using GroupCapstoneProoj.Contracts;
using GroupCapstoneProoj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Data
{
    public class AdminRepository : RepositoryBase<Admin>, IAdminRepository
    {
        public AdminRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
        }
        public Admin GetAdmin(int adminId) => FindByCondition(c => c.Id.Equals(adminId)).SingleOrDefault();

        public void CreateAdmin(Admin admin) => Create(admin);
    }
}
