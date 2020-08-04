using GroupCapstoneProoj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Contracts
{
    public interface IAdminRepository : IRepositoryBase<Admin>
    {
        Admin GetAdmin(int adminId);
        void CreateAdmin(Admin admin);
    }
}
