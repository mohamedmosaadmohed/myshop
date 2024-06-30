﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.DataAccess.Migrations;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using myshop.Entities.ViewModels;
using myshop.Utilities;
using System.Security.Claims;

namespace myshop.Web.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class CartController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		public ShoppingCartVM shoppingCartVM { get; set; }
		public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			shoppingCartVM = new ShoppingCartVM()
			{
				shoppingCarts = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value,IncludeWord:"Product")
			};
			foreach(var item in shoppingCartVM.shoppingCarts)
			{
				shoppingCartVM.totalCarts += (item.Count * item.Product.Price);
			}
			return View(shoppingCartVM);
		}
		public IActionResult plus(int cartid) 
		{
			var shoppingCart = _unitOfWork.ShoppingCart.GetFirstorDefault(x => x.shoppingId == cartid);
			_unitOfWork.ShoppingCart.IncreaseCount(shoppingCart, 1);
			_unitOfWork.Complete();
			return RedirectToAction("Index");
		}
		public IActionResult Minus(int cartid) 
		{
			var shoppingCart = _unitOfWork.ShoppingCart.GetFirstorDefault(x => x.shoppingId == cartid);
			if (shoppingCart.Count <= 1)
			{
				_unitOfWork.ShoppingCart.Remove(shoppingCart);
				_unitOfWork.Complete();
				return RedirectToAction("Index", "Home");
			}
			_unitOfWork.ShoppingCart.decreaseCount(shoppingCart, 1);
			_unitOfWork.Complete();
			return RedirectToAction("Index");
		}
		public IActionResult Remove(int cartid)
		{
			var shoppingCart = _unitOfWork.ShoppingCart.GetFirstorDefault(x => x.shoppingId == cartid);
			_unitOfWork.ShoppingCart.Remove(shoppingCart);
			_unitOfWork.Complete();
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult Summary()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			shoppingCartVM = new ShoppingCartVM()
			{
				shoppingCarts = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value, IncludeWord: "Product"),
				OrderHeader = new()
			};
			shoppingCartVM.OrderHeader.applicationUser = _unitOfWork.ApplicationUser.GetFirstorDefault(X => X.Id == claim.Value);
			shoppingCartVM.OrderHeader.name = shoppingCartVM.OrderHeader.applicationUser.Name;
			shoppingCartVM.OrderHeader.email = shoppingCartVM.OrderHeader.applicationUser.Email;
			shoppingCartVM.OrderHeader.address = shoppingCartVM.OrderHeader.applicationUser.Address;
			shoppingCartVM.OrderHeader.city = shoppingCartVM.OrderHeader.applicationUser.City;
			shoppingCartVM.OrderHeader.phone = shoppingCartVM.OrderHeader.applicationUser.PhoneNumber;
			shoppingCartVM.OrderHeader.zipCode = shoppingCartVM.OrderHeader.applicationUser.ZipCode;

			foreach (var item in shoppingCartVM.shoppingCarts)
			{
				shoppingCartVM.totalCarts += (item.Count * item.Product.Price);
			}
			return View(shoppingCartVM);
		}
		[HttpPost]
		[ActionName("Summary")]
		[AutoValidateAntiforgeryToken]
		public IActionResult PostSummary(ShoppingCartVM shoppingCartvm)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

			shoppingCartVM.shoppingCarts = _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value, IncludeWord: "Product");

			shoppingCartVM.OrderHeader.orderStatus = SD.Pending;
			//shoppingCartVM.OrderHeader.paymentStatus = SD.Pending;
			shoppingCartVM.OrderHeader.orderDate = DateTime.Now;
			shoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;


			foreach (var item in shoppingCartVM.shoppingCarts)
			{
				shoppingCartVM.totalCarts += (item.Count * item.Product.Price);
			}

			_unitOfWork.OrderHeader.Add(shoppingCartVM.OrderHeader);
			_unitOfWork.Complete();

			foreach(var item in shoppingCartVM.shoppingCarts)
			{
				OrderDetails orderDetails = new OrderDetails()
				{
					productId = item.ProductId,
					orderId = shoppingCartVM.OrderHeader.Id,
					price = item.Product.Price,
					Count =item.Count,
				};
				_unitOfWork.OrderDetails.Add(orderDetails);
				_unitOfWork.Complete();
			}
			return View();
		}
	}
}
