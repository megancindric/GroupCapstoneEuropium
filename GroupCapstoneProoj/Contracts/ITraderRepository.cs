using GroupCapstoneProoj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj.Contracts
{
    public interface ITraderRepository : IRepositoryBase<Trader>
    {
        Trader GetTrader(int traderId);
        void CreateTrader(Trader trader);
    }
}
