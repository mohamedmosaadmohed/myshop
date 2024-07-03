﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
			shoppingCartVM.OrderHeader.FirstName = shoppingCartVM.OrderHeader.applicationUser.FirstName;
			shoppingCartVM.OrderHeader.LastName = shoppingCartVM.OrderHeader.applicationUser.LastName;
			shoppingCartVM.OrderHeader.Email = shoppingCartVM.OrderHeader.applicationUser.Email;

            // Get Saved Past Transaction
            shoppingCartVM.OrderHeader.PhoneNumber = shoppingCartVM.OrderHeader.applicationUser.PhoneNumber;
            shoppingCartVM.OrderHeader.AdditionalPhoneNumber = shoppingCartVM.OrderHeader.applicationUser.AdditionalPhoneNumber;
            shoppingCartVM.OrderHeader.Address = shoppingCartVM.OrderHeader.applicationUser.Address;
            shoppingCartVM.OrderHeader.Region = shoppingCartVM.OrderHeader.applicationUser.Region;
            shoppingCartVM.OrderHeader.City = shoppingCartVM.OrderHeader.applicationUser.City;


            foreach (var item in shoppingCartVM.shoppingCarts)
			{
				shoppingCartVM.OrderHeader.totalPrice += (item.Count * item.Product.Price);
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

            shoppingCartvm.shoppingCarts = _unitOfWork.ShoppingCart
				.GetAll(x => x.ApplicationUserId == claim.Value, IncludeWord: "Product");

            shoppingCartvm.OrderHeader.orderStatus = SD.Pending;
            shoppingCartvm.OrderHeader.paymentStatus = SD.Pending;
            shoppingCartvm.OrderHeader.orderDate = DateTime.Now;
            shoppingCartvm.OrderHeader.ApplicationUserId = claim.Value;

            var applicationUser = _unitOfWork.ApplicationUser.GetFirstorDefault(u => u.Id == claim.Value);
            // Update the user with the data from shoppingCartvm
            applicationUser.PhoneNumber = shoppingCartvm.OrderHeader.PhoneNumber;
            applicationUser.AdditionalPhoneNumber = shoppingCartvm.OrderHeader.AdditionalPhoneNumber;
            applicationUser.Address = shoppingCartvm.OrderHeader.Address;
            applicationUser.Region = shoppingCartvm.OrderHeader.Region;
            applicationUser.City = shoppingCartvm.OrderHeader.City;

            foreach (var item in shoppingCartvm.shoppingCarts)
			{
                shoppingCartvm.OrderHeader.totalPrice += (item.Count * item.Product.Price);
			}

			_unitOfWork.OrderHeader.Add(shoppingCartvm.OrderHeader);
			_unitOfWork.Complete();

			foreach(var item in shoppingCartvm.shoppingCarts)
			{
				OrderDetails orderDetails = new OrderDetails()
				{
					productId = item.ProductId,
                    OrderHeaderId = shoppingCartvm.OrderHeader.Id,
					price = item.Product.Price,
					Count =item.Count,
				};
				_unitOfWork.OrderDetails.Add(orderDetails);
				_unitOfWork.Complete();
			}
			return RedirectToAction("Index", "Home");
		}
	}
}
