﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using System.Security.Claims;

namespace myshop.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
	public class HomeController : Controller
	{
		private IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
		{
			var products = _unitOfWork.Product.GetAll();
			return View(products);
		}
		public IActionResult Details(int id)
		{
			var product = _unitOfWork.Product.GetFirstorDefault(X => X.Id == id, IncludeWord: "TbCatagory");
            var relatedProducts = _unitOfWork.Product.GetAll(x => x.TbCatagory.Name == product.TbCatagory.Name && x.Id != id);
            ShoppingCart obj =new ShoppingCart()
			{
                ProductId =id,
                Product = product,
				Count = 1,
                RelatedProducts = relatedProducts
            };
			return View(obj);
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;
            ShoppingCart cartObj = _unitOfWork.ShoppingCart.GetFirstorDefault(
                U => U.ApplicationUserId == claim.Value && U.ProductId == shoppingCart.ProductId
            );
            if (cartObj == null)
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            else
            {
                _unitOfWork.ShoppingCart.IncreaseCount(cartObj,shoppingCart.Count);
            }
            _unitOfWork.Complete();
            return RedirectToAction("Index");
        }
    }
}
