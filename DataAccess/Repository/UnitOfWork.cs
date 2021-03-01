using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbcontext;


        public IRepository<Sale> Sales { get; private set; }
        public IRepository<Seller> Sellers { get; private set; }

        public UnitOfWork(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;

            Sales = new Repository<Sale>(dbcontext);
            Sellers = new Repository<Seller>(dbcontext);
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }

        public void Save()
        {
            _dbcontext.SaveChanges();
        }
    }
}
