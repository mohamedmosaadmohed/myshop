﻿using myshop.DataAccess.Data;
using myshop.Entities.Repositories;

namespace myshop.DataAccess.Implementation
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly ApplicationDbContext _context;
		public ICatagoryRepository Catagory { get; private set; }
		public IProductRepository Product { get; private set; }
		public IShoppingCartRepository ShoppingCart { get; private set; }
		public UnitOfWork(ApplicationDbContext context)
		{
			_context = context;
			Catagory = new CatagoryRepository(context);
			Product = new ProductRepository(context);
			ShoppingCart = new ShoppingCartRepository(context);
		}
		public int Complete()
		{
			return _context.SaveChanges();
		}
		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
