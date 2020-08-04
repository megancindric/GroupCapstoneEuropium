using GroupCapstoneProoj.Contracts;
using GroupCapstoneProoj.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupCapstoneProoj
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;
        private IAdminRepository _admin;
        private ITraderRepository _trader;
        private IListingRepository _listing;

        public IAdminRepository Admin
        {
            get
            {
                if(_admin == null)
                {
                    _admin = new AdminRepository(_context);
                }
                return _admin;
            }
        }
        public ITraderRepository Trader
        {
            get
            {
                if (_trader == null)
                {
                    _trader = new TraderRepository(_context);
                }
                return _trader;
            }
        }

        public IListingRepository Listing
        {
            get
            {
                if (_listing == null)
                {
                    _listing = new ListingRepository(_context);
                }
                return _listing;
            }
        }
        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
