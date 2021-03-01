using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Sale> Sales { get; }
        IRepository<Seller> Sellers { get; }

        void Save();
    }
}
