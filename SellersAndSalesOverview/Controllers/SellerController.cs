using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SellersAndSalesOverview.Controllers
{
    public class SellerController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SellerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var listOfSellers = _unitOfWork.Sellers.GetAll(includeProperties: "Sales");

            return View(listOfSellers);
        }

        public IActionResult Details(int id)
        {
            var sellerFromDb = _unitOfWork.Sellers.GetFirstOrDefault(i => i.Id == id, includeProperties: "Sales");

            if (sellerFromDb is null) return NotFound();

            var data = sellerFromDb.Sales.Where(y => y.DateOfSale.Year == DateTime.Now.Year)
               .OrderBy(o=>o.DateOfSale.Month)
               .Select(k => new { k.DateOfSale.Month, k.TransactionValue, k.Commission })
               .GroupBy(x => new { x.Month }, (key, group) =>
               new SellerViewModel
               {
                   Seller = sellerFromDb,
                   Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(key.Month),
                   Total = group.Sum(k => k.TransactionValue),
                   Commission = group.Sum(c => c.Commission)
               });

            if(data.Any()) return View(data);

            List<SellerViewModel> VM = new List<SellerViewModel>();
            VM.Add(new SellerViewModel { Seller=sellerFromDb});

            return View(VM);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (!ModelState.IsValid) return View(seller);

            _unitOfWork.Sellers.Add(seller);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            var sellerFromDb = _unitOfWork.Sellers.GetById(id);

            if (sellerFromDb is null) return NotFound();

            return View(sellerFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                return View(seller);
            }

            _unitOfWork.Sellers.Update(seller);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var sellerFromDb = _unitOfWork.Sellers.GetById(id);

            if (sellerFromDb is null) return NotFound();

            return View(sellerFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            var SellerFromDb = _unitOfWork.Sellers.GetById(id);

            if (SellerFromDb is null) return NotFound();

            _unitOfWork.Sellers.Remove(SellerFromDb);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
