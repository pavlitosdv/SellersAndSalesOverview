using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellersAndSalesOverview.Controllers
{
    public class SaleController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var ListOfSales= _unitOfWork.Sales.GetAll(includeProperties: "Seller");
            return View(ListOfSales);
        }

        public IActionResult Details(int id)
        {
            var saleFromDb = _unitOfWork.Sales.GetFirstOrDefault(i=>i.Id==id, includeProperties: "Seller");

            if (saleFromDb is null) return NotFound();

            return View(saleFromDb);
        }


        public IActionResult Create()
        {
            SaleViewModel saleVM = new SaleViewModel();

            saleVM.Sellers = _unitOfWork.Sellers.GetAll().Select(i => new SelectListItem
            {
                Text = i.FirstName + " " + i.LastName,
                Value = i.Id.ToString()
            });

            return View(saleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SaleViewModel saleVM)
        {
            if (!ModelState.IsValid) return View(saleVM);

            _unitOfWork.Sales.Add(saleVM.Sale);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Update(int id)
        {
            var saleFromDb = _unitOfWork.Sales.GetFirstOrDefault(i=>i.Id==id, includeProperties: "Seller");
            
            if (saleFromDb is null) return NotFound();

            SaleViewModel VM = new SaleViewModel();
            VM.Sale = saleFromDb;
            VM.Sellers = _unitOfWork.Sellers.GetAll().Select(i => new SelectListItem
            {
                Text = i.FirstName + " " + i.LastName,
                Value = i.Id.ToString()
            });

            return View(VM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(SaleViewModel VM)
        {
            if (!ModelState.IsValid)
            {
                VM.Sellers = _unitOfWork.Sellers.GetAll().Select(i => new SelectListItem
                {
                    Text = i.FirstName + " " + i.LastName,
                    Value = i.Id.ToString()
                });

                return View(VM);
            }

            _unitOfWork.Sales.Update(VM.Sale);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var saleFromDb = _unitOfWork.Sales.GetFirstOrDefault(i=>i.Id ==id, includeProperties:"Seller");

            if (saleFromDb is null) return NotFound();

            return View(saleFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            var saleFromDb = _unitOfWork.Sales.GetById(id);

            if (saleFromDb is null) return NotFound();

            _unitOfWork.Sales.Remove(saleFromDb);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
