using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using myshop.Entities.ViewModels;
using myshop.Utilities;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.AdminRole)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetData()
        {
            IEnumerable<OrderHeader> OrderHeaders;
            OrderHeaders = _unitOfWork.OrderHeader.GetAll(IncludeWord: "applicationUser");
            return Json(new {data = OrderHeaders});
        }
        public IActionResult Details(int orderid)
        {
            OrderVM orderVM = new OrderVM()
            {
                orderHeader =_unitOfWork.OrderHeader.GetFirstorDefault(X => X.Id == orderid, IncludeWord: "applicationUser"),
                orderDetails =_unitOfWork.OrderDetails.GetAll(X=> X.OrderHeaderId == orderid , IncludeWord: "product"),
            };
            return View(orderVM);
        }
    }
}
