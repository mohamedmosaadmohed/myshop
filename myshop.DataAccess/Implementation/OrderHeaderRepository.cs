﻿using myshop.DataAccess.Data;
using myshop.Entities.Models;
using myshop.Entities.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myshop.DataAccess.Implementation
{
	public class OrderHeaderRepository : GenericRepository<OrderHeader>, IOrderHeaderRepository
	{
		private readonly ApplicationDbContext _context;
		public OrderHeaderRepository(ApplicationDbContext context) : base(context)
		{
			_context = context;
		}

		public void Update(OrderHeader orderHeader)
		{
			_context.TbOrderHeaders.Update(orderHeader);
		}
		public void UpdateOrderStatus(int id, string? orderStatus, string? paymentStatus)
		{
			var orderFromDb = _context.TbOrderHeaders.FirstOrDefault(X => X.Id == id);
			if (orderFromDb != null) 
			{
				orderFromDb.orderStatus = orderStatus;
				if (paymentStatus != null) { orderFromDb.paymentStatus = paymentStatus; }
			}
		}
	}
}
