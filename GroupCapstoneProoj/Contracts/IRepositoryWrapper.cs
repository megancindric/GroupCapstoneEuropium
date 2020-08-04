using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Contracts
{
    public interface IRepositoryWrapper
    {
        IAdminRepository Admin { get; }
        ITraderRepository Trader { get; }
        IListingRepository Listing { get; }
        void Save();
    }
}
