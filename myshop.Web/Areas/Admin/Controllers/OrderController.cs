using Microsoft.AspNetCore.Mvc;
using myshop.Entities.Models;
using myshop.Entities.Repositories;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
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
    }
}
