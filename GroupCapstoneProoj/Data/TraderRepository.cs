using GroupCapstoneProoj.Contracts;
using GroupCapstoneProoj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Data
{
    public class TraderRepository : RepositoryBase<Trader>, ITraderRepository
    {
        public TraderRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }
        public Trader GetTrader(int traderId) => FindByCondition(c => c.Id.Equals(traderId)).SingleOrDefault();

        public void CreateTrader(Trader trader) => Create(trader);
    }
}
