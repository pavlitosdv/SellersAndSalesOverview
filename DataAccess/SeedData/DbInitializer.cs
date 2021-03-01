using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.SeedData
{

    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _db;


        public DbInitializer(ApplicationDbContext db)
        {
            _db = db;
        }

        public async void Initialize()
        {
            if (_db.Database.GetPendingMigrations().Count() > 0)
            {
                _db.Database.Migrate();
            }

            List<Seller> sellers=new List<Seller>(){
                            new Seller { FirstName = "Pavlos", LastName = "Papadopoulos" },
                            new Seller { FirstName = "Name1", LastName = "Surname1" },
                            new Seller { FirstName = "Name2", LastName = "Surname3" }
            };
           
            if (!_db.Sellers.Any())
            {
                _db.Sellers.AddRange(sellers);
            }

            if (!_db.Sales.Any())
            {
                _db.Sales.AddRange(
                    new Sale { TransactionValue = 15.2, DateOfSale = new DateTime(2019, 9, 1), Seller = sellers.ElementAt(0) },
                    new Sale { TransactionValue = 18, DateOfSale = new DateTime(2020, 10, 20), Seller = sellers.ElementAt(0) },
                    new Sale { TransactionValue = 5.5, DateOfSale = new DateTime(2020, 11, 21), Seller = sellers.ElementAt(0) },
                    new Sale { TransactionValue = 19, DateOfSale = new DateTime(2021, 11, 28), Seller = sellers.ElementAt(0) },
                    new Sale { TransactionValue = 22, DateOfSale = new DateTime(2021, 1, 5), Seller = sellers.ElementAt(0) },
                    new Sale { TransactionValue = 31.5, DateOfSale = new DateTime(2021, 2, 10), Seller = sellers.ElementAt(0) },
                    new Sale { TransactionValue = 28.1, DateOfSale = new DateTime(2021, 1, 6), Seller = sellers.ElementAt(0) },
                    new Sale { TransactionValue = 9.9, DateOfSale = new DateTime(2021, 2, 15), Seller = sellers.ElementAt(0) },
                    new Sale { TransactionValue = 8.9, DateOfSale = new DateTime(2021, 3, 1), Seller = sellers.ElementAt(0) },
                    new Sale { TransactionValue = 17.9, DateOfSale = new DateTime(2021, 2, 10), Seller = sellers.ElementAt(1) },
                    new Sale { TransactionValue = 4.8, DateOfSale = new DateTime(2021, 2, 11), Seller = sellers.ElementAt(1) },
                    new Sale { TransactionValue = 3.9, DateOfSale = new DateTime(2021, 2, 11), Seller = sellers.ElementAt(1) },
                    new Sale { TransactionValue = 45.3, DateOfSale = new DateTime(2021, 2, 11), Seller = sellers.ElementAt(1) },
                    new Sale { TransactionValue = 55.9, DateOfSale = new DateTime(2021, 1, 29), Seller = sellers.ElementAt(2) },
                    new Sale { TransactionValue = 74.3, DateOfSale = new DateTime(2021, 1, 30), Seller = sellers.ElementAt(2) });
            }
             
            _db.SaveChanges();
        }

    }
}
