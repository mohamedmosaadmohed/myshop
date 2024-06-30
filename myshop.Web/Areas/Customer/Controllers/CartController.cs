using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using myshop.Entities.ViewModels;
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
	}
}
